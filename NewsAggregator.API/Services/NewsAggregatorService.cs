using NewsAggregator.API.Models;

namespace NewsAggregator.API.Services
{
    public class NewsAggregatorService : INewsAggregator
    {
        private readonly IEnumerable<INewsService> _newsServices;
        private readonly ILogger<NewsAggregatorService> _logger;

        public NewsAggregatorService(IEnumerable<INewsService> newsServices, ILogger<NewsAggregatorService> logger)
        {
            _newsServices = newsServices;
            _logger = logger;
        }

        public async Task<NewsResponse> AggregateNewsAsync(NewsRequest request)
        {
            var response = new NewsResponse();

            try
            {
                var tasks = _newsServices.Select(service => SafeGetNewsAsync(service, request));
                var results = await Task.WhenAll(tasks);

                foreach (var articles in results)
                {
                    response.Articles.AddRange(articles);
                }

                response.Articles = response.Articles
                    .OrderByDescending(a => a.PublishedAt)
                    .Take(request.MaxResults)
                    .ToList();

                response.TotalCount = response.Articles.Count;

                // Логируем статистику по типам источников
                var sourceTypeStats = response.Articles.GroupBy(a => a.SourceType)
                    .ToDictionary(g => g.Key, g => g.Count());

                _logger.LogInformation("Статистика по типам источников: {Stats}",
                    string.Join(", ", sourceTypeStats.Select(kv => $"{kv.Key}: {kv.Value}")));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при агрегации новостей");
                response.Error = "Произошла ошибка при получении новостей";
            }

            return response;
        }

        private async Task<List<Article>> SafeGetNewsAsync(INewsService service, NewsRequest request)
        {
            try
            {
                return await service.GetNewsAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Ошибка в сервисе {ServiceName}", service.ServiceName);
                return new List<Article>();
            }
        }
    }
}
