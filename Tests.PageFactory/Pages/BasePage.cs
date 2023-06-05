using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;

namespace TestProject_UI_tests.Pages
{
    public class BasePage
    {
        [FindsBy(How = How.Id, Using = "ui-id-6")]
        private IWebElement _gearCategoryButton;

        protected readonly IWebDriver _driver;

        public BasePage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public ProductPage OpenGearCategoryPage()
        {
            _gearCategoryButton.Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            wait.Until((driver) => driver.Title.StartsWith("Gear"));

            return new ProductPage(_driver);
        }
    }
}
