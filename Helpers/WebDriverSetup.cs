using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using WebOrderTests.Helpers;

namespace WebOrderTests
{
    public static class WebDriverSetup
    {
        public static IWebDriver GetWebDriver()
        {
            string browser = ConfigReader.GetConfigValue("Browser").ToLower();

            return browser switch
            {
                "firefox" => new FirefoxDriver(),
                "chrome" => new ChromeDriver(),
                "edge" => new EdgeDriver(),
                _ => throw new ArgumentException($"Unsupported browser: {browser}")
            };
        }
    }
}
