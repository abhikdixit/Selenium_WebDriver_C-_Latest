using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace WebOrderTests.Helpers
{
    public static class ConfigReader
    {
        private static JObject config;

        static ConfigReader()
        {
            try
            {
                string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings_all_browser.json");
                string jsonContent = File.ReadAllText(jsonPath);
                config = JObject.Parse(jsonContent);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading config file: {ex.Message}");
            }
        }

        public static string GetConfigValue(string key)
        {
            return config[key]?.ToString();
        }
    }
}
