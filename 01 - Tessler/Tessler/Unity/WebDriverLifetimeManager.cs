using InfoSupport.Tessler.Drivers;
using Microsoft.Practices.Unity;

namespace InfoSupport.Tessler.Unity
{
    public class WebDriverLifetimeManager<T> : LifetimeManager where T : ITesslerWebDriver
    {
        private T instance;

        public override object GetValue()
        {
            if (instance != null)
            {
                if (instance.IsActive)
                {
                    RemoveValue();
                }
            }

            return instance;
        }

        public override void RemoveValue()
        {
            // Dispose in case the object hasn't been disposed from the outside
            instance.Close();

            instance = default(T);
        }

        public override void SetValue(object newValue)
        {
            instance = (T)newValue;
        }
    }
}