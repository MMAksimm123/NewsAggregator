namespace NewsAggregator.Client.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox textBoxQuery;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.CheckedListBox checkedListBoxCountries;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.DataGridView dataGridViewNews;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView2Content;
        private System.Windows.Forms.Button buttonSaveToWord;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelFilters;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label labelQuery;
        private System.Windows.Forms.Label labelCategory;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Label labelCountries;
        private System.Windows.Forms.Label labelDateSeparator;
        private System.Windows.Forms.ComboBox comboBoxSourceType;
        private System.Windows.Forms.Label labelSourceType;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            panelFilters = new Panel();
            labelSourceType = new Label();
            comboBoxSourceType = new ComboBox();
            labelDateSeparator = new Label();
            labelCountries = new Label();
            labelDate = new Label();
            labelCategory = new Label();
            labelQuery = new Label();
            progressBar = new ProgressBar();
            labelStatus = new Label();
            buttonSearch = new Button();
            checkedListBoxCountries = new CheckedListBox();
            dateTimePickerTo = new DateTimePicker();
            dateTimePickerFrom = new DateTimePicker();
            comboBoxCategory = new ComboBox();
            textBoxQuery = new TextBox();
            panelMain = new Panel();
            splitContainer1 = new SplitContainer();
            dataGridViewNews = new DataGridView();
            buttonSaveToWord = new Button();
            webView2Content = new Microsoft.Web.WebView2.WinForms.WebView2();
            tableLayoutPanel1.SuspendLayout();
            panelFilters.SuspendLayout();
            panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewNews).BeginInit();
            ((System.ComponentModel.ISupportInitialize)webView2Content).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(panelFilters, 0, 0);
            tableLayoutPanel1.Controls.Add(panelMain, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 220F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1400, 900);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panelFilters
            // 
            panelFilters.Controls.Add(labelSourceType);
            panelFilters.Controls.Add(comboBoxSourceType);
            panelFilters.Controls.Add(labelDateSeparator);
            panelFilters.Controls.Add(labelCountries);
            panelFilters.Controls.Add(labelDate);
            panelFilters.Controls.Add(labelCategory);
            panelFilters.Controls.Add(labelQuery);
            panelFilters.Controls.Add(progressBar);
            panelFilters.Controls.Add(labelStatus);
            panelFilters.Controls.Add(buttonSearch);
            panelFilters.Controls.Add(checkedListBoxCountries);
            panelFilters.Controls.Add(dateTimePickerTo);
            panelFilters.Controls.Add(dateTimePickerFrom);
            panelFilters.Controls.Add(comboBoxCategory);
            panelFilters.Controls.Add(textBoxQuery);
            panelFilters.Dock = DockStyle.Fill;
            panelFilters.Location = new Point(3, 3);
            panelFilters.Name = "panelFilters";
            panelFilters.Size = new Size(1394, 214);
            panelFilters.TabIndex = 0;
            // 
            // labelSourceType
            // 
            labelSourceType.Anchor = AnchorStyles.Top;
            labelSourceType.AutoSize = true;
            labelSourceType.Location = new Point(384, 163);
            labelSourceType.Name = "labelSourceType";
            labelSourceType.Size = new Size(99, 15);
            labelSourceType.TabIndex = 14;
            labelSourceType.Text = "Тип источников:";
            // 
            // comboBoxSourceType
            // 
            comboBoxSourceType.Anchor = AnchorStyles.Top;
            comboBoxSourceType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxSourceType.FormattingEnabled = true;
            comboBoxSourceType.Items.AddRange(new object[] { "Все источники", "Новостные сайты", "Университеты", "Исследовательские центры" });
            comboBoxSourceType.Location = new Point(480, 160);
            comboBoxSourceType.Name = "comboBoxSourceType";
            comboBoxSourceType.Size = new Size(200, 23);
            comboBoxSourceType.TabIndex = 13;
            // 
            // labelDateSeparator
            // 
            labelDateSeparator.Anchor = AnchorStyles.Top;
            labelDateSeparator.AutoSize = true;
            labelDateSeparator.Location = new Point(236, 63);
            labelDateSeparator.Name = "labelDateSeparator";
            labelDateSeparator.Size = new Size(21, 15);
            labelDateSeparator.TabIndex = 12;
            labelDateSeparator.Text = "по";
            // 
            // labelCountries
            // 
            labelCountries.Anchor = AnchorStyles.Top;
            labelCountries.AutoSize = true;
            labelCountries.Location = new Point(425, 63);
            labelCountries.Name = "labelCountries";
            labelCountries.Size = new Size(52, 15);
            labelCountries.TabIndex = 11;
            labelCountries.Text = "Страны:";
            // 
            // labelDate
            // 
            labelDate.Anchor = AnchorStyles.Top;
            labelDate.AutoSize = true;
            labelDate.Location = new Point(23, 63);
            labelDate.Name = "labelDate";
            labelDate.Size = new Size(52, 15);
            labelDate.TabIndex = 10;
            labelDate.Text = "Период:";
            // 
            // labelCategory
            // 
            labelCategory.Anchor = AnchorStyles.Top;
            labelCategory.AutoSize = true;
            labelCategory.Location = new Point(407, 23);
            labelCategory.Name = "labelCategory";
            labelCategory.Size = new Size(66, 15);
            labelCategory.TabIndex = 9;
            labelCategory.Text = "Категория:";
            // 
            // labelQuery
            // 
            labelQuery.Anchor = AnchorStyles.Top;
            labelQuery.AutoSize = true;
            labelQuery.Location = new Point(30, 23);
            labelQuery.Name = "labelQuery";
            labelQuery.Size = new Size(50, 15);
            labelQuery.TabIndex = 8;
            labelQuery.Text = "Запрос:";
            // 
            // progressBar
            // 
            progressBar.Anchor = AnchorStyles.Top;
            progressBar.Location = new Point(1050, 100);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(140, 20);
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.TabIndex = 7;
            progressBar.Visible = false;
            // 
            // labelStatus
            // 
            labelStatus.Anchor = AnchorStyles.Top;
            labelStatus.AutoSize = true;
            labelStatus.Location = new Point(1050, 80);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(88, 15);
            labelStatus.TabIndex = 6;
            labelStatus.Text = "Готов к работе";
            // 
            // buttonSearch
            // 
            buttonSearch.Anchor = AnchorStyles.Top;
            buttonSearch.BackColor = Color.SteelBlue;
            buttonSearch.FlatStyle = FlatStyle.Flat;
            buttonSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonSearch.ForeColor = Color.White;
            buttonSearch.Location = new Point(1050, 20);
            buttonSearch.Name = "buttonSearch";
            buttonSearch.Size = new Size(140, 35);
            buttonSearch.TabIndex = 5;
            buttonSearch.Text = "🔍 Поиск новостей";
            buttonSearch.UseVisualStyleBackColor = false;
            // 
            // checkedListBoxCountries
            // 
            checkedListBoxCountries.Anchor = AnchorStyles.Top;
            checkedListBoxCountries.CheckOnClick = true;
            checkedListBoxCountries.FormattingEnabled = true;
            checkedListBoxCountries.Items.AddRange(new object[] { "us - США", "gb - Великобритания", "ca - Канада", "jp - Япония", "kr - Южная Корея", "ch - Швейцария", "de - Германия", "fr - Франция", "au - Австралия", "sg - Сингапур", "qa - Катар", "es - Испания", "nl - Нидерланды", "gr - Греция", "un - ООН", "ru - Россия", "it - Италия", "cn - Китай" });
            checkedListBoxCountries.Location = new Point(480, 60);
            checkedListBoxCountries.Name = "checkedListBoxCountries";
            checkedListBoxCountries.Size = new Size(200, 94);
            checkedListBoxCountries.TabIndex = 4;
            // 
            // dateTimePickerTo
            // 
            dateTimePickerTo.Anchor = AnchorStyles.Top;
            dateTimePickerTo.Location = new Point(260, 60);
            dateTimePickerTo.Name = "dateTimePickerTo";
            dateTimePickerTo.Size = new Size(150, 23);
            dateTimePickerTo.TabIndex = 3;
            dateTimePickerTo.Value = new DateTime(2025, 10, 26, 17, 28, 42, 358);
            // 
            // dateTimePickerFrom
            // 
            dateTimePickerFrom.Anchor = AnchorStyles.Top;
            dateTimePickerFrom.Location = new Point(80, 60);
            dateTimePickerFrom.Name = "dateTimePickerFrom";
            dateTimePickerFrom.Size = new Size(150, 23);
            dateTimePickerFrom.TabIndex = 2;
            dateTimePickerFrom.Value = new DateTime(2025, 10, 19, 17, 28, 42, 359);
            // 
            // comboBoxCategory
            // 
            comboBoxCategory.Anchor = AnchorStyles.Top;
            comboBoxCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxCategory.FormattingEnabled = true;
            comboBoxCategory.Location = new Point(480, 20);
            comboBoxCategory.Name = "comboBoxCategory";
            comboBoxCategory.Size = new Size(400, 23);
            comboBoxCategory.TabIndex = 1;
            // 
            // textBoxQuery
            // 
            textBoxQuery.Anchor = AnchorStyles.Top;
            textBoxQuery.Location = new Point(80, 20);
            textBoxQuery.Name = "textBoxQuery";
            textBoxQuery.PlaceholderText = "Введите поисковый запрос...";
            textBoxQuery.Size = new Size(300, 23);
            textBoxQuery.TabIndex = 0;
            // 
            // panelMain
            // 
            panelMain.Controls.Add(splitContainer1);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(3, 223);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(1394, 674);
            panelMain.TabIndex = 1;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dataGridViewNews);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(buttonSaveToWord);
            splitContainer1.Panel2.Controls.Add(webView2Content);
            splitContainer1.Size = new Size(1394, 674);
            splitContainer1.SplitterDistance = 699;
            splitContainer1.TabIndex = 0;
            // 
            // dataGridViewNews
            // 
            dataGridViewNews.AllowUserToAddRows = false;
            dataGridViewNews.AllowUserToDeleteRows = false;
            dataGridViewNews.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewNews.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewNews.Dock = DockStyle.Fill;
            dataGridViewNews.Location = new Point(0, 0);
            dataGridViewNews.Name = "dataGridViewNews";
            dataGridViewNews.ReadOnly = true;
            dataGridViewNews.RowHeadersVisible = false;
            dataGridViewNews.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewNews.Size = new Size(699, 674);
            dataGridViewNews.TabIndex = 0;
            // 
            // buttonSaveToWord
            // 
            buttonSaveToWord.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonSaveToWord.BackColor = Color.ForestGreen;
            buttonSaveToWord.FlatStyle = FlatStyle.Flat;
            buttonSaveToWord.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonSaveToWord.ForeColor = Color.White;
            buttonSaveToWord.Location = new Point(550, 6);
            buttonSaveToWord.Name = "buttonSaveToWord";
            buttonSaveToWord.Size = new Size(140, 30);
            buttonSaveToWord.TabIndex = 1;
            buttonSaveToWord.Text = "💾 Сохранить в Word";
            buttonSaveToWord.UseVisualStyleBackColor = false;
            buttonSaveToWord.Visible = false;
            // 
            // webView2Content
            // 
            webView2Content.AllowExternalDrop = true;
            webView2Content.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            webView2Content.CreationProperties = null;
            webView2Content.DefaultBackgroundColor = Color.White;
            webView2Content.Location = new Point(3, 40);
            webView2Content.Name = "webView2Content";
            webView2Content.Size = new Size(685, 631);
            webView2Content.TabIndex = 0;
            webView2Content.ZoomFactor = 1D;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1400, 900);
            Controls.Add(tableLayoutPanel1);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "News Aggregator - Многоязычный агрегатор новостей";
            tableLayoutPanel1.ResumeLayout(false);
            panelFilters.ResumeLayout(false);
            panelFilters.PerformLayout();
            panelMain.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewNews).EndInit();
            ((System.ComponentModel.ISupportInitialize)webView2Content).EndInit();
            ResumeLayout(false);

        }

        #endregion
    }
}