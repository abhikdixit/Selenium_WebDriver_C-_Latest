using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace pk_Advance_Topics
{
    public class ListBox_Example
    {
        private IWebDriver driver;

        [SetUp]
        public void LaunchBrowser()
        {
            // Set up ChromeDriver
            driver = new ChromeDriver();

            // Maximize the browser window
            driver.Manage().Window.Maximize();

            // Navigate to the test page
            driver.Navigate().GoToUrl("https://testautomationpractice.blogspot.com/");
        }

        [Test, Order(1)]
        public void DropDown()
        {
            // Locate the dropdown element
            IWebElement dropdownElement = driver.FindElement(By.Id("country"));

            // Create a SelectElement object
            SelectElement selectPass = new SelectElement(dropdownElement);

            // Select an option by visible text
            selectPass.SelectByText("Japan");
        }

        [Test, Order(2)]
        public void ListBox()
        {
            // Locate the list box element
            IWebElement listBoxElement = driver.FindElement(By.Id("colors"));

            // Create a SelectElement object
            SelectElement selectPass = new SelectElement(listBoxElement);

            // Select options by value and visible text
            selectPass.SelectByValue("red");
            selectPass.SelectByText("Yellow");
        }

        [TearDown]
        public void CloseBrowser()
        {
            // Close the browser
            driver.Quit();
        }
    }
}