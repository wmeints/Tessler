using InfoSupport.Tessler.Drivers;
using InfoSupport.Tessler.UnitTest.Mock;
using InfoSupport.Tessler.Unity;
using log4net;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace InfoSupport.Tessler.UnitTest.Unity
{
    [TestClass]
    public class CallHandlerTests
    {
        private CallHandler callHandler;

        private StubPageObjectA pageObjectMock;

        private Mock<ITesslerWebDriver> webDriverMock;

        private Mock<IMethodInvocation> methodMock;

        private InvokeHandlerStub invokeHandlerMock;

        private Mock<IMethodReturn> methodReturnMock;

        private Mock<ILog> logMock;

        [TestInitialize]
        public void Initialize()
        {
            callHandler = new CallHandler();

            pageObjectMock = UnityInstance.Resolve<StubPageObjectA>();

            webDriverMock = new Mock<ITesslerWebDriver>() { DefaultValue = DefaultValue.Mock };
            UnityInstance.Instance.RegisterInstance<ITesslerWebDriver>(webDriverMock.Object);

            methodMock = new Mock<IMethodInvocation>();
            methodMock.Setup(m => m.MethodBase).Returns(typeof(PageObjectMock).GetMethod("PageActionWithoutScreenshot"));
            methodMock.Setup(m => m.Target).Returns(pageObjectMock);

            invokeHandlerMock = new InvokeHandlerStub();

            methodReturnMock = new Mock<IMethodReturn>();
            
            logMock = new Mock<ILog>();

            UnityInstance.Instance.RegisterInstance<ILog>(logMock.Object);
        }

        [TestMethod]
        public void TakeScreenshotTest()
        {
            invokeHandlerMock.InvokeHandlerMock.Setup(m => m.InvokeHandler(It.IsAny<IMethodInvocation>(), It.IsAny<GetNextHandlerDelegate>())).Returns(methodReturnMock.Object);

            callHandler.Invoke(methodMock.Object, invokeHandlerMock.GetNextHandlerDelegate);

            Assert.AreEqual(1, pageObjectMock.OnCallingCalls);
            Assert.AreEqual(1, pageObjectMock.OnCalledCalls);

            invokeHandlerMock.InvokeHandlerMock.Verify(m => m.InvokeHandler(It.IsAny<IMethodInvocation>(), It.IsAny<GetNextHandlerDelegate>()), Times.Once());
        }
    }
}
