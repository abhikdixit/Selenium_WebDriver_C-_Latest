using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
//using OpenQA.Selenium.Chrome.ChromeOptions;


namespace WebOrder
{
    [TestFixture]
    public class WebOrder_Login_Headless_Browser
    {
        private IWebDriver driver;

        [SetUp]
        public void PreCondition()
        {
            ChromeOptions options = new ChromeOptions();
            // Uncomment to run headless
             options.AddArgument("headless");
            //options.AddArgument("incognito");

            driver = new ChromeDriver(options);
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
        }

        [TearDown]
        public void PostCondition()
        {
            driver.FindElement(By.LinkText("Logout")).Click();
            driver.Quit();
        }
    }
}
