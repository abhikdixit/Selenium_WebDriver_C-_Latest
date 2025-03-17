using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;

namespace WebOrder
{
    [TestFixture]
    [Category("SmokeTest")]
    public class WebOrder_UpdateOrder_VerifyOrder
    {
        private IWebDriver driver;
        private string UserName;

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new EdgeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://secure.smartbearsoftware.com/samples/TestComplete11/WebOrders/Login.aspx");
        }

        [Test, Order(1)]
        public void LoginToApp()
        {
            driver.FindElement(By.XPath("//input[@name='ctl00$MainContent$username']")).SendKeys("Tester");
            driver.FindElement(By.XPath("//input[@name='ctl00$MainContent$password']")).SendKeys("test");
            driver.FindElement(By.CssSelector("input[name='ctl00$MainContent$login_button']")).Click();
            //Assert.IsTrue(driver.FindElement(By.LinkText("Logout")).Displayed);
            Assert.That(driver.FindElement(By.LinkText("Logout")).Displayed, Is.True);

            // Verify Text Present or not
            string actListElementName = driver.FindElement(By.XPath("//h2[normalize-space()='List of All Orders']")).Text;
            string expListElementName = "List of All Orders";
            //Assert.AreEqual(expListElementName, actListElementName);
            Assert.That(expListElementName, Is.EqualTo(actListElementName));

            // Verify Title of the Page
            string actTitle = driver.Title;
            string expTitle = "Web Orders";
            //Assert.AreEqual(expTitle, actTitle);
            Assert.That(expTitle, Is.EqualTo(actTitle));

            // Verify URL of the Page
            string actURL = driver.Url;
            string expURL = "http://secure.smartbearsoftware.com/samples/TestComplete11/WebOrders/default.aspx";
            //Assert.AreEqual(expURL, actURL);
            Assert.That(expURL, Is.EqualTo(actURL));
        }

        [Test, Order(2)]
        public void CreateOrder()
        {
            driver.FindElement(By.LinkText("Order")).Click();
            var product = new SelectElement(driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$ddlProduct")));
            product.SelectByText("FamilyAlbum");
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$txtQuantity")).SendKeys("5");

            Random randomGenerator = new Random();
            int randomInt = randomGenerator.Next(1000);
            UserName = "Dixit" + randomInt;
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$txtName")).SendKeys(UserName);
            System.Threading.Thread.Sleep(5000);
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$TextBox2")).SendKeys("ABC");
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$TextBox3")).SendKeys("Redwood");
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$TextBox5")).SendKeys("5");
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_cardList_1")).Click();
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$TextBox6")).SendKeys("123456789");
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$TextBox1")).SendKeys("12/23");
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_InsertButton")).Click();

            string expSuccessMsg = driver.FindElement(By.XPath("//strong[normalize-space()='New order has been successfully added.']")).Text;
            string actSuccessMsg = "New order has been successfully added.";
            //Assert.AreEqual(expSuccessMsg, actSuccessMsg);
            Assert.That(expSuccessMsg, Is.EqualTo(actSuccessMsg));

            // Go back to View All Order page and Verify that user got created
            driver.FindElement(By.LinkText("View all orders")).Click();
            string actUserName = driver.FindElement(By.XPath("//td[text()='" + UserName + "']")).Text;
            //Assert.AreEqual(UserName, actUserName);
            Assert.That(UserName, Is.EqualTo(actUserName));
        }

        [Test, Order(3)]
        public void UpdateOrder()
        {
            driver.FindElement(By.XPath("//td[text()='" + UserName + "']//following-sibling::td/input")).Click();
            //Assert.IsTrue(driver.FindElement(By.XPath("//h2[normalize-space()='Edit Order']")).Displayed);
            Assert.That(driver.FindElement(By.XPath("//h2[normalize-space()='Edit Order']")).Displayed, Is.True);
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$TextBox4")).SendKeys("CA");
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_UpdateButton")).Click();

            string actState = driver.FindElement(By.XPath("//td[text()='" + UserName + "']//following-sibling::td[text()='CA']")).Text;
            string expState = "CA";
            //Assert.AreEqual(expState, actState);
            Assert.That(expState, Is.EqualTo(actState));
        }
        [OneTimeTearDown]
        public void PostCondition()
        {
            driver.FindElement(By.LinkText("Logout")).Click();
            driver.Quit();
        }
    }
}
