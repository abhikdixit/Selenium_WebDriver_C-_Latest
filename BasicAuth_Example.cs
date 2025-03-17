using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace pk_Advance_Topics
{
    [TestFixture]
    [Category("SanityTest")]
    public class Scenario2_BasicAuth
    {
        private IWebDriver driver;

        [Test]
        public void SetUp()
        {
            // Set up FirefoxDriver (ensure GeckoDriver is in your PATH or specify its location)
            driver = new ChromeDriver();

            // For Window Pop up, pass username and password along with the URL
            // Format: https://username:password@
            driver.Navigate().GoToUrl("https://admin:admin@the-internet.herokuapp.com/basic_auth");

            // Get the confirmation message from the page
            IWebElement confirmationMessageElement = driver.FindElement(By.CssSelector("p"));
            string confMsg = confirmationMessageElement.Text;
            Console.WriteLine(confMsg);

            // Expected message
            string expMessage = "Congratulations! You must have the proper credentials.";

            // Assert that the confirmation message matches the expected message
            Assert.That(confMsg, Is.EqualTo(expMessage), "The confirmation message does not match the expected message.");
        }

        [TearDown]
        public void TearDown()
        {
            // Close the browser after the test
            if (driver != null)
                driver.Quit();
        }
    }
}