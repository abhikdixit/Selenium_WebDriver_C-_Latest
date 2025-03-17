using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;

namespace AutomationExerciseTests
{
    [TestFixture]
    public class RemoveProductsFromCart
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
        public void TestAddAndRemoveProductFromCart()
        {
            // Step 2: Navigate to the URL
            driver.Navigate().GoToUrl("http://automationexercise.com");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            // Step 3: Verify that the home page is visible
            Assert.That(driver.Title, Is.EqualTo("Automation Exercise"), "Home page is not visible.");

            // Step 4: Add products to the cart using mouse hover
            // Hover over the first product
            IWebElement firstProduct = driver.FindElement(By.XPath("(//div[@class='productinfo text-center'])[1]"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(firstProduct).Perform();

            // Wait for the "Add to Cart" button to be clickable
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement addToCartButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//a[text()='Add to cart'])[1]")));

            // Scroll the element into view (if needed)
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", addToCartButton);

            // Click the "Add to cart" button
            addToCartButton.Click();

            // Wait for the "Continue Shopping" button to appear and click it
            IWebElement continueShoppingButton = driver.FindElement(By.XPath("//button[text()='Continue Shopping']"));
            continueShoppingButton.Click();

            // Step 5: Click the "Cart" button
            IWebElement cartButton = driver.FindElement(By.XPath("//a[contains(text(), 'Cart')]"));
            cartButton.Click();

            // Step 6: Verify that the cart page is displayed
            Assert.That(driver.Title, Is.EqualTo("Automation Exercise - Checkout"), "Cart page is not displayed.");
            Thread.Sleep(2000); 
            // Step 7: Click the "X" button to remove the product
            IWebElement removeButton = driver.FindElement(By.XPath("//a[@class='cart_quantity_delete']"));
            removeButton.Click();
            Thread.Sleep(2000);
            // Step 8: Verify that the product is removed from the cart
            IWebElement emptyCartMessage = driver.FindElement(By.XPath("//b[normalize-space()='Cart is empty!']"));
            Assert.That(emptyCartMessage.Displayed, Is.True, "Product was not removed from the cart.");
        }

        [TearDown]
        public void Teardown()
        {
            // Close the browser
            driver.Quit();
        }
    }
}