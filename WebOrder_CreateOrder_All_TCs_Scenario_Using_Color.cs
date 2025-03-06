using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace WebOrderTests
{
    public class WebOrder_CreateOrder_All_TCs_Scenario_Using_Color
    {
        private IWebDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://secure.smartbearsoftware.com/samples/TestComplete11/WebOrders/Login.aspx");
            driver.FindElement(By.Name("ctl00$MainContent$username")).SendKeys("Tester");
            driver.FindElement(By.Name("ctl00$MainContent$password")).SendKeys("test");
            driver.FindElement(By.Name("ctl00$MainContent$login_button")).Click();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            driver.FindElement(By.LinkText("Logout")).Click();
            driver.Quit();
        }

        [Test, TestCaseSource(typeof(WebOrder_TestData), "OrderTestData")]
        public void Create_Order(string quantity, string discount, string name, string street, string city, string state, string zip, string cardNo, string expiry, string Exp_Msg)
        {
            driver.FindElement(By.LinkText("Order")).Click();
            SelectElement se = new SelectElement(driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_ddlProduct")));
            se.SelectByValue("FamilyAlbum");

            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_txtQuantity")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_txtQuantity")).SendKeys(quantity);
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_txtDiscount")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_txtDiscount")).SendKeys(discount);
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_txtName")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_txtName")).SendKeys(name);
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_TextBox2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_TextBox2")).SendKeys(street);
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_TextBox3")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_TextBox3")).SendKeys(city);
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_TextBox4")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_TextBox4")).SendKeys(state);
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_TextBox5")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_TextBox5")).SendKeys(zip);

            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_cardList_0")).Click();
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_TextBox6")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_TextBox6")).SendKeys(cardNo);
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_TextBox1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_TextBox1")).SendKeys(expiry);
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_InsertButton")).Click();

            if (Exp_Msg.Equals("New order has been successfully added.", StringComparison.OrdinalIgnoreCase))
            {
                string Act_Msg = driver.FindElement(By.XPath("//strong[normalize-space()='New order has been successfully added.']")).Text;
                Assert.That(Act_Msg, Is.EqualTo(Exp_Msg));
            }
            else
            {
                string Act_Error_Msg = driver.FindElement(By.XPath("//span[@style='color: red; display: inline;']")).Text;
                Assert.That(Act_Error_Msg, Is.EqualTo(Exp_Msg));
            }
        }
    }
}
