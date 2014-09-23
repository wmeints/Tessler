using InfoSupport.Tessler.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InfoSupport.Tessler.UITests.PageObjects;

namespace InfoSupport.Tessler.UITest
{
    [TestClass]
    public static class AssemblyInitializer
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testContext)
        {
            TesslerState.AssemblyInitialize();

            TesslerState.RegisterPageObjectsInAssembly(typeof(AjaxPageObject).Assembly);
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            TesslerState.AssemblyCleanup();
        }
    }
}
