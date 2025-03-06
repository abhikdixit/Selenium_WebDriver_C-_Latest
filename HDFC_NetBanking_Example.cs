using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace WebOrder
{
    [TestFixture]
    public class HDFC_Netbanking
    {
        private EdgeDriver driver;

        [OneTimeSetUp]
        public void LaunchBrowser()
        {
            driver = new EdgeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://netbanking.hdfcbank.com/netbanking/");
        }

        [OneTimeTearDown]
        public void CloseBrowser()
        {
            //driver.Quit();
        }

        [Test, Order(2)]
        public void EnterCustomerID()
        {
            Thread.Sleep(5000);
            driver.SwitchTo().Frame("login_page");
            driver.FindElement(By.XPath("//input[@name='fldLoginUserId']")).SendKeys("1000");
            driver.FindElement(By.XPath("//a[normalize-space()='CONTINUE']")).Click();
            driver.SwitchTo().DefaultContent();
            System.Threading.Thread.Sleep(3000);
           // Assert.IsTrue(driver.FindElement(By.XPath("//label[text()='Password/IPIN']")).Displayed);
            Assert.That(driver.FindElement(By.XPath("//label[text()='Password/IPIN']")).Displayed, Is.True);
        }

        [Test, Order(1), Ignore("Disabled test")]
        public void Donot_Enter_CustomerID()
        {
            driver.SwitchTo().Frame("login_page");
            driver.FindElement(By.XPath("//a[contains(text(),'CONTINUE')]")).Click();
            string expAlertMsg = "Customer ID cannot be left blank.";
            string actAlertMsg = driver.SwitchTo().Alert().Text;
            //Assert.AreEqual(expAlertMsg, actAlertMsg);
            Assert.That(expAlertMsg, Is.EqualTo(actAlertMsg));
            System.Threading.Thread.Sleep(3000);
            driver.SwitchTo().Alert().Accept();
            System.Threading.Thread.Sleep(3000);
            driver.SwitchTo().DefaultContent();
        }
    }
}
