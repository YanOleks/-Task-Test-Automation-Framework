using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests;

namespace _Task__Page_Object_Pattern
{
    [TestFixture]
    internal class InsightsTest : BaseTest
    {
        [Test]
        public void ValidateArticleTitleMatchesCarouselTitle()
        {
            var insightsPage = indexPage
                .Open()
                .ClickInsightsLink();

            var title = insightsPage
                .SwipeSlider()
                .SwipeSlider()
                .GetSliderTitle();

            var articleTitle = insightsPage
                .ClickReadMoreLink()
                .GetArticleTitleText();

            Assert.That(articleTitle, Is.EqualTo(title));
        }
    }
}
