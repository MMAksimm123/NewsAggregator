using Microsoft.Extensions.DependencyInjection;
using NewsAggregator.Client.Forms;
using NewsAggregator.Client.Services;

namespace NewsAggregator.Client
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Настройка Dependency Injection
            var services = new ServiceCollection();
            ConfigureServices(services);

            using var serviceProvider = services.BuildServiceProvider();
            var mainForm = serviceProvider.GetRequiredService<MainForm>();

            Application.Run(mainForm);
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddHttpClient();
            services.AddSingleton<IApiService, ApiService>();
            services.AddSingleton<IWordExportService, WordExportService>();
            services.AddTransient<MainForm>();
        }
    }
}