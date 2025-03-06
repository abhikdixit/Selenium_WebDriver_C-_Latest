using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.IO;


namespace pk_Advance_Topics
{
    public class Extent_Report_Example
    {
        // ExtentReport objects
        private ExtentReports extent;
        private ExtentTest test;

        // WebDriver object
        private IWebDriver driver;

        // Path for the ExtentReport
        private string reportPath = Path.Combine(Directory.GetCurrentDirectory(), "test-output", "Epsilon_ExtentReport.html");

        [OneTimeSetUp]
        public void StartReport()
        {
            // Initialize ExtentReports and attach the HTML reporter
            var htmlReporter = new ExtentSparkReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

            // Add system or environment info
            extent.AddSystemInfo("OS", Environment.OSVersion.ToString());
            extent.AddSystemInfo("Browser", "Firefox Latest");
            extent.AddSystemInfo("QA Name", "Abhi");

            // Configuration items to change the look and feel
            htmlReporter.Config.DocumentTitle = "Smoke - Extent Report for WebOrder";
            htmlReporter.Config.ReportName = "Batch Smoke Test Report for WebOrder";
            //htmlReporter.Config.Theme = Theme.Dark;
            htmlReporter.Config.Encoding = "UTF-8";
        }

        [SetUp]
        public void LaunchBrowser()
        {
            // Create a test in the ExtentReport
            test = extent.CreateTest("Test Case 1", "Launching Firefox Browser");

            // Initialize FirefoxDriver
            driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();

            // Log the status in the ExtentReport
            test.Log(Status.Info, "Firefox browser launched and maximized.");
        }

        [Test, Order(2)]
        public void SignOn()
        {
            // Create a test in the ExtentReport
            test = extent.CreateTest("Test Case 2", "Login to WebOrder Application");

            // Navigate to the login page
            driver.Navigate().GoToUrl("http://secure.smartbearsoftware.com/samples/TestComplete11/WebOrders/Login.aspx");
            test.Log(Status.Info, "Navigated to the login page.");

            // Enter credentials and log in
            driver.FindElement(By.Name("ctl00$MainContent$username")).SendKeys("Tester");
            driver.FindElement(By.Name("ctl00$MainContent$password")).SendKeys("test");
            driver.FindElement(By.Name("ctl00$MainContent$login_button")).Click();
            test.Log(Status.Info, "Entered credentials and clicked the login button.");

            // Verify logout link is displayed
            try
            {
                Assert.That(driver.FindElement(By.LinkText("Logout1")).Displayed, Is.True, "Logout link is not displayed.");
                test.Log(Status.Pass, "Logout link is displayed.");
            }
            catch (Exception ex)
            {
                test.Log(Status.Fail, "Logout link is not displayed. Exception: " + ex.Message);
                throw;
            }

            // Log out
            driver.FindElement(By.LinkText("Logout")).Click();
            test.Log(Status.Info, "Clicked the logout link.");
        }

        [TearDown]
        public void GetResult()
        {
            // Capture the test result and log it in the ExtentReport
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;
            var errorMessage = TestContext.CurrentContext.Result.Message;

            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                test.Log(Status.Fail, $"Test failed: {errorMessage}");
                test.Log(Status.Fail, $"Stack Trace: {stackTrace}");
                CaptureScreenshot("Failure");
            }
            else if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                test.Log(Status.Pass, "Test passed.");
            }
            else
            {
                test.Log(Status.Skip, "Test skipped.");
            }

            // Close the browser
            driver.Quit();
            test.Log(Status.Info, "Browser closed.");
        }

        [OneTimeTearDown]
        public void CloseReport()
        {
            // Flush the ExtentReport to generate the final report
            extent.Flush();
        }

        private void CaptureScreenshot(string status)
        {
            // Capture a screenshot and save it to the test-output directory
            string screenshotPath = Path.Combine(Directory.GetCurrentDirectory(), "test-output", $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(screenshotPath);
            test.Log(Status.Info, $"Screenshot saved: {screenshotPath}");

            // Add the screenshot to the ExtentReport
            test.AddScreenCaptureFromPath(screenshotPath);
        }
    }
}