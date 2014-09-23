// WARNING: This file is defined in Tessler.UITest and copied over to DemoUITests
// Any changes done in the DemoUITests project to this file will therefore be overwritten before each build
using InfoSupport.Tessler.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSupport.Tessler.UITests.PageObjects.JQueryTests
{
    public class NoJQueryPageObject : BasePageObject<NoJQueryPageObject>
    {
        public NoJQueryPageObject WithById(Action<string> action)
        {
            action(JQuery.By("div#by-id").Element().Text);

            return ResolveSelf();
        }

        public NoJQueryPageObject WithByClass(Action<string> action)
        {
            action(JQuery.By("div.by-class").Element().Text);

            return ResolveSelf();
        }
    }
}
