using Microsoft.Extensions.Configuration;
using System.IO;

namespace AllCommands.Portal.Helpers
{
    internal static class ConfigUtility
    {
        private static readonly IConfiguration _configuration;
        static ConfigUtility()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _configuration = builder.Build();

        }

        internal static IConfigurationSection GetSection(string key)
        {
            return _configuration.GetSection(key);
        }
    }
}
