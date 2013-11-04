using System;
using System.Collections.Generic;
using InfoSupport.Tessler.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InfoSupport.Tessler.UnitTest.Core
{
    [TestClass]
    public class VerifyTests
    {
        private List<string> listOfStrings;

        [TestInitialize]
        public void TestInitialize()
        {
            listOfStrings = new List<string>()
            {
                "InfoSupport",
                "C2Wmo",
                "Tessler",
                "UnitTest",
                "Core",
                "VerifyTests",
            };

            Verify.Failed = false;
        }

        #region AreEqual

        [TestMethod]
        public void AreEqualStringTest()
        {
            Verify.AreEqual("Verify.AreEqual", "Verify.AreEqual");

            Assert.IsFalse(Verify.Failed);
        }

        [TestMethod]
        public void AreEqualStringFailTest()
        {
            Verify.AreEqual("Verify.AreEqual", "Not equal");

            Assert.IsTrue(Verify.Failed);
        }

        [TestMethod]
        public void AreEqualDateTimeTest()
        {
            Verify.AreEqual("01-01-2012", new DateTime(2012, 01, 01));

            Assert.IsFalse(Verify.Failed);
        }

        [TestMethod]
        public void AreEqualDateTimeFailTest()
        {
            Verify.AreEqual("12-12-2013", new DateTime(2012, 01, 01));

            Assert.IsTrue(Verify.Failed);
        }

        #endregion

        #region AreNotEqual

        [TestMethod]
        public void AreNotEqualStringTest()
        {
            Verify.AreNotEqual("Verify.AreNotEqual", "Not equal");

            Assert.IsFalse(Verify.Failed);
        }

        [TestMethod]
        public void AreNotEqualDateTimeTest()
        {
            Verify.AreNotEqual("12-12-2013", new DateTime(2012, 01, 01));

            Assert.IsFalse(Verify.Failed);
        }

        [TestMethod]
        public void AreNotEqualStringFailTest()
        {
            Verify.AreNotEqual("Verify.AreNotEqual", "Verify.AreNotEqual");

            Assert.IsTrue(Verify.Failed);
        }

        [TestMethod]
        public void AreNotEqualDateTimeFailTest()
        {
            Verify.AreNotEqual("01-01-2012", new DateTime(2012, 01, 01));

            Assert.IsTrue(Verify.Failed);
        }

        #endregion

        #region IsEmpty

        [TestMethod]
        public void IsEmptyTest()
        {
            Verify.IsEmpty("");

            Assert.IsFalse(Verify.Failed);
        }

        [TestMethod]
        public void IsEmptyFailTest()
        {
            Verify.IsEmpty("Not empty");

            Assert.IsTrue(Verify.Failed);
        }

        #endregion

        #region IsNotEmpty

        [TestMethod]
        public void IsNotEmptyTest()
        {
            Verify.IsNotEmpty("Not empty");

            Assert.IsFalse(Verify.Failed);
        }

        [TestMethod]
        public void IsNotEmptyFailTest()
        {
            Verify.IsNotEmpty("");

            Assert.IsTrue(Verify.Failed);
        }

        #endregion

        #region IsTrue

        [TestMethod]
        public void IsTrueTest()
        {
            Verify.IsTrue(true);

            Assert.IsFalse(Verify.Failed);
        }

        [TestMethod]
        public void IsTrueFailTest()
        {
            Verify.IsTrue(false);

            Assert.IsTrue(Verify.Failed);
        }

        #endregion

        #region IsFalse

        [TestMethod]
        public void IsFalseTest()
        {
            Verify.IsFalse(false);

            Assert.IsFalse(Verify.Failed);
        }

        [TestMethod]
        public void IsFalseFailTest()
        {
            Verify.IsFalse(true);

            Assert.IsTrue(Verify.Failed);
        }

        #endregion

        #region Contains

        [TestMethod]
        public void ContainsTest()
        {
            Verify.Contains("Verify.Contains", "Contains");

            Assert.IsFalse(Verify.Failed);
        }

        [TestMethod]
        public void ContainsFailTest()
        {
            Verify.Contains("Verify.Contains", "Not in string");

            Assert.IsTrue(Verify.Failed);
        }

        [TestMethod]
        public void ContainsListTest()
        {
            Verify.Contains(listOfStrings, "UnitTest");

            Assert.IsFalse(Verify.Failed);
        }

        [TestMethod]
        public void ContainsListFailTest()
        {
            Verify.Contains(listOfStrings, "Not in list");

            Assert.IsTrue(Verify.Failed);
        }

        #endregion

        #region NotContains

        [TestMethod]
        public void NotContainsTest()
        {
            Verify.NotContains("Verify.NotContainsTest", "Not in string");

            Assert.IsFalse(Verify.Failed);
        }

        [TestMethod]
        public void NotContainsFailTest()
        {
            Verify.NotContains("Verify.NotContainsTest", "NotContainsTest");

            Assert.IsTrue(Verify.Failed);
        }

        [TestMethod]
        public void NotContainsListTest()
        {
            Verify.NotContains(listOfStrings, "Not in list");

            Assert.IsFalse(Verify.Failed);
        }

        [TestMethod]
        public void NotContainsListFailTest()
        {
            Verify.NotContains(listOfStrings, "UnitTest");

            Assert.IsTrue(Verify.Failed);
        }

        #endregion
    }
}
