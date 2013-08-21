using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoSupport.Tessler.Drivers;
using InfoSupport.Tessler.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InfoSupport.Tessler.Core
{
    public static class TesslerObjectExtensions
    {
        public static TPageObject CheckVisibility<TPageObject>(this TesslerObject<TPageObject> pageObject, Func<TPageObject, Func<TesslerObject>> func, bool isVisible)
            where TPageObject : TesslerObject<TPageObject>
        {
            CheckVisibility(() => func((TPageObject)pageObject)(), isVisible);

            return UnityInstance.Resolve<TPageObject>();
        }

        public static TPageObject CheckVisibility<TPageObject>(this TesslerObject<TPageObject> pageObject, Func<TPageObject, Func<Action<string>, TesslerObject>> func, bool isVisible)
            where TPageObject : TesslerObject<TPageObject>
        {
            CheckVisibility(() => func((TPageObject)pageObject)(a => { }), isVisible);

            return UnityInstance.Resolve<TPageObject>();
        }

        public static void CheckVisibility(Action action, bool isVisible)
        {
            TesslerWebDriver.InhibitExecution = true;

            action();

            var elements = TesslerWebDriver.StoredElements.Select(a => a).ToList();

            TesslerWebDriver.InhibitExecution = false;

            foreach (var element in elements)
            {
                bool isElementVisible = TesslerState.GetWebDriver().IsVisible(element);

                if (isVisible != isElementVisible)
                {
                    Assert.Fail("Element with selector '{0}', was expected to be {1}, but was {2}", element.Selector, (isVisible ? "visible" : "invisible"), (isVisible ? "invisible" : "visible"));
                }
            }
        }
    }
}
