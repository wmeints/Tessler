using System.Collections.Generic;
using System.Linq;
using InfoSupport.Tessler.Core;
using InfoSupport.Tessler.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace InfoSupport.Tessler.Selenium
{
    public class JQuery : FluentObject
    {
        public static JQuery By(string selector, params string[] parameters)
        {
            return new JQuery("(\"" + string.Format(selector, parameters) + "\")");
        }

        public string Selector
        {
            get;
            set;
        }

        public JQuery(string selector)
        {
            this.Selector = selector;
        }

        public JQuery Children(string selector = "")
        {
            return Function("children", selector);
        }

        public JQuery Children(int index)
        {
            return Function("children", string.Format(":nth-child({0})", index));
        }

        public JQuery Closest(string selector = "")
        {
            return Function("closest", selector);
        }

        public JQuery Filter(string selector = "")
        {
            return Function("filter", selector);
        }

        public JQuery Find(string selector = "")
        {
            return Function("find", selector);
        }

        public JQuery Next(string selector = "")
        {
            return Function("next", selector);
        }

        public JQuery NextAll(string selector = "")
        {
            return Function("nextAll", selector);
        }

        public JQuery NextUntil(string selector = "", string filter = "")
        {
            return Function("nextUntil", selector, filter);
        }

        public JQuery OffsetParent()
        {
            return Function("offsetParent");
        }

        public JQuery Parent(string selector = "")
        {
            return Function("parent", selector);
        }

        public JQuery Parents(string selector = "")
        {
            return Function("parents", selector);
        }

        public JQuery ParentsUntil(string selector = "", string filter = "")
        {
            return Function("parentsUntil", selector, filter);
        }

        public JQuery Prev(string selector = "")
        {
            return Function("prev", selector);
        }

        public JQuery PrevAll(string selector = "")
        {
            return Function("prevAll", selector);
        }

        public JQuery PrevUntil(string selector = "", string filter = "")
        {
            return Function("prevUntil", selector, filter);
        }

        public JQuery Eq(int index)
        {
            return Function("eq", index.ToString());
        }

        public JQuery First()
        {
            return Function("first");
        }

        public JQuery Has(string selector)
        {
            return Function("has", selector);
        }

        public JQuery Last()
        {
            return Function("last");
        }

        public JQuery Not(string selector)
        {
            return Function("not", selector);
        }

        public JQuery Sibling(int index)
        {
            return this.Parent().Children(index);
        }

        public JQuery ExactText(string text)
        {
            return Function("find", "function(index) { return jQuery(this).text() === '" + text + "'; })", null, false);
        }

        private JQuery Function(string func, string selector = "", string additionalArg = "", bool quotes = true)
        {
            // Add quotes to selector
            if (quotes && !string.IsNullOrEmpty(selector))
            {
                selector = "\"" + selector + "\"";
            }

            // Add additional parameter
            if (!string.IsNullOrEmpty(additionalArg))
            {
                selector += ",\"" + additionalArg + "\"";
            }

            // Add either: .func() or .func("selector") to original selector
            return new JQuery(this.Selector + "." + func + "(" + selector + ")");
        }
    }

    public static class JQueryExtensions
    {
        public static JQueryElement Element(this JQuery selector)
        {
            var result = selector.Elements();
            
            if(result.Count() > 1)
            {
                Assert.Fail("Selector '{0}' returned multiple elements", selector.Selector);
                throw new InvalidSelectorException();
            }

            return result.Single();
        }

        public static IEnumerable<JQueryElement> Elements(this JQuery selector)
        {
            return TesslerState.GetWebDriver().WaitFor(selector);
        }

        public static bool IsVisible(this JQuery selector)
        {
            return TesslerState.GetWebDriver().IsVisible(selector);
        }

        public static SelectElement Select(this JQuery selector)
        {
            return new SelectElement(TesslerState.GetWebDriver(), selector.Element(), selector);
        }

        public static IEnumerable<SelectElement> Selects(this JQuery selector)
        {
            return selector.Elements().Select(e => new SelectElement(TesslerState.GetWebDriver(), e, selector));
        }
    }
}