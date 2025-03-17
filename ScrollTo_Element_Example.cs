using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace pk_Advance_Topics
{
    public class ScrollTo_Element_Example
    {
        private IWebDriver driver;
        [Test]
        public void ScrollToParticularElementExample()
        {
            // Set up FirefoxDriver (ensure GeckoDriver is in your PATH or specify its location)
            driver = new ChromeDriver();
            // Navigate to the webpage
            driver.Navigate().GoToUrl("https://stackoverflow.com/");

            // Locate the element to scroll to
            IWebElement element = driver.FindElement(By.XPath("//a[text()='Press']"));

            // Scroll the element into view using JavaScript
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);

            // Wait for the element to be clickable
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => element.Displayed && element.Enabled);

            // Click the element
            element.Click();
            Thread.Sleep(5000);
            driver.Quit();
        }
    }
}