# NewsAggregator

**Key features**
🔍 News search by keywords and categories
🌍 Country-based filtering (over 20 countries)
📂 Source types: news sites, universities, research centers
🏷️ 8 targeted categories for professional analysis
📄 Article viewing via built-in WebView2 with HTML formatting support
💾 Saving articles in Word format (.docx)

**🛠 Technology stack**
_Backend (API)_
1. ASP.NET Core 9
2. C#
3. System.ServiceModel.Syndication (RSS parsing)
4. Swagger/OpenAPI (API documentation)
5. CORS (for client access)


_Client (Windows Forms)_
1. Windows Forms
2. Microsoft.Web.WebView2
3. DocumentFormat.OpenXml
4. Microsoft.Extensions.DependencyInjection
5. System.Text.Json


**🚀 Installation and Launch**

_Requirements_

.NET SDK 6.0+
WebView2 Runtime (for client)
Visual Studio 2022 или VS Code

_Step 1. Cloning the repository_

`git clone https://github.com/your-username/news-aggregator.git`

`cd news-aggregator`

_Step 2. Launching the backend (API)_

`cd NewsAggregator.API`

`dotnet restore`

`dotnet run`

he API will be available at `http://localhost:5000`.


_Step 3. Launching the client (Windows Forms)_

`cd NewsAggregator.Client

`dotnet restore

`dotnet run`

**Note:** Ensure the API is running before the client. Upon the first launch, the client will check the connection.
