using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InfoSupport.Tessler.Core;

namespace InfoSupport.Tessler.UITest
{
    [TestClass]
    public class TablesTest : TestBase
    {
        [TestMethod]
        public void SimpleTable()
        {
            TesslerToys
                .Navigation()
                    .ChooseTables()

                .WithSimpleTable(2)
                    .WithId(a => a.AssertEqual("2"))
                    .WithName(a => a.AssertEqual("Lego technic truck"))
                    .WithPrice(a => a.AssertEqual("55,00"))
                    .WithInStock(a => a.AssertEqual("23"))
                    .Then()

                .WithSimpleTable(3)
                    .WithId(a => a.AssertEqual("3"))
                    .WithName(a => a.AssertEqual("Cars puzzel"))
                    .WithPrice(a => a.AssertEqual("10,99"))
                    .WithInStock(a => a.AssertEqual("12"))
            ;
        }
    }
}
