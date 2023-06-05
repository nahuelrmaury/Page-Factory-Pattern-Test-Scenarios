﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;

namespace TestProject_UI_tests.Pages
{
    public class MainPage : BasePage
    {
        [FindsBy(How = How.ClassName, Using = "authorization-link")]
        private IWebElement _signInButton;

        [FindsBy(How = How.ClassName, Using = "logged-in")]
        private IWebElement _welcomeMessage;

        public MainPage(IWebDriver driver) : base(driver)
        {
        }

        public CustomerLoginPage ClickSignInButton()
        {
            _signInButton.Click();

            return new CustomerLoginPage(_driver);
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
