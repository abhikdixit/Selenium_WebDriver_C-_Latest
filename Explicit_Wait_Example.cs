using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace pk_Advance_Topics
{
    public class Explicit_Wait_Example
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
        public void ExplicitWait()
        {
            // Enter username and password
            driver.FindElement(By.Name("username")).SendKeys("Admin");
            driver.FindElement(By.Name("password")).SendKeys("admin123");

            // Click the login button
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            // Use Explicit Wait to wait for the user dropdown to be clickable
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            // Click the user dropdown
            driver.FindElement(By.XPath("//p[@class='oxd-userdropdown-name']")).Click();

            // Wait for the "Logout" link to be clickable
            IWebElement logoutLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Logout")));

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