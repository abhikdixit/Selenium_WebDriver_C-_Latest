using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SeleniumTests
{
    [TestFixture]
    public class BingSearchTests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            // Initialize ChromeDriver
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        [Category("Smoke")]
        public void SearchOnBing_Smoke()
        {
            // Navigate to Bing
            driver.Navigate().GoToUrl("https://www.bing.com");

            // Accept Cookies if prompted
            try
            {
                var acceptCookiesButton = driver.FindElement(By.XPath("//button[contains(text(), 'Accept all')]"));
                acceptCookiesButton.Click();
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("No cookie prompt found.");
            }

            // Type in the search box
            var searchBox = driver.FindElement(By.Name("q"));
            searchBox.SendKeys("playwright automation");

            // Wait for auto-suggestions to appear
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.CssSelector("ul[aria-label='Suggestions']")));

            // Capture all auto-suggestions
            var suggestionElements = driver.FindElements(By.CssSelector("ul[aria-label='Suggestions']"));
            var suggestions = suggestionElements.Select(el => el.Text).ToList();

            Console.WriteLine("Auto-suggestions:");
            foreach (var suggestion in suggestions)
            {
                Console.WriteLine(suggestion);
            }

            // Click the first suggestion
            suggestionElements.First().Click();

            // Wait for results to load
            wait.Until(d => d.Title.Contains("playwright automation"));

            // Log the page title
            Console.WriteLine("Page title after selecting suggestion: " + driver.Title);
        }

        [TearDown]
        public void Teardown()
        {
            // Close the browser
            driver.Quit();
        }
    }
}