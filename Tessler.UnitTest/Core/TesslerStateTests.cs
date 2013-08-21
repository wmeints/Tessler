using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoSupport.Tessler.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using InfoSupport.Tessler.Screenshots;
using Moq;
using InfoSupport.Tessler.Core;

namespace InfoSupport.Tessler.UnitTest.Core
{
    [TestClass]
    public class TesslerStateTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            var screenshotManagerMock = new Mock<IScreenshotManager>();

            UnityInstance.Instance.RegisterInstance<IScreenshotManager>(screenshotManagerMock.Object);
        }

        //[TestMethod]
        //public void TestInitializeTest()
        //{
        //    var testContextMock = new Mock<TestContext>();
        //    testContextMock.SetupGet(m => m.FullyQualifiedTestClassName).Returns("FullyQualifiedTestClassName");

        //    TesslerState.TestInitialize(testContextMock.Object);
        //}
    }
}
