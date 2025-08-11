using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Pages;
using OpenQA.Selenium;

namespace _Task__Page_Object_Pattern.Pages
{
    public class JobDetailsPage(IWebDriver driver) : BasePage(driver)
    {
        public bool IsWordPresentOnPage(string word)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(word);
            var elementsWithWordCount = driver.FindElements(By.XPath($"//*[contains(text(), '{word}')]")).Count;
            return elementsWithWordCount > 0;
        }
    }
}
