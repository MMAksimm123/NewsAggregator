using System.Text.Json.Serialization;

namespace NewsAggregator.API.Models
{
    public class Article
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public string UrlToImage { get; set; }
        public DateTime? PublishedAt { get; set; }
        public string SourceName { get; set; }
        public string Country { get; set; }
        public string Category { get; set; }
        public string Language { get; set; } = "en";
        public string SourceType { get; set; } = "News";

        [JsonIgnore]
        public bool IsFullContent { get; set; }
    }

    public class NewsRequest
    {
        public string Query { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public List<string> Countries { get; set; } = new List<string>();
        public List<string> SourceTypes { get; set; } = new List<string> { "News", "University", "Research" };
        public int MaxResults { get; set; } = 100;
    }

    public class NewsResponse
    {
        public List<Article> Articles { get; set; } = new List<Article>();
        public int TotalCount { get; set; }
        public string Error { get; set; }
    }

    public static class Categories
    {
        public static readonly Dictionary<string, string> All = new()
        {
            {"Ecology", "Экология, развитие экологического мышления"},
            {"CreativeIndustries", "Развитие креативных индустрий и инженерного мышления"},
            {"UrbanDevelopment", "Создание и развитие архитектурно-градостроительного облика города и городской среды"},
            {"EducationTech", "Современные технологии в образовании и воспитании"},
            {"HealthcareTech", "Современные технологии в здравоохранении и здравостроительстве"},
            {"Tourism", "Развитие туристического потенциала и сервиса, брендинг территории"},
            {"SmartCity", "Система управления умным городом"},
            {"Culture", "Вхождение в мир культуры, занятия искусством, развитие культурной среды города"}
        };

        public static string GetDisplayName(string category)
        {
            return All.ContainsKey(category) ? All[category] : category;
        }
    }
}