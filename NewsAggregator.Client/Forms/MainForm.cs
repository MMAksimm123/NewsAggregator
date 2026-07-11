using NewsAggregator.Client.Models;
using NewsAggregator.Client.Services;

namespace NewsAggregator.Client.Forms
{
    public partial class MainForm : Form
    {
        private readonly IApiService _apiService;
        private readonly IWordExportService _wordExportService;
        private List<Article> _currentArticles = new List<Article>();
        private Article _selectedArticle = null;

        public MainForm(IApiService apiService, IWordExportService wordExportService)
        {
            _apiService = apiService;
            _wordExportService = wordExportService;

            InitializeComponent();
            ResizeDataGridView();
            InitializeWebView2Async();
            InitializeDataGridView();
            InitializeCategories();
            InitializeSourceTypes();
            //InitializeLanguages();
            WireEvents();
            CheckConnection();
        }

        private void ResizeDataGridView()
        {
            // Увеличиваем ширину DataGridView
            if (splitContainer1 != null)
            {
                splitContainer1.SplitterDistance = 700;
            }

            // Увеличиваем общую ширину формы
            this.Width = 1400;
            this.Height = 900;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void WireEvents()
        {
            this.buttonSearch.Click += buttonSearch_Click;
            this.dataGridViewNews.SelectionChanged += dataGridViewNews_SelectionChanged;
            this.buttonSaveToWord.Click += buttonSaveToWord_Click;
        }

        private async void InitializeWebView2Async()
        {
            try
            {
                // Инициализация WebView2
                await webView2Content.EnsureCoreWebView2Async(null);

                // Настройка WebView2
                webView2Content.CoreWebView2.Settings.IsStatusBarEnabled = false;
                webView2Content.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
                webView2Content.CoreWebView2.Settings.AreDevToolsEnabled = false;

                // Устанавливаем начальную страницу
                webView2Content.CoreWebView2.NavigateToString(GetWelcomeHtml());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации WebView2: {ex.Message}\n\nУбедитесь, что WebView2 Runtime установлен.\nСкачать можно здесь: https://developer.microsoft.com/en-us/microsoft-edge/webview2/",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Fallback - показываем сообщение в WebView2
                webView2Content.CoreWebView2?.NavigateToString(GetErrorHtml(ex.Message));
            }
        }

        private void InitializeCategories()
        {
            comboBoxCategory.Items.Clear();
            comboBoxCategory.Items.Add(""); // Пустой элемент для всех категорий

            // Добавляем русские названия категорий
            comboBoxCategory.Items.Add("Экология, развитие экологического мышления");
            comboBoxCategory.Items.Add("Развитие креативных индустрий и инженерного мышления");
            comboBoxCategory.Items.Add("Создание и развитие архитектурно-градостроительного облика города");
            comboBoxCategory.Items.Add("Современные технологии в образовании и воспитании");
            comboBoxCategory.Items.Add("Современные технологии в здравоохранении и здравостроительстве");
            comboBoxCategory.Items.Add("Развитие туристического потенциала и сервиса, брендинг территории");
            comboBoxCategory.Items.Add("Система управления умным городом");
            comboBoxCategory.Items.Add("Вхождение в мир культуры, занятия искусством, развитие культурной среды города");
        }

        private void InitializeSourceTypes()
        {
            comboBoxSourceType.SelectedIndex = 0; // "Все источники"
        }

        //private void InitializeLanguages()
        //{
        //    // Выбираем все языки по умолчанию
        //    for (int i = 0; i < checkedListBoxLanguages.Items.Count; i++)
        //    {
        //        checkedListBoxLanguages.SetItemChecked(i, true);
        //    }
        //}

        private string GetWelcomeHtml()
        {
            return @"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8'>
                    <style>
                        body { 
                            font-family: 'Segoe UI', Arial, sans-serif; 
                            margin: 40px;
                            text-align: center;
                            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                            color: white;
                            height: 100vh;
                            display: flex;
                            flex-direction: column;
                            justify-content: center;
                            align-items: center;
                        }
                        .welcome-container {
                            background: rgba(255,255,255,0.1);
                            padding: 40px;
                            border-radius: 15px;
                            backdrop-filter: blur(10px);
                            max-width: 800px;
                        }
                        h1 {
                            font-size: 2.5em;
                            margin-bottom: 20px;
                            text-shadow: 2px 2px 4px rgba(0,0,0,0.3);
                        }
                        p {
                            font-size: 1.2em;
                            margin-bottom: 10px;
                            opacity: 0.9;
                        }
                        .icon {
                            font-size: 4em;
                            margin-bottom: 20px;
                        }
                        .features {
                            text-align: left;
                            margin-top: 30px;
                        }
                        .feature-item {
                            margin: 10px 0;
                            padding: 10px;
                            background: rgba(255,255,255,0.1);
                            border-radius: 8px;
                        }
                    </style>
                </head>
                <body>
                    <div class='welcome-container'>
                        <div class='icon'>🌍📰</div>
                        <h1>Добро пожаловать в News Aggregator</h1>
                        <p>Многоязычный агрегатор новостей с фокусом на развитие городов и технологий</p>
                        
                        <div class='features'>
                            <div class='feature-item'>🎯 <strong>8 целевых категорий</strong> для профессионального анализа</div>
                            <div class='feature-item'>🌍 <strong>150+ международных источников</strong> из 20+ стран</div>
                            <div class='feature-item'>💬 <strong>10+ языков</strong> - английский, русский, французский, немецкий и другие</div>
                            <div class='feature-item'>🎓 <strong>Университетские и исследовательские</strong> источники</div>
                            <div class='feature-item'>💾 <strong>Сохранение статей</strong> в формате Word</div>
                            <div class='feature-item'>🔍 <strong>Расширенная фильтрация</strong> по типам источников и языкам</div>
                        </div>
                        
                        <p style='margin-top: 30px;'>Используйте фильтры выше для поиска релевантных новостей на нужных языках</p>
                    </div>
                </body>
                </html>";
        }

        private string GetErrorHtml(string errorMessage)
        {
            return $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8'>
                    <style>
                        body {{ 
                            font-family: 'Segoe UI', Arial, sans-serif; 
                            margin: 40px;
                            text-align: center;
                            background: #f8f9fa;
                            color: #dc3545;
                        }}
                        .error-container {{
                            background: white;
                            padding: 30px;
                            border-radius: 10px;
                            box-shadow: 0 4px 6px rgba(0,0,0,0.1);
                        }}
                        h1 {{
                            color: #dc3545;
                            margin-bottom: 20px;
                        }}
                        .icon {{
                            font-size: 3em;
                            margin-bottom: 20px;
                        }}
                    </style>
                </head>
                <body>
                    <div class='error-container'>
                        <div class='icon'>⚠️</div>
                        <h1>Ошибка инициализации</h1>
                        <p>{EscapeHtml(errorMessage)}</p>
                        <p>Пожалуйста, установите WebView2 Runtime</p>
                    </div>
                </body>
                </html>";
        }

        private void InitializeDataGridView()
        {
            // Очищаем существующие колонки
            dataGridViewNews.Columns.Clear();

            // Настраиваем автоматическое создание колонок
            dataGridViewNews.AutoGenerateColumns = false;
            dataGridViewNews.AllowUserToAddRows = false;
            dataGridViewNews.AllowUserToDeleteRows = false;
            dataGridViewNews.ReadOnly = true;
            dataGridViewNews.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewNews.MultiSelect = false;
            dataGridViewNews.RowHeadersVisible = false;
            dataGridViewNews.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

            // Заголовок новости
            dataGridViewNews.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Title",
                HeaderText = "📰 Заголовок",
                Width = 400,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            // Источник
            dataGridViewNews.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "SourceName",
                HeaderText = "🏢 Источник",
                Width = 180,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            // Тип новости
            dataGridViewNews.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "SourceType",
                HeaderText = "📊 Тип",
                Width = 100,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            // Дата новости
            dataGridViewNews.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "PublishedAt",
                HeaderText = "📅 Дата",
                Width = 140,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "dd.MM.yy HH:mm",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                },
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            // Страна новости
            dataGridViewNews.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Country",
                HeaderText = "🌍 Страна",
                Width = 90,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            // Категория новости
            dataGridViewNews.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Category",
                HeaderText = "🏷️ Категория",
                Width = 200,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            // Язык новости
            dataGridViewNews.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Language",
                HeaderText = "💬 Язык",
                Width = 80,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            // Настраиваем внешний вид
            dataGridViewNews.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft
            };

            dataGridViewNews.EnableHeadersVisualStyles = false;
            dataGridViewNews.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dataGridViewNews.DefaultCellStyle.Padding = new Padding(3);

            // Чередование цветов строк для лучшей читаемости
            dataGridViewNews.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.Lavender
            };

            // Настраиваем отображение длинного текста в заголовке
            dataGridViewNews.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Увеличиваем высоту строк для лучшей читаемости
            dataGridViewNews.RowTemplate.Height = 28;
        }

        private async void CheckConnection()
        {
            labelStatus.Text = "Проверка соединения с сервером...";
            var isConnected = await _apiService.TestConnectionAsync();

            if (isConnected)
            {
                labelStatus.Text = "✅ Соединение установлено";
                labelStatus.ForeColor = Color.Green;
            }
            else
            {
                labelStatus.Text = "❌ Сервер недоступен";
                labelStatus.ForeColor = Color.Red;
            }
        }

        private async void buttonSearch_Click(object sender, EventArgs e)
        {
            await SearchNewsAsync();
        }

        private async Task SearchNewsAsync()
        {
            try
            {
                buttonSearch.Enabled = false;
                progressBar.Visible = true;
                labelStatus.Text = "🔍 Поиск новостей...";

                // Сбрасываем предыдущие результаты
                _currentArticles = new List<Article>();
                dataGridViewNews.DataSource = null;
                buttonSaveToWord.Visible = false;

                // Показываем сообщение о начале поиска в WebView2
                webView2Content.CoreWebView2?.NavigateToString(GetSearchingHtml());

                var request = new NewsRequest
                {
                    Query = textBoxQuery.Text?.Trim() ?? string.Empty,
                    Category = comboBoxCategory.SelectedItem?.ToString()?.Trim() ?? string.Empty,
                    From = dateTimePickerFrom.Value.Date,
                    To = dateTimePickerTo.Value.Date,
                    MaxResults = 100
                };

                // Собираем выбранные страны
                request.Countries.Clear();
                foreach (string countryItem in checkedListBoxCountries.CheckedItems)
                {
                    var countryParts = countryItem.ToString().Split('-');
                    if (countryParts.Length > 0)
                    {
                        var countryCode = countryParts[0].Trim();
                        request.Countries.Add(countryCode);
                    }
                }

                // Обрабатываем тип источников
                var sourceTypeMapping = new Dictionary<string, List<string>>
                {
                    { "Все источники", new List<string> { "News", "University", "Research" } },
                    { "Новостные сайты", new List<string> { "News" } },
                    { "Университеты", new List<string> { "University" } },
                    { "Исследовательские центры", new List<string> { "Research" } }
                };

                if (comboBoxSourceType.SelectedItem != null)
                {
                    var selectedType = comboBoxSourceType.SelectedItem.ToString();
                    if (sourceTypeMapping.ContainsKey(selectedType))
                    {
                        request.SourceTypes = sourceTypeMapping[selectedType];
                    }
                }

                // Выполняем запрос
                var response = await _apiService.GetNewsAsync(request);

                if (string.IsNullOrEmpty(response.Error))
                {
                    _currentArticles = response.Articles;

                    // Обновляем DataGridView
                    dataGridViewNews.DataSource = _currentArticles;

                    // Преобразуем типы источников в читаемые названия
                    FormatSourceTypesInGrid();

                    // Преобразуем категории в читаемые названия
                    FormatCategoriesInGrid();

                    // Преобразуем языки в читаемые названия
                    FormatLanguagesInGrid();

                    // Показываем статистику по языкам
                    var languageStats = _currentArticles.GroupBy(a => a.Language)
                        .ToDictionary(g => g.Key, g => g.Count());

                    var statsText = $"✅ Найдено {response.TotalCount} новостей";
                    //if (languageStats.Any())
                    //{
                    //    statsText += $" ({string.Join(", ", languageStats.Select(kv => $"{kv.Value} {GetLanguageName(kv.Key)}"))})";
                    //}

                    labelStatus.Text = statsText;

                    if (response.TotalCount == 0)
                    {
                        ShowNoResultsMessage();
                    }
                    else
                    {
                        // Автоматически выбираем первую статью
                        if (dataGridViewNews.Rows.Count > 0)
                        {
                            dataGridViewNews.Rows[0].Selected = true;
                            dataGridViewNews.CurrentCell = dataGridViewNews.Rows[0].Cells[0];
                        }

                        // Показываем статистику в WebView2
                        //ShowSearchResultsSummary(response.TotalCount, languageStats);
                    }
                }
                else
                {
                    MessageBox.Show($"Ошибка при поиске новостей: {response.Error}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    labelStatus.Text = "❌ Ошибка при поиске";

                    // Показываем сообщение об ошибке в WebView2
                    ShowErrorMessage(response.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла непредвиденная ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                labelStatus.Text = "❌ Ошибка при поиске";

                // Показываем сообщение об ошибке в WebView2
                ShowErrorMessage($"Непредвиденная ошибка: {ex.Message}");
            }
            finally
            {
                buttonSearch.Enabled = true;
                progressBar.Visible = false;
            }
        }

        private void FormatSourceTypesInGrid()
        {
            if (dataGridViewNews.Columns["SourceType"] != null)
            {
                foreach (DataGridViewRow row in dataGridViewNews.Rows)
                {
                    if (row.Cells["SourceType"].Value != null)
                    {
                        var sourceType = row.Cells["SourceType"].Value.ToString();
                        row.Cells["SourceType"].Value = GetSourceTypeDisplayName(sourceType);
                    }
                }
            }
        }

        private void FormatCategoriesInGrid()
        {
            if (dataGridViewNews.Columns["Category"] != null)
            {
                foreach (DataGridViewRow row in dataGridViewNews.Rows)
                {
                    if (row.Cells["Category"].Value != null)
                    {
                        var category = row.Cells["Category"].Value.ToString();
                        row.Cells["Category"].Value = GetCategoryDisplayName(category);
                    }
                }
            }
        }

        private void FormatLanguagesInGrid()
        {
            if (dataGridViewNews.Columns["Language"] != null)
            {
                foreach (DataGridViewRow row in dataGridViewNews.Rows)
                {
                    if (row.Cells["Language"].Value != null)
                    {
                        var language = row.Cells["Language"].Value.ToString();
                        row.Cells["Language"].Value = GetLanguageName(language);
                    }
                }
            }
        }

        private string GetSearchingHtml()
        {
            return @"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8'>
                    <style>
                        body { 
                            font-family: 'Segoe UI', Arial, sans-serif; 
                            margin: 40px;
                            text-align: center;
                            background: #f8f9fa;
                            color: #6c757d;
                        }
                        .searching-container {
                            background: white;
                            padding: 40px;
                            border-radius: 10px;
                            box-shadow: 0 4px 6px rgba(0,0,0,0.1);
                        }
                        .icon {
                            font-size: 4em;
                            margin-bottom: 20px;
                            opacity: 0.5;
                            animation: pulse 1.5s infinite;
                        }
                        @keyframes pulse {
                            0% { opacity: 0.5; }
                            50% { opacity: 1; }
                            100% { opacity: 0.5; }
                        }
                        h2 {
                            color: #6c757d;
                            margin-bottom: 15px;
                        }
                    </style>
                </head>
                <body>
                    <div class='searching-container'>
                        <div class='icon'>🔍</div>
                        <h2>Идет поиск новостей</h2>
                        <p>Пожалуйста, подождите...</p>
                        <p>Обрабатываются выбранные источники, категории и языки</p>
                    </div>
                </body>
                </html>";
        }

        private void ShowErrorMessage(string errorMessage)
        {
            var html = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8'>
                    <style>
                        body {{ 
                            font-family: 'Segoe UI', Arial, sans-serif; 
                            margin: 40px;
                            text-align: center;
                            background: #f8f9fa;
                            color: #dc3545;
                        }}
                        .error-container {{
                            background: white;
                            padding: 30px;
                            border-radius: 10px;
                            box-shadow: 0 4px 6px rgba(0,0,0,0.1);
                        }}
                        .error-icon {{
                            font-size: 3em;
                            margin-bottom: 20px;
                        }}
                        h2 {{
                            color: #dc3545;
                            margin-bottom: 20px;
                        }}
                        .suggestions {{
                            text-align: left;
                            margin-top: 20px;
                            padding: 15px;
                            background: #f8d7da;
                            border-radius: 5px;
                            border-left: 4px solid #dc3545;
                        }}
                    </style>
                </head>
                <body>
                    <div class='error-container'>
                        <div class='error-icon'>❌</div>
                        <h2>Ошибка при поиске</h2>
                        <p>{EscapeHtml(errorMessage)}</p>
                        
                        <div class='suggestions'>
                            <p><strong>Рекомендации:</strong></p>
                            <p>• Проверьте подключение к интернету</p>
                            <p>• Убедитесь, что сервер API запущен</p>
                            <p>• Попробуйте изменить параметры поиска</p>
                            <p>• Проверьте выбранные страны, категории и языки</p>
                        </div>
                    </div>
                </body>
                </html>";

            webView2Content.CoreWebView2?.NavigateToString(html);
        }

        private void ShowNoResultsMessage()
        {
            var html = @"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8'>
                    <style>
                        body { 
                            font-family: 'Segoe UI', Arial, sans-serif; 
                            margin: 40px;
                            text-align: center;
                            background: #f8f9fa;
                            color: #6c757d;
                        }
                        .no-results {
                            background: white;
                            padding: 40px;
                            border-radius: 10px;
                            box-shadow: 0 4px 6px rgba(0,0,0,0.1);
                        }
                        .icon {
                            font-size: 4em;
                            margin-bottom: 20px;
                            opacity: 0.5;
                        }
                        h2 {
                            color: #6c757d;
                            margin-bottom: 15px;
                        }
                        .suggestions {
                            text-align: left;
                            margin-top: 20px;
                        }
                        .suggestion-item {
                            margin: 8px 0;
                            padding: 5px;
                        }
                    </style>
                </head>
                <body>
                    <div class='no-results'>
                        <div class='icon'>🔍</div>
                        <h2>Новости не найдены</h2>
                        <p>Попробуйте изменить параметры поиска:</p>
                        <div class='suggestions'>
                            <div class='suggestion-item'>• Измените поисковый запрос</div>
                            <div class='suggestion-item'>• Выберите другую категорию</div>
                            <div class='suggestion-item'>• Расширьте временной период</div>
                            <div class='suggestion-item'>• Добавьте больше стран</div>
                            <div class='suggestion-item'>• Выберите больше языков</div>
                            <div class='suggestion-item'>• Попробуйте другой тип источников</div>
                            <div class='suggestion-item'>• Убедитесь, что выбранные RSS-ленты доступны</div>
                        </div>
                    </div>
                </body>
                </html>";

            webView2Content.CoreWebView2?.NavigateToString(html);
        }

        private void dataGridViewNews_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewNews.CurrentRow?.DataBoundItem is Article article)
            {
                _selectedArticle = article;
                DisplayArticle(article);
                buttonSaveToWord.Visible = true;
            }
        }

        private void DisplayArticle(Article article)
        {
            string languageBadge = GetLanguageName(article.Language).ToUpper();

            string htmlContent = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8'>
                    <style>
                        body {{ 
                            font-family: 'Segoe UI', Arial, sans-serif; 
                            margin: 30px;
                            line-height: 1.6;
                            color: #333;
                            background: #ffffff;
                        }}
                        .article-container {{
                            max-width: 900px;
                            margin: 0 auto;
                        }}
                        h1 {{ 
                            color: #2c3e50; 
                            border-bottom: 3px solid #3498db;
                            padding-bottom: 15px;
                            margin-bottom: 20px;
                            font-size: 2em;
                        }}
                        .meta-info {{ 
                            background: #f8f9fa;
                            padding: 20px;
                            border-radius: 8px;
                            margin-bottom: 25px;
                            border-left: 4px solid #3498db;
                        }}
                        .meta-item {{
                            margin-bottom: 8px;
                            display: flex;
                            align-items: center;
                        }}
                        .meta-icon {{
                            margin-right: 10px;
                            font-size: 1.1em;
                        }}
                        .content-section {{
                            margin-bottom: 25px;
                        }}
                        .section-title {{
                            color: #2c3e50;
                            border-bottom: 2px solid #e9ecef;
                            padding-bottom: 8px;
                            margin-bottom: 15px;
                            font-size: 1.3em;
                        }}
                        img {{ 
                            max-width: 100%; 
                            height: auto;
                            border-radius: 8px;
                            margin: 15px 0;
                            box-shadow: 0 4px 8px rgba(0,0,0,0.1);
                        }}
                        .original-link {{
                            display: inline-block;
                            background: #3498db;
                            color: white;
                            padding: 12px 25px;
                            border-radius: 6px;
                            text-decoration: none;
                            font-weight: bold;
                            margin-top: 20px;
                            transition: background 0.3s;
                        }}
                        .original-link:hover {{
                            background: #2980b9;
                            text-decoration: none;
                            color: white;
                        }}
                        .no-image {{
                            text-align: center;
                            color: #7f8c8d;
                            font-style: italic;
                            padding: 40px;
                            background: #f8f9fa;
                            border-radius: 8px;
                        }}
                        .source-badge {{
                            display: inline-block;
                            background: #e74c3c;
                            color: white;
                            padding: 4px 8px;
                            border-radius: 4px;
                            font-size: 0.8em;
                            margin-left: 10px;
                        }}
                        .language-badge {{
                            display: inline-block;
                            background: #9b59b6;
                            color: white;
                            padding: 4px 8px;
                            border-radius: 4px;
                            font-size: 0.8em;
                            margin-left: 10px;
                            font-weight: bold;
                        }}
                    </style>
                </head>
                <body>
                    <div class='article-container'>
                        <h1>{EscapeHtml(article.Title)}</h1>
                        
                        <div class='meta-info'>
                            <div class='meta-item'><span class='meta-icon'>📰</span> <strong>Источник:</strong> {EscapeHtml(article.SourceName)} <span class='source-badge'>{GetSourceTypeDisplayName(article.SourceType)}</span></div>
                            <div class='meta-item'><span class='meta-icon'>📅</span> <strong>Дата публикации:</strong> {article.PublishedAt:dd.MM.yyyy HH:mm}</div>
                            <div class='meta-item'><span class='meta-icon'>🏷️</span> <strong>Категория:</strong> {GetCategoryDisplayName(article.Category)}</div>
                            <div class='meta-item'><span class='meta-icon'>🌍</span> <strong>Страна:</strong> {article.Country}</div>
                            <div class='meta-item'><span class='meta-icon'>💬</span> <strong>Язык:</strong> {GetFullLanguageName(article.Language)}</div>
                        </div>

                        {(string.IsNullOrEmpty(article.UrlToImage) ?
                            "<div class='no-image'>📷 Изображение отсутствует</div>" :
                            $"<img src='{article.UrlToImage}' alt='Изображение статьи' onerror='this.style.display=\"none\";'>")}
                        
                        <div class='content-section'>
                            <h3 class='section-title'>📝 Описание</h3>
                            <p>{(string.IsNullOrEmpty(article.Description) ? "Описание отсутствует" : EscapeHtml(article.Description))}</p>
                        </div>

                        <div class='content-section'>
                            <h3 class='section-title'>📄 Содержание</h3>
                            <p>{(string.IsNullOrEmpty(article.Content) ? "Содержание отсутствует" : EscapeHtml(article.Content))}</p>
                        </div>

                        <a href='{article.Url}' class='original-link' target='_blank'>🔗 Читать оригинал статьи</a>
                    </div>
                </body>
                </html>";

            webView2Content.CoreWebView2?.NavigateToString(htmlContent);
        }

        private string EscapeHtml(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "";

            return System.Net.WebUtility.HtmlEncode(text)
                .Replace("\n", "<br/>")
                .Replace("\r", "")
                .Replace("  ", " &nbsp;");
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

        private string GetLanguageName(string code)
        {
            return code.ToLower() switch
            {
                "en" => "англ.",
                "ru" => "рус.",
                "fr" => "фр.",
                "de" => "нем.",
                "es" => "исп.",
                "it" => "ит.",
                "ja" => "яп.",
                "ko" => "кор.",
                "zh" => "кит.",
                "ar" => "ар.",
                _ => code
            };
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

        private async void buttonSaveToWord_Click(object sender, EventArgs e)
        {
            if (_selectedArticle == null)
            {
                MessageBox.Show("Сначала выберите статью из списка", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Документы Word (*.docx)|*.docx";
                saveDialog.FileName = SanitizeFileName(_selectedArticle.Title) + ".docx";
                saveDialog.DefaultExt = ".docx";
                saveDialog.AddExtension = true;

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        buttonSaveToWord.Enabled = false;
                        buttonSaveToWord.Text = "💾 Сохранение...";

                        var success = await _wordExportService.ExportToWordAsync(_selectedArticle, saveDialog.FileName);

                        if (success)
                        {
                            MessageBox.Show("Статья успешно сохранена в Word!", "Успех",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при сохранении файла", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        buttonSaveToWord.Enabled = true;
                        buttonSaveToWord.Text = "💾 Сохранить в Word";
                    }
                }
            }
        }

        private string SanitizeFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return "article";

            var invalidChars = Path.GetInvalidFileNameChars();
            var sanitized = new string(fileName.Where(ch => !invalidChars.Contains(ch)).ToArray());

            // Ограничиваем длину имени файла
            if (sanitized.Length > 50)
                sanitized = sanitized.Substring(0, 50);

            return sanitized.Trim();
        }
    }
}