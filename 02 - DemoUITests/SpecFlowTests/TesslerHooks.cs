using InfoSupport.Tessler.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecFlowTests
{
    [TestClass]
    public class TesslerHooks
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
