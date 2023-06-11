using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace TestProject_UI_tests.Pages
{
    public class ProductPage : BasePage
    {
        [FindsBy(How = How.ClassName, Using = "product-item-info")]
        private IList<IWebElement> _productInfoElementCollection;

        [FindsBy(How = How.ClassName, Using = "messages")]
        private IWebElement _alertMessage;

        [FindsBy(How = How.XPath, Using = "(//li[@class='item product product-item'])[6]/div/a")]
        private IWebElement _clickOnProduct;

        [FindsBy(How = How.XPath, Using = "//button[@id='product-addtocart-button']")]
        private IWebElement _clickOnAddToCart;

        [FindsBy(How = How.XPath, Using = "//span[@class='counter-number']")]
        private IWebElement _counterNumber;

        private By _productInfoNames = By.ClassName("product-item-link");

        private By _toCartButton = By.ClassName("tocart");

        public ProductPage(IWebDriver driver) : base(driver)
        {
        }

        public void ScrollToProducts()
        {
            Actions actions = new Actions(_driver);
            actions.ScrollToElement(_productInfoElementCollection.First());
            actions.Perform();
        }

        public IEnumerable<string> GetProductInfoNames()
        {
            IEnumerable<IWebElement> productInfoNames = _productInfoElementCollection
                .Select(i => i.FindElement(_productInfoNames));

            IEnumerable<string> actual = productInfoNames.Select(i => i.Text);
            return actual;
        }

        public void AddProductToCart(int index)
        {
            IWebElement targetProduct = _productInfoElementCollection.ElementAt(index);

            Actions actions = new Actions(_driver);

            actions.MoveToElement(targetProduct).Perform();

            IWebElement productAddToCartButton = targetProduct.FindElement(_toCartButton);

            productAddToCartButton.Click();
        }

        public string AddThirdProductCart()
        {
            IWebElement targetProduct = _productInfoElementCollection.ElementAt(2);

            Actions actions = new Actions(_driver);

            actions.MoveToElement(targetProduct).Perform();

            _clickOnProduct.Click();

            _clickOnAddToCart.Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("message-success")));

            return _counterNumber.Text;
        }

        public string InitialCartCounter()
        {
            Thread.Sleep(2000);

            return string.IsNullOrEmpty(_counterNumber.Text) ? "0" : _counterNumber.Text;
        }


        public void AddSpecificProductToCart(string productName)
        {
            IWebElement targetProduct = _productInfoElementCollection.FirstOrDefault(e => e.Text.Contains(productName));

            if (targetProduct != null)
            {
                ScrollToElement(targetProduct);

                IWebElement productAddToCartButton = targetProduct.FindElement(_toCartButton);

                productAddToCartButton.Click();

                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

                By messageLocator = By.XPath("//div[@class='message-success success message']");

                wait.Until(ExpectedConditions.ElementIsVisible(messageLocator));
            }
            else
            {
                throw new NoSuchElementException("Product not found");
            }
        }

        private void ScrollToElement(IWebElement element)
        {
            Actions actions = new Actions(_driver);
            actions.MoveToElement(element);
            actions.Perform();
        }

        public string GetAlertMessage()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            wait.Until((_) => _alertMessage.Text.StartsWith("You added "));

            IWebElement alert = _alertMessage;

            return alert.Text;
        }


    }
}
