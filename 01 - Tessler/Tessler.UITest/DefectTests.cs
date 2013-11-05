﻿using System;
using System.Linq;
using InfoSupport.Tessler.Core;
using InfoSupport.Tessler.Drivers;
using InfoSupport.Tessler.Selenium;
using InfoSupport.Tessler.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tessler.UITest
{
    [TestClass]
    public class DefectTests : TestBase
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();

            JQuery.By("a:contains('Tables')").Element().Click();
        }

        [TestMethod]
        public void EmptyTableTest()
        {
            var rows = JQuery.By("#empty-table tbody").Elements();

            Assert.AreEqual(0, rows.Count());
        }

        [TestMethod]
        public void OneElementTest()
        {
            var rows = JQuery.By("#one-element").Elements();

            Assert.AreEqual(1, rows.Count());

            var row1 = rows.First();

            Assert.IsTrue(row1.Text.Contains("40"));
            Assert.IsTrue(row1.Text.Contains("First element"));
            Assert.IsTrue(row1.Text.Contains("100,20"));
            Assert.IsTrue(row1.Text.Contains("Yes"));
        }
    }
}