using InfoSupport.Tessler.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tessler.UITest
{
    [TestClass]
    public abstract class TestBase
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            TesslerState.TestInitialize(TestContext);
        }

        [TestMethod]
        public virtual void TestCleanup()
        {
            TesslerState.TestCleanup();
        }
    }
}
