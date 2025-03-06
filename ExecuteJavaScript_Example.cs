using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Threading;

namespace pk_Advance_Topics
{
    public class Execute_JavaScript_Code
    {
        IWebDriver driver;

        [OneTimeSetUp]
        public void LaunchBrowser()
        {
            // Set up EdgeDriver (ensure EdgeDriver is installed and in PATH)
            driver = new EdgeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Login()
        {
            driver.Navigate().GoToUrl("http://secure.smartbearsoftware.com/samples/TestComplete11/WebOrders/Login.aspx");
            Thread.Sleep(5000);

            // Creating the JavascriptExecutor interface object by Type casting
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            IWebElement uname = driver.FindElement(By.Name("ctl00$MainContent$username"));
            IWebElement upass = driver.FindElement(By.Name("ctl00$MainContent$password"));
            IWebElement button = driver.FindElement(By.Name("ctl00$MainContent$login_button"));

            // Enter UserName using JavascriptExecutor
            js.ExecuteScript("arguments[0].value='Tester';", uname);
            Thread.Sleep(2000);

            // Enter Password using JavascriptExecutor
            js.ExecuteScript("arguments[0].value='test';", upass);
            Thread.Sleep(3000);

            // Perform Click on LOGIN button using JavascriptExecutor
            js.ExecuteScript("arguments[0].click();", button);
            Thread.Sleep(5000);

            // Verify that Dashboard page is displayed
            string ActElement = driver.FindElement(By.XPath("//h1[normalize-space()='Web Orders']")).Text;
            string ExpElement = "Web Orders";

            // Using Assert.That for better readability
            Assert.That(ActElement, Is.EqualTo(ExpElement), "The expected page title does not match the actual title.");

            Console.WriteLine(ActElement);

            // Get the title of the page using JavascriptExecutor
            string TitleName = (string)js.ExecuteScript("return document.title;");
            Console.WriteLine("Title of the page = " + TitleName);
        }

        [OneTimeTearDown]
        public void CloseBrowser()
        {
            // Close the browser
            driver.Quit();
        }
    }
}