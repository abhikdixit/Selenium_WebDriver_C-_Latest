using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace pk_Advance_Topics
{
    public class Navigate_To_All_Links_Example
    {
        [Test]
        public void NavigateToAllLinks()
        {
            IWebDriver driver = new ChromeDriver();
            try
            {
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://www.google.co.in/");

                IList<IWebElement> linksize = driver.FindElements(By.TagName("a"));
                int linksCount = linksize.Count;
                Console.WriteLine("Total no of links Available: " + linksCount);
                string[] links = new string[linksCount];
                Console.WriteLine("List of links Available:");

                // Print all the links from the webpage
                for (int i = 0; i < linksCount; i++)
                {
                    links[i] = linksize[i].GetAttribute("href");
                    Console.WriteLine(links[i]);
                }

                // Navigate to each link on the webpage
                foreach (string link in links)
                {
                    if (!string.IsNullOrEmpty(link))
                    {
                        driver.Navigate().GoToUrl(link);
                        Console.WriteLine("Title: " + driver.Title);
                        Console.WriteLine("URL: " + driver.Url);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                driver.Quit();
            }
        }
    }
}
