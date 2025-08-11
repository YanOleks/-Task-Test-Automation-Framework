using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Business.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace _Task__Page_Object_Pattern.Pages
{
    public class InsightsPage(IWebDriver driver) : BasePage(driver)
    {
        private readonly By sliderBy = By.XPath("//div[@class='owl-item active']");
        private readonly By rightButtonBy = By.CssSelector("button.slider__right-arrow");
        private readonly By sliderTitleBy = By.XPath("//div[@class='owl-item active']//span");
        private readonly By readMoreLinkBy = By.PartialLinkText("Read More");
        private readonly By articleTitleBy = By.CssSelector("div.header_and_download");

        public InsightsPage SwipeSlider()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("document.querySelector('#onetrust-banner-sdk')?.remove();");
            var rightButtonWait = new WebDriverWait(this.driver, this.defaultTimeout);
            var rightButton = rightButtonWait.Until(d => d.FindElement(rightButtonBy));
            rightButton.Click();
            Thread.Sleep(2000);
            //var sliderWait = new WebDriverWait(this.driver, this.defaultTimeout);
            //var slider = sliderWait.Until(d => d.FindElement(sliderBy));            
            //var actions = new OpenQA.Selenium.Interactions.Actions(driver);
            //actions
            //   .ClickAndHold(slider)
            //   .MoveByOffset(200, 0) 
            //   .Release()
            //   .Perform();
            //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].style.border='3px solid red'", slider);            
            return this;
        }

        public string GetSliderTitle()
        {
            var sliderTitleWait = new WebDriverWait(this.driver, this.defaultTimeout);
            var sliderTitle = sliderTitleWait.Until(d => d.FindElement(sliderTitleBy));
            string title = sliderTitle.Text.Replace("\n", " ").Trim();
            return title;
        }

        public InsightsPage ClickReadMoreLink()
        {
            var readMoreLinkWait = new WebDriverWait(this.driver, this.defaultTimeout);
            var readMoreLink = readMoreLinkWait.Until(d => d.FindElement(readMoreLinkBy));
            readMoreLink.Click();
            return this;
        }

        public string GetArticleTitleText()
        {
            var articleTitleWait = new WebDriverWait(this.driver, this.defaultTimeout);
            var articleTitle = articleTitleWait.Until(d => d.FindElement(articleTitleBy));
            string title = articleTitle.Text.Replace("\n", " ").Trim();
            return title;
        }
    }
}
