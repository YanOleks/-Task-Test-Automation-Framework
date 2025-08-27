using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using OpenQA.Selenium;

namespace Tests
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

            var isTermPresent = links.All(link => link.Text.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase));
            Assert.That(isTermPresent, Is.True, $"The term '{searchTerm}' was not found in the search results.");
        }  
    }
}
