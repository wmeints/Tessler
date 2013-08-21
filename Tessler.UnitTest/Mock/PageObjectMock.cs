using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoSupport.Tessler.Core;

namespace InfoSupport.Tessler.UnitTest.Mock
{
    [TakeScreenshot(true)]
    public class PageObjectMock : PageObject<PageObjectMock>
    {
        public PageObjectMock PageActionWithScreenshot()
        {
            return ResolveSelf();
        }

        [TakeScreenshot(true)]
        public PageObjectMock PageActionWithExplicitScreenshot()
        {
            return ResolveSelf();
        }

        [TakeScreenshot(false)]
        public PageObjectMock PageActionWithoutScreenshot()
        {
            return ResolveSelf();
        }
    }

    /// <summary>
    /// Simple PageObject stubs
    /// </summary>
    public class StubPageObjectA : StubBasePageObject<StubPageObjectA>
    {

    }

    public class StubPageObjectB : StubBasePageObject<StubPageObjectB>
    {

    }

    public class StubParentPageObject : StubBasePageObject<StubParentPageObject>
    {

    }

    public class StubChildPageObject : StubBaseChildPageObject<StubChildPageObject, StubParentPageObject>
    {

    }

    public class StubSubChildPageObject : StubBaseChildPageObject<StubSubChildPageObject, StubChildPageObject>
    {

    }

    public class StubScopeObject : StubBaseScopeObject<StubScopeObject, StubParentPageObject>
    {

    }

    public class StubChildScopeObject : StubBaseScopeObject<StubChildScopeObject, StubScopeObject>
    {

    }

    public class StubPageChildScopeObject : StubBaseScopeObject<StubScopeObject, StubChildPageObject>
    {

    }

    public class StubPageObjectAChild : StubBaseChildPageObject<StubPageObjectAChild, StubPageObjectA>
    {

    }

    public class StubPageObjectASubChild : StubBaseChildPageObject<StubPageObjectASubChild, StubPageObjectAChild>
    {

    }

    public class StubBasePageObject<TPageObject> : PageObject<TPageObject>
        where TPageObject : TesslerObject<TPageObject>
    {
        public int OnEnterCalls { get; set; }
        public int OnLeaveCalls { get; set; }

        public int OnCallingCalls { get; set; }
        public int OnCalledCalls { get; set; }

        public T DoResolve<T>()
            where T : TesslerObject<T>
        {
            return Resolve<T>();
        }

        public void ResetCounters()
        {
            OnEnterCalls = 0;
            OnLeaveCalls = 0;
            OnCallingCalls = 0;
            OnCalledCalls = 0;
        }

        protected override void OnEnter()
        {
            OnEnterCalls++;
        }

        protected override void OnLeave()
        {
            OnLeaveCalls++;
        }

        protected override void OnCalling()
        {
            OnCallingCalls++;
        }

        protected override void OnCalled()
        {
            OnCalledCalls++;
        }
    }

    public class StubBaseChildPageObject<TPageObject, TParentObject> : ChildPageObject<TPageObject, TParentObject>
        where TPageObject : TesslerObject<TPageObject>
        where TParentObject : TesslerObject<TParentObject>
    {
        public int OnEnterCalls { get; set; }
        public int OnLeaveCalls { get; set; }

        public int OnCallingCalls { get; set; }
        public int OnCalledCalls { get; set; }

        public T DoResolve<T>()
            where T : TesslerObject<T>
        {
            return Resolve<T>();
        }

        public void ResetCounters()
        {
            OnEnterCalls = 0;
            OnLeaveCalls = 0;
            OnCallingCalls = 0;
            OnCalledCalls = 0;
        }

        protected override void OnEnter()
        {
            OnEnterCalls++;
        }

        protected override void OnLeave()
        {
            OnLeaveCalls++;
        }

        protected override void OnCalling()
        {
            OnCallingCalls++;
        }

        protected override void OnCalled()
        {
            OnCalledCalls++;
        }
    }

    public class StubBaseScopeObject<TPageObject, TParentObject> : ScopeObject<TPageObject, TParentObject>
        where TPageObject : TesslerObject<TPageObject>
        where TParentObject : TesslerObject<TParentObject>
    {
        public int OnCallingCalls { get; set; }
        public int OnCalledCalls { get; set; }

        public T DoResolve<T>()
            where T : TesslerObject<T>
        {
            return Resolve<T>();
        }

        public void ResetCounters()
        {
            OnCallingCalls = 0;
            OnCalledCalls = 0;
        }

        protected override void OnCalling()
        {
            OnCallingCalls++;
        }

        protected override void OnCalled()
        {
            OnCalledCalls++;
        }
    }
}
