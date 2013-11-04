using System.Linq;
using InfoSupport.Tessler.Core;
using InfoSupport.Tessler.Drivers;
using Microsoft.Practices.Unity;

namespace InfoSupport.Tessler.Unity
{
    public static class UnityInstance
    {
        private static UnityContainer instance;

        public static UnityContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UnityContainer();
                }

                return instance;
            }
        }

        public static T Resolve<T>()
        {
            return Instance.Resolve<T>();
        }

        public static TTargetPageType ResolvePageObject<TSourcePageType, TTargetPageType>(TSourcePageType sourceObject, params ResolverOverride[] overrides)
            where TSourcePageType : TesslerObject
            where TTargetPageType : TesslerObject
        {
            if (!TesslerWebDriver.InhibitExecution)
            {
                TesslerState.GetWebDriver().LoadJQuery();

                TesslerState.GetWebDriver().WaitForAjax();
            }

            var targetObject = Instance.Resolve<TTargetPageType>(overrides);

            if (TesslerWebDriver.InhibitExecution)
            {
                return targetObject;
            }

            var sourcePageType = typeof(TSourcePageType);
            var targetPageType = typeof(TTargetPageType);

            // If the target type is not the same as the source type, we need to do some OnEnter and OnLeave calls
            if (sourcePageType != targetPageType)
            {
                // Get the source's parents
                var sourceParents = sourceObject.GetParents().ToList();

                // Get the target's parents
                var targetParents = targetObject.GetParents().ToList();

                // If the source object is not a parent of the target, fire an onleave to the source object
                if (!targetParents.Any(p => p.GetType() == sourcePageType))
                {
                    sourceObject.FireOnLeave();

                    // Bubble the OnLeave event to all parents, until the target one
                    foreach (var sourceParent in sourceParents)
                    {
                        if (sourceParent.GetType() == targetPageType)
                        {
                            break;
                        }

                        sourceParent.FireOnLeave();
                    }
                }

                // If the target object is not a parent of the source, fire an onenter to the target object
                if (!sourceParents.Any(p => p.GetType() == targetPageType))
                {
                    targetObject.FireOnEnter();

                    // Buble the OnEnter event to all parents, until the source object
                    foreach (var targetParent in targetParents)
                    {
                        if (sourcePageType == targetParent.GetType())
                        {
                            break;
                        }

                        targetParent.FireOnEnter();
                    }
                }
            }

            return targetObject;
        }
    }
}