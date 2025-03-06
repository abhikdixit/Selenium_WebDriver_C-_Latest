using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace WebOrderTests
{
    [TestFixture]
    public class WebOrder_Login_DataDriven_Using_Excel
    {
        private IWebDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://secure.smartbearsoftware.com/samples/TestComplete11/WebOrders/Login.aspx");
        }

       
        [Test, TestCaseSource(typeof(WebOrder_TestData), "ReadDataFromExcel")]
        public void LoginToApp(string username, string password)
        {
            driver.FindElement(By.Name("ctl00$MainContent$username")).Clear();
            driver.FindElement(By.Name("ctl00$MainContent$username")).SendKeys(username);
            driver.FindElement(By.Name("ctl00$MainContent$password")).Clear();
            driver.FindElement(By.Name("ctl00$MainContent$password")).SendKeys(password);
            driver.FindElement(By.CssSelector("input[name='ctl00$MainContent$login_button']")).Click();
            // Verify Text Present or not
            string actListElementName = driver.FindElement(By.XPath("//h2[normalize-space()='List of All Orders']")).Text;
            string expListElementName = "List of All Orders";
            //Assert.AreEqual(expListElementName, actListElementName);
            Assert.That(expListElementName, Is.EqualTo(actListElementName));

            driver.FindElement(By.LinkText("Logout")).Click();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
