using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoSupport.Tessler.Drivers;
using InfoSupport.Tessler.Unity;
using InfoSupport.Tessler.Util;

namespace InfoSupport.Tessler.Core
{
    public class TesslerObject : FluentInterceptableObject
    {
        protected ITesslerWebDriver Driver
        {
            get { return TesslerState.GetWebDriver(); }
        }

        internal virtual void FireOnEnter()
        {

        }

        internal virtual void FireOnLeave()
        {

        }

        internal virtual void FireOnCalling()
        {

        }

        internal virtual void FireOnCalled()
        {

        }

        internal virtual TesslerObject GetParent()
        {
            return null;
        }

        internal virtual IEnumerable<TesslerObject> GetParents()
        {
            var parent = GetParent();

            if (parent != null)
            {
                yield return parent;

                var grandParents = parent.GetParents();

                foreach (var grandParent in grandParents)
                {
                    yield return grandParent;
                }
            }
        }
    }

    public class TesslerObject<TPageObject> : TesslerObject
        where TPageObject : TesslerObject<TPageObject>
    {
        //protected T Resolve<T>() where T : PageObject
        //{
        //    Driver.LoadJQuery();

        //    Driver.WaitForAjax();

        //    var result = UnityInstance.Resolve<T>();

        //    var fromType = GetType();
        //    var toType = typeof(T);

        //    if (fromType != toType && !TypeUtil.IsDeclaredIn(toType, fromType) && !TesslerWebDriver.InhibitExecution)
        //    {
        //        OnLeave();
        //        result.OnEnter();
        //    }

        //    return result;
        //}

        //internal TTargetObject DoResolve<TTargetObject>()
        //    where TTargetObject : PageObject<TTargetObject>
        //{
        //    return Resolve<TTargetObject>();
        //}

        protected TTargetPageObject Resolve<TTargetPageObject>()
            where TTargetPageObject : TesslerObject
        {
            return UnityInstance.ResolvePageObject<TPageObject, TTargetPageObject>((TPageObject)this);
        }

        protected TPageObject ResolveSelf()
        {
            return Resolve<TPageObject>();
        }

        public static TPageObject Create()
        {
            return UnityInstance.Resolve<TPageObject>();
        }
    }
}
