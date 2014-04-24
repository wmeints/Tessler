using InfoSupport.Tessler.Core;
using InfoSupport.Tessler.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tessler.UITests.PageObjects;

namespace TesslerToysUITests
{
    /// <summary>
    /// In deze klasse worden de TestInitialize en TestCleanup methodes van het framework aangeroepen,
    /// bovendien wordt het eerste page object klaar gezet voor de tests.
    /// </summary>
    [TestClass]
    public abstract class TestBase
    {
        public HomePageObject TesslerToys { get; set; }
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            TesslerState.TestInitialize(TestContext);

            TesslerToys = UnityInstance.Resolve<HomePageObject>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            TesslerState.TestCleanup();
        }
    }
}
