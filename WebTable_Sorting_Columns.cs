using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace pk_Advance_Topics
{
    public class WebTable_Sorting_Columns
    {
        IWebDriver driver;

        [SetUp]
        public void PreCondition()
        {
            // Set up ChromeDriver using WebDriverManager (not directly available in C#, use ChromeDriver executable in PATH)
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void PostCondition()
        {
            driver.Quit(); // Close the browser after the test
        }

        [Test]
        public void VerifySort()
        {
            driver.Navigate().GoToUrl("https://datatables.net/examples/data_sources/server_side");
            Thread.Sleep(2000); // Wait for the page to load

            // Locate the header of the first column
            IWebElement header = driver.FindElement(By.XPath("//table[@id='example']/thead/tr/th[1]"));

            // Assert that the column is initially sorted in ascending order
            Assert.That(header.GetAttribute("class"), Does.Contain("asc"), "Column is not sorted in ascending order initially.");

            // Get the first row's first cell value
            IWebElement firstRow = driver.FindElement(By.XPath("//table[@id='example']/tbody/tr[1]/td[1]"));
            string firstFName = firstRow.Text;

            // Assert that the first name is "Airi"
            Assert.That(firstFName, Is.EqualTo("Airi"), "First name in the first row is not 'Airi'.");

            // Click the header to sort in descending order
            header.Click();
            Thread.Sleep(2000); // Wait for the sorting to complete

            // Assert that the column is now sorted in descending order
            Assert.That(header.GetAttribute("class"), Does.Contain("desc"), "Column is not sorted in descending order after clicking.");

            // Get the first row's first cell value after sorting
            firstRow = driver.FindElement(By.XPath("//table[@id='example']/tbody/tr[1]/td[1]"));
            firstFName = firstRow.Text;

            // Assert that the first name is now "Zorita"
            Assert.That(firstFName, Is.EqualTo("Zorita"), "First name in the first row is not 'Zorita' after sorting.");
        }
    }
}