using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;

namespace WebOrder
{
    [TestFixture]
    public class LetsCodeIt_Example
    {
        private IWebDriver driver;

        [OneTimeSetUp]
        public void LaunchBrowser()
        {
            driver = new EdgeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.letskodeit.com/practice");
        }

        [OneTimeTearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }

        [Test]
        public void Hide_Element_Example()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            driver.FindElement(By.XPath("//input[@id='hide-textbox']")).Click();
            bool txtbox = driver.FindElement(By.Id("displayed-text")).Displayed;
            //Assert.IsFalse(txtbox);
            Assert.That(txtbox, Is.False);
        }

        [Test]
        public void Elanle_Field_Example()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            driver.FindElement(By.XPath("//input[@id='disabled-button']")).Click();
            bool txtbox = driver.FindElement(By.XPath("//input[@id='enabled-example-input']")).Enabled;
            //Assert.IsFalse(txtbox);
            Assert.That(txtbox, Is.False);
        }
    }
}
