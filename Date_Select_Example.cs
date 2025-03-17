using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;

namespace pk_Advance_Topics
{
    [TestFixture]
    [Category("SanityTest")]
    public class Date_Select_Example
    {
        private IWebDriver driver;
        private string userChoiceDate = "22"; // The date you want to select

        [Test]
        public void DateSelect()
        {
            // Set up ChromeDriver
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            // Navigate to the date picker page
            driver.Navigate().GoToUrl("http://jqueryui.com/resources/demos/datepicker/other-months.html");

            // Click the date picker input field to open the calendar
            driver.FindElement(By.Id("datepicker")).Click();

            // Find all date elements in the calendar
            IReadOnlyCollection<IWebElement> allDates = driver.FindElements(By.XPath("//table[@class='ui-datepicker-calendar']//td"));

            // Get the current day of the month
            int currentDay = DateTime.Now.Day;

            // Iterate through the dates and select the current day
            foreach (IWebElement ele in allDates)
            {
                string date = ele.Text;
                if (date.Equals(currentDay.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    ele.Click();
                    break;
                }
            }

            // Close the browser
            driver.Quit();
        }
    }
}