using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTests.Pages;

namespace SeleniumTests.Tests
{
    [TestFixture]
    public class LoginLogoutTests
    {
        private IWebDriver driver;
        private LoginLogoutPage loginLogoutPage;

        [SetUp]
        public void Setup()
        {
            // Initialize ChromeDriver
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            // Initialize the page object
            loginLogoutPage = new LoginLogoutPage(driver);
        }

        [Test]
        public void TestLoginAndLogout()
        {
            // Navigate to the application URL
            loginLogoutPage.GoToURL();

            // Login to the application
            loginLogoutPage.LoginToApp("Tester", "test");

            // Logout from the application
            loginLogoutPage.LogoutFromApp();

            // Assert that the user is logged out (e.g., check for the login button)
            Assert.That(driver.FindElement(By.Id("ctl00_MainContent_login_button")).Displayed, Is.True, "The input field is not enabled.");

            //Assert.IsTrue(driver.FindElement(By.Id("ctl00_MainContent_login_button")).Displayed);
        }

        [TearDown]
        public void Teardown()
        {
            // Close the browser
            driver.Quit();
        }
    }
}