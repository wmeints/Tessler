using System;

namespace InfoSupport.Tessler.Core
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ResetDatabaseAttribute : Attribute
    {
        public ResetDatabaseAttribute(bool resetDatabase = true)
        {
            Reset = resetDatabase;
        }

        public bool Reset { get; set; }
    }
}