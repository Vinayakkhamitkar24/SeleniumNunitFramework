using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Support.UI;
using System.Collections;
using NUnit.Framework.Constraints;

namespace CSharpSeleniumFramework.Tests
{
    public class SortWebTables
    {
        IWebDriver driver;

        [SetUp]
        public void startBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            //Implicit Wiat in slenium c#
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/seleniumPractise/#/offers";
        }

        [Test]
        public void sortTable()
        {
            ArrayList a = new ArrayList();
            IWebElement optiondropdown = driver.FindElement(By.Id("page-menu"));

            SelectElement select = new SelectElement(optiondropdown);
            select.SelectByValue("20");

            IList<IWebElement> veggies = driver.FindElements(By.XPath("//tr//td[1]"));

            foreach (IWebElement veggie in veggies)
            {
                a.Add(veggie.Text);
            }

            a.Sort();

            driver.FindElement(By.XPath("//th[starts-with(@aria-label,\"Veg/fruit\")]")).Click();

            ArrayList b = new ArrayList();

            IList<IWebElement> sortedVeggies = driver.FindElements(By.XPath("//tr//td[1]"));

            foreach (IWebElement veggie in sortedVeggies)
            {
                b.Add(veggie.Text);
            }


            Assert.AreEqual(a, b);

            driver.Close();

        }
    }
}
