using NUnit.Framework;
using OpenQA.Selenium;
using WebOrderTests.Helpers;
using System;

namespace WebOrderTests
{
    [TestFixture]
    public class WebOrderCrossBrowserTests
    {
        private IWebDriver driver;
        private string url;

        [OneTimeSetUp]
        public void Setup()
        {
            url = ConfigReader.GetConfigValue("Url");
            driver = WebDriverSetup.GetWebDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);
        }

        [Test]
        public void Login_To_App()
        {
            Console.WriteLine($"Running test on browser: {ConfigReader.GetConfigValue("Browser")}");

            driver.FindElement(By.Name("ctl00$MainContent$username")).Clear();
            driver.FindElement(By.Name("ctl00$MainContent$username")).SendKeys("Tester");
            driver.FindElement(By.Name("ctl00$MainContent$password")).Clear();
            driver.FindElement(By.Name("ctl00$MainContent$password")).SendKeys("test");
            driver.FindElement(By.CssSelector("input[name='ctl00$MainContent$login_button']")).Click();

            bool isLogoutDisplayed = driver.FindElement(By.LinkText("Logout")).Displayed;
            Assert.That(isLogoutDisplayed, Is.True, "Logout button should be displayed after successful login.");

            driver.FindElement(By.LinkText("Logout")).Click();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }
    }
}
