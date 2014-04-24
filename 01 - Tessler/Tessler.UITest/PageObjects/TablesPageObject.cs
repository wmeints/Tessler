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
    public class TablesPageObject : BasePageObject<TablesPageObject>
    {
        public SimpleTableScopeObject WithSimpleTable(int id)
        {
            var po = Resolve<SimpleTableScopeObject>();
            po.Id = id;
            return po;
        }

        public class SimpleTableScopeObject : ScopeObject<SimpleTableScopeObject, TablesPageObject>
        {
            public int Id { get; set; }

            private JQuery Selector { get { return JQuery.By("#products tr td:nth-child(1):equals('{0}')", Id.ToString()); } }

            public SimpleTableScopeObject WithId(Action<string> action)
            {
                action(Selector.Sibling(1).Element().Text);

                return ResolveSelf();
            }

            public SimpleTableScopeObject WithName(Action<string> action)
            {
                action(Selector.Sibling(2).Element().Text);

                return ResolveSelf();
            }

            public SimpleTableScopeObject WithPrice(Action<string> action)
            {
                action(Selector.Sibling(3).Element().Text);

                return ResolveSelf();
            }

            public SimpleTableScopeObject WithInStock(Action<string> action)
            {
                action(Selector.Sibling(4).Element().Text);

                return ResolveSelf();
            }
        }
    }
}
