using System;
using System.Collections.Generic;
using InfoSupport.Tessler.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InfoSupport.Tessler.UnitTest.Core.Extensions
{
    [TestClass]
    public class SystemExtensionsTests
    {
        [TestMethod]
        public void ToDateStringTest()
        {
            var expected = "01-01-2012";

            var actual = new DateTime(2012, 01, 01).ToDateString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ForEachTest()
        {
            var list = new List<int>()
            {
                1,
                2,
                3,
                4,
            };

            var result = new List<int>();

            list.ForEach<int>(a => result.Add(a + 10));

            Assert.AreEqual(4, result.Count);

            Assert.AreEqual(11, result[0]);
            Assert.AreEqual(12, result[1]);
            Assert.AreEqual(13, result[2]);
            Assert.AreEqual(14, result[3]);
        }

        [TestMethod]
        public void ConcatTest()
        {
            var list = new List<string>()
            {
                "InfoSupport",
                "Tessler",
                "UnitTest",
            };

            var expected = "InfoSupport, Tessler, UnitTest";

            var actual = list.Concat<string>();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConcatSeperatorTest()
        {
            var list = new List<string>()
            {
                "InfoSupport",
                "Tessler",
                "UnitTest",
            };

            var expected = "InfoSupport.Tessler.UnitTest";

            var actual = list.Concat<string>(".");

            Assert.AreEqual(expected, actual);
        }
    }
}
