// WARNING: This file is defined in Tessler.UITest and copied over to DemoUITests
// Any changes done in the DemoUITests project to this file will therefore be overwritten before each build
using System;
using InfoSupport.Tessler.Core;
using InfoSupport.Tessler.Selenium;
using Tessler.UITests.PageObjects.JQueryTests;

namespace Tessler.UITests.PageObjects
{
    /// <summary>
    /// Dit page object stelt de tweede pagina voor
    /// </summary>
    public class JQueryPageObject : BasePageObject<JQueryPageObject>
    {
        public JQueryPageObject WithDivById(Action<string> action)
        {
            action(JQuery.By("div#by-id").Element().Text);

            return ResolveSelf();
        }

        public JQueryPageObject WithDivByClass(Action<string> action)
        {
            action(JQuery.By("div.by-class").Element().Text);

            return ResolveSelf();
        }

        public JQueryPageObject WithSpanByClass(Action<string> action)
        {
            action(JQuery.By("span.by-class").Element().Text);

            return ResolveSelf();
        }

        public NoJQueryPageObject ChooseNoJQuery()
        {
            JQuery.By("a:contains('No jQuery')").Element().Click();

            return Resolve<NoJQueryPageObject>();
        }
    }
}
