using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;

namespace pk_Advance_Topics
{
    public class FileUploadTest
    {
        IWebDriver driver;

        [SetUp]
        public void LaunchBrowser()
        {
            // Initialize ChromeDriver
            driver = new ChromeDriver();

            // Maximize the browser window
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void RightClickAndUploadFile()
        {
            // Navigate to the upload page
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/upload");

            // Define the file path
            string filePath = Path.Combine("C:", "Training_Scripts", "Selenium_WebDriver_C#", "bin", "Debug", "net8.0", "Images", "Image0019.jpg");

            // Upload the file
            IWebElement fileInput = driver.FindElement(By.Id("file-upload"));
            fileInput.SendKeys(filePath);

            // Click the submit button
            IWebElement submitButton = driver.FindElement(By.Id("file-submit"));
            submitButton.Click();

            // Wait for the success message to appear
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement successMessage = wait.Until(driver => driver.FindElement(By.XPath("//h3[normalize-space()='File Uploaded!']")));

            // Verify the success message
            Assert.That(successMessage.Displayed, Is.True, "File upload success message is not displayed.");

            // Verify the uploaded file name
            IWebElement uploadedFileName = driver.FindElement(By.Id("uploaded-files"));
            string actualFileName = uploadedFileName.Text;
            string expectedFileName = "Image0019.jpg";
            Assert.That(actualFileName, Is.EqualTo(expectedFileName), "The uploaded file name does not match the expected file name.");
            Thread.Sleep(5000);
        }

        [TearDown]
        public void CloseBrowser()
        {
            // Close the browser
            driver.Quit();
        }
    }
}