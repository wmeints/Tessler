// WARNING: This file is defined in Tessler.UITest and copied over to DemoUITests
// Any changes done in the DemoUITests project to this file will therefore be overwritten before each build
using InfoSupport.Tessler.Core;
using InfoSupport.Tessler.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tessler.UITests.PageObjects
{
    public class BasePageObject<TPageObject> : PageObject<TPageObject> where TPageObject : PageObject<TPageObject>
    {
        public TPageObject WithTitle(Action<string> action)
        {
            action(JQuery.By("h1").Element().Text);

            return ResolveSelf();
        }

        public NavigationChildObject Navigation()
        {
            return Resolve<NavigationChildObject>();
        }

        public class NavigationChildObject : ChildPageObject<NavigationChildObject, TPageObject>
        {
            private JQuery Selector { get { return JQuery.By("ul.nav li"); } }

            public HomePageObject ChooseHome()
            {
                Selector.Children("a:contains('Home')").Element().Click();

                return Resolve<HomePageObject>();
            }

            public JQueryPageObject ChooseJQuery()
            {
                Selector.Children("a:contains('jQuery')").Element().Click();

                return Resolve<JQueryPageObject>();
            }

            public TablesPageObject ChooseTables()
            {
                Selector.Children("a:contains('Tables')").Element().Click();

                return Resolve<TablesPageObject>();
            }

            public AjaxPageObject ChooseAjax()
            {
                Selector.Children("a:contains('Ajax')").Element().Click();

                return Resolve<AjaxPageObject>();
            }

            public PopupsPageObject ChoosePopups()
            {
                Selector.Children("a:contains('Popups')").Element().Click();

                return Resolve<PopupsPageObject>();
            }

            public FormsPageObject ChooseForms()
            {
                Selector.Children("a:contains('Forms')").Element().Click();

                return Resolve<FormsPageObject>();
            }

            public SpecialCasesPageObject ChooseSpecialCases()
            {
                Selector.Children("a:contains('Special cases')").Element().Click();

                return Resolve<SpecialCasesPageObject>();
            }

            public WhitePageObject ChooseWhite()
            {
                Selector.Children("a:contains('White')").Element().Click();

                return Resolve<WhitePageObject>();
            }
        }
    }
}
