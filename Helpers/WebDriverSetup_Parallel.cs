using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace WebOrderTestsParallel
{
    public static class WebDriverSetup_Parallel
    {
        public static IWebDriver GetWebDrivers(string browser)
        {
            return browser.ToLower() switch
            {
                "firefox" => new FirefoxDriver(),
                "chrome" => new ChromeDriver(),
                "edge" => new EdgeDriver(),
                _ => throw new ArgumentException($"Unsupported browser: {browser}")
            };
        }
    }
}
