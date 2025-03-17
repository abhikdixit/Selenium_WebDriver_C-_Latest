using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading;

namespace OrangeHRM
{
    [TestFixture]
    public class AdminTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private string newAdminUsername = "minhadmin";
        private string newAdminPassword = "a123456";

        private dynamic users;

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();

            // Read JSON file
            string jsonString = File.ReadAllText("./TestDataFile/OrangeHRM_AddUsers_TCs.json");
            users = JsonSerializer.Deserialize<dynamic>(jsonString);

            // Login
            driver.Navigate().GoToUrl("https://opensource-demo.orangehrmlive.com/web/index.php/auth/login");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); // Add implicit wait

            driver.FindElement(By.Name("username")).SendKeys("Admin");
            driver.FindElement(By.Name("password")).SendKeys("admin123");
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            // Wait for the dashboard to load
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("Admin")));
        }

        [Test, TestCaseSource(nameof(GetTestData))]
        public void OrangeHRM_AddUsers_Test(dynamic record)
        {
            string expResult = record.GetProperty("expResult").GetString();
            string role = record.GetProperty("role").GetString();
            string name = record.GetProperty("name").GetString();
            string status = record.GetProperty("status").GetString();
            string username = record.GetProperty("username").GetString();
            string pass = record.GetProperty("pass").GetString();
            string confirm = record.GetProperty("confirm").GetString();

            driver.FindElement(By.LinkText("Admin")).Click();
            driver.FindElement(By.XPath("//button[normalize-space()='Add']")).Click();

            // Filling role
            if (!string.IsNullOrEmpty(role))
            {
                // Filling role (first dropdown)
                driver.FindElement(By.XPath("(//i[@class='oxd-icon bi-caret-down-fill oxd-select-text--arrow'])[1]")).Click();
                driver.FindElement(By.XPath($"//div[@role='option']/span[text()='{role}']")).Click();
            }

            // Filling name
            if (!string.IsNullOrEmpty(name))
            {
                driver.FindElement(By.XPath("//input[@placeholder='Type for hints...']")).SendKeys(name);
                driver.FindElement(By.XPath($"//div[@role='option']/span[text()='{name}']")).Click();
            }

            // Filling status
            if (!string.IsNullOrEmpty(status))
            {
                // Filling status (second dropdown)
                driver.FindElement(By.XPath("(//i[@class='oxd-icon bi-caret-down-fill oxd-select-text--arrow'])[2]")).Click();
                driver.FindElement(By.XPath($"//div[@role='option']/span[text()='{status}']")).Click();
            }

            // Filling username
            if (!string.IsNullOrEmpty(username))
            {
                driver.FindElement(By.XPath("(//input[@class='oxd-input oxd-input--active'])[2]")).SendKeys(username);
            }

            // Filling password
            if (!string.IsNullOrEmpty(pass))
            {
                driver.FindElement(By.XPath("(//input[@type='password'])[1]")).SendKeys(pass);
            }

            // Filling confirm password
            if (!string.IsNullOrEmpty(confirm))
            {
                driver.FindElement(By.XPath("(//input[@type='password'])[2]")).SendKeys(confirm);
            }

            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            Thread.Sleep(3000);

            switch (expResult)
            {
                case "valid":
                    Assert.That(driver.FindElement(By.XPath($"//div[text()='{username}']")).Displayed, Is.True, $"Username '{username}' is not visible.");
                    Assert.That(driver.FindElement(By.XPath($"//div[text()='{username}']/parent::div/following-sibling::div/div[text()='{role}']")).Displayed, Is.True, $"Role '{role}' for username '{username}' is not visible.");
                    break;
                case "empty_role":
                    Assert.That(driver.FindElement(By.XPath("//label[text()='User Role']/parent::div/following-sibling::span[text()='Required']")).Displayed, Is.True, "User Role required message is not visible.");
                    break;
                case "empty_name":
                    Assert.That(driver.FindElement(By.XPath("//label[text()='Employee Name']/parent::div/following-sibling::span[text()='Required']")).Displayed, Is.True, "Employee Name required message is not visible.");
                    break;
                case "empty_status":
                    Assert.That(driver.FindElement(By.XPath("//label[text()='Status']/parent::div/following-sibling::span[text()='Required']")).Displayed, Is.True, "Status required message is not visible.");
                    break;
                case "empty_username":
                    Assert.That(driver.FindElement(By.XPath("//label[text()='Username']/parent::div/following-sibling::span[text()='Required']")).Displayed, Is.True, "Username required message is not visible.");
                    break;
                case "invalid_username":
                    Assert.That(driver.FindElement(By.XPath("//label[text()='Username']/parent::div/following-sibling::span[text()='Should be at least 5 characters']")).Displayed, Is.True, "Invalid username message is not visible.");
                    break;
                case "exist_username":
                    Assert.That(driver.FindElement(By.XPath("//label[text()='Username']/parent::div/following-sibling::span[text()='Already exists']")).Displayed, Is.True, "Username already exists message is not visible.");
                    break;
                case "empty_pass":
                    Assert.That(driver.FindElement(By.XPath("//label[text()='Password']/parent::div/following-sibling::span[text()='Required']")).Displayed, Is.True, "Password required message is not visible.");
                    Assert.That(driver.FindElement(By.XPath("//label[text()='Confirm Password']/parent::div/following-sibling::span[text()='Passwords do not match']")).Displayed, Is.True, "Passwords do not match message is not visible.");
                    break;
                case "empty_confirm_pass":
                    Assert.That(driver.FindElement(By.XPath("//label[text()='Confirm Password']/parent::div/following-sibling::span[text()='Passwords do not match']")).Displayed, Is.True, "Passwords do not match message is not visible.");
                    break;
            }
        }

        public static IEnumerable<dynamic> GetTestData()
        {
            string jsonString = File.ReadAllText("./TestDataFile/OrangeHRM_AddUsers_TCs.json");
            var users = JsonSerializer.Deserialize<List<dynamic>>(jsonString);
            return users;
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.FindElement(By.XPath("//span[@class='oxd-userdropdown-tab']")).Click();
            driver.FindElement(By.LinkText("Logout")).Click();
            driver.Quit();
        }
    }
}