using NewsAggregator.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// HTTP Client
builder.Services.AddHttpClient();

// Логирование
builder.Services.AddLogging();

// CORS для доступа из Windows Forms приложения
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Регистрация сервисов
builder.Services.AddScoped<INewsAggregator, NewsAggregatorService>();
builder.Services.AddScoped<INewsService, RssNewsService>();
// Добавьте здесь другие сервисы новостей

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseRouting();

// Добавляем обработчик для корневого URL
app.MapGet("/", () => Results.Json(new
{
    message = "News Aggregator API",
    version = "1.0",
    endpoints = new
    {
        test = "/api/news/test",
        sources = "/api/news/sources",
        search = "/api/news/search"
    },
    timestamp = DateTime.Now
}));

app.MapControllers();

app.Run();