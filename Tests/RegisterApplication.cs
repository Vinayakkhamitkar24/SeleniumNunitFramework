using CSharpSeleniumFramework.utilities;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpSeleniumFramework.pageObjects;

namespace CSharpSeleniumFramework.Tests
{
    [Parallelizable(ParallelScope.Children)]
    public class RegisterApplication : Base
    {

        //run all data sets of Test method in parallel - [Parallelizable(ParallelScope.All)]
        //run all test methods in one class parallel -  [Parallelizable(ParallelScope.Children)] //class level
        //run all test files in project parallel -  [Parallelizable(ParallelScope.Self)] // //class level

        //dotnet test .\CSharpSeleniumFramework.csproj  - run all testcases
        //dotnet test .\CSharpSeleniumFramework.csproj --filter TestCategory=Regression^ //to run bases on category

        [Test]
        [TestCaseSource("TestDataConfig"), Category("Smoke")]
        [Parallelizable(ParallelScope.All)]
        public void RegisterIntoApplication(String user, String pass, String birtDate, String emailId, String gender)
        {
            RegisterPage registerPage = new RegisterPage(getDriver());
            registerPage.registerUser(user, pass, birtDate, emailId, gender);
            //registerPage.registerUser("abc", "Dummy@123", "24-03-1997", "abc@gmail.com", "Female");
            registerPage.verifyUserRegisteredSuccessfully();

        }


        [Test, Category("Regression")]
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

        public static IEnumerable<TestCaseData> TestDataConfig()
        {
            yield return new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password"),
                getDataParser().extractData("birthdate") , getDataParser().extractData("emailId"), 
                getDataParser().extractData("gender"));

            yield return new TestCaseData(getDataParser().extractData("usernameD2"), getDataParser().extractData("passwordD2"),
                getDataParser().extractData("birthdateD2"), getDataParser().extractData("emailIdD2"),
                getDataParser().extractData("genderD2"));
        }
    }
}
