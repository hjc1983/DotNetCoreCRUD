using System.IO;
using Microsoft.Extensions.Configuration;

namespace Database
{
        static class ConfigurationManager
        {
            public static IConfiguration AppSetting { get; }
            static ConfigurationManager()
            {
                AppSetting = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appSettings.json")
                    .Build();
            }
        }
    
}
