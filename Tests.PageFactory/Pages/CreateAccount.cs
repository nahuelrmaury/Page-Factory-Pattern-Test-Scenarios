using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.IO;
using System.Net.Sockets;
using Tests.PageFactory.Pages;

namespace TestProject_UI_tests.Pages
{
    public class CreateAccount : BasePage
    {
        [FindsBy(How = How.XPath, Using = "//input[@name='firstname']")]
        private IWebElement _firstName;

        [FindsBy(How = How.XPath, Using = "//input[@name='lastname']")]
        private IWebElement _lastName;

        [FindsBy(How = How.XPath, Using = "//input[@name='password']")]
        private IWebElement _password;

        [FindsBy(How = How.XPath, Using = "//input[@name='password_confirmation']")]
        private IWebElement _passwordConfirmation;

        [FindsBy(How = How.XPath, Using = "//button[@title='Create an Account']")]
        private IWebElement _createAccountButton;

        [FindsBy(How = How.XPath, Using = "//div[@id='email_address-error']")]
        private IWebElement _emailErrorMessage;

        


                public CreateAccount(IWebDriver driver) : base(driver)
        {
        }

        public void CreateNewAccount(string firstname, string lastname, string password)
        {
            EnterNames(firstname, lastname);
            EnterPassword(password);
            ClickCreateAccountButton();
        }

        public void EnterNames(string firstname, string lastname)
        {
            _firstName.SendKeys(firstname);
            _lastName.SendKeys(lastname);
        }

        public void EnterPassword(string password)
        {
            _password.SendKeys(password);
            _passwordConfirmation.SendKeys(password);
        }

        public string GetEmailErrorMessage() 
        {
            return _emailErrorMessage.Text;
        }

        public void ClickCreateAccountButton() 
        {
            _createAccountButton.Click();
        }

        
    }
}
