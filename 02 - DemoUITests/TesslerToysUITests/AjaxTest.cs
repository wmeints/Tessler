using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InfoSupport.Tessler.Core;

namespace TesslerToysUITests
{
    [TestClass]
    public class AjaxTest : TestBase
    {
        [TestMethod]
        public void FastAjaxTest()
        {
            TesslerToys
                .Navigation()
                    .ChooseAjax()

                .WithFastAjaxText(a => a.AssertEqual("When you click the button under this text, a new text will be loaded using ajax, instantly."))
                .ClickDoFastAjaxCall()
                .WithFastAjaxText(a => a.AssertEqual("Hurray! It worked!"))
            ;
        }

        [TestMethod]
        public void SlowAjaxTest()
        {
            TesslerToys
                .Navigation()
                    .ChooseAjax()

                .WithSlowAjaxText(a => a.AssertEqual("This ajax call will take some time, like a few seconds."))
                .ClickDoSlowAjaxCall()
                .WithSlowAjaxText(a => a.AssertEqual("Took some time, but it worked again!"))
            ;
        }

        [TestMethod]
        public void VerySlowAjaxTest()
        {
            TesslerToys
                .Navigation()
                    .ChooseAjax()

                .WithVerySlowAjaxCallText(a => a.AssertEqual("When this button is pressed, a really heavy backend operation will be emulated, around 5 seconds."))
                .ClickDoVerySlowAjaxCallText()
                .WithVerySlowAjaxCallText(a => a.AssertEqual("Woah, that was just plain slow!"))
            ;
        }
    }
}
