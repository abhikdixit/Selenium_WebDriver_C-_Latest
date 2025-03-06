using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace pk_Advance_Topics
{
    public class SwitchTabTest
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
        public void TestSwitchTab()
        {
            // Get the current window handle (parent tab)
            string parentTabHandle = driver.CurrentWindowHandle;
            Console.WriteLine("Parent Tab Handle: " + parentTabHandle);

            // Click the "Open Tab" button to open a new tab
            driver.FindElement(By.Id("opentab")).Click();

            // Get all window handles
            var tabHandles = driver.WindowHandles;

            // Switch to the new tab
            foreach (var handle in tabHandles)
            {
                if (handle != parentTabHandle)
                {
                    driver.SwitchTo().Window(handle);
                    break;
                }
            }

            // Print the title of the new tab
            Console.WriteLine("New Tab Title: " + driver.Title);

            // Perform an action in the new tab (e.g., click a link)
            driver.FindElement(By.XPath("//a[contains(text(),'Sign In')]")).Click();

            // Wait for a moment to observe the action (optional)
            System.Threading.Thread.Sleep(2000);

            // Close the new tab
            driver.Close();

            // Switch back to the parent tab
            driver.SwitchTo().Window(parentTabHandle);

            // Print the title of the parent tab
            Console.WriteLine("Parent Tab Title: " + driver.Title);

            // Perform an action in the parent tab (e.g., enter text)
            driver.FindElement(By.Id("name")).SendKeys("Switched back to parent tab");

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