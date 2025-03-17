using System;
using System.Xml;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace pk_Advance_Topics
{
    public class Extent_Report_Genrator
    {
        [Test]
        public void ReportConverter()
        {
            // Initialize ExtentReports
            var extent = new ExtentReports();

            // Initialize ExtentSparkReporter
            var htmlReporter = new ExtentSparkReporter("C:\\Training_Scripts\\Selenium_WebDriver_C#\\bin\\Debug\\net8.0\\TestReport.html");

            // Set the dark theme
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Dark;

            // Attach the reporter to ExtentReports
            extent.AttachReporter(htmlReporter);

            // Add system information (OS and Browser details)
            extent.AddSystemInfo("OS", Environment.OSVersion.ToString());
            extent.AddSystemInfo("Browser", "Chrome"); // Replace with actual browser name if available

            // Define the full path to the TestResults.xml file
            //string testResultsPath = "C:\\Training_Scripts\\Selenium_WebDriver_C#\\bin\\Debug\\net8.0\\TestResult.xml";
            string testResultsPath = "C:\\Users\\abhin\\TestResult.xml";

            // Load the NUnit XML file
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(testResultsPath);

            // Parse the XML file
            XmlNodeList testCases = xmlDoc.SelectNodes("//test-case");
            foreach (XmlNode testCase in testCases)
            {
                string testName = testCase.Attributes["name"].Value;
                string result = testCase.Attributes["result"].Value;
                string duration = testCase.Attributes["duration"].Value;

                // Create a test in ExtentReports
                var test = extent.CreateTest(testName);

                // Log the test status
                if (result.Equals("Passed", StringComparison.OrdinalIgnoreCase))
                {
                    test.Pass("Test passed. Duration: " + duration);
                }
                else if (result.Equals("Failed", StringComparison.OrdinalIgnoreCase))
                {
                    string failureMessage = testCase.SelectSingleNode("failure/message")?.InnerText;
                    test.Fail("Test failed. Duration: " + duration + ". Reason: " + failureMessage);
                }
                else if (result.Equals("Skipped", StringComparison.OrdinalIgnoreCase))
                {
                    test.Skip("Test skipped. Duration: " + duration);
                }
            }

            // Flush the report (save to file)
            extent.Flush();

            Console.WriteLine("HTML report generated: TestReport.html");
        }
    }
}