using InfoSupport.Tessler.Core;
using InfoSupport.Tessler.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InfoSupport.Tessler.UITest
{
    [TestClass]
    public class ExampleTest : TestBase
    {
        /// <summary>
        /// This test is provided with the NuGet package as a basic example.
        /// It can be run straight out of the box, so we'll check in the build if it actually works.
        /// </summary>
        [TestMethod]
        public void NuGetExampleTest()
        {
            TesslerState.GetWebDriver().Url = "https://github.com/infosupport";

            JQuery.By("input.subnav-search-input").Element().ClearAndSendKeys("Tessler");

            JQuery.By("a:contains('{0}')", "Tessler").Element().Click();

            var repoDescription = JQuery.By("div.repository-description").Element().Text;

            Assert.AreEqual("UI Testing Framework", repoDescription);
        }
    }
}
