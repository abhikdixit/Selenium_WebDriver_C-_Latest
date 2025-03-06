using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;

namespace pk_Advance_Topics
{
    public class SSL_Certificate_Example_All_Browser
    {
        IWebDriver driver;

        [SetUp]
        public void LaunchBrowserEdge()
        {
            EdgeOptions options = new EdgeOptions();
            options.AddArgument("incognito");
            options.AcceptInsecureCertificates = true; // Corrected property name
            driver = new EdgeDriver(options);
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void OpenApplication()
        {
            Console.WriteLine("Navigating to application");
            driver.Navigate().GoToUrl("https://expired.badssl.com/");
            string actTitle = driver.Title;
            string expTitle = "expired.badssl.com";

            // Using Assert.That with Is.EqualTo
            Assert.That(actTitle, Is.EqualTo(expTitle), "The page title does not match the expected title.");
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
                driver.Quit(); // Corrected method name
        }
    }
}