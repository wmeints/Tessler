using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using InfoSupport.Tessler.Adapters.Ajax;
using InfoSupport.Tessler.Core;
using InfoSupport.Tessler.Drivers;
using InfoSupport.Tessler.Selenium;
using InfoSupport.Tessler.UnitTest.Mock;
using InfoSupport.Tessler.Unity;
using log4net;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OpenQA.Selenium;

namespace InfoSupport.Tessler.UnitTest.Drivers
{
    [TestClass]
    public class TesslerWebDriverTests
    {
        private Mock<IWebDriverMock> webDriverMock;
        private Mock<IWebDriverFactory> webDriverFactory;

        private ITesslerWebDriver driver;

        private Mock<ILog> logMock;

        [TestInitialize]
        public void TestInitialize()
        {
            webDriverMock = new Mock<IWebDriverMock>();

            webDriverFactory = new Mock<IWebDriverFactory>() { DefaultValue = DefaultValue.Mock };
            webDriverFactory.Setup(m => m.BuildWebDriver()).Returns(webDriverMock.Object);

            UnityInstance.Instance.RegisterInstance<IWebDriverFactory>(webDriverFactory.Object);

            driver = new TesslerWebDriver();

            logMock = new Mock<ILog>();

            UnityInstance.Instance.RegisterInstance<ILog>(logMock.Object);

            Verify.Fails.Clear();
            Verify.Failed = false;

            TesslerState.Configure().RestoreState();
        }

        [TestMethod]
        public void IsActiveTest()
        {
            Assert.IsTrue(driver.IsActive);

            driver.Close();

            Assert.IsFalse(driver.IsActive);

            webDriverMock.Verify(m => m.Quit(), Times.Once());
        }

        [TestMethod]
        public void CloseTest()
        {
            driver.Close();

            webDriverMock.Verify(m => m.Quit(), Times.Once());
        }

