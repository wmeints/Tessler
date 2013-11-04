using InfoSupport.Tessler.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tessler.UITest
{
    [TestClass]
    public static class AssemblyInitializer
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testContext)
        {
            TesslerState.AssemblyInitialize();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            TesslerState.AssemblyCleanup();
        }
    }
}
