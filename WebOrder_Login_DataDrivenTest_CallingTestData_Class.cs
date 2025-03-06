using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System.Collections;

namespace WebOrderTests
{
    public class WebOrder_Login_DataDrivenTest_Calling_TestData_Class
    {
        private IWebDriver driver;

        [OneTimeSetUp]
        public void PreCondition()
        {
            driver = new EdgeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://secure.smartbearsoftware.com/samples/TestComplete11/WebOrders/Login.aspx");
        }

        [Test, TestCaseSource(typeof(WebOrder_TestData), "LoginData")]
        public void LoginToApp(string uname, string pass)
        {
            driver.FindElement(By.XPath("//input[@name='ctl00$MainContent$username']")).SendKeys(uname);
            driver.FindElement(By.XPath("//input[@name='ctl00$MainContent$password']")).SendKeys(pass);
            driver.FindElement(By.CssSelector("input[name='ctl00$MainContent$login_button']")).Click();

            Assert.That(driver.FindElement(By.LinkText("Logout")).Displayed, Is.True);

            // Verify Text Present or not
            string actListElementName = driver.FindElement(By.XPath("//h2[normalize-space()='List of All Orders']")).Text;
            string expListElementName = "List of All Orders";
            //Assert.AreEqual(expListElementName, actListElementName);
            Assert.That(expListElementName, Is.EqualTo(actListElementName)); 
            driver.FindElement(By.LinkText("Logout")).Click();
        }

        [OneTimeTearDown]
        public void PostCondition()
        {
            driver.Quit();
        }
    }
}