        [TestMethod]
        public void PageSourceTest()
        {
            var expected = "PageSource Mock";

            webDriverMock.Setup(m => m.PageSource).Returns(expected);

            var actual = driver.PageSource;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TitleTest()
        {
            var expected = "Title Mock";

            webDriverMock.Setup(m => m.Title).Returns(expected);

            var actual = driver.Title;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UrlGetTest()
        {
            var expected = "Url Mock";

            webDriverMock.SetupGet(m => m.Url).Returns(expected);

            var actual = driver.Url;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UrlSetTest()
        {
            var url = "Url Set";

            driver.Url = url;

            webDriverMock.VerifySet(m => m.Url = It.Is<string>(a => a == url), Times.Once());
        }

        [TestMethod]
        public void GetScreenshotTest()
        {
            driver.GetScreenshot();

            webDriverMock.Verify(m => m.GetScreenshot(), Times.Once());
        }

        [TestMethod]
        public void ManageTest()
        {
            var manageMock = new Mock<IOptions>();

            webDriverMock.Setup(m => m.Manage()).Returns(manageMock.Object);

            var actual = driver.Manage();

            Assert.AreEqual(manageMock.Object, actual);

            webDriverMock.Verify(m => m.Manage(), Times.Once());
        }

        [TestMethod]
        public void NavigateTest()
        {
            var navigateMock = new Mock<INavigation>();

            webDriverMock.Setup(m => m.Navigate()).Returns(navigateMock.Object);

            var actual = driver.Navigate();

            Assert.AreEqual(navigateMock.Object, actual);

            webDriverMock.Verify(m => m.Navigate(), Times.Once());
        }

        [TestMethod]
        public void WaitTest()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            driver.Wait();

            stopwatch.Stop();

            Assert.IsTrue(stopwatch.ElapsedMilliseconds >= 800);
        }

        [TestMethod]
        public void LoadJQueryTest()
        {
            var jsExec = "return typeof(jQuery)";

            var jsReturn1 = "undefined";
            var jsReturn2 = "function";

            var jsReturn = jsReturn1;

            webDriverMock.Setup(m => m.ExecuteScript(jsExec)).Returns(() => jsReturn).Callback(() => jsReturn = jsReturn2);

            driver.LoadJQuery();

            webDriverMock.Verify(m => m.ExecuteScript(It.Is<string>(a => a.Contains("jquery.min.js"))), Times.Once());
            logMock.Verify(m => m.Info(It.Is<string>(a => a.Contains("jQuery succesfully loaded in"))), Times.Once());
        }

        [TestMethod]
        public void LoadJQueryAlreadyLoadedTest()
        {
            var jsExec = "return typeof(jQuery)";

            var jsReturn = "function";

            webDriverMock.Setup(m => m.ExecuteScript(jsExec)).Returns(() => jsReturn);

            driver.LoadJQuery();
        }

        [TestMethod]
        public void LoadJQueryFailedTest()
        {
            var jsExec = "return typeof(jQuery)";

            var jsReturn = "undefined";

            webDriverMock.Setup(m => m.ExecuteScript(jsExec)).Returns(() => jsReturn);

            var threwException = false;

            try
            {
                driver.LoadJQuery();
            }
            catch (Exception)
            {
                threwException = true;
            }

            Assert.IsTrue(threwException);

            logMock.Verify(m => m.Fatal(It.Is<string>(a => a.Contains("Could not manually load jQuery"))), Times.Once());
        }

        [TestMethod]
        public void LoadJQueryExceptionTest()
        {
            var jsExec = "return typeof(jQuery)";

            webDriverMock.Setup(m => m.ExecuteScript(jsExec)).Throws<Exception>();

            var threwException = false;

            try
            {
                driver.LoadJQuery();
            }
            catch (Exception)
            {
                threwException = true;
            }

            Assert.IsTrue(threwException);

            logMock.Verify(m => m.Fatal(It.Is<string>(a => a.Contains("Error checking jQuery presence"))), Times.Once());
        }

        [TestMethod]
        public void JsTest()
        {
            var js = "Javascript Mock";
            var expected = new { Result = "Result" };

            webDriverMock.Setup(m => m.ExecuteScript(js)).Returns(expected);

            var actual = driver.Js(js);

            Assert.AreEqual(expected, actual);

            webDriverMock.Verify(m => m.ExecuteScript(js), Times.Once());
        }

        [TestMethod]
        public void JsExceptionTest()
        {
            webDriverFactory.Setup(m => m.BuildWebDriver()).Returns(new Mock<IWebDriver>().Object);

            var js = "Javascript Mock";

            driver = new TesslerWebDriver();

            bool threwException = false;

            try
            {
                driver.Js(js);
            }
            catch
            {
                threwException = true;
            }

            Assert.IsTrue(threwException);

            logMock.Verify(m => m.Fatal(It.Is<string>(a => a.Contains("Driver cannot be used to execute javascript"))), Times.Once());
        }

        [TestMethod]
        public void WaitForAjaxTest()
        {
            var jsAdapterMock = new Mock<IJavascriptAdapter>();

            UnityInstance.Instance.RegisterInstance<IJavascriptAdapter>(jsAdapterMock.Object);

            driver.WaitForAjax();

            jsAdapterMock.Verify(m => m.IsActive(driver), Times.Once());
        }

        [TestMethod]
        public void WaitForAjaxTimeoutTest()
        {
            var jsAdapterMock = new Mock<IJavascriptAdapter>();

            jsAdapterMock.Setup(m => m.IsActive(driver)).Returns(true);

            UnityInstance.Instance.RegisterInstance<IJavascriptAdapter>(jsAdapterMock.Object);

            driver.WaitForAjax();

            jsAdapterMock.Verify(m => m.IsActive(driver), Times.AtLeast(10));
        }

        [TestMethod]
        public void WaitForAjaxExceptionTest()
        {
            var jsAdapterMock = new Mock<IJavascriptAdapter>();

            jsAdapterMock.Setup(m => m.IsActive(driver)).Throws<InvalidOperationException>();

            UnityInstance.Instance.RegisterInstance<IJavascriptAdapter>(jsAdapterMock.Object);

            driver.WaitForAjax();

            jsAdapterMock.Verify(m => m.IsActive(driver), Times.Once());

            logMock.Verify(m => m.Warn(It.Is<string>(a => a.Contains("Could not execute action WaitForAjax"))), Times.Once());
        }

        [TestMethod]
        public void FindElementsTest()
        {
            var expected = new List<IWebElement>()
                {
                    new Mock<IWebElement>().Object,
                    new Mock<IWebElement>().Object,
                };

            webDriverMock.Setup(m => m.ExecuteScript(It.IsAny<string>())).Returns(expected);

            var actual = driver.FindElements(JQuery.By(""));
            
            Assert.AreEqual(expected.Count, actual.Count());

            webDriverMock.Verify(m => m.ExecuteScript(It.Is<string>(a => a.Contains("return jQuery"))), Times.Once());
        }

        [TestMethod]
        public void FindElementsNoResultTest()
        {
            webDriverMock.Setup(m => m.ExecuteScript(It.IsAny<string>())).Returns(null);

            var actual = driver.FindElements(JQuery.By(""));

            Assert.AreEqual(0, actual.Count());

            webDriverMock.Verify(m => m.ExecuteScript(It.Is<string>(a => a.Contains("return jQuery"))), Times.Once());
        }

        [TestMethod]
        public void FindElementsExceptionTest()
        {
            webDriverMock.Setup(m => m.ExecuteScript(It.IsAny<string>())).Throws<Exception>();

            var actual = driver.FindElements(JQuery.By(""));

            Assert.AreEqual(0, actual.Count());

            webDriverMock.Verify(m => m.ExecuteScript(It.Is<string>(a => a.Contains("return jQuery"))), Times.Once());
        }

        [TestMethod]
        public void WaitForTest()
        {
            TesslerState.Configure().SetAutoLoadJQuery(false);

            var expected = new List<IWebElement>()
            {
                new Mock<IWebElement>().Object
            };

            webDriverMock.Setup(m => m.ExecuteScript(It.IsAny<string>())).Returns(expected);
            
            var actual = driver.WaitFor(JQuery.By(""));

            Assert.AreEqual(expected.Count, actual.Count());
        }

        [TestMethod]
        public void WaitForNotFoundTest()
        {
            TesslerState.Configure()
                .SetAutoLoadJQuery(false)
            ;

            webDriverMock.Setup(m => m.ExecuteScript(It.IsAny<string>())).Returns(null);

            var actual = driver.WaitFor(JQuery.By(""));

            Assert.AreEqual(0, actual.Count());
        }

        [TestMethod]
        public void IsVisibleTest()
        {
            var expected = new List<IWebElement>()
            {
                new Mock<IWebElement>().Object
            };

            webDriverMock.Setup(m => m.ExecuteScript(It.IsAny<string>())).Returns(expected);

            var actual = driver.IsVisible(JQuery.By(""));

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void IsVisibleNotVisibleTest()
        {
            var expected = new object();

            webDriverMock.Setup(m => m.ExecuteScript(It.IsAny<string>())).Returns(expected);

            var actual = driver.IsVisible(JQuery.By(""));

            Assert.IsFalse(actual);
        }
    }
}
