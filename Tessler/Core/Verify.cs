using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InfoSupport.Tessler.Core
{
    public static class Verify
    {
        private static List<string> fails;

        public static bool Failed { get; set; }

        public static List<string> Fails
        {
            get
            {
                if (fails == null)
                {
                    fails = new List<string>();
                }

                return fails;
            }
        }

        public static string GetFailMessage()
        {
            var message = new StringBuilder();

            foreach (var fail in Fails)
            {
                message.AppendLine(fail);
            }

            return message.ToString();
        }

        public static void AreEqual(string actual, string expected)
        {
            actual = actual.Trim();

            if (expected != actual)
            {
                Fail(actual, expected);
            }
        }

        public static void AreEqual(string actual, DateTime expected)
        {
            actual = actual.Trim();

            if (expected.ToDateString() != actual)
            {
                Fail(actual, expected.ToDateString());
            }
        }

        public static void AreNotEqual(string actual, string expected)
        {
            actual = actual.Trim();

            if (expected == actual)
            {
                Fail(actual, expected, "Actual <{0}> does contain unexpected <{1}>");
            }
        }

        public static void AreNotEqual(string actual, DateTime expected)
        {
            actual = actual.Trim();

            if (expected.ToDateString() == actual)
            {
                Fail(actual, expected.ToDateString(), "Actual <{0}> does contain unexpected <{1}>");
            }
        }

        public static void IsEmpty(string actual)
        {
            actual = actual.Trim();

            if (string.Empty != actual)
            {
                Fail(actual, null, "Actual <{0}> is not empty");
            }
        }

        public static void IsNotEmpty(string actual)
        {
            actual = actual.Trim();

            if (string.Empty == actual)
            {
                Fail(actual, null, "Actual <{0}> is empty");
            }
        }

        public static void IsTrue(bool actual)
        {
            if (actual != true)
                Fail(actual, true);
        }

        public static void IsFalse(bool actual)
        {
            if (actual != false)
                Fail(actual, false);
        }

        public static void Contains(string actual, string text)
        {
            if (!actual.Contains(text))
            {
                Fail(actual, text, "Actual <{0}> does not contain expected <{1}>");
            }
        }

        public static void Contains(List<string> actual, string item)
        {
            foreach (var i in actual)
            {
                if (i.Contains(item))
                {
                    return;
                }
            }

            Fail(actual.Concat(), item, "Actual <{0}> did not contain expected <{1}>");
        }

        public static void NotContains(string actual, string text)
        {
            if (actual.Contains(text))
            {
                Fail(actual, text, "Actual <{0}> does contain unexpected <{1}>");
            }
        }

        public static void NotContains(List<string> actual, string item)
        {
            foreach (var i in actual)
            {
                if (i.Contains(item))
                {
                    Fail(actual.Concat(), item, "Actual <{0}> did contain unexpected <{1}>");
                }
            }
        }

        internal static void Fail(object actual, object expected, string message = "Actual <{0}> does not equal expected <{1}>")
        {
            Fails.Add(string.Format(message, actual, expected));

            Failed = true;
        }
    }
}
