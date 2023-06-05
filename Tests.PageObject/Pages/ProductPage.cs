using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestProject_UI_tests.Pages
{
    public class ProductPage : BasePage
    {
        private By _productInfoElementLocator = By.ClassName("product-item-info");

        private By _alertMessageLocator = By.ClassName("messages");

        private By _productInfoNames = By.ClassName("product-item-link");

        private By _toCartButton = By.ClassName("tocart");

        public ProductPage(IWebDriver driver) : base(driver)
        {
        }

        public void ScrollToProducts()
        {
            var elements = _driver.FindElements(_productInfoElementLocator);

            Actions actions = new Actions(_driver);
            actions.ScrollToElement(elements.First());
            actions.Perform();
        }

        public IEnumerable<string> GetProductInfoNames()
        {
            var elements = _driver.FindElements(_productInfoElementLocator);

            IEnumerable<IWebElement> productInfoNames = elements
                .Select(i => i.FindElement(_productInfoNames));

            IEnumerable<string> actual = productInfoNames.Select(i => i.Text);
            return actual;
        }

        public void AddFirstProductToCart()
        {
            var elements = _driver.FindElements(_productInfoElementLocator);
            IWebElement targetProduct = elements.First();

            Actions actions = new Actions(_driver);
            actions.MoveToElement(targetProduct);
            actions.Perform();

            IWebElement productAddToCartButton = targetProduct.FindElement(_toCartButton);

            productAddToCartButton.Click();
        }

        public string GetAlertMessage()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            wait.Until((driver) => driver.FindElement(_alertMessageLocator).Text.StartsWith("You added "));

            IWebElement alert = _driver.FindElement(_alertMessageLocator);

            return alert.Text;
        }
    }
}
