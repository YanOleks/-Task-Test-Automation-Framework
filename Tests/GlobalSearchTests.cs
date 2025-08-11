using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests;

namespace _Task__Page_Object_Pattern
{
    [TestFixture]
    public class GlobalSearchTests : BaseTest
    {
        [TestCase("BLOCKCHAIN")]
        [TestCase("Cloud")]
        [TestCase("Automation")]
        public void GlobalSearch_ShouldReturnExpectedResults (string searchTerm)
        {
            var links = indexPage
                .Open()
                .ClickSearchButton()
                .InputInSearchField(searchTerm)
                .ClickFindButton()
                .GetSearchResultLinks();

            var isTermPresent = links.All(link => link.Text.ToLowerInvariant().Contains(searchTerm.ToLowerInvariant()));
            Assert.That(isTermPresent, Is.True, $"The term '{searchTerm}' was not found in the search results.");
        }  
    }
}
