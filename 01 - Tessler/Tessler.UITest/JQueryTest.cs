using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InfoSupport.Tessler.Core;

namespace InfoSupport.Tessler.UITest
{
    [TestClass]
    public class JQueryTest : TestBase
    {
        [TestMethod]
        public void TitleTest()
        {
            TesslerToys
                .WithTitle(a => a.AssertEqual("Tessler Toys"))

                .Navigation()
                    .ChooseJQuery()

                .WithTitle(a => a.AssertEqual("jQuery"))
            ;
        }

        [TestMethod]
        public void DivByIdTest()
        {
            TesslerToys
                .Navigation()
                    .ChooseJQuery()

                .WithDivById(a => a.AssertEqual("This div has the id 'by-id'"))
            ;
        }

        [TestMethod]
        public void DivByClassTest()
        {
            TesslerToys
                .Navigation()
                    .ChooseJQuery()

                .WithSpanByClass(a => a.AssertEqual("This span has the class 'by-class'"))
            ;
        }
    }
}
