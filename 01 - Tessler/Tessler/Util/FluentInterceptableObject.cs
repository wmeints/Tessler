using System;
using System.ComponentModel;
using System.Runtime.Remoting;

namespace InfoSupport.Tessler.Util
{
    /// <summary>
    /// Hides any default object methods from Intellisense, such as Equals and GetHashCode
    /// </summary>
    public class FluentObject
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Type GetType()
        {
            return base.GetType();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToString()
        {
            return base.ToString();
        }
    }

    /// <summary>
    /// Hides any default object methods from Intellisense, such as Equals and GetHashCode
    /// Additionally inherits from MarshalByRefObject to enable transparent proxy interception
    /// </summary>
    public class FluentInterceptableObject : MarshalByRefObject
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override ObjRef CreateObjRef(Type requestedType)
        {
            return base.CreateObjRef(requestedType);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public new object GetLifetimeService()
        {
            return base.GetLifetimeService();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override object InitializeLifetimeService()
        {
            return base.InitializeLifetimeService();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Type GetType()
        {
            return base.GetType();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToString()
        {
            return base.ToString();
        }
    }
}