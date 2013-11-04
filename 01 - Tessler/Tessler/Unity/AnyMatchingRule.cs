using Microsoft.Practices.Unity.InterceptionExtension;

namespace InfoSupport.Tessler.Unity
{
    public class AnyMatchingRule : IMatchingRule
    {
        public bool Matches(System.Reflection.MethodBase member)
        {
            return true;
        }
    }
}