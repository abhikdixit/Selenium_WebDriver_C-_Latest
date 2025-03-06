using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace pk_Advance_Topics
{
    public class EnabledDisabledDisplayedExample
    {
        private IWebDriver driver;

        [OneTimeSetUp]
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
        public void TestEnabledDisabledExample()
        {
            // Locate the "Enabled/Disabled" input field
            IWebElement enabledDisabledInput = driver.FindElement(By.Id("enabled-example-input"));

            // Check if the input field is enabled
            bool isEnabled = enabledDisabledInput.Enabled;
            Console.WriteLine("Is the input field enabled? " + isEnabled);

            // Assert that the input field is enabled
            Assert.That(isEnabled, Is.True, "The input field is not enabled.");

            // Click the "Disable" button to disable the input field
            driver.FindElement(By.Id("disabled-button")).Click();

            // Wait for a moment to observe the change (optional)
            Thread.Sleep(2000);

            // Check if the input field is disabled
            bool isDisabled = !enabledDisabledInput.Enabled;
            Console.WriteLine("Is the input field disabled? " + isDisabled);

            // Assert that the input field is disabled
            Assert.That(isDisabled, Is.True, "The input field is not disabled.");
        }

        [Test]
        public void TestElementDisplayedExample()
        {
            // Locate the "Hide/Show" input field
            IWebElement hideShowInput = driver.FindElement(By.Id("displayed-text"));

            // Check if the input field is displayed
            bool isDisplayed = hideShowInput.Displayed;
            Console.WriteLine("Is the input field displayed? " + isDisplayed);

            // Assert that the input field is displayed
            Assert.That(isDisplayed, Is.True, "The input field is not displayed.");

            // Click the "Hide" button to hide the input field
            driver.FindElement(By.Id("hide-textbox")).Click();

            // Wait for a moment to observe the change (optional)
            System.Threading.Thread.Sleep(2000);

            // Check if the input field is hidden
            bool isHidden = !hideShowInput.Displayed;
            Console.WriteLine("Is the input field hidden? " + isHidden);

            // Assert that the input field is hidden
            Assert.That(isHidden, Is.True, "The input field is not hidden.");
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            // Close the browser
            driver.Quit();
        }
    }
}