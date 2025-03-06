using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace WebOrder
{
    public class WebOrderLoginAllTCsExample
    {
        private IWebDriver driver;

        [OneTimeSetUp]
        public void PreCondition()
        {
            driver = new EdgeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://secure.smartbearsoftware.com/samples/TestComplete11/WebOrders/Login.aspx");
        }

        [Test, TestCaseSource(nameof(LoginAllTCs))]
        public void LoginToApp(string uname, string pass, string expResult)
        {
            driver.FindElement(By.XPath("//input[@name='ctl00$MainContent$username']")).Clear();
            driver.FindElement(By.XPath("//input[@name='ctl00$MainContent$username']")).SendKeys(uname);
            driver.FindElement(By.XPath("//input[@name='ctl00$MainContent$password']")).Clear();
            driver.FindElement(By.XPath("//input[@name='ctl00$MainContent$password']")).SendKeys(pass);
            driver.FindElement(By.CssSelector("input[name='ctl00$MainContent$login_button']")).Click();

            if (expResult.Equals("Logout", System.StringComparison.OrdinalIgnoreCase))
            {
                string actMsg = driver.FindElement(By.LinkText("Logout")).Text;
                //Assert.AreEqual(actMsg, expResult);
                Assert.That(expResult, Is.EqualTo(actMsg));
                driver.FindElement(By.LinkText("Logout")).Click();
            }
            else
            {
                string actErrorMsg = driver.FindElement(By.Id("ctl00_MainContent_status")).Text;
                //Assert.AreEqual(actErrorMsg, expResult);
                Assert.That(expResult, Is.EqualTo(actErrorMsg));
            }
        }

        [OneTimeTearDown]
        public void PostCondition()
        {
            driver.Close();
        }

        public static IEnumerable<TestCaseData> LoginAllTCs()
        {
            yield return new TestCaseData("Tester", "test", "Logout");
            yield return new TestCaseData("Tester", "test1", "Invalid Login or Password.");
            yield return new TestCaseData("Tester1", "test", "Invalid Login or Password.");
            yield return new TestCaseData("", "test", "Invalid Login or Password.");
            yield return new TestCaseData("Tester", "", "Invalid Login or Password.");
            // Add more test cases as needed
        }
    }
}
