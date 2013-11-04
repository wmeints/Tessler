using InfoSupport.Tessler.Unity;

namespace InfoSupport.Tessler.Core
{
    public abstract class ScopeObject<TPageObject, TParentObject> : TesslerObject<TPageObject>
        where TPageObject : TesslerObject<TPageObject>
        where TParentObject : TesslerObject<TParentObject>
    {
        [TakeScreenshot(false)]
        public TParentObject Then()
        {
            return Resolve<TParentObject>();
        }

        protected virtual void OnCalling()
        {

        }

        protected virtual void OnCalled()
        {

        }

        internal override void FireOnCalling()
        {
            base.FireOnCalling();

            OnCalling();
        }

        internal override void FireOnCalled()
        {
            base.FireOnCalled();

            OnCalled();
        }

        internal override TesslerObject GetParent()
        {
            return UnityInstance.Resolve<TParentObject>();
        }
    }
}
