using Microsoft.Extensions.Configuration;

namespace Presentation.WPF.Configuration
{
    public static class AppConfiguration
    {
        public static IConfiguration Load()
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }
    }
}