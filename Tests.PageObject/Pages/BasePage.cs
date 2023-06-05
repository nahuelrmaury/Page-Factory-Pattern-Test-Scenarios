using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace TestProject_UI_tests.Pages
{
    public class BasePage
    {
        private By _gearCategoryButtonLocator = By.Id("ui-id-6");

        protected readonly IWebDriver _driver;

        public BasePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public ProductPage OpenGearCategoryPage()
        {
            var element = _driver.FindElement(_gearCategoryButtonLocator);
            element.Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            wait.Until((driver) => driver.Title.StartsWith("Gear"));

            return new ProductPage(_driver);
        }
    }
}
