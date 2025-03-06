using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;


namespace BaseClass
{
    public class WebDriverSetup
    {
        private static IWebDriver driver;

        public static IWebDriver CrossBrowserTesting(string browser)
        {
            switch (browser.ToLower())
            {
                case "firefox":
                    driver = new FirefoxDriver();
                    break;

                case "chrome":
                    driver = new ChromeDriver();
                    break;

                case "edge":
                    driver = new EdgeDriver();
                    break;

                default:
                    Console.WriteLine("Invalid browser name. Defaulting to Chrome.");
                    driver = new ChromeDriver();
                    break;
            }

            driver.Manage().Window.Maximize();
            return driver;
        }
    }
}
