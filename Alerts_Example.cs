using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebOrder
{
    [TestFixture]
    public class Alerts_Example
    {
        private IWebDriver driver;

        [OneTimeSetUp]
        public void LaunchBrowser()
        {
            driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");
        }

        [OneTimeTearDown]
        public void CloseBrowser()
        {
            driver.Close();
        }

        [Test]
        public void JS_Alert()
        {
            driver.FindElement(By.CssSelector("button[onclick='jsAlert()']")).Click();
            string actText = driver.SwitchTo().Alert().Text;
            string expText = "I am a JS Alert";
            //Assert.AreEqual(expText, actText);
            Assert.That(expText, Is.EqualTo(actText));
            driver.SwitchTo().Alert().Accept();
        }

        [Test]
        public void JS_Confirm()
        {
            driver.FindElement(By.CssSelector("button[onclick='jsConfirm()']")).Click();
            string actText = driver.SwitchTo().Alert().Text;
            string expText = "I am a JS Confirm";
            //Assert.AreEqual(expText, actText);
            Assert.That(expText, Is.EqualTo(actText));
            driver.SwitchTo().Alert().Dismiss();
        }

        [Test]
        public void JS_Prompt()
        {
            driver.FindElement(By.CssSelector("button[onclick='jsPrompt()']")).Click();
            driver.SwitchTo().Alert().SendKeys("Abhi");
            driver.SwitchTo().Alert().Accept();
            string actText = driver.FindElement(By.XPath("//p[@id='result']")).Text;
            string expText = "You entered: Abhi";
            //Assert.AreEqual(expText, actText);
            Assert.That(expText, Is.EqualTo(actText));
        }

       
    }
}
