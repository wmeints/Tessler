using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InfoSupport.Tessler.Selenium;
using InfoSupport.Tessler.Core;

namespace NuGetTest
{
    [TestClass]
    public class ExampleTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Example()
        {
            JQuery.By("input.subnav-search-input").Element().ClearAndSendKeys("Tessler");

            JQuery.By("a:contains('{0}')", "Tessler").Element().Click();

            var repoDescription = JQuery.By("div.repository-description").Element().Text;

            Assert.AreEqual("UI Testing Framework", repoDescription);
        }

        [TestInitialize]
        public void Initialize()
        {
            TesslerState.TestInitialize(TestContext);
        }

        [TestCleanup]
        public void Cleanup()
        {
            TesslerState.TestCleanup();
        }
    }
}
