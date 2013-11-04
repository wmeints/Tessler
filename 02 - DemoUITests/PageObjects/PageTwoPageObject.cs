using System;
using InfoSupport.Tessler.Core;
using InfoSupport.Tessler.Selenium;

namespace PageObjects
{
    /// <summary>
    /// Dit page object stelt de tweede pagina voor
    /// </summary>
    public class PageTwoPageObject : TesslerObject<PageTwoPageObject>
    {
        public HomePageObject KiesHome()
        {
            JQuery.By("li:contains('Home') > a").Element().Click();

            return Resolve<HomePageObject>();
        }

        public PageTwoPageObject MetTitel(Action<string> action)
        {
            action(JQuery.By("h1").Element().Text);

            return ResolveSelf();
        }
    }
}
