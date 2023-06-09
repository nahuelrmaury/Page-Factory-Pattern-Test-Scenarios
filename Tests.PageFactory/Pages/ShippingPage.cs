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
    public class ShippingPage : BasePage
    {
        [FindsBy(How = How.XPath, Using = "//input[@name='street[0]']")]
        private IWebElement _addressField;

        [FindsBy(How = How.XPath, Using = "//input[@name='city']")]
        private IWebElement _cityField;

        [FindsBy(How = How.XPath, Using = "(//select)[1]")]
        private IWebElement _stateField;

        [FindsBy(How = How.XPath, Using = "//input[@name='postcode']")]
        private IWebElement _postalCodeField;

        [FindsBy(How = How.XPath, Using = "(//select)[2]")]
        private IWebElement _countryField;

        [FindsBy(How = How.XPath, Using = "//input[@name='telephone']")]
        private IWebElement _phoneField;

        [FindsBy(How = How.XPath, Using = "(//input[@class='radio'])[2]")]
        private IWebElement _shippingField;

        [FindsBy(How = How.XPath, Using = "//button[@class='button action continue primary']")]
        private IWebElement _continueButton;

        [FindsBy(How = How.XPath, Using = "//button[@class='action primary checkout']")]
        private IWebElement _placeOrderButton;

        [FindsBy(How = How.XPath, Using = "//a[@class='order-number']")]
        private IWebElement _orderNumber;

        [FindsBy(How = How.XPath, Using = "//a[@class='action primary continue']")]
        private IWebElement _continueShopping;

        [FindsBy(How = How.XPath, Using = "//button[@class='action switch']")]
        private IWebElement _myAccountDropdown;

        [FindsBy(How = How.XPath, Using = "//ul[@class='header links']/li[1]/a")]
        private IWebElement _myAccount;

        [FindsBy(How = How.XPath, Using = "//ul[@class='nav items']/li[2]")]
        private IWebElement _myOrders;

        [FindsBy(How = How.XPath, Using = "//button[@class='action action-show-popup']")]
        private IWebElement _newAddressButton;

        [FindsBy(How = How.XPath, Using = "//button[@class='button action continue primary']")]
        private IWebElement _nextButton;

        [FindsBy(How = How.XPath, Using = "//strong[@class='product name product-item-name']")]
        private IWebElement _productName;

        [FindsBy(How = How.XPath, Using = "(//td[@data-th='Subtotal'])[last()]//span")]
        private IWebElement _productSubtotal;

        [FindsBy(How = How.XPath, Using = "//td[@data-th='Shipping & Handling']//span")]
        private IWebElement _shippingTotal;

        [FindsBy(How = How.XPath, Using = "//td[@data-th='Grand Total']//span")]
        private IWebElement _grandTotal;

        [FindsBy(How = How.XPath, Using = "//div[@class='checkout-billing-address']")]
        private IWebElement _interceptClick;

        RandomTelephoneGenerator _randomPhoneGenerator = new RandomTelephoneGenerator();

        public ShippingPage(IWebDriver driver) : base(driver)
        {
        }

        public void FillShippingInfo(string address, string city, string state, string postalCode, string country)
        {
            if (_newAddressButton != null)
            {
                SelectShippingMethod();
                _nextButton.Click();
            }
            else
            {
                _newAddressButton.Click();
                EnterAddress(address);
                EnterCity(city);
                SelectState(state);
                EnterPostalCode(postalCode);
                SelectCountry(country);
                EnterPhoneRandomNumber();
                SelectShippingMethod();
                ClickContinue();
            }
            ClickPlaceOrder();
            ContinueShopping();
        }

        public void EnterAddress(string address)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(_addressField));
            _addressField.Clear();
            _addressField.SendKeys(address);
        }

        public void EnterCity(string city)
        {
            _cityField.SendKeys(city);
        }
        public void SelectState(string state)
        {
            SelectElement select = new SelectElement(_stateField);

            select.SelectByText(state);
        }

        public void EnterPostalCode(string postalCode)
        {
            _postalCodeField.SendKeys(postalCode);
        }

        public void SelectCountry(string country)
        {
            Actions actions = new Actions(_driver);

            actions.ScrollToElement(_countryField);

            actions.Perform();

            SelectElement select = new SelectElement(_countryField);

            select.SelectByText(country);
        }

        public void EnterPhoneRandomNumber()
        {
            SelectElement select = new SelectElement(_stateField);

            string phoneNumber = _randomPhoneGenerator.GeneratePhoneNumber();

            _phoneField.SendKeys(phoneNumber);
        }

        public void SelectShippingMethod()
        {

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            By messageLocator = By.XPath("(//input[@class='radio'])[2]");

            wait.Until(ExpectedConditions.ElementIsVisible(messageLocator));

            Actions actions = new Actions(_driver);

            actions.ScrollToElement(_shippingField);

            actions.Perform();

            _shippingField.Click();
        }

        public void ClickContinue()
        {
            _continueButton.Click();
        }

        public void ClickPlaceOrder()
        {

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            By messageLocator = By.XPath("//div[@class='checkout-billing-address']");

            wait.Until(ExpectedConditions.ElementIsVisible(messageLocator));

            _interceptClick.Click();

            _placeOrderButton.Click();
        }

        public void ContinueShopping()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            By messageLocator = By.XPath("//a[@class='order-number']");

            wait.Until(ExpectedConditions.ElementIsVisible(messageLocator));

            string orderNumber = _orderNumber.Text;

            string orderNumberXPath = $"//td[@class='col id' and text()='{orderNumber}']";

            _continueShopping.Click();

            _myAccountDropdown.Click();

            _myAccount.Click();

            _myOrders.Click();

            IWebElement element = _driver.FindElement(By.XPath(orderNumberXPath));

            IWebElement linkElement = element.FindElement(By.XPath("./following::a[1]"));

            linkElement.Click();
        }

        public string GetProductName()
        {
            return _productName.Text;
        }

        public string GetProductSubtotal()
        {
            return _productSubtotal.Text;
        }

        public string GetShippingTotal()
        {
            return _shippingTotal.Text;
        }

        public string GetGrandTotal()
        {
            return _grandTotal.Text;
        }

    }
}
