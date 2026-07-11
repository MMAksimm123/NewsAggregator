using System.Text.Json.Serialization;

namespace NewsAggregator.Client.Models
{
    public class Article
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public string UrlToImage { get; set; }
        public DateTime? PublishedAt { get; set; }
        public string SourceName { get; set; }
        public string Country { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        public string SourceType { get; set; }
    }

    public class NewsRequest
    {
        [JsonPropertyName("query")]
        public string Query { get; set; } = string.Empty;

        [JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty;

        [JsonPropertyName("from")]
        public DateTime? From { get; set; }

        [JsonPropertyName("to")]
        public DateTime? To { get; set; }

        [JsonPropertyName("countries")]
        public List<string> Countries { get; set; } = new List<string>();

        [JsonPropertyName("sourceTypes")]
        public List<string> SourceTypes { get; set; } = new List<string> { "News", "University", "Research" };

        [JsonPropertyName("maxResults")]
        public int MaxResults { get; set; } = 100;
    }

    public class NewsResponse
    {
        [JsonPropertyName("articles")]
        public List<Article> Articles { get; set; } = new List<Article>();

        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }

        [JsonPropertyName("error")]
        public string Error { get; set; }
    }

    public static class CategoryHelper
    {
        public static readonly Dictionary<string, string> CategoryMapping = new()
        {
            {"Экология, развитие экологического мышления", "Ecology"},
            {"Развитие креативных индустрий и инженерного мышления", "CreativeIndustries"},
            {"Создание и развитие архитектурно-градостроительного облика города", "UrbanDevelopment"},
            {"Современные технологии в образовании и воспитании", "EducationTech"},
            {"Современные технологии в здравоохранении и здравостроительстве", "HealthcareTech"},
            {"Развитие туристического потенциала и сервиса, брендинг территории", "Tourism"},
            {"Система управления умным городом", "SmartCity"},
            {"Вхождение в мир культуры, занятия искусством, развитие культурной среды города", "Culture"}
        };

        public static string GetApiCategory(string displayCategory)
        {
            if (string.IsNullOrEmpty(displayCategory))
                return string.Empty;

            return CategoryMapping.ContainsKey(displayCategory) ? CategoryMapping[displayCategory] : displayCategory;
        }
    }
}