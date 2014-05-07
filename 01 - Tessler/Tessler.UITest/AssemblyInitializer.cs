﻿using InfoSupport.Tessler.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tessler.UITests.PageObjects;

namespace Tessler.UITest
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
