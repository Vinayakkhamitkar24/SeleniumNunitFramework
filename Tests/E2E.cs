using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpSeleniumFramework.utilities;
using CSharpSeleniumFramework.pageObjects;
using OpenQA.Selenium.Internal.Logging;

namespace CSharpSeleniumFramework.Tests
{
    public class E2E : Base
    {


        [Test]
        //[TestCaseSource ("AddTestDataConfig")]
        //public void purchaseProduct(String username,String [] expectedProducts)

        //run all data sets of Test method in parallel
        //run all test methods in one class parallel
        //run all test files in project parallel

        public void purchaseProduct()
        {


            string[] expectedProducts = getDataParser().extractDataArray("expectedProducts");
               
            CartPage cartPage = new CartPage(getDriver());
            CheckoutPage checkoutPage = new CheckoutPage(getDriver());
            
            cartPage.addProductToCart(expectedProducts);
            checkoutPage.verifyAddedCartProducts(expectedProducts);
            checkoutPage.checkoutProduct(getDataParser().extractData("country"));
            checkoutPage.verifyProductPurchesedSuccessfully();


        }

        
        /*
         * 
         * We are using this for parametrization
       
         public IEnumerable<TestCaseData> AddTestDataConfig()
        {
            yield return new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password"));
            yield return new TestCaseData("pqr", "lmn");
        }
         

         */

    }
}
