using System;

namespace InfoSupport.Tessler.Core
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class BrowserProfileAttribute : Attribute
    {
        public string Profile { get; set; }

        public BrowserProfileAttribute(string profile)
        {
            Profile = profile;
        }
    }
}
