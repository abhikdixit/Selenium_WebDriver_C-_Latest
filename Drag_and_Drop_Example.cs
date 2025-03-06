using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Threading;

namespace pk_Advance_Topics
{
    public class Drag_and_Drop_TestAutomation_WebSite
    {
        private IWebDriver driver;

        [Test]
        public void DragDrop()
        {
            // Set up ChromeDriver
            driver = new ChromeDriver();

            // Maximize the browser window
            driver.Manage().Window.Maximize();

            // Open the webpage
            driver.Navigate().GoToUrl("http://testautomationpractice.blogspot.com/");

            // Wait for 2 seconds
            Thread.Sleep(2000);

            // Create an object of the Actions class
            Actions act = new Actions(driver);

            // Find the element to drag
            IWebElement drag = driver.FindElement(By.Id("draggable"));

            // Find the element to drop
            IWebElement drop = driver.FindElement(By.Id("droppable"));

            // Perform the drag-and-drop operation
            act.DragAndDrop(drag, drop).Build().Perform();

            // Wait for 2 seconds to observe the result
            Thread.Sleep(2000);

            // Close the browser
            driver.Quit();
        }
    }
}