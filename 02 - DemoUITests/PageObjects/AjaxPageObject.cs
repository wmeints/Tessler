// WARNING: This file is defined in Tessler.UITest and copied over to DemoUITests
// Any changes done in the DemoUITests project to this file will therefore be overwritten before each build
using InfoSupport.Tessler.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tessler.UITests.PageObjects
{
    public class AjaxPageObject : BasePageObject<AjaxPageObject>
    {
        public AjaxPageObject WithFastAjaxText(Action<string> action)
        {
            action(JQuery.By("#fast-ajax-text").Element().Text);

            return ResolveSelf();
        }

        public AjaxPageObject ClickDoFastAjaxCall()
        {
            JQuery.By("#fast-ajax-button").Element().Click();

            return ResolveSelf();
        }

        public AjaxPageObject WithSlowAjaxText(Action<string> action)
        {
            action(JQuery.By("#slow-ajax-text").Element().Text);

            return ResolveSelf();
        }

        public AjaxPageObject ClickDoSlowAjaxCall()
        {
            JQuery.By("#slow-ajax-button").Element().Click();

            return ResolveSelf();
        }

        public AjaxPageObject WithVerySlowAjaxCallText(Action<string> action)
        {
            action(JQuery.By("#very-slow-ajax-text").Element().Text);

            return ResolveSelf();
        }

        public AjaxPageObject ClickDoVerySlowAjaxCallText()
        {
            JQuery.By("#very-slow-ajax-button").Element().Click();

            return ResolveSelf();
        }
    }
}
