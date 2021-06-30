using System.Threading;
using System.Threading.Tasks;
using NewsChronicle.Data.QueryParameters;
using Refit;

namespace NewsChronicle.Data.Services
{
    public interface IRefitArticleService
    {
        [Get("/v2/top-headlines")]
        Task<string> GetTopHeadlinesByQueryParams(CancellationToken token, MyQueryParams queryParams);

        [Get("/v2/everything")]
        Task<string> GetAllArticlesByQueryParams(CancellationToken token, MyQueryParams queryParams);
    }
}
