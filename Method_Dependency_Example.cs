using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace pk_Advance_Topics
{
    public class TestMethod_Dependent_On_AnotherMethod
    {
        IWebDriver driver;
        string UserName;
        static bool signOnPassed = false; // Static variable to track the result of Sign_On

        [OneTimeSetUp]
        public void LaunchBrowser()
        {
            // Initialize ChromeDriver
            driver = new ChromeDriver();

            // Maximize the browser window
            driver.Manage().Window.Maximize();

            // Open the login page
            driver.Navigate().GoToUrl("http://secure.smartbearsoftware.com/samples/TestComplete11/WebOrders/Login.aspx");
        }

        [Test, Order(1)]
        public void Sign_On()
        {
            try
            {
                // Perform login
                driver.FindElement(By.XPath("//input[@name='ctl00$MainContent$username']")).SendKeys("Tester");
                driver.FindElement(By.XPath("//input[@name='ctl00$MainContent$password']")).SendKeys("test");
                driver.FindElement(By.CssSelector("input[name='ctl00$MainContent$login_button']")).Click();

                // Verify logout link is displayed
                Assert.That(driver.FindElement(By.LinkText("Logout")).Displayed, Is.True, "Logout link is not displayed.");

                // Verify page title
                string actTitle = driver.Title;
                string expTitle = "Web Orders";
                Assert.That(actTitle, Is.EqualTo(expTitle), "The page title does not match the expected title.");

                // Verify URL
                string actUrl = driver.Url;
                string expUrl = "http://secure.smartbearsoftware.com/samples/TestComplete11/WebOrders/default.aspx";
                Assert.That(actUrl, Is.EqualTo(expUrl), "The URL does not match the expected URL.");

                // Verify "List of All Orders" text
                string actListElementName = driver.FindElement(By.XPath("//h2[normalize-space()='List of All Orders']")).Text;
                string expListElementName = "List of All Orders";
                Assert.That(actListElementName, Is.EqualTo(expListElementName), "The 'List of All Orders' text does not match the expected text.");

                // Mark Sign_On as passed
                signOnPassed = true;
            }
            catch (Exception ex)
            {
                // Mark Sign_On as failed
                signOnPassed = false;
                throw new Exception("Sign_On test failed: " + ex.Message);
            }
        }

        [Test, Order(2)]
        public void AddUsers_Page()
        {
            // Skip this test if Sign_On failed
            if (!signOnPassed)
            {
                Assert.Ignore("Skipping AddUsers_Page because Sign_On failed.");
            }

            // Navigate to the Order page
            driver.FindElement(By.LinkText("Order")).Click();

            // Select product from dropdown
            var product = new SelectElement(driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$ddlProduct")));
            product.SelectByText("FamilyAlbum");

            // Enter quantity
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$txtQuantity")).SendKeys("5");

            // Generate random username
            Random randomGenerator = new Random();
            int randomInt = randomGenerator.Next(1000);
            UserName = "Dixit" + randomInt;

            // Fill in user details
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$txtName")).SendKeys(UserName);
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$TextBox2")).SendKeys("ABC");
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$TextBox3")).SendKeys("Redwood");
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$TextBox5")).SendKeys("5");
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_cardList_1")).Click();
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$TextBox6")).SendKeys("123456789");
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$TextBox1")).SendKeys("12/23");

            // Submit the form
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_InsertButton")).Click();

            // Verify success message
            string actSuccessMsg = driver.FindElement(By.XPath("//strong[normalize-space()='New order has been successfully added.']")).Text;
            string expSuccessMsg = "New order has been successfully added.";
            Assert.That(actSuccessMsg, Is.EqualTo(expSuccessMsg), "The confirmation message does not match the expected message.");

            // Navigate back to View All Orders page
            driver.FindElement(By.LinkText("View all orders1")).Click();

            // Verify the added user
            string actUserName = driver.FindElement(By.XPath($"//td[text()='{UserName}']")).Text;
            Assert.That(actUserName, Is.EqualTo(UserName), "The added user name does not match the expected user name.");
        }

        [Test, Order(3)]
        public void VerifyAddedUser()
        {
            // Skip this test if Sign_On failed
            if (!signOnPassed)
            {
                Assert.Ignore("Skipping VerifyAddedUser because Sign_On failed.");
            }

            // Click on the user to edit
            driver.FindElement(By.XPath($"//td[text()='{UserName}']//following-sibling::td/input")).Click();

            // Verify Edit Order page is displayed
            Assert.That(driver.FindElement(By.XPath("//h2[normalize-space()='Edit Order']")).Displayed, Is.True, "Edit Order page is not displayed.");

            // Update the state
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$TextBox4")).Clear();
            driver.FindElement(By.Name("ctl00$MainContent$fmwOrder$TextBox4")).SendKeys("CA");

            // Submit the update
            driver.FindElement(By.Id("ctl00_MainContent_fmwOrder_UpdateButton")).Click();

            // Verify the updated state
            string actState = driver.FindElement(By.XPath($"//td[text()='{UserName}']//following-sibling::td[text()='CA']")).Text;
            string expState = "CA";
            Assert.That(actState, Is.EqualTo(expState), "The updated state does not match the expected state.");
        }

        [OneTimeTearDown]
        public void CloseBrowser()
        {
            // Close the browser
            driver.Quit();
        }
    }
}