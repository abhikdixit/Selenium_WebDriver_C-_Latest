using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests.Pages
{
    public class LoginLogoutPage
    {
        private readonly IWebDriver driver;

        // Locators
        private By InputUserName => By.XPath("//label[text()='Username:']/following-sibling::input");
        private By InputPassword => By.XPath("//label[text()='Password:']/following-sibling::input");
        private By LoginButton => By.Id("ctl00_MainContent_login_button");
        private By LogoutMenu => By.XPath("//i[@class='oxd-icon bi-caret-down-fill oxd-userdropdown-icon']");
        private By LogoutButton => By.XPath("//a[text()='Logout']");

        public LoginLogoutPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // Navigate to the application URL
        public void GoToURL()
        {
            driver.Navigate().GoToUrl("http://secure.smartbearsoftware.com/samples/TestComplete11/WebOrders/Login.aspx");
        }

        // Login to the application
        public void LoginToApp(string username, string password)
        {
            driver.FindElement(InputUserName).SendKeys(username);
            driver.FindElement(InputPassword).SendKeys(password);
            driver.FindElement(LoginButton).Click();
        }

        // Logout from the application
        public void LogoutFromApp()
        {
            //driver.FindElement(LogoutMenu).Click();
            driver.FindElement(LogoutButton).Click();
        }

        // Placeholder for "Forget Your Password" functionality
        public void ForgetYourPassword()
        {
            // Implement logic for "Forget Your Password" functionality
        }
    }
}