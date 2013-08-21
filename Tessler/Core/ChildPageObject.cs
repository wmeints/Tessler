using InfoSupport.Tessler.Unity;
namespace InfoSupport.Tessler.Core
{
    public abstract class ChildPageObject<TPageObject, TParentObject> : PageObject<TPageObject> //, IChildPageObject<TPageObject, TParentObject>
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

        //internal bool IChildPageObject<TPageObject, TParentObject>.IsChildOf<T>()
        //{
        //    throw new System.NotImplementedException();
        //}

        //internal bool IChildPageObject<TPageObject, TParentObject>.IsParentOf<T>()
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}