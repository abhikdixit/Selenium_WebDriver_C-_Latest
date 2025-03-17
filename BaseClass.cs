using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.IO;

namespace BaseClass
{
    public class WebDriverSetup
    {
        protected static IWebDriver driver;
        protected static ExtentReports extent;
        protected static ExtentTest test;
        protected static ExtentSparkReporter htmlReporter;
        // Thread-safe WebDriver instance
        //private static readonly ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();

        public static IWebDriver CrossBrowserTesting(string browser)
        {
            switch (browser.ToLower())
            {
                case "firefox":
                    driver = new FirefoxDriver();
                    break;

                case "chrome":
                    driver = new ChromeDriver();
                    break;

                case "edge":
                    driver = new EdgeDriver();
                    break;

                default:
                    Console.WriteLine("Invalid browser name. Defaulting to Chrome.");
                    driver = new ChromeDriver();
                    break;
            }

            driver.Manage().Window.Maximize();
            return driver;
        }

        [OneTimeSetUp]
        public static void StartReport()
        {
            // Extract only the method name from the full test name
            string fullTestName = TestContext.CurrentContext.Test.MethodName ?? "ExtentReport";
            string methodName = fullTestName.Split('.')[^1]; // Get the last part after splitting by '.'

            // Generate a unique report name based on the method name and current date/time
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string reportPath = Path.Combine(Directory.GetCurrentDirectory(), "test-output", $"{methodName}_{timestamp}.html");

            // Initialize ExtentReports and attach the HTML reporter
            htmlReporter = new ExtentSparkReporter(reportPath);

            // Configuration items to change the look and feel
            htmlReporter.Config.DocumentTitle = $"{methodName} - Extent Report";
            htmlReporter.Config.ReportName = $"{methodName} Test Report";
            htmlReporter.Config.Theme = Theme.Dark; // Set theme to Dark
            htmlReporter.Config.Encoding = "UTF-8";

            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

            // Add system or environment info
            extent.AddSystemInfo("OS", Environment.OSVersion.ToString());
            extent.AddSystemInfo("Browser", "Chrome Latest");
            extent.AddSystemInfo("QA Name", "Abhi");
        }

        protected static void CreateTest(string testName, string testDescription)
        {
            // Create a test in the ExtentReport
            test = extent.CreateTest(testName, testDescription);
        }

        [OneTimeTearDown]
        public static void CloseReport()
        {
            // Flush the ExtentReport to generate the final report
            extent.Flush();
        }

        protected static void CaptureScreenshot(string status)
        {
            // Ensure the test-output directory exists
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "test-output"));

            // Capture a screenshot and save it to the test-output directory
            string screenshotName = $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            string screenshotPath = Path.Combine(Directory.GetCurrentDirectory(), "test-output", screenshotName);

            // Save the screenshot to the specified path
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(screenshotPath);

            // Log the screenshot path in the ExtentReport
            test.Log(Status.Info, $"Screenshot saved: {screenshotPath}");

            // Add the screenshot to the ExtentReport using a relative path
            string relativeScreenshotPath = screenshotName; // Relative path from the Extent Report HTML file
            test.AddScreenCaptureFromPath(relativeScreenshotPath);
        }

        protected static void LogTestResult()
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
        }
    }
}