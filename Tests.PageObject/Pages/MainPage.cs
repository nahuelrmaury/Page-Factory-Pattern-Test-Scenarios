using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace TestProject_UI_tests.Pages
{
    public class MainPage : BasePage
    {
        private By _signInButtonLocator = By.ClassName("authorization-link");

        private By _welcomeMessageLocator = By.ClassName("logged-in");

        public MainPage(IWebDriver driver) : base(driver)
        {
        }

        public CustomerLoginPage ClickSignInButton()
        {
            var element = _driver.FindElement(_signInButtonLocator);
            element.Click();

            return new CustomerLoginPage(_driver);
        }

        public string GetSignInButtonText()
        {
            var element = _driver.FindElement(_signInButtonLocator);

            return element.Text;
        }

        public string GetWelcomeMessage()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            wait.Until((driver) => driver.FindElement(_welcomeMessageLocator).Text.StartsWith("Welcome, "));

            return _driver.FindElement(_welcomeMessageLocator).Text;
        }
    }
}
