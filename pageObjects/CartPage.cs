using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSeleniumFramework.pageObjects
{
    public class CartPage
    {
        private IWebDriver driver;
        public CartPage(IWebDriver driver) 
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        private By checkoutBtn = By.PartialLinkText("Checkout");
        private By productTitldLbl = By.CssSelector("h4 a");
        private By addBtn =By.CssSelector(".card-footer button");

        [FindsBy(How = How.LinkText, Using = "Shop")]
        private IWebElement shopLink;


        [FindsBy(How = How.TagName, Using = "app-card")]
        private IList<IWebElement> products;

        public void addProductToCart(String[] expectedProducts)
        {
            shopLink.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(checkoutBtn));

            foreach (IWebElement product in products)
            {
                string actualTitle = product.FindElement(productTitldLbl).Text;

                if (expectedProducts.Contains(actualTitle))
                {
                    product.FindElement(addBtn).Click();
                }
            }
        }

    }
}
