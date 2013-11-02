using InfoSupport.Tessler.Unity;

namespace InfoSupport.Tessler.Core
{
    public abstract class ChildPageObject<TPageObject, TParentObject> : PageObject<TPageObject>
        where TPageObject : TesslerObject<TPageObject>
        where TParentObject : TesslerObject<TParentObject>
    {
        protected TParentObject ResolveParent()
        {
            return Resolve<TParentObject>();
        }

        internal override TesslerObject GetParent()
        {
            return UnityInstance.Resolve<TParentObject>();
        }
    }
}