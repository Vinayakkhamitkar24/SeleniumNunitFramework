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
    public class RegisterPage
    {
        private IWebDriver driver;
        public RegisterPage(IWebDriver driver) 
        { 
            this.driver = driver;
            PageFactory.InitElements(driver,this);
        }
       
        [FindsBy(How = How.XPath, Using = "//div[@class='form-group']//input[@name='name']")]
        private IWebElement username;

        [FindsBy(How = How.CssSelector, Using = "input[name = 'email']")]
        private IWebElement email;

        [FindsBy(How = How.Id, Using = "exampleInputPassword1")]
        private IWebElement password;

        [FindsBy(How = How.CssSelector, Using = "input[id='exampleCheck1']")]
        private IWebElement checkBox;

        [FindsBy(How = How.Id, Using = "exampleFormControlSelect1")]
        private IWebElement selectOptions;

        [FindsBy(How = How.XPath, Using = "//div[@class='form-group']//div//input")]
        private IList<IWebElement> radio;

        [FindsBy(How = How.Name, Using = "bday")]
        private IWebElement birthDate;

        [FindsBy(How = How.CssSelector, Using = "input[value='Submit']")]
        private IWebElement submitBtn;

        [FindsBy(How = How.XPath, Using = "//div[starts-with(@class,'alert')]")]
        private IWebElement sucessAlert;

        public void registerUser(String user ,String pass , String birtDate, String emailId ,String gender)
        {
            username.SendKeys(user);
            email.SendKeys(emailId);
            password.SendKeys(pass);

            if (!checkBox.Selected)
            {
                checkBox.Click();
            }

            SelectElement option = new SelectElement(selectOptions);
            option.SelectByText(gender);

            Random rnd = new Random();
            int rndIndex = rnd.Next(radio.Count());

            radio[rndIndex].Click();

            birthDate.SendKeys(birtDate);
            submitBtn.Click();
        }

        public void verifyUserRegisteredSuccessfully()
        {
            //Explicit Wait in Selenium c#

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[starts-with(@class,'alert')]")));

            String successMsg = sucessAlert.Text;

            var expectedMsg = "×\r\nSuccess! The Form has been submitted successfully!.";

            Assert.AreEqual(expectedMsg, successMsg);

        }
       
    }
}
