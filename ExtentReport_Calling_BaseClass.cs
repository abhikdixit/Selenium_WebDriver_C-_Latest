using NUnit.Framework;
using OpenQA.Selenium;
using BaseClass;
using AventStack.ExtentReports;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace pk_Advance_Topics
{
    public class Extent_Report_Using_BaseClass : WebDriverSetup
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

        [Test]
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