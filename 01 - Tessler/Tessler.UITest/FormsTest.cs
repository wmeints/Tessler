using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InfoSupport.Tessler.Core;

namespace Tessler.UITest
{
    [TestClass]
    public class FormsTest : TestBase
    {
        [TestMethod]
        public void SimpleForm()
        {
            var email = "marco@flyingpie.nl";
            var name = "Marco vd Oever";

            TesslerToys
                .Navigation()
                    .ChooseForms()

                .WithSimpleFormTable(email)
                    .CheckVisibility(v => v.WithEmail, false)
                    .Then()

                .EnterEmailAddress(email)
                .EnterName(name)
                .ClickSubmit()

                .WithSimpleFormTable(email)
                    .CheckVisibility(v => v.WithEmail, true)
            ;
        }
    }
}
