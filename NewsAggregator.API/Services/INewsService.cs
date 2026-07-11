using NewsAggregator.API.Models;

namespace NewsAggregator.API.Services
{
    public interface INewsService
    {
        string ServiceName { get; }
        Task<List<Article>> GetNewsAsync(NewsRequest request);
    }

    public interface INewsAggregator
    {
        Task<NewsResponse> AggregateNewsAsync (NewsRequest request);
    }
}
