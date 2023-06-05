using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace TestProject_UI_tests.Pages
{
    public class CustomerLoginPage: BasePage
    {
        private By _emailInputLocator = By.Name("login[username]");

        private By _passwordInputLocator = By.Name("login[password]");

        private By _signInFormButtonLocator = By.Id("send2");

        public CustomerLoginPage(IWebDriver driver): base(driver)
        {
        }

        public void Login(string email, string password)
        {
            EnterEmail(email);
            EnterPassword(password);
            ClickSignInButton();
        }

        public void EnterEmail(string value)
        {
            var element = _driver.FindElement(_emailInputLocator);

            element.SendKeys(value);
        }

        public void EnterPassword(string value)
        {
            var element = _driver.FindElement(_passwordInputLocator);

            element.SendKeys(value);
        }

        public void ClickSignInButton()
        {
            var element = _driver.FindElement(_signInFormButtonLocator);

            element.Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            wait.Until((driver) => !driver.Title.StartsWith("Customer Login "));
        }
    }
}
