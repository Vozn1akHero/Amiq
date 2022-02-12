using Microsoft.Extensions.Configuration;
using System.IO;

namespace Amiq.Services.Group.Core
{
    /// <summary>
    /// Pomocnicza klasa dostarczająca konfigurację w np. kontekstach statycznych
    /// </summary>
    public class StaticContextConfigurationProvider
    {
        public string appSettingValue { get; set; }

        public StaticContextConfigurationProvider(IConfiguration config, string Key)
        {
            appSettingValue = config.GetValue<string>(Key);
        }

        public static string GetAppSetting(string key)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();

            var settings = new StaticContextConfigurationProvider(configuration.GetSection("ApplicationSettings"), key);

            return settings.appSettingValue;
        }
    }
}
