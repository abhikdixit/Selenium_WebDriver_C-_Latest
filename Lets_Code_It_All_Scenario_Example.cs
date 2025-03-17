using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;

namespace LetsKodeItPracticeTests
{
    [TestFixture]
    public class PracticeTests_All_Example
    {
        private IWebDriver driver;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // Initialize the Chrome driver (runs once before all tests)
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl("https://www.letskodeit.com/practice");
        }

        [Test]
        public void TestRadioButtons()
        {
            // Select the "BMW" radio button
            IWebElement bmwRadioButton = driver.FindElement(By.Id("bmwradio"));
            bmwRadioButton.Click();

            // Assert that the "BMW" radio button is selected
            Assert.That(bmwRadioButton.Selected, Is.True, "BMW radio button is not selected.");
        }

        [Test]
        public void TestDropdown()
        {
            // Select "Honda" from the dropdown
            IWebElement dropdown = driver.FindElement(By.Id("carselect"));
            var selectElement = new OpenQA.Selenium.Support.UI.SelectElement(dropdown);
            selectElement.SelectByText("Honda");

            // Assert that "Honda" is selected
            Assert.That(selectElement.SelectedOption.Text, Is.EqualTo("Honda"), "Honda is not selected.");
        }

        [Test]
        public void TestCheckbox()
        {
            // Select the "Benz" checkbox
            IWebElement benzCheckbox = driver.FindElement(By.Id("benzcheck"));
            benzCheckbox.Click();

            // Assert that the "Benz" checkbox is selected
            Assert.That(benzCheckbox.Selected, Is.True, "Benz checkbox is not selected.");
        }

        [Test]
        public void TestAlert()
        {
            // Enter text in the alert input box
            IWebElement alertInput = driver.FindElement(By.Id("name"));
            alertInput.SendKeys("John Doe");

            // Click the "Alert" button
            IWebElement alertButton = driver.FindElement(By.Id("alertbtn"));
            alertButton.Click();

            // Switch to the alert and accept it
            var alert = driver.SwitchTo().Alert();
            string alertText = alert.Text;
            alert.Accept();

            // Assert the alert text
            Assert.That(alertText, Does.Contain("John Doe"), "Alert text does not contain the expected value.");
        }

        [Test]
        public void TestAutoSuggest()
        {
            // Enter text in the autosuggest input box
            IWebElement autoSuggestInput = driver.FindElement(By.Id("autosuggest"));
            autoSuggestInput.SendKeys("Sele");

            // Wait for suggestions to appear and select "Canada"
            IWebElement canadaOption = driver.FindElement(By.XPath("//a[contains(text(), 'Selenium WebDriver Java')]"));
            canadaOption.Click();

            // Assert that the input box contains "Canada"
            Assert.That(autoSuggestInput.GetAttribute("value"), Is.EqualTo("Selenium WebDriver Java"), "Auto-suggest value is not correct.");
        }

        [Test]
        public void TestElementDisplayed()
        {
            // Check if the "Hide/Show Example" text box is displayed
            IWebElement textBox = driver.FindElement(By.Id("displayed-text"));
            Assert.That(textBox.Displayed, Is.True, "Text box is not displayed.");

            // Click the "Hide" button
            IWebElement hideButton = driver.FindElement(By.Id("hide-textbox"));
            hideButton.Click();

            // Assert that the text box is not displayed
            Assert.That(textBox.Displayed, Is.False, "Text box is still displayed after clicking Hide.");
        }

        [Test]
        public void TestEnabledDisabled()
        {
            // Check if the "Enabled/Disabled Example" text box is enabled
            IWebElement textBox = driver.FindElement(By.Id("enabled-example-input"));
            Assert.That(textBox.Enabled, Is.True, "Text box is not enabled.");

            // Click the "Disable" button
            IWebElement disableButton = driver.FindElement(By.Id("disabled-button"));
            disableButton.Click();

            // Assert that the text box is disabled
            Assert.That(textBox.Enabled, Is.False, "Text box is still enabled after clicking Disable.");
        }

        [Test]
        public void TestMouseHover()
        {
            // Perform mouse hover on the "Mouse Hover Example" button
            IWebElement hoverButton = driver.FindElement(By.Id("mousehover"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(hoverButton).Perform();

            // Click the "Top" option from the hover menu
            IWebElement topOption = driver.FindElement(By.XPath("//a[text()='Top']"));
            topOption.Click();

            // Assert that the page scrolls to the top
            Assert.That(driver.FindElement(By.XPath("//h1[normalize-space()='Practice Page']")).Displayed, Is.True, "Page did not scroll to the top.");
            Thread.Sleep(2000); 
        }

        [Test]
        public void TestIFrame()
        {
            // Switch to the iframe
            driver.SwitchTo().Frame("courses-iframe");

            // Find the search box inside the iframe and enter a search term
            IWebElement searchBox = driver.FindElement(By.Name("course"));
            searchBox.SendKeys("Python");

            // Click the search button
            IWebElement searchButton = driver.FindElement(By.XPath("//button[@type='submit']"));
            searchButton.Click();
            Thread.Sleep(2000);
            // Switch back to the default content
            //driver.SwitchTo().DefaultContent();

            //// Assert that the search was performed (you can add more specific assertions based on the search results)
            Assert.That(driver.Title, Does.Contain("Practice Page"), "Practice Page was not performed successfully.");
            Thread.Sleep(2000);
        }

        [OneTimeTearDown]
        public void OneTimeTeardown()
        {
            // Close the browser (runs once after all tests)
            driver.Quit();
        }
    }
}