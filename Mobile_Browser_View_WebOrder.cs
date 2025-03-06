using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MobileViewTest
{
    public class MobileBrowserViewWebOrderLogin
    {
        private IWebDriver driver;

        [SetUp]
        public void LaunchBrowser()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Size = new System.Drawing.Size(430, 932);
        }

        [Test]
        public void SignOn()
        {
            driver.Navigate().GoToUrl("http://secure.smartbearsoftware.com/samples/TestComplete11/WebOrders/Login.aspx");
            driver.FindElement(By.Name("ctl00$MainContent$username")).SendKeys("Tester");
            driver.FindElement(By.Name("ctl00$MainContent$password")).SendKeys("test");
            driver.FindElement(By.Name("ctl00$MainContent$login_button")).Click();
            Assert.That(driver.FindElement(By.LinkText("Logout")).Displayed, Is.True);
            //Assert.IsTrue(driver.FindElement(By.LinkText("Logout")).Displayed, "Login failed");
            driver.FindElement(By.LinkText("Logout")).Click();
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}
