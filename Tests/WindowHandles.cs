using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace CSharpSeleniumFramework.Tests
{
    public class WindowHandles
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
            driver.Url = "https://rahulshettyacademy.com/seleniumPractise/#/";
        }

        [Test]
        public void handleWindow()
        {
            driver.FindElement(By.XPath("//a[contains(@class,'blinkingText')]")).Click();

            string parentWindow = driver.CurrentWindowHandle;

            foreach (string child in driver.WindowHandles)
            {
                if (!parentWindow.Equals(child))
                {
                    driver.SwitchTo().Window(child);
                    TestContext.Progress.WriteLine(driver.Title);
                }
            }
        }

    }
}
