using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using NewsAggregator.Client.Models;

namespace NewsAggregator.Client.Services
{
    public interface IWordExportService
    {
        Task<bool> ExportToWordAsync(Article article, string filePath);
    }

    public class WordExportService : IWordExportService
    {
        public async Task<bool> ExportToWordAsync(Article article, string filePath)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var wordDocument = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document))
                    {
                        // Добавляем главную часть документа
                        var mainPart = wordDocument.AddMainDocumentPart();
                        mainPart.Document = new DocumentFormat.OpenXml.Wordprocessing.Document();
                        var body = mainPart.Document.AppendChild(new DocumentFormat.OpenXml.Wordprocessing.Body());

                        // Заголовок
                        AddParagraph(body, article.Title, true, 24);

                        // Информация об источнике
                        AddParagraph(body, $"Источник: {article.SourceName} | Тип: {GetSourceTypeDisplayName(article.SourceType)}");
                        AddParagraph(body, $"Дата публикации: {article.PublishedAt:dd.MM.yyyy HH:mm}");
                        AddParagraph(body, $"Категория: {GetCategoryDisplayName(article.Category)}");
                        AddParagraph(body, $"Страна: {article.Country} | Язык: {GetFullLanguageName(article.Language)}");

                        // Разделитель
                        AddParagraph(body, new string('=', 80));

                        // Описание
                        if (!string.IsNullOrEmpty(article.Description))
                        {
                            AddParagraph(body, "ОПИСАНИЕ", true, 14);
                            AddParagraph(body, article.Description);
                            AddParagraph(body, "");
                        }

                        // Содержание
                        if (!string.IsNullOrEmpty(article.Content))
                        {
                            AddParagraph(body, "СОДЕРЖАНИЕ", true, 14);
                            AddParagraph(body, article.Content);
                            AddParagraph(body, "");
                        }

                        // Ссылка
                        AddParagraph(body, "ССЫЛКА НА ОРИГИНАЛ", true, 12);
                        AddParagraph(body, article.Url);

                        mainPart.Document.Save();
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Ошибка при сохранении Word: {ex.Message}");
                    return false;
                }
            });
        }

        private void AddParagraph(DocumentFormat.OpenXml.Wordprocessing.Body body, string text, bool isBold = false, int fontSize = 12)
        {
            var paragraph = body.AppendChild(new Paragraph());
            var run = paragraph.AppendChild(new Run());

            var runProperties = new RunProperties();
            if (isBold)
            {
                runProperties.Append(new Bold());
            }
            runProperties.Append(new FontSize { Val = (fontSize * 2).ToString() });

            run.RunProperties = runProperties;
            run.AppendChild(new DocumentFormat.OpenXml.Wordprocessing.Text(text));
        }

        private string GetSourceTypeDisplayName(string sourceType)
        {
            return sourceType switch
            {
                "News" => "Новостной сайт",
                "University" => "Университет",
                "Research" => "Исследовательский центр",
                _ => sourceType
            };
        }

        private string GetCategoryDisplayName(string category)
        {
            return CategoryHelper.CategoryMapping.FirstOrDefault(x => x.Value == category).Key ?? category;
        }

        private string GetFullLanguageName(string code)
        {
            return code.ToLower() switch
            {
                "en" => "Английский",
                "ru" => "Русский",
                "fr" => "Французский",
                "de" => "Немецкий",
                "es" => "Испанский",
                "it" => "Итальянский",
                "ja" => "Японский",
                "ko" => "Корейский",
                "zh" => "Китайский",
                "ar" => "Арабский",
                _ => code
            };
        }
    }
}