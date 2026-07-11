using System.Text;
using System.Text.Json;
using NewsAggregator.Client.Models;

namespace NewsAggregator.Client.Services
{
    public interface IApiService
    {
        Task<NewsResponse> GetNewsAsync(NewsRequest request);
        Task<bool> TestConnectionAsync();
        Task<Dictionary<string, string>> GetCategoriesAsync();
    }

    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5000/api/";

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<NewsResponse> GetNewsAsync(NewsRequest request)
        {
            try
            {
                // Преобразуем отображаемое название категории в API категорию
                request.Category = CategoryHelper.GetApiCategory(request.Category);

                // Убеждаемся, что все поля инициализированы
                request.Query ??= string.Empty;
                request.Category ??= string.Empty;
                request.Countries ??= new List<string>();
                request.SourceTypes ??= new List<string> { "News", "University", "Research" };

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                };

                var json = JsonSerializer.Serialize(request, options);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                Console.WriteLine($"Отправка запроса: {json}");

                var response = await _httpClient.PostAsync("news/search", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var newsResponse = JsonSerializer.Deserialize<NewsResponse>(
                        responseJson,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    return newsResponse ?? new NewsResponse { Error = "Неверный формат ответа" };
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Ошибка сервера: {response.StatusCode}. Контент: {errorContent}");
                    return new NewsResponse { Error = $"Ошибка сервера: {response.StatusCode}. {errorContent}" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка соединения: {ex.Message}");
                return new NewsResponse { Error = $"Ошибка соединения: {ex.Message}" };
            }
        }

        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("news/test");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Тест подключения успешен: {content}");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Тест подключения неудачен: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка подключения к {_baseUrl}: {ex.Message}");
                return false;
            }
        }

        public async Task<Dictionary<string, string>> GetCategoriesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("news/categories");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var categories = JsonSerializer.Deserialize<Dictionary<string, string>>(
                        content,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    return categories ?? new Dictionary<string, string>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка получения категорий: {ex.Message}");
            }

            return CategoryHelper.CategoryMapping.ToDictionary(x => x.Value, x => x.Key);
        }
    }
}