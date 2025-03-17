using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace pk_Advance_Topics
{
    [TestFixture]
    [Category("SanityTest")]
    public class Execute_JavaScript_CaptureScrapeData
    {
        IWebDriver driver;

        [OneTimeSetUp]
        public void LaunchBrowser()
        {
            // Set up ChromeDriver (ensure ChromeDriver is installed and in PATH)
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void GetURL_Domain_Title()
        {
            // Creating the JavascriptExecutor interface object by Type casting
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            // Launching the Site
            driver.Navigate().GoToUrl("https://opensource-demo.orangehrmlive.com/web/index.php/auth/login");

            // Fetching the Domain Name of the site
            string DomainName = (string)js.ExecuteScript("return document.domain;");
            Console.WriteLine("Domain name of the site = " + DomainName);

            // Fetching the URL of the site
            string url = (string)js.ExecuteScript("return document.URL;");
            Console.WriteLine("URL of the site = " + url);

            // Fetching the Title of the site
            string TitleName = (string)js.ExecuteScript("return document.title;");
            Console.WriteLine("Title of the page = " + TitleName);

            // Assertions to verify the data
            Assert.That(DomainName, Is.EqualTo("opensource-demo.orangehrmlive.com"), "Domain name does not match.");
            Assert.That(url, Is.EqualTo("https://opensource-demo.orangehrmlive.com/web/index.php/auth/login"), "URL does not match.");
            Assert.That(TitleName, Is.EqualTo("OrangeHRM"), "Page title does not match.");
        }

        [OneTimeTearDown]
        public void CloseBrowser()
        {
            // Close the browser
            driver.Quit();
        }
    }
}