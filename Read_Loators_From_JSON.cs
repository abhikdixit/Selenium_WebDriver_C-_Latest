using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace pk_Advance_Topics
{
    public class Read_Data_From_JSON
    {
        private IWebDriver driver;

        [Test]
        public void Login()
        {
            // Set up ChromeDriver
            driver = new ChromeDriver();

            // Load JSON file
            string jsonFilePath = Path.Combine("C:", "Training_Scripts", "Selenium_WebDriver_C#", "bin", "Debug", "net8.0", "TestDataFile", "ObjectRepository.json");
            JObject jsonObj = JObject.Parse(File.ReadAllText(jsonFilePath));

            // Read data from the JSON file
            string url = (string)jsonObj["Web_URL"];
            string uname = (string)jsonObj["id_UserName"];
            string upass = (string)jsonObj["id_UserPass"];
            string btnLogin = (string)jsonObj["id_Login"];

            // Navigate to the URL
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();

            // Enter credentials and log in
            driver.FindElement(By.Id(uname)).SendKeys("Tester");
            driver.FindElement(By.Id(upass)).SendKeys("test");
            driver.FindElement(By.Id(btnLogin)).Click();

            // Close the browser
            driver.Quit();
        }
    }
}