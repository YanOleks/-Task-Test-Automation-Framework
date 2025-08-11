using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace _Task__Page_Object_Pattern.Pages
{
    public class IndexPage(IWebDriver driver) : BasePage(driver)
    {
        private const string PageUrl = "https://www.epam.com/";
        private readonly By careersLinkBy = By.LinkText("Careers");
        private readonly By searchButtonBy = By.CssSelector(".header-search__button");
        private readonly By searchFieldBy = By.Id("new_form_search");
        private readonly By findButtonBy = By.CssSelector(".search-results__action-section > button");
        private readonly By aboutLinkBy = By.LinkText("About");
        private readonly By insightsLinkBy = By.LinkText("Insights");

        public IndexPage Open()
        {
            driver.Navigate().GoToUrl(PageUrl);
            return this;
        }

        public CareersPage ClickCareersLink()
        {
            driver.FindElement(careersLinkBy).Click();
            return new CareersPage(driver);
        }

        public IndexPage ClickSearchButton()
        {
            driver.FindElement(searchButtonBy).Click();
            return this;
        }

        public IndexPage InputInSearchField(string searchText)
        {
            var searchFieldWait = new WebDriverWait(this.driver, this.defaultTimeout);
            var searchField = searchFieldWait.Until(d => d.FindElement(searchFieldBy));
            searchFieldWait.Until(s => searchField.Enabled && searchField.Displayed);
            searchField.Clear();
            searchField.SendKeys(searchText);
            return this;
        }

        public SearchResultPage ClickFindButton()
        {
            var findButtonWait = new WebDriverWait(this.driver, this.defaultTimeout);
            var findButton = findButtonWait.Until(d => d.FindElement(findButtonBy));
            findButton.Click();
            return new SearchResultPage(driver);
        }

        public SearchResultPage SearchFor(string searchText)
        {
            ClickSearchButton();
            InputInSearchField(searchText);
            return ClickFindButton();
        }

        public AboutPage ClickAboutLink()
        {
            driver.FindElement(aboutLinkBy).Click();
            return new AboutPage(driver);
        }

        public InsightsPage ClickInsightsLink()
        {
            driver.FindElement(insightsLinkBy).Click();
            return new InsightsPage(driver);
        }
    }
}
