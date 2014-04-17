using InfoSupport.Tessler.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tessler.UITests.PageObjects.JQueryTests
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
