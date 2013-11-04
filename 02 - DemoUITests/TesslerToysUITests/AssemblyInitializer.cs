using InfoSupport.Tessler.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TesslerToysUITests
{
    /// <summary>
    /// AssemblyInitialize en AssemblyCleanup zijn nodig, daar worden zaken als driver deployment en screenshot cleanup gedaan.
    /// </summary>
    [TestClass]
    public class AssemblyInitializer
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
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
