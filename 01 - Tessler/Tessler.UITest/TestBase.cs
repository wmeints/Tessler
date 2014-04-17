using InfoSupport.Tessler.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tessler.UITests.PageObjects;
using InfoSupport.Tessler.Unity;
using InfoSupport.Tessler.Drivers;

namespace Tessler.UITest
{
    [TestClass]
    public abstract class TestBase
    {
        public HomePageObject TesslerToys { get; set; }
        public TestContext TestContext { get; set; }
        public ITesslerWebDriver Driver { get; set; }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            TesslerState.TestInitialize(TestContext);

            TesslerToys = UnityInstance.Resolve<HomePageObject>();
            Driver = TesslerState.GetWebDriver();
        }

        [TestMethod]
        public virtual void TestCleanup()
        {
            TesslerState.TestCleanup();
        }
    }
}
