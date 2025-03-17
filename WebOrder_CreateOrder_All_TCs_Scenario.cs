using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System.Collections;

namespace WebOrderTests
{
    //[TestFixture]
    //[Category("SmokeTest")]
    public class WebOrder_CreateOrder_All_TCs_Scenario
    {
        private IWebDriver driver;

        [OneTimeSetUp]
        public void PreCondition()
        {
            driver = new EdgeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://secure.smartbearsoftware.com/samples/TestComplete11/WebOrders/Login.aspx");
            driver.FindElement(By.XPath("//input[@name='ctl00$MainContent$username']")).SendKeys("Tester");
            driver.FindElement(By.XPath("//input[@name='ctl00$MainContent$password']")).SendKeys("test");
            driver.FindElement(By.XPath("//input[@name='ctl00$MainContent$login_button']")).Click();
        }

        [Test, TestCaseSource(typeof(WebOrder_TestData), "OrderTestData")]
        public void CreateOrder(string quantity, string discount, string name, string street, string city, string state, string zip, string cardNo, string expiry, string Exp_Msg)
        {
            driver.FindElement(By.LinkText("Order")).Click();
            SelectElement select = new SelectElement(driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_ddlProduct")));
            select.SelectByValue("FamilyAlbum");
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_txtQuantity")).SendKeys(quantity);
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_txtDiscount")).SendKeys(discount);
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_txtName")).SendKeys(name);
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_TextBox2")).SendKeys(street);
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_TextBox3")).SendKeys(city);
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_TextBox4")).SendKeys(state);
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_TextBox5")).SendKeys(zip);
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_cardList_0")).Click();
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_TextBox6")).SendKeys(cardNo);
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_TextBox1")).SendKeys(expiry);
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_InsertButton")).Click();

            bool foundMsg = driver.PageSource.Contains(Exp_Msg);
            Assert.That(foundMsg, Is.True);
            //Assert.IsTrue(foundMsg);
        }

        [OneTimeTearDown]
        public void PostCondition()
        {
            driver.FindElement(By.LinkText("Logout")).Click();
            driver.Quit();
        }
    }
}
