using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoSupport.Tessler.Core;

namespace InfoSupport.Tessler.Util
{
    public static class TypeUtil
    {
        //public static bool IsDeclaredIn(Type child, Type parent)
        //{
        //    if (child.DeclaringType == null)
        //    {
        //        return false;
        //    }

        //    if (child.DeclaringType == parent)
        //    {
        //        return true;
        //    }

        //    var baseType = parent;
        //    while (baseType.IsSubclassOf(typeof(PageObject)))
        //    {
        //        if (baseType == child.DeclaringType || (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == child.DeclaringType))
        //        {
        //            return true;
        //        }

        //        baseType = baseType.BaseType;
        //    }

        //    return false;
        //}
    }
}
