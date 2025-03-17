using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace pk_Advance_Topics
{
    public class BasicAuthTest_PassData
    {
        IWebDriver driver;

        [SetUp]
        public void LaunchBrowser_Test()
        {
            // Initialize ChromeDriver
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable-extensions");
            options.AddArgument("--disable-notifications");
            options.AddArgument("--disable-popup-blocking");

            //Add basic authentication credentials to the URL
            options.AddArgument("--auth-server-whitelist=*");
            options.AddArgument("--auth-negotiate-delegate-whitelist=*");
            options.AddArgument("--disable-web-security");
            options.AddArgument("--allow-running-insecure-content");

            driver = new ChromeDriver(options);

            // Maximize the browser window
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void BasicAuthPageHandling()
        {
            // Navigate to the page with basic authentication
            string username = "admin";
            string password = "admin";
            string url = $"http://{username}:{password}@the-internet.herokuapp.com/basic_auth";
            driver.Navigate().GoToUrl(url);

            // Wait for the success message to be displayed
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement successMessage = wait.Until(driver => driver.FindElement(By.XPath("//p[contains(text(),'Congratulations! You must have the proper credenti')]")));

            // Verify the success message
            string actualMessage = successMessage.Text;
            string expectedMessage = "Congratulations! You must have the proper credentials.";
            Assert.That(actualMessage, Is.EqualTo(expectedMessage), "The confirmation message does not match the expected message.");
        }

        [TearDown]
        public void CloseBrowser()
        {
            // Close the browser
            driver.Quit();
        }
    }
}