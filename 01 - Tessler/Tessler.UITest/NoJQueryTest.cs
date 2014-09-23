using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InfoSupport.Tessler.Core;

namespace InfoSupport.Tessler.UITest
{
    [TestClass]
    public class NoJQueryTest : TestBase
    {
        [TestMethod]
        public void NoJQueryAvailableTest()
        {
            TesslerState.Configure()
                .SetAutoLoadJQuery(false)
            ;

            TesslerToys
                .Navigation()
                    .ChooseJQuery()

                .ChooseNoJQuery()
            ;

            Assert.IsFalse(Driver.IsJQueryLoaded);
        }

        [TestMethod]
        public void AutoLoadedJQueryTest()
        {
            TesslerState.Configure()
                .SetAutoLoadJQuery(true) // Should be default, but for test reasons we define it explicitly
            ;

            TesslerToys
                .Navigation()
                    .ChooseJQuery()

                .ChooseNoJQuery()
            ;

            TesslerState.GetWebDriver().LoadJQuery(); // Explicitly load jQuery

            Assert.IsTrue(Driver.IsJQueryLoaded);
        }

        [TestMethod]
        public void NoJQueryLoadedTest()
        {
            TesslerToys
                .Navigation()
                    .ChooseJQuery()

                .ChooseNoJQuery()

                .WithById(a => a.AssertEqual("This div has the id 'by-id'."))
                .WithByClass(a => a.AssertEqual("This div has the class 'by-class'."))
            ;
        }
    }
}
