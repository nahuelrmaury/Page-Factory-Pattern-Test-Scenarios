using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using TestProject_UI_tests.Pages;

namespace TestProject_UI_tests
{
    public class Tests
    {
        [ThreadStatic]
        private static IWebDriver _driver;

        [ThreadStatic]
        private static MainPage _mainPage;

        [SetUp]
        public void SetUp()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("headless");

            _driver = new ChromeDriver(chromeOptions);

            _driver.Manage().Window.Maximize();

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

            _driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/men.html");

            _mainPage = new MainPage(_driver);
        }

        [Test]
        public void LogoutUser_CheckSignInButtonText_IsSignIn()
        {
            string actual = _mainPage.GetSignInButtonText();

            Assert.AreEqual("Sign In", actual);
        }

        [Test]
        public void MainPage_LoginByValidUser_WelcomeMessageIsCorrect()
        {
            var customerLoginPage = _mainPage.ClickSignInButton();

            customerLoginPage.Login("moderya7@gmail.com", "test_password1");

            string actual = _mainPage.GetWelcomeMessage();

            Assert.AreEqual("Welcome, Serhii Mykhailov!", actual);
        }

        [Test]
        public void ValideUser_OpenGear_ProductListIsCorrect()
        {
            var customerLoginPage = _mainPage.ClickSignInButton();

            customerLoginPage.Login("moderya7@gmail.com", "test_password1");

            var productPage = _mainPage.OpenGearCategoryPage();

            productPage.ScrollToProducts();

            IEnumerable<string> actual = productPage.GetProductInfoNames();
            IEnumerable<string> expected = new[]
            {
                "Fusion Backpack",
                "Push It Messenger Bag",
                "Affirm Water Bottle",
                "Sprite Yoga Companion Kit"
            };

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void LogoutUser_AddProductToCart_AlertIsCorect()
        {
            ProductPage productPage = _mainPage.OpenGearCategoryPage();

            productPage.AddFirstProductToCart();

            var actual = productPage.GetAlertMessage();

            Assert.AreEqual("You added Fusion Backpack to your shopping cart.", actual);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}