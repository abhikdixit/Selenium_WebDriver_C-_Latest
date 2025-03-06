using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace DemoWebShopTests
{
    public class ShoppingCartPriceVerification
    {
        private IWebDriver driver;
        private string costOnAdd;
        private string costInCart;

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://demowebshop.tricentis.com/login");
        }

        [Test, Order(1)]
        public void LoginToApplication()
        {
            driver.FindElement(By.Name("Email")).SendKeys("m.kavithamaran@gmail.com");
            driver.FindElement(By.Name("Password")).SendKeys("test123456");
            driver.FindElement(By.XPath("//input[contains(@class,'login')]")).Click();
            Assert.That(driver.FindElement(By.LinkText("Log out")).Displayed, Is.True);
            //Assert.IsTrue(driver.FindElement(By.LinkText("Log out")).Displayed, "Login failed");
        }

        [Test, Order(2)]
        public void VerifyBookPage()
        {
            driver.FindElement(By.XPath("//a[contains(@href,'books')]")).Click();
            Assert.That("BOOKS", Is.EqualTo(driver.FindElement(By.XPath("//a[contains(@href,'books')]")).Text));
            //Assert.AreEqual("BOOKS", driver.FindElement(By.XPath("//a[contains(@href,'books')]")).Text);
            //Assert.AreEqual("Demo Web Shop. Books", driver.Title);
            Assert.That("Demo Web Shop. Books", Is.EqualTo(driver.Title));
            //Assert.AreEqual("https://demowebshop.tricentis.com/books", driver.Url);
            Assert.That("https://demowebshop.tricentis.com/books", Is.EqualTo(driver.Url));
        }

        [Test, Order(3)]
        public void AddToCart()
        {
            Assert.That(driver.FindElement(By.LinkText("Computing and Internet")).Displayed, Is.True);
            //Assert.IsTrue(driver.FindElement(By.LinkText("Computing and Internet")).Displayed);
            costOnAdd = driver.FindElement(By.XPath("//a[text()='Computing and Internet']/ancestor::div[@class='item-box']//span[@class='price actual-price']")).Text;
            driver.FindElement(By.XPath("//a[text()='Computing and Internet']/ancestor::div[@class='item-box']//div[@class='buttons']")).Click();
            System.Threading.Thread.Sleep(2000); // Use explicit waits in real projects
        }

        [Test, Order(4)]
        public void VerifyCartPage()
        {
            driver.FindElement(By.XPath("//span[text()='Shopping cart']")).Click();
            Assert.That("Shopping cart", Is.EqualTo(driver.FindElement(By.XPath("//div[@class='page shopping-cart-page']//h1")).Text));

            //Assert.AreEqual("Shopping cart", driver.FindElement(By.XPath("//div[@class='page shopping-cart-page']//h1")).Text);
            Assert.That("Demo Web Shop. Shopping Cart", Is.EqualTo(driver.Title));

            //Assert.AreEqual("Demo Web Shop. Shopping Cart", driver.Title);
            Assert.That("https://demowebshop.tricentis.com/cart", Is.EqualTo(driver.Url));

            //Assert.AreEqual("https://demowebshop.tricentis.com/cart", driver.Url);
        }

        [Test, Order(5)]
        public void VerifyProductInCart()
        {
            Assert.That(driver.FindElement(By.XPath("//tr[@class='cart-item-row']//a[contains(@href,'/computing-and-internet')]")).Displayed, Is.True);

            //Assert.IsTrue(driver.FindElement(By.XPath("//tr[@class='cart-item-row']//a[contains(@href,'/computing-and-internet')]")).Displayed);
            costInCart = driver.FindElement(By.XPath("//tr[@class='cart-item-row']//span[@class='product-unit-price']")).Text;
            Assert.That(costOnAdd, Is.EqualTo(costInCart));

            //Assert.AreEqual(costOnAdd, costInCart, "Price mismatch in cart");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            if (driver.FindElement(By.LinkText("Log out")).Displayed)
            {
                driver.FindElement(By.LinkText("Log out")).Click();
            }
            driver.Quit();
        }
    }
}
