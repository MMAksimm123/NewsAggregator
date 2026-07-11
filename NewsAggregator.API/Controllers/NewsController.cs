using Microsoft.AspNetCore.Mvc;
using NewsAggregator.API.Models;
using NewsAggregator.API.Services;

namespace NewsAggregator.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsAggregator _newsAggregator;
        private readonly ILogger<NewsController> _logger;

        public NewsController(INewsAggregator newsAggregator, ILogger<NewsController> logger)
        {
            _newsAggregator = newsAggregator;
            _logger = logger;
        }

        [HttpPost("search")]
        public async Task<ActionResult<NewsResponse>> SearchNews([FromBody] NewsRequest request)
        {
            _logger.LogInformation("Получен запрос на поиск новостей: Query='{Query}', Category='{Category}', Countries={Countries}, SourceTypes={SourceTypes}",
                request.Query, request.Category, string.Join(", ", request.Countries), string.Join(", ", request.SourceTypes));

            // Валидация запроса
            if (request == null)
            {
                _logger.LogWarning("Получен пустой запрос");
                return BadRequest(new NewsResponse { Error = "Запрос не может быть пустым" });
            }

            try
            {
                // Нормализация данных
                if (string.IsNullOrWhiteSpace(request.Category))
                {
                    request.Category = string.Empty; // Убеждаемся, что это пустая строка, а не null
                }

                var response = await _newsAggregator.AggregateNewsAsync(request);

                if (!string.IsNullOrEmpty(response.Error))
                {
                    _logger.LogError("Ошибка при агрегации новостей: {Error}", response.Error);
                    return StatusCode(500, response);
                }

                _logger.LogInformation("Успешно найдено {Count} новостей", response.TotalCount);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Необработанная ошибка при обработке запроса новостей");
                return StatusCode(500, new NewsResponse { Error = $"Внутренняя ошибка сервера: {ex.Message}" });
            }
        }

        [HttpGet("sources")]
        public ActionResult GetAvailableSources()
        {
            var categoryStats = new Dictionary<string, int>
    {
        {"Ecology", 25},
        {"CreativeIndustries", 21},
        {"UrbanDevelopment", 25},
        {"EducationTech", 25},
        {"HealthcareTech", 25},
        {"Tourism", 20},
        {"SmartCity", 20},
        {"Culture", 25},
        {"University", 50},
        {"General", 31}
    };

            var languageStats = new Dictionary<string, int>
    {
        {"en", 45},
        {"ru", 15},
        {"fr", 8},
        {"de", 8},
        {"es", 8},
        {"it", 5},
        {"ja", 5},
        {"ko", 4},
        {"zh", 3},
        {"ar", 2}
    };

            var sources = new
            {
                Countries = new[] { "us", "gb", "ca", "jp", "kr", "ch", "de", "fr", "au", "sg", "qa", "es", "nl", "gr", "un", "in", "hk", "th", "ae", "il", "ru", "tw", "ph", "my", "vn", "np", "lk", "sa", "dk", "hu", "cy", "be", "nz", "id", "cn", "it" },
                Categories = Categories.All,
                CategoryStatistics = categoryStats,
                Languages = languageStats,
                SourceTypes = new[] { "News", "University", "Research" },
                TotalFeeds = 150,
                Status = "API работает с многоязычными источниками",
                Timestamp = DateTime.Now
            };

            return Ok(sources);
        }

        [HttpGet("categories")]
        public ActionResult GetCategories()
        {
            return Ok(Categories.All);
        }

        [HttpGet("test")]
        public ActionResult Test()
        {
            return Ok(new
            {
                message = "News Aggregator API работает!",
                version = "2.0",
                features = "Расширенные категории, университетские источники, международные СМИ",
                timestamp = DateTime.Now
            });
        }
    }
}