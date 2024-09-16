using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSeleniumFramework.pageObjects
{
    public class CheckoutPage
    {
        private IWebDriver driver;
        public CheckoutPage(IWebDriver driver) 
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        private By countryOptions = By.XPath("//div[@class='suggestions']//ul//li//a");

        [FindsBy(How = How.PartialLinkText, Using = "Checkout")]
        private IWebElement CheckoutOption;


        [FindsBy(How = How.CssSelector, Using = ".media-body h4 a")]
        private IList<IWebElement> addedCartProducts;


        [FindsBy(How = How.XPath, Using = "//button[@class='btn btn-success']")]
        private IWebElement CheckoutBtn;

        [FindsBy(How = How.Id, Using = "country")]
        private IWebElement CountryOptionsTxtBox;

        [FindsBy(How = How.XPath, Using = "//div[@class='suggestions']//ul//li//a")]
        private IWebElement CountryItem;

        [FindsBy(How = How.XPath, Using = "//input[@type=\"checkbox\"]")]
        private IWebElement TermsConditionChkBox;

        [FindsBy(How = How.XPath, Using = "//input[@value='Purchase']")]
        private IWebElement PurchaseBtn;

        [FindsBy(How = How.XPath, Using = "//div[starts-with(@class,'alert')]")]
        private IWebElement SucessAlert;


        public void verifyAddedCartProducts(String[] expectedProducts)
        {
            CheckoutOption.Click();

            bool result = addedCartProducts.Any(element => expectedProducts.Contains(element.Text));
            Assert.IsTrue(result);
        }

        public void checkoutProduct(String Country)
        {
            CheckoutBtn.Click();
            CountryOptionsTxtBox.SendKeys(Country);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(countryOptions));

            CountryItem.Click();

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click();", TermsConditionChkBox);

            PurchaseBtn.Click();        

        }

        public void verifyProductPurchesedSuccessfully()
        {
            string successMsg = SucessAlert.Text;

            TestContext.Progress.WriteLine(successMsg);

            Assert.That(successMsg.Contains("Success"));
        }
    }
}
