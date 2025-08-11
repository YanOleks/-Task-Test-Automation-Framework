using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Business.Pages
{
    public class AboutPage(IWebDriver driver) : BasePage(driver)
    {
        private readonly By glanceSectionBy = By.XPath("//section[contains(., 'Glance')]");
        private readonly By desktopDownloadButtonBy = By.XPath("//span[contains(@class, 'desktop') and contains(text(), 'DOWNLOAD')]");

        public AboutPage ScrollToGlanceSection()
        {
            var glanceSection = driver.FindElement(glanceSectionBy);
            new Actions(driver)
                .ScrollToElement(glanceSection)
                .Perform();
            return this;
        }

        public AboutPage ClickDesktopDownloadButton()
        {
            var desktopDownloadButton = driver.FindElement(desktopDownloadButtonBy);
            desktopDownloadButton.Click();
            return this;
        }
    }
}
