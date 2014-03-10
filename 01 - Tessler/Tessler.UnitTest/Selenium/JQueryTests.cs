using System;
using System.Collections.Generic;
using System.Linq;
using InfoSupport.Tessler.Drivers;
using InfoSupport.Tessler.Selenium;
using InfoSupport.Tessler.UnitTest.Mock;
using InfoSupport.Tessler.Unity;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tessler.UnitTest.Selenium
{
    [TestClass]
    public class JQueryTests
    {
        private JQuery jQuery;

        private Mock<ITesslerWebDriver> webDriverMock;

        private List<JQueryElement> elementList;
        private Mock<JQueryElementMock> elementMock;

        [TestInitialize]
        public void TestInitialize()
        {
            jQuery = JQuery.By("div.someClass");

            elementList = new List<JQueryElement>();

            elementMock = new Mock<JQueryElementMock>();

            elementList.Add(elementMock.Object);

            webDriverMock = new Mock<ITesslerWebDriver>();
            webDriverMock.Setup(m => m.WaitFor(jQuery)).Returns(elementList);

            UnityInstance.Instance.RegisterInstance<ITesslerWebDriver>(webDriverMock.Object);
        }

        [TestMethod]
        public void ConstructorTest()
        {
            var jQuery = JQuery.By("div.someClass");

            Assert.AreEqual("(\"div.someClass\")", jQuery.Selector);
        }

        [TestMethod]
        public void FunctionTest()
        {
            var jQueryFind = jQuery.Find("div.someOtherClass");

            Assert.AreEqual("(\"div.someClass\").find(\"div.someOtherClass\")", jQueryFind.Selector);
        }

        #region Element

        [TestMethod]
        public void ElementTest()
        {
            var element = jQuery.Element();

            Assert.IsNotNull(element);
            Assert.AreEqual(elementList.First(), element);

            webDriverMock.Verify(m => m.WaitFor(jQuery), Times.Once());
        }

        [TestMethod]
        public void ElementNoElementsTest()
        {
            var threwException = false;

            try
            {
                webDriverMock.Setup(m => m.WaitFor(jQuery)).Returns(new List<JQueryElement>());

                var element = jQuery.Element();
            }
            catch (AssertFailedException e)
            {
                threwException = true;
                Assert.IsTrue(e.Message.Contains("returned no elements"));
            }

            Assert.IsTrue(threwException);

            webDriverMock.Verify(m => m.WaitFor(jQuery), Times.Once());
        }

        [TestMethod]
        public void ElementMultipleElementsTest()
        {
            var threwException = false;

            try
            {
                elementList.Add(new JQueryElementMock());

                var element = jQuery.Element();
            }
            catch (AssertFailedException e)
            {
                threwException = true;
                Assert.IsTrue(e.Message.Contains("returned multiple elements"));
            }

            Assert.IsTrue(threwException);

            webDriverMock.Verify(m => m.WaitFor(jQuery), Times.Once());
        }

        #endregion

        #region Elements

        [TestMethod]
        public void ElementsTest()
        {
            elementList.Add(new JQueryElementMock());

            var elements = jQuery.Elements();

            Assert.IsNotNull(elements);
            CollectionAssert.AreEqual(elementList, elements.ToList());

            webDriverMock.Verify(m => m.WaitFor(jQuery), Times.Once());
        }

        #endregion

        #region Select

        [TestMethod]
        public void SelectTest()
        {
            var element = jQuery.Select();

            Assert.IsNotNull(element);

            webDriverMock.Verify(m => m.WaitFor(jQuery), Times.Once());
        }

        [TestMethod]
        public void SelectNoElementsTest()
        {
            var threwException = false;

            try
            {
                webDriverMock.Setup(m => m.WaitFor(jQuery)).Returns(new List<JQueryElement>());

                var element = jQuery.Select();
            }
            catch (AssertFailedException e)
            {
                threwException = true;
                Assert.IsTrue(e.Message.Contains("returned no elements"));
            }

            Assert.IsTrue(threwException);

            webDriverMock.Verify(m => m.WaitFor(jQuery), Times.Once());
        }

        [TestMethod]
        public void SelectMultipleElementsTest()
        {
            var threwException = false;

            try
            {
                elementList.Add(new JQueryElementMock());

                var element = jQuery.Select();
            }
            catch (AssertFailedException e)
            {
                threwException = true;
                Assert.IsTrue(e.Message.Contains("returned multiple elements"));
            }

            Assert.IsTrue(threwException);

            webDriverMock.Verify(m => m.WaitFor(jQuery), Times.Once());
        }

        #endregion

        #region Selects

        [TestMethod]
        public void SelectsTest()
        {
            elementList.Add(new JQueryElementMock());

            var elements = jQuery.Selects();

            Assert.IsNotNull(elements);
            Assert.AreEqual(elementList.Count, elements.Count());

            webDriverMock.Verify(m => m.WaitFor(jQuery), Times.Once());
        }

        #endregion

        #region IsVisible

        [TestMethod]
        public void IsVisibleTest()
        {
            webDriverMock.Setup(m => m.IsVisible(jQuery)).Returns(true);

            var isVisible = jQuery.IsVisible();

            Assert.IsTrue(isVisible);

            webDriverMock.Verify(m => m.IsVisible(jQuery), Times.Once());
        }

        #endregion
    }
}