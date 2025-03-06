using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;

namespace pk_Advance_Topics
{
    public class SwitchWindowTest
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            // Initialize ChromeDriver
            driver = new ChromeDriver();

            // Maximize the browser window
            driver.Manage().Window.Maximize();

            // Navigate to the practice page
            driver.Navigate().GoToUrl("https://www.letskodeit.com/practice");
        }

        [Test]
        public void TestSwitchWindow()
        {
            // Get the current window handle (parent window)
            string parentWindowHandle = driver.CurrentWindowHandle;
            Console.WriteLine("Parent Window Handle: " + parentWindowHandle);

            // Click the "Open Window" button to open a new window
            driver.FindElement(By.Id("openwindow")).Click();

            // Get all window handles
            var windowHandles = driver.WindowHandles;

            // Switch to the new window
            foreach (var handle in windowHandles)
            {
                if (handle != parentWindowHandle)
                {
                    driver.SwitchTo().Window(handle);
                    break;
                }
            }

            // Print the title of the new window
            Console.WriteLine("New Window Title: " + driver.Title);

            // Perform an action in the new window (e.g., click a link)
            driver.FindElement(By.XPath("//a[contains(text(),'Sign In')]")).Click();

            // Wait for a moment to observe the action (optional)
            System.Threading.Thread.Sleep(2000);

            // Close the new window
            driver.Close();

            // Switch back to the parent window
            driver.SwitchTo().Window(parentWindowHandle);

            // Print the title of the parent window
            Console.WriteLine("Parent Window Title: " + driver.Title);

            // Perform an action in the parent window (e.g., click a button)
            driver.FindElement(By.Id("name")).SendKeys("Switched back to parent window");

            // Wait for a moment to observe the action (optional)
            System.Threading.Thread.Sleep(2000);
        }

        [TearDown]
        public void Teardown()
        {
            // Close the browser
            driver.Quit();
        }
    }
}