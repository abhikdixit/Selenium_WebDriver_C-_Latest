using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;
using System;

namespace pk_Advance_Topics
{
    public class Double_Click_Example
    {
        IWebDriver driver;

        [Test]
        public void DoubleClick()
        {
            // Initialize EdgeDriver
            driver = new EdgeDriver();

            try
            {
                // Maximize the browser window
                driver.Manage().Window.Maximize();

                // Open the webpage
                driver.Navigate().GoToUrl("https://testautomationpractice.blogspot.com/");

                // Find the element to double-click
                IWebElement doubleClickButton = driver.FindElement(By.XPath("//button[normalize-space()='Copy Text']"));

                // Perform double-click using Actions class
                Actions action = new Actions(driver);
                action.DoubleClick(doubleClickButton).Perform();

                // Optional: Add assertions or additional logic here
                // For example, you can verify the text after double-clicking

                // Wait for a few seconds to observe the action (optional)
                System.Threading.Thread.Sleep(2000);
            }
            finally
            {
                // Close the browser
                driver.Quit();
            }
        }
    }
}