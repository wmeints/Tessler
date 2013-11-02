using InfoSupport.Tessler.Screenshots;
using InfoSupport.Tessler.Unity;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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
    }
}
