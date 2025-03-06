using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

namespace WebOrderTests
{
    public class CaptureScreenShotExample
    {
        private IWebDriver driver;
        private string filePath = Directory.GetCurrentDirectory();
        private string filePathFailure;
        private string filePathSuccess;

        [OneTimeSetUp]
        public void LaunchBrowser()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://secure.smartbearsoftware.com/samples/TestComplete11/WebOrders/Login.aspx");

            // Ensure directories exist before saving screenshots
            filePathFailure = Path.Combine(filePath, "Screenshot_Failure");
            filePathSuccess = Path.Combine(filePath, "Screenshot_Success");
            Directory.CreateDirectory(filePathFailure);
            Directory.CreateDirectory(filePathSuccess);
        }

        [Test, Order(1)]
        public void LoginToApp()
        {
            driver.FindElement(By.Name("ctl00$MainContent$username")).SendKeys("Tester");
            driver.FindElement(By.Name("ctl00$MainContent$password")).SendKeys("test");
            driver.FindElement(By.CssSelector("input[name='ctl00$MainContent$login_button']")).Click();

            Assert.That(driver.FindElement(By.LinkText("Logout")).Displayed, Is.True, "Logout link should be visible after login.");
            Assert.That(driver.Title, Is.EqualTo("Web Orders"), "Page title should be 'Web Orders' after login.");
        }

        [Test, Order(2)]
        public void CreateOrder()
        {
            driver.FindElement(By.LinkText("Order")).Click();
            new OpenQA.Selenium.Support.UI.SelectElement(driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$ddlProduct")))
                .SelectByText("FamilyAlbum");

            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$txtQuantity")).Clear(); // Clear default text if needed
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$txtQuantity")).SendKeys("5");
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$txtName")).SendKeys("Dixit");
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$TextBox2")).SendKeys("ABC");
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$TextBox3")).SendKeys("Redwood");
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$TextBox5")).SendKeys("5");
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_cardList_1")).Click();
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$TextBox6")).SendKeys("123456789");
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$TextBox1")).SendKeys("12/23");
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_InsertButton")).Click();

            string successMsg = driver.FindElement(By.XPath("//strong[normalize-space()='New order has been successfully added.']")).Text;
            Assert.That(successMsg, Is.EqualTo("New order has been successfully added."), "Success message should match.");
        }

        [TearDown]
        public void CaptureScreenShot()
        {
            try
            {
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                string filePath = TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed ? filePathFailure : filePathSuccess;
                string fileName = Path.Combine(filePath, $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now.Ticks}.png");
                screenshot.SaveAsFile(fileName);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception while taking screenshot: " + e.Message);
            }
        }

        [OneTimeTearDown]
        public void CloseBrowser()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }
    }
}
