using System.Collections.Generic;
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
