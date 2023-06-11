using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;

namespace TestProject_UI_tests.Pages
{
    public class MainPage : BasePage
    {
        [FindsBy(How = How.ClassName, Using = "authorization-link")]
        private IWebElement _signInButton;

        [FindsBy(How = How.ClassName, Using = "logged-in")]
        private IWebElement _welcomeMessage;

        [FindsBy(How = How.ClassName, Using = "minicart-wrapper")]
        private IWebElement _cartButton;

        [FindsBy(How = How.Id, Using = "top-cart-btn-checkout")]
        private IWebElement _checkoutButton;

        [FindsBy(How = How.XPath, Using = "//ul[@class='header links']/li[3]")]
        private IWebElement _createAccountFormButton;

        public MainPage(IWebDriver driver) : base(driver)
        {
        }

        public CustomerLoginPage ClickSignInButton()
        {
            _signInButton.Click();

            return new CustomerLoginPage(_driver);
        }

        public CustomerLoginPage ClickCreateAccountButton()
        {
            _createAccountFormButton.Click();

            return new CustomerLoginPage(_driver);
        }



        public void ClickCheckoutButton()
        {
            Actions actions = new Actions(_driver);

            actions.ScrollToElement(_cartButton).Perform();

            _cartButton.Click();

            Thread.Sleep(2000);

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            By messageLocator = By.XPath("//div[@id='ui-id-1']");

            wait.Until(ExpectedConditions.ElementIsVisible(messageLocator));

            _checkoutButton.Click();
        }

        public string GetSignInButtonText()
        {
            return _signInButton.Text;
        }

        public string GetWelcomeMessage()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            wait.Until((_) => _welcomeMessage.Text.StartsWith("Welcome, "));

            return _welcomeMessage.Text;
        }

        
    }
}
