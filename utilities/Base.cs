using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using static AventStack.ExtentReports.Model.Media;
using AventStack.ExtentReports.Model;



namespace CSharpSeleniumFramework.utilities
{
    public class Base
    {
        
        public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        String browserName;

        public ExtentReports extent;
        public ExtentTest test;

        //public IWebDriver driver;
        [OneTimeSetUp]
        public void Setup()
        {

            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string reportPath = projectDirectory + "//index.html";
            var htmlReporter = new ExtentSparkReporter(reportPath);
            extent = new ExtentReports();
            

            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Host Name", "Local host");
            extent.AddSystemInfo("User", "Vinayak");
        }


        [SetUp]
        public void startBrowser()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            //configuration
            browserName = TestContext.Parameters["browserName"];
            if (browserName == null)
            {
                browserName = ConfigurationManager.AppSettings["browser"];
            }
            initBrowser(browserName);

            //Implicit Wiat in slenium c#
            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Value.Manage().Window.Maximize();
            driver.Value.Url = "https://rahulshettyacademy.com/angularpractice/";
        }

        public void initBrowser(String browserName)
        {
            switch (browserName)
            {
                case "Edge":

                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver.Value = new EdgeDriver();
                    break;

                case "Chrome":

                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver.Value = new ChromeDriver();
                    break;

                case "Firefox":

                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver.Value = new FirefoxDriver();
                    break;
            }

            
        }

        public IWebDriver getDriver()
        {
            return driver.Value;
        }

        public static JsonReader getDataParser()
        {
            return new JsonReader();
        }

        [TearDown]
        public void AfterTest()
        {
            var status=TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;
            DateTime time = DateTime.Now;
            String fileName = "Screenshot" + time.ToString("h_mm_ss") + ".png";

            if(status == TestStatus.Failed)
            {
                test.Fail("Test Failed", captureScreenShot(driver.Value, fileName));
                test.Log(Status.Fail,"Test Failed With logtrace"+stackTrace);
            }
            else if(status == TestStatus.Passed)
            {
                 
            }
            driver.Value.Quit();
            extent.Flush();

        }

        public static Media captureScreenShot(IWebDriver driver,String screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot=ts.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();
        }
    }
}
