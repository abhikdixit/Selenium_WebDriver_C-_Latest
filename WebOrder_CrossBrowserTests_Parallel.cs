using NUnit.Framework;
using OpenQA.Selenium;
using WebOrderTests.Helpers;
using WebOrderTestsParallel;
using System;

namespace WebOrderTests_Parallel_Browsers
{
    [TestFixture("chrome")]
    [TestFixture("firefox")]
    [TestFixture("edge")]
    [Parallelizable(ParallelScope.All)] // Enables parallel execution
    public class WebOrderCrossBrowserTestsParallel
    {
        private IWebDriver driver;
        private readonly string browser;
        private readonly string url;

        public WebOrderCrossBrowserTestsParallel(string browser)
        {
            this.browser = browser;
            url = ConfigReader.GetConfigValue("Url");
        }

        [SetUp]
        public void Setup()
        {
            driver = WebDriverSetup_Parallel.GetWebDrivers(browser);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);
        }

        [Test]
        public void Login_To_App()
        {
            Console.WriteLine($"Running test on browser: {browser}");

            driver.FindElement(By.Name("ctl00$MainContent$username")).Clear();
            driver.FindElement(By.Name("ctl00$MainContent$username")).SendKeys("Tester");
            driver.FindElement(By.Name("ctl00$MainContent$password")).Clear();
            driver.FindElement(By.Name("ctl00$MainContent$password")).SendKeys("test");
            driver.FindElement(By.CssSelector("input[name='ctl00$MainContent$login_button']")).Click();

            bool isLogoutDisplayed = driver.FindElement(By.LinkText("Logout")).Displayed;
            Assert.That(isLogoutDisplayed, Is.True, "Logout button should be displayed after successful login.");

            driver.FindElement(By.LinkText("Logout")).Click();
        }

        [TearDown]
        public void Cleanup()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }
    }
}
