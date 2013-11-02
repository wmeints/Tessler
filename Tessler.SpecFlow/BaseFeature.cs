using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InfoSupport.Tessler.SpecFlow
{
    [TestClass]
    public abstract class BaseFeature
    {
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }
    }
}
