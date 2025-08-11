using _Task__Page_Object_Pattern.Pages;
using OpenQA.Selenium;
using Tests;

namespace _Task__Page_Object_Pattern
{
    [TestFixture]
    public class CareerTests : BaseTest
    {      
        [TestCase("Java", "All Locations")]
        [TestCase("C#", "All Locations")]
        [TestCase("Python", "All Locations")]
        public void ValidateJobSearchByCriteria(string language, string locationName)
        {
            var isWordPresent = indexPage
                .Open()
                .ClickCareersLink()
                .WriteInKeywordsField(language)
                .SelectLocation(locationName)
                .ClickRemoteOption()
                .ClickFindButton()
                .ClickLatestResult()
                .IsWordPresentOnPage(language);

            Assert.That(isWordPresent, Is.True, $"The word '{language}' was not found on the job details page.");
        }
    }
}
