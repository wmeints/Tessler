using System;
using System.Collections.Generic;

namespace InfoSupport.Tessler.Core
{
    public static class VerifyExtensions
    {
        public static string VerifyContains(this string actual, string text)
        {
            Verify.Contains(actual, text);

            return actual;
        }

        public static string VerifyNotContains(this string actual, string text)
        {
            Verify.NotContains(actual, text);

            return actual;
        }

        public static string VerifyEqual(this string actual, string expected)
        {
            Verify.AreEqual(actual, expected);

            return actual;
        }

        public static string VerifyNotEqual(this string actual, string expected)
        {
            Verify.AreNotEqual(actual, expected);

            return actual;
        }

        public static string VerifyEqual(this string actual, DateTime expected)
        {
            Verify.AreEqual(actual, expected);

            return actual;
        }

        public static string VerifyNotEqual(this string actual, DateTime expected)
        {
            Verify.AreNotEqual(actual, expected);

            return actual;
        }

        public static string VerifyEmpty(this string actual)
        {
            Verify.IsEmpty(actual);

            return actual;
        }

        public static string VerifyNotEmpty(this string actual)
        {
            Verify.IsNotEmpty(actual);

            return actual;
        }

        public static bool VerifyTrue(this bool actual)
        {
            Verify.IsTrue(actual);

            return actual;
        }

        public static bool VerifyFalse(this bool actual)
        {
            Verify.IsFalse(actual);

            return actual;
        }

        public static List<string> VerifyContains(this List<string> actual, string item)
        {
            Verify.Contains(actual, item);

            return actual;
        }

        public static List<string> VerifyNotContains(this List<string> actual, string item)
        {
            Verify.NotContains(actual, item);

            return actual;
        }
    }
}