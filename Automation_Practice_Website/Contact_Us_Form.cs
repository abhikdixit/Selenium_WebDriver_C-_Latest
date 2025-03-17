using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace AutomationExerciseTests
{
    [TestFixture]
    public class ContactUsTests
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
        public void TestContactUsFormSubmission()
        {
            // Step 2: Navigate to the URL
            driver.Navigate().GoToUrl("http://automationexercise.com");

            // Step 3: Verify that the home page is visible
            Assert.That(driver.Title, Is.EqualTo("Automation Exercise"), "Home page is not visible.");

            // Step 4: Click on 'Contact Us' button
            IWebElement contactUsButton = driver.FindElement(By.XPath("//a[normalize-space()='Contact us']"));
            contactUsButton.Click();

            // Step 5: Verify 'GET IN TOUCH' is visible
            IWebElement getInTouchText = driver.FindElement(By.XPath("//h2[contains(text(), 'Get In Touch')]"));
            Assert.That(getInTouchText.Displayed, Is.True, "'GET IN TOUCH' is not visible.");

            // Step 6: Enter name, email, subject, and message
            IWebElement nameInput = driver.FindElement(By.Name("name"));
            nameInput.SendKeys("John Doe");

            IWebElement emailInput = driver.FindElement(By.Name("email"));
            emailInput.SendKeys("johndoe@example.com");

            IWebElement subjectInput = driver.FindElement(By.Name("subject"));
            subjectInput.SendKeys("Test Subject");

            IWebElement messageInput = driver.FindElement(By.Name("message"));
            messageInput.SendKeys("This is a test message.");

            // Define the file path
            string filePath = Path.Combine("C:", "Training_Scripts", "Selenium_WebDriver_C#", "bin", "Debug", "net8.0", "Images", "Image0019.jpg");

            // Step 7: Upload file
            IWebElement uploadFileInput = driver.FindElement(By.Name("upload_file"));
            uploadFileInput.SendKeys(filePath); // Replace with the actual file path

            // Step 8: Click 'Submit' button
            IWebElement submitButton = driver.FindElement(By.Name("submit"));

            // Scroll the "Submit" button into view
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", submitButton);

            // Use JavaScript to click the "Submit" button
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", submitButton);

            // Step 9: Click OK button on the alert
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();

            // Step 10: Verify success message 'Success! Your details have been submitted successfully.' is visible
            IWebElement successMessage = driver.FindElement(By.XPath("//div[contains(text(), 'Success! Your details have been submitted successfully.')]"));
            Assert.That(successMessage.Displayed, Is.True, "Success message is not visible.");

            // Step 11: Click 'Home' button and verify that landed to home page successfully
            IWebElement homeButton = driver.FindElement(By.XPath("//a[contains(text(), 'Home')]"));
            homeButton.Click();

            // Verify that the home page is visible
            Assert.That(driver.Title, Is.EqualTo("Automation Exercise"), "Home page is not visible after clicking 'Home' button.");
        }

        [TearDown]
        public void Teardown()
        {
            // Close the browser
            driver.Quit();
        }
    }
}