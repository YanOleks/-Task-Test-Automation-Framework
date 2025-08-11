using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Business.Pages
{
    public class SearchResultPage(IWebDriver driver) : BasePage(driver)
    {
        private readonly By searchResultLinksBy = By.CssSelector(".search-results__items a");

        public IEnumerable<IWebElement> GetSearchResultLinks()
        {
            var searchResultLinksWait = new WebDriverWait(driver, defaultTimeout);
            var searchResultLinks = searchResultLinksWait.Until(d => {
                var elements = d.FindElements(searchResultLinksBy);
                return elements.Count > 0 ? elements : null;
            });
            if (searchResultLinks.Count == 0)
            {
                throw new NoSuchElementException("No search result links found.");
            }
            return searchResultLinks;
        }
    }
}
