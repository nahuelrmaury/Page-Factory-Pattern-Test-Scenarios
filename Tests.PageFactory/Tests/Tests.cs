using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TestProject_UI_tests.Pages;

namespace TestProject_UI_tests
{
    public class Tests
    {
        [ThreadStatic]
        private static IWebDriver _driver;

        [ThreadStatic]
        private static MainPage _mainPage;

        [ThreadStatic]
        private static ShippingPage _shippingPage;

        [ThreadStatic]
        private static CreateAccount _createAccount;

        [SetUp]
        public void SetUp()
        {
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArgument("headless");

            _driver = new ChromeDriver(chromeOptions);

            _driver.Manage().Window.Maximize();

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

            _driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");

            _mainPage = new MainPage(_driver);

            _shippingPage = new ShippingPage(_driver);

            _createAccount = new CreateAccount(_driver);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }

        [Test]
        public void T01_LogoutUser_CheckSignInButtonText_IsSignIn()
        {
            string actual = _mainPage.GetSignInButtonText();

            Assert.AreEqual("Sign In", actual);
        }

        [Test]
        public void T02_MainPage_LoginByValidUser_WelcomeMessageIsCorrect()
        {
            var customerLoginPage = _mainPage.ClickSignInButton();

            customerLoginPage.Login("nahuelrmaury@gmail.com", "Padalsop123");

            string actual = _mainPage.GetWelcomeMessage();

            Assert.AreEqual("Welcome, Nahuel Rodriguez!", actual);
        }

        [Test]
        public void T03_ValidUser_OpenGear_ProductListIsCorrect()
        {
            var customerLoginPage = _mainPage.ClickSignInButton();

            customerLoginPage.Login("nahuelrmaury@gmail.com", "Padalsop123");

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
        public void T04_LogoutUser_AddProductToCart_AlertIsCorect()
        {
            ProductPage productPage = _mainPage.OpenGearCategoryPage();

            productPage.AddFirstProductToCart();

            var actual = productPage.GetAlertMessage();

            Assert.AreEqual("You added Push It Messenger Bag to your shopping cart.", actual);
        }

        [Test]
        public void T05_LoginValidUser_PlaceOrderOfProduct_OrderInfoAndProductInfoAreTheSame()
        {
            var customerLoginPage = _mainPage.ClickSignInButton();

            customerLoginPage.Login("nahuelrmaury@gmail.com", "Padalsop123");

            ProductPage productPage = _mainPage.OpenWatchesCategoryPage();

            productPage.AddSpecificProductToCart("Dash Digital Watch");

            _mainPage.ClickCheckoutButton();

            _shippingPage.FillShippingInfo("Spur Road", "Olympia", "Washington", "12345", "United States");

            var productNameOrdered = _shippingPage.GetProductName();

            var productSubtotal = _shippingPage.GetProductSubtotal();

            var shippingPrice = _shippingPage.GetShippingTotal();

            var grandTotal = _shippingPage.GetGrandTotal();

            Assert.Multiple(() =>
            {
                Assert.AreEqual("Dash Digital Watch", productNameOrdered);
                Assert.AreEqual("$92.00", productSubtotal);
                Assert.AreEqual("$5.00", shippingPrice);
                Assert.AreEqual("$97.00", grandTotal);
            });
        }

        [Test]
        public void T06_CreateAccount_FillAllFieldsExpectEmail_ErrorMessageIsCorrect()
        {
            _mainPage.ClickCreateAccountButton();

            _createAccount.CreateNewAccount("Nahuel", "Rodriguez", "Padalsop123");

            _createAccount.ClickCreateAccountButton();

            string emailErrorMessage = _createAccount.GetEmailErrorMessage();

            Assert.AreEqual("This is a required field.", emailErrorMessage);
        }

        [Test]
        public void T07_LoginValidUser_AddTwoBagsandAnotherOneToCart_NumberOfProductsInCartIsCorrect()
        {
            var customerLoginPage = _mainPage.ClickSignInButton();

            customerLoginPage.Login("nahuelrmaury@gmail.com", "Padalsop123");

            _mainPage.OpenGearCategoryPage();

            ProductPage productBagsPage = _mainPage.ClickBagsButton();

            productBagsPage.AddFirstProductCart();

            productBagsPage.AddSecondProductCart();

            var numberCounter = productBagsPage.AddThirdProductCart();

            Assert.AreEqual("3", numberCounter);


        }

    }
}
