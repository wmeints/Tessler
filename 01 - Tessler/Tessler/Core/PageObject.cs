﻿
namespace InfoSupport.Tessler.Core
{
    [TakeScreenshot]
    public abstract class PageObject<TPageObject> : TesslerObject<TPageObject>
        where TPageObject : TesslerObject<TPageObject>
    {
        protected virtual void OnEnter()
        {

        }

        protected virtual void OnLeave()
        {

        }

        protected virtual void OnCalling()
        {

        }

        protected virtual void OnCalled()
        {

        }

        internal override void FireOnEnter()
        {
            base.FireOnEnter();

            OnEnter();
        }

        internal override void FireOnLeave()
        {
            base.FireOnLeave();

            OnLeave();
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
    }
}