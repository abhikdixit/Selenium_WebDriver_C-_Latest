using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebOrder
{
    [TestFixture]
    public class WebOrder_Login_Get_Text_URL_and_Title
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {

            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://secure.smartbearsoftware.com/samples/TestComplete11/WebOrders/Login.aspx");
        }

        [Test]
        public void LoginToApp()
        {
            driver.FindElement(By.XPath("//input[@name='ctl00$MainContent$username']")).SendKeys("Tester");
            driver.FindElement(By.XPath("//input[@name='ctl00$MainContent$password']")).SendKeys("test");
            driver.FindElement(By.CssSelector("input[name='ctl00$MainContent$login_button']")).Click();
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

        [TearDown]
        public void TearDown()
        {
            driver.FindElement(By.LinkText("Logout")).Click();
            driver.Quit();
        }
    }
}
