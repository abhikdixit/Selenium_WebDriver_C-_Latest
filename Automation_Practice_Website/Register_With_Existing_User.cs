using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace AutomationExerciseTests
{
    [TestFixture]
    public class SignupTests_With_Existing_User
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            // Initialize the Chrome driver
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void TestSignupWithExistingEmail()
        {
            // Step 2: Navigate to the URL
            driver.Navigate().GoToUrl("http://automationexercise.com");

            // Step 3: Verify that the home page is visible
            Assert.That(driver.Title, Is.EqualTo("Automation Exercise"), "Home page is not visible.");

            // Step 4: Click on 'Signup / Login' button
            IWebElement signupLoginButton = driver.FindElement(By.XPath("//a[contains(text(), 'Signup / Login')]"));
            signupLoginButton.Click();

            // Step 5: Verify 'New User Signup!' is visible
            IWebElement newUserSignupText = driver.FindElement(By.XPath("//h2[contains(text(), 'New User Signup!')]"));
            Assert.That(newUserSignupText.Displayed, Is.True, "'New User Signup!' is not visible.");

            // Step 6: Enter name and already registered email address
            IWebElement nameInput = driver.FindElement(By.Name("name"));
            nameInput.SendKeys("John Doe");

            IWebElement emailInput = driver.FindElement(By.XPath("//input[@data-qa='signup-email']"));
            emailInput.SendKeys("johndoe@example.com"); // Use an already registered email address

            // Step 7: Click 'Signup' button
            IWebElement signupButton = driver.FindElement(By.XPath("//button[contains(text(), 'Signup')]"));
            signupButton.Click();

            // Step 8: Verify error 'Email Address already exist!' is visible
            IWebElement errorMessage = driver.FindElement(By.XPath("//p[contains(text(), 'Email Address already exist!')]"));
            Assert.That(errorMessage.Displayed, Is.True, "Error message 'Email Address already exist!' is not visible.");
        }

        [TearDown]
        public void Teardown()
        {
            // Close the browser
            driver.Quit();
        }
    }
}