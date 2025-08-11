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
    public class CareersPage(IWebDriver driver) : BasePage(driver)
    {
        private readonly By keywordsFieldBy = By.Id("new_form_job_search-keyword");
        private readonly By locationFieldBy = By.ClassName("recruiting-search__location");
        private readonly By remoteOptionBy = By.XPath("//div[@class='job-search__filter-list']/*[1]");
        private readonly By findButtonBy = By.XPath("//form[@id='jobSearchFilterForm']/button");
        private readonly By latestResultBy = By.XPath("//ul[@class='search-result__list']/li[last()]//a[contains(text(), 'View and apply')]");

        public CareersPage WriteInKeywordsField(string keywords)
        {
            var keywordsFieldWait = new WebDriverWait(this.driver, this.defaultTimeout);
            var keywordsField = keywordsFieldWait.Until(d => d.FindElement(keywordsFieldBy));
            keywordsField.SendKeys(keywords);
            return this;
        }

        public CareersPage ClickLocationSelectField()
        {
            var locationFieldWait = new WebDriverWait(this.driver, this.defaultTimeout);
            var locationField = locationFieldWait.Until(d => d.FindElement(locationFieldBy));
            locationField.Click();
            return this;
        }

        public CareersPage SelectLocation(string location)
        {
            ClickLocationSelectField();
            var locationSelectWait = new WebDriverWait(this.driver, this.defaultTimeout);
            var locationSelect = locationSelectWait.Until(d => d.FindElement(By.XPath($"//li[contains(text(), '{location}')]")));
            locationSelect.Click();
            return this;
        }

        public CareersPage ClickRemoteOption()
        {
            var remoteOptionWait = new WebDriverWait(this.driver, this.defaultTimeout);
            var remoteOption = remoteOptionWait.Until(d => d.FindElement(remoteOptionBy));
            remoteOption.Click();
            return this;
        }

        public CareersPage ClickFindButton()
        {
            var findButtonWait = new WebDriverWait(this.driver, this.defaultTimeout);
            var findButton = findButtonWait.Until(d => d.FindElement(findButtonBy));
            findButton.Click();
            return this;
        }

        public JobDetailsPage ClickLatestResult()
        {
            var latestResultWait = new WebDriverWait(this.driver, this.defaultTimeout);
            var latestResult = latestResultWait.Until(d => d.FindElement(latestResultBy));
            latestResult.Click();
            return new JobDetailsPage(driver);
        }
    }
}
