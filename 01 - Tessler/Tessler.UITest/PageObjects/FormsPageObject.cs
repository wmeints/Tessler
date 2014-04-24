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
    public class FormsPageObject : BasePageObject<FormsPageObject>
    {
        public FormsPageObject EnterEmailAddress(string email)
        {
            JQuery.By("#simple-email").Element().ClearAndSendKeys(email);

            return ResolveSelf();
        }

        public FormsPageObject EnterName(string name)
        {
            JQuery.By("#simple-name").Element().ClearAndSendKeys(name);

            return ResolveSelf();
        }

        public FormsPageObject ClickSubmit()
        {
            JQuery.By("#btn-simple-form-submit").Element().Click();

            return ResolveSelf();
        }

        public SimpleFormUserScopeObject WithSimpleFormTable(string email)
        {
            var po = Resolve<SimpleFormUserScopeObject>();
            po.Email = email;
            return po;
        }

        public class SimpleFormUserScopeObject : ScopeObject<SimpleFormUserScopeObject, FormsPageObject>
        {
            public string Email { get; set; }

            private JQuery Selector { get { return JQuery.By("#simple-form table tr td:contains('{0}')", Email); } }

            public SimpleFormUserScopeObject WithEmail(Action<string> action)
            {
                action(Selector.Sibling(1).Element().Text);

                return ResolveSelf();
            }

            public SimpleFormUserScopeObject WithName(Action<string> action)
            {
                action(Selector.Sibling(2).Element().Text);

                return ResolveSelf();
            }
        }
    }
}
