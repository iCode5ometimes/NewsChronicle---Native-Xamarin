using System;
using Refit;

namespace NewsChronicle.Data.Services
{
    public sealed class ArticleNetworkService
    {
        private static readonly Lazy<IRefitArticleService> _refitArticleServiceInstance =
                     new Lazy<IRefitArticleService>(() => RestService.For<IRefitArticleService>(baseUrl));
        public static IRefitArticleService RefitArticleServiceInstance => _refitArticleServiceInstance.Value;

        private const string baseUrl = "https://newsapi.org";

        private ArticleNetworkService()
        {
        }
    }
}
