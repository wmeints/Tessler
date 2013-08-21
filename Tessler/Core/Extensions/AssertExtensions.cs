using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InfoSupport.Tessler.Core
{
    public static class AssertExtensions
    {
        public static string AssertContains(this string actual, string text)
        {
            if (!actual.Contains(text))
            {
                Assert.Fail("Actual <{0}> did not contain expected <{1}>", actual, text);
            }

            return actual;
        }

        public static string AssertNotContains(this string actual, string text)
        {
            if (actual.Contains(text))
            {
                Assert.Fail("Actual <{0}> did contain unexpected <{1}>", actual, text);
            }

            return actual;
        }

        public static string AssertContains(this string actual, DateTime expected)
        {
            return actual.AssertContains(expected.ToDateString());
        }

        public static string AssertNotContains(this string actual, DateTime expected)
        {
            return actual.AssertNotContains(expected.ToDateString());
        }

        public static string AssertEqual(this string actual, string expected)
        {
            Assert.AreEqual(expected.Trim(), actual.Trim());

            return actual;
        }

        public static string AssertNotEqual(this string actual, string expected)
        {
            Assert.AreNotEqual(expected.Trim(), actual.Trim());

            return actual;
        }

        public static string AssertEqual(this string actual, DateTime expected)
        {
            Assert.AreEqual(expected.ToDateString(), actual.Trim());

            return actual;
        }

        public static string AssertNotEqual(this string actual, DateTime expected)
        {
            Assert.AreNotEqual(expected.ToDateString(), actual.Trim());

            return actual;
        }

        public static string AssertEmpty(this string actual)
        {
            Assert.AreEqual(string.Empty, actual.Trim());

            return actual;
        }

        public static string AssertNotEmpty(this string actual)
        {
            Assert.AreNotEqual(string.Empty, actual.Trim());

            return actual;
        }

        public static bool AssertTrue(this bool actual)
        {
            Assert.IsTrue(actual);

            return actual;
        }

        public static bool AssertFalse(this bool actual)
        {
            Assert.IsFalse(actual);

            return actual;
        }

        public static List<string> AssertContains(this List<string> actual, string item)
        {
            foreach (var i in actual)
            {
                if (i.Contains(item))
                {
                    return actual;
                }
            }

            Assert.Fail("Actual <{0}> did not contain expected <{1}>", actual.Concat(), item);

            return actual;
        }

        public static List<string> AssertNotContains(this List<string> actual, string item)
        {
            foreach (var i in actual)
            {
                if (i.Contains(item))
                {
                    Assert.Fail("Actual <{0}> did contain unexpected <{1}>", actual.Concat(), item);
                }
            }

            return actual;
        }
    }
}