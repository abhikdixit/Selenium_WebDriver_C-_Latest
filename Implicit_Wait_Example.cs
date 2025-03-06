using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace pk_Advance_Topics
{
    public class Implicit_Wait_Example
    {
        private IWebDriver driver;

        [SetUp]
        public void LaunchBrowser()
        {
            // Initialize EdgeDriver
            driver = new EdgeDriver();

            // Maximize the browser window
            driver.Manage().Window.Maximize();

            // Navigate to the OrangeHRM login page
            driver.Navigate().GoToUrl("https://opensource-demo.orangehrmlive.com/web/index.php/auth/login");

            // Set implicit wait (optional, but included for completeness)
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
        }

        [Test]
        public void ImplicitWait()
        {
            // Enter username and password
            driver.FindElement(By.Name("username")).SendKeys("Admin");
            driver.FindElement(By.Name("password")).SendKeys("admin123");

            // Click the login button
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            driver.FindElement(By.XPath("//p[@class='oxd-userdropdown-name']")).Click();

            IWebElement logoutLink = driver.FindElement(By.LinkText("Logout"));
            // Print the text of the "Logout" link (optional)
            Console.WriteLine(logoutLink.Text);

            // Click the "Logout" link
            logoutLink.Click();
        }

        [TearDown]
        public void CloseBrowser()
        {
            // Close the browser
            driver.Quit();
        }
    }
}