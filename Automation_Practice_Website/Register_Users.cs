using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace AutomationExerciseTests
{
    [TestFixture]
    public class RegisterUsers
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
        public void TestAccountCreationAndDeletion()
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

            // Step 6: Enter name and email address
            IWebElement nameInput = driver.FindElement(By.Name("name"));
            nameInput.SendKeys("John Doe");

            IWebElement emailInput = driver.FindElement(By.XPath("//input[@data-qa='signup-email']"));
            emailInput.SendKeys("johndoe" + new Random().Next(1000, 9999) + "@example.com");

            // Step 7: Click 'Signup' button
            IWebElement signupButton = driver.FindElement(By.XPath("//button[contains(text(), 'Signup')]"));
            signupButton.Click();

            // Step 8: Verify that 'ENTER ACCOUNT INFORMATION' is visible
            IWebElement accountInfoText = driver.FindElement(By.XPath("//b[contains(text(), 'Enter Account Information')]"));
            Assert.That(accountInfoText.Displayed, Is.True, "'ENTER ACCOUNT INFORMATION' is not visible.");

            // Step 9: Fill details: Title, Name, Email, Password, Date of birth
            IWebElement titleRadioButton = driver.FindElement(By.Id("id_gender1")); // Select "Mr."
            titleRadioButton.Click();

            IWebElement passwordInput = driver.FindElement(By.Id("password"));
            passwordInput.SendKeys("Password123");

            IWebElement daysDropdown = driver.FindElement(By.Id("days"));
            new SelectElement(daysDropdown).SelectByValue("10");

            IWebElement monthsDropdown = driver.FindElement(By.Id("months"));
            new SelectElement(monthsDropdown).SelectByValue("5");

            IWebElement yearsDropdown = driver.FindElement(By.Id("years"));
            new SelectElement(yearsDropdown).SelectByValue("1990");

            // Step 10: Select checkbox 'Sign up for our newsletter!'
            IWebElement newsletterCheckbox = driver.FindElement(By.Id("newsletter"));

            // Scroll the checkbox into view
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", newsletterCheckbox);

            // Use JavaScript to click the checkbox
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", newsletterCheckbox);

            // Step 11: Select checkbox 'Receive special offers from our partners!'
            IWebElement offersCheckbox = driver.FindElement(By.Id("optin"));
            offersCheckbox.Click();

            // Step 12: Fill details: First name, Last name, Company, Address, Address2, Country, State, City, Zipcode, Mobile Number
            IWebElement firstNameInput = driver.FindElement(By.Id("first_name"));
            firstNameInput.SendKeys("John");

            IWebElement lastNameInput = driver.FindElement(By.Id("last_name"));
            lastNameInput.SendKeys("Doe");

            IWebElement companyInput = driver.FindElement(By.Id("company"));
            companyInput.SendKeys("Example Company");

            IWebElement addressInput = driver.FindElement(By.Id("address1"));
            addressInput.SendKeys("123 Main St");

            IWebElement address2Input = driver.FindElement(By.Id("address2"));
            address2Input.SendKeys("Apt 101");

            IWebElement countryDropdown = driver.FindElement(By.Id("country"));
            new SelectElement(countryDropdown).SelectByText("United States");

            IWebElement stateInput = driver.FindElement(By.Id("state"));
            stateInput.SendKeys("California");

            IWebElement cityInput = driver.FindElement(By.Id("city"));
            cityInput.SendKeys("Los Angeles");

            IWebElement zipcodeInput = driver.FindElement(By.Id("zipcode"));
            zipcodeInput.SendKeys("90001");

            IWebElement mobileNumberInput = driver.FindElement(By.Id("mobile_number"));
            mobileNumberInput.SendKeys("1234567890");

            // Step 13: Click 'Create Account' button
            IWebElement createAccountButton = driver.FindElement(By.XPath("//button[contains(text(), 'Create Account')]"));
            createAccountButton.Click();

            // Step 14: Verify that 'ACCOUNT CREATED!' is visible
            IWebElement accountCreatedText = driver.FindElement(By.XPath("//b[contains(text(), 'Account Created!')]"));
            Assert.That(accountCreatedText.Displayed, Is.True, "'ACCOUNT CREATED!' is not visible.");

            // Step 15: Click 'Continue' button
            IWebElement continueButton = driver.FindElement(By.XPath("//a[contains(text(), 'Continue')]"));
            continueButton.Click();

            // Step 16: Verify that 'Logged in as username' is visible
            IWebElement loggedInText = driver.FindElement(By.XPath("//a[contains(text(), 'Logged in as')]"));
            Assert.That(loggedInText.Displayed, Is.True, "'Logged in as username' is not visible.");

            // Step 17: Click 'Delete Account' button
            IWebElement deleteAccountButton = driver.FindElement(By.XPath("//a[contains(text(), 'Delete Account')]"));
            deleteAccountButton.Click();

            // Step 18: Verify that 'ACCOUNT DELETED!' is visible and click 'Continue' button
            IWebElement accountDeletedText = driver.FindElement(By.XPath("//b[contains(text(), 'Account Deleted!')]"));
            Assert.That(accountDeletedText.Displayed, Is.True, "'ACCOUNT DELETED!' is not visible.");

            IWebElement continueAfterDeletionButton = driver.FindElement(By.XPath("//a[contains(text(), 'Continue')]"));
            continueAfterDeletionButton.Click();
        }

        [TearDown]
        public void Teardown()
        {
            // Close the browser
            driver.Quit();
        }
    }
}