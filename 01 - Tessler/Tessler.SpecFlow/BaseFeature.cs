using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InfoSupport.Tessler.SpecFlow
{
    /// <summary>
    /// Base class for alle generated test classes
    /// </summary>
    [TestClass]
    public abstract class BaseFeature
    {
        private TestContext testContextInstance;

        /// <summary>
        /// Test context, gets injected by the test runner
        /// </summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }
    }
}
