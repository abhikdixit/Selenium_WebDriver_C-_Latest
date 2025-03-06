using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;

namespace WebOrderTests
{
    public class WebOrder_CreateOrder_DataDrivenTest_All_TCs_Scenario
    {
        private IWebDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new EdgeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://secure.smartbearsoftware.com/samples/TestComplete11/WebOrders/Login.aspx");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.FindElement(By.Name("ctl00$MainContent$username")).SendKeys("Tester");
            driver.FindElement(By.Name("ctl00$MainContent$password")).SendKeys("test");
            driver.FindElement(By.CssSelector("input[name='ctl00$MainContent$login_button']")).Click();
            Assert.That(driver.FindElement(By.LinkText("Logout")).Displayed, Is.True);
            Assert.That(driver.FindElement(By.XPath("//h2[normalize-space()='List of All Orders']")).Displayed, Is.True);
            driver.FindElement(By.XPath("//a[text()='Order']")).Click();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            driver.FindElement(By.LinkText("Logout")).Click();
            driver.Quit();
        }

        [Test, TestCaseSource(typeof(WebOrder_TestData), "CreateOrderTestData")]
        public void CreateOrder(string product, string quantity, string name, string street, string city, string zipcode, string card, string cardNr, string expdate, string expResult)
        {
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_txtQuantity")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_txtQuantity")).SendKeys(quantity);

            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_txtName")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_txtName")).SendKeys(name);

            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_TextBox2")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_TextBox2")).SendKeys(street);

            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_TextBox3")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_TextBox3")).SendKeys(city);

            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_TextBox5")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_TextBox5")).SendKeys(zipcode);

            if (!string.IsNullOrEmpty(card))
            {
                driver.FindElement(By.XPath($"//input[@value='{card}']")).Click();
            }

            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_TextBox6")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_TextBox6")).SendKeys(cardNr);

            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_TextBox1")).Clear();
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_TextBox1")).SendKeys(expdate);

            if (expResult != "empty_quantity")
            {
                driver.FindElement(By.LinkText("Process")).Click();
            }

            switch (expResult)
            {
                case "valid":
                    Assert.That(driver.PageSource.Contains("New order has been successfully added."), Is.True);
                    break;
                case "empty_quantity":
                    Assert.That(driver.PageSource.Contains("Field 'Quantity' cannot be empty."), Is.True);
                    break;
                case "invalid_quantity":
                    Assert.That(driver.PageSource.Contains("Quantity must be greater than zero."), Is.True);
                    break;
                case "empty_name":
                    Assert.That(driver.PageSource.Contains("Field 'Customer name' cannot be empty."), Is.True);
                    break;
                case "empty_street":
                    Assert.That(driver.PageSource.Contains("Field 'Street' cannot be empty."), Is.True);
                    break;
                case "empty_city":
                    Assert.That(driver.PageSource.Contains("Field 'City' cannot be empty."), Is.True);
                    break;
                case "empty_zip":
                    Assert.That(driver.PageSource.Contains("Field 'Zip' cannot be empty."), Is.True);
                    break;
                case "invalid_zip":
                    Assert.That(driver.PageSource.Contains("Invalid format. Only digits allowed."), Is.True);
                    break;
                case "empty_card":
                    Assert.That(driver.PageSource.Contains("Select a card type."), Is.True);
                    break;
                case "empty_cardnr":
                    Assert.That(driver.PageSource.Contains("Field 'Card Nr' cannot be empty."), Is.True);
                    break;
                case "invalid_cardnr":
                    Assert.That(driver.PageSource.Contains("Invalid format. Only digits allowed."), Is.True);
                    break;
                case "empty_expdate":
                    Assert.That(driver.PageSource.Contains("Field 'Expire date' cannot be empty."), Is.True);
                    break;
                case "invalid_expdate":
                    Assert.That(driver.PageSource.Contains("Invalid format. Required format is mm/yy."), Is.True);
                    break;
                default:
                    Assert.Fail("Unexpected test case result.");
                    break;
            }
        }
    }
}
