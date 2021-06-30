using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using NewsChronicle.Data.Enum;
using NewsChronicle.Data.Exceptions;
using NewsChronicle.Data.Interfaces;
using NewsChronicle.Data.Model;
using NewsChronicle.Data.Model.DTOs;
using NewsChronicle.Data.QueryParameters;
using NewsChronicle.Data.Utils;
using Newtonsoft.Json;
using Refit;

namespace NewsChronicle.Data.Services
{
    public class ArticleService : IArticleService
    {
        #region Constructor(s) (and Dependencies)

        private readonly IRefitArticleService _refitArticleService = ArticleNetworkService.RefitArticleServiceInstance;
        private readonly IDBRepository<Article> _articleRepo;

        public ArticleService(IDBRepository<Article> articleDbRepository)
        {
            _articleRepo = articleDbRepository ?? throw new ArgumentNullException(nameof(articleDbRepository));
        }

        #endregion

        #region Methods

        public async Task<List<Article>> GetArticlesByQueryParamsAsync(MyQueryParams queryParams,
                                                                       bool isInternetConnection,
                                                                       CancellationToken token)
        {
            if(!isInternetConnection)
            {
                return await ReturnFilteredRecordsFromLocalDB(queryParams);
            }

            var json = await GetJsonResponseAsync(queryParams, token);
            var rawResponse = JsonConvert.DeserializeObject<ArticleListDTO>(json);
            var modelList = await Utilities.ToModelAsync(rawResponse, token);

            if (isInternetConnection)
            {
                Task.Run(async () =>
                {
                    await InsertRecordsInDBAsync(modelList);
                });
            }

            return modelList;
        }

        private async Task InsertRecordsInDBAsync(List<Article> articles)
        {
            if(articles.Any())
            {
                await _articleRepo.DeleteAllRecordsAsync();
                await _articleRepo.AddNewRecordListAsync(articles);
            }
        }

        /// <summary>
        /// Returns the json response from the api as a string.
        /// </summary>
        /// <param name="queryParams">The query parameters mandatory for the search.</param>
        /// <param name="token">Cancellation token that can stop the request.</param>
        /// <returns></returns>
        private async Task<string> GetJsonResponseAsync(MyQueryParams queryParams, CancellationToken token)
        {
            try
            {
                switch (queryParams.ApiCallType)
                {
                    case ArticleApiCall.TopHeadlines:
                        {
                            return await _refitArticleService.GetTopHeadlinesByQueryParams(token, queryParams);
                        }
                    case ArticleApiCall.AllArticles:
                        {
                            return await _refitArticleService.GetAllArticlesByQueryParams(token, queryParams);
                        }
                    default:
                        {
                            throw new UnableToGetResponseException("The api does not support this type of endpoint call");
                        }
                }
            }
            catch(HttpRequestException ex)
            {
                throw new UnableToGetResponseException("Could not make the request to the server.", ex);
            }
            catch(ApiException ex)
            {
                throw new UnableToGetResponseException("The request to the server was broken.", ex);
            }
        }

        private async Task<List<Article>> ReturnFilteredRecordsFromLocalDB(MyQueryParams queryParams)
        {
            var offlineArticles = await _articleRepo.GetAllRecordsAsync();
            return GetFilteredArticlesWhenOffline(offlineArticles.ToList(), queryParams);
        }

        /// <summary>
        /// When offline, the articles loaded when the user types something in the search bar
        /// will be filtered from the ones in the database here.
        /// </summary>
        /// <param name="offlineArticles"></param>
        /// /// <param name="queryParams"></param>
        /// <returns></returns>
        private List<Article> GetFilteredArticlesWhenOffline(List<Article> offlineArticles, MyQueryParams queryParams)
        {
            if (!string.IsNullOrWhiteSpace(queryParams.KeywordsOrPhrases))
            {
                var filteredArticles = offlineArticles.Where(article => article.Title.Contains(queryParams.KeywordsOrPhrases,
                                                                                               StringComparison.OrdinalIgnoreCase) ||
                                                                        article.Content.Contains(queryParams.KeywordsOrPhrases,
                                                                                               StringComparison.OrdinalIgnoreCase)).ToList();
                return filteredArticles;
            }
            else
            {
                return offlineArticles;
            }
        }

        #endregion
    }
}
