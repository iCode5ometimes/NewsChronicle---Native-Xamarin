using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NewsChronicle.Data.Model;
using NewsChronicle.Data.QueryParameters;

namespace NewsChronicle.Data.Interfaces
{
    public interface IArticleService
    {
        /// <summary>
        /// Handles the api call and the database storage and or retrieval of data.
        /// </summary>
        /// <param name="queryParams">Parameters accepted by the api call.</param>
        /// <param name="isInternetConnection">Represents wether there is internet connection or not.</param>
        /// <param name="articleApiCall">What kind of articles to call for from the api.</param>
        /// <param name="token">Used to cancel the api request.</param>
        /// <returns></returns>
        Task<List<Article>> GetArticlesByQueryParamsAsync(MyQueryParams queryParams,
                                                          bool isInternetConnection,
                                                          CancellationToken token);
    }
}
