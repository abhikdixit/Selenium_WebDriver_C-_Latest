using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;


namespace WebOrder
{
    [TestFixture]
    [Category("RegressionTest")]
    public class WebOrder_Login_TestNG
    {
        private IWebDriver driver;

        [Test]
        public void LoginToApp()
        {

            driver = new ChromeDriver();

            // For Chrome
            // new DriverManager().SetUpDriver(new ChromeConfig());
            // driver = new ChromeDriver();

            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://secure.smartbearsoftware.com/samples/TestComplete11/WebOrders/Login.aspx");
            driver.FindElement(By.Name("ctl00$MainContent$username")).SendKeys("Tester");
            driver.FindElement(By.Name("ctl00$MainContent$password")).SendKeys("test");
            driver.FindElement(By.Name("ctl00$MainContent$login_button")).Click();
            Assert.That(driver.FindElement(By.LinkText("Logout")).Displayed, Is.True);
            // Close the browser
            driver.Close();
        }
    }
}
