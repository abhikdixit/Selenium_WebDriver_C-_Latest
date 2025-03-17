using NUnit.Framework;
using OpenQA.Selenium;
using BaseClass;
using AventStack.ExtentReports;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace pk_Advance_Topics
{
    public class Extent_Report_Multi_Method_Using_BaseClass : WebDriverSetup
    {
        private WebDriverWait wait;

        [OneTimeSetUp]
        public void StartReport()
        {
            // Initialize the Extent Report
            WebDriverSetup.StartReport();

            // Initialize ChromeDriver
            driver = CrossBrowserTesting("chrome");

            // Initialize WebDriverWait
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [SetUp]
        public void LaunchBrowser()
        {
            // Create a test in the ExtentReport
            CreateTest(TestContext.CurrentContext.Test.Name, "Launching Chrome Browser");

            // Log the status in the ExtentReport
            test.Log(Status.Info, "Chrome browser launched and maximized.");
        }

        [Test, Order(1)]
        public void SignOn()
        {
            // Log the test steps in the ExtentReport
            test.Log(Status.Info, "Navigating to the login page.");
            driver.Navigate().GoToUrl("http://secure.smartbearsoftware.com/samples/TestComplete11/WebOrders/Login.aspx");

            test.Log(Status.Info, "Entering credentials and clicking the login button.");
            driver.FindElement(By.Name("ctl00$MainContent$username")).SendKeys("Tester");
            driver.FindElement(By.Name("ctl00$MainContent$password")).SendKeys("test");
            driver.FindElement(By.Name("ctl00$MainContent$login_button")).Click();

            test.Log(Status.Info, "Verifying logout link is displayed.");
            Assert.That(driver.FindElement(By.LinkText("Logout")).Displayed, Is.True, "Logout link is not displayed.");
            test.Log(Status.Pass, "Logout link is displayed.");
        }

        [Test, Order(2)]
        public void CreateOrder()
        {
            test.Log(Status.Info, "Navigating to the login page.");
            driver.FindElement(By.LinkText("Order")).Click();
            var product = new SelectElement(driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$ddlProduct")));
            product.SelectByText("FamilyAlbum");
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$txtQuantity")).SendKeys("5");

            // Generate Random number
            test.Log(Status.Info, "Generate Random Number");
            Random randomGenerator = new Random();
            int randomInt = randomGenerator.Next(100000000);
            string userName = "Dixit" + randomInt;
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$txtName")).SendKeys(userName);

            test.Log(Status.Info, "Enter all details like City, State, Zip code, card no., exp date");
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$TextBox2")).SendKeys("ABC");
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$TextBox3")).SendKeys("Redwood");
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$TextBox5")).SendKeys("5");
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_cardList_1")).Click();
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$TextBox6")).SendKeys("123456789");
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$TextBox1")).SendKeys("12/23");
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_InsertButton")).Click();

            test.Log(Status.Info, "Wait for Success message");
            string actSuccessMsg = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//strong[normalize-space()='New order has been successfully added.']"))).Text;
            string expSuccessMsg = "New order has been successfully added";
            Assert.That(expSuccessMsg, Is.EqualTo(actSuccessMsg));

            // Go back to View All Order page and verify that user was created
            driver.FindElement(By.LinkText("View all orders")).Click();
            string actUserName = driver.FindElement(By.XPath("//td[text()='" + userName + "']")).Text;
            Assert.That(userName, Is.EqualTo(actUserName));
            test.Log(Status.Info, "Verify that user got created");
        }

        [TearDown]
        public void GetResult()
        {
            // Log the test result
            LogTestResult();
        }

        [OneTimeTearDown]
        public void CloseReport()
        {
            test.Log(Status.Info, "Clicking the logout link.");
            driver.FindElement(By.LinkText("Logout")).Click();

            // Close the browser
            driver.Quit();
            test.Log(Status.Info, "Browser closed.");

            // Flush the ExtentReport to generate the final report
            WebDriverSetup.CloseReport();
        }
    }
}