using InfoSupport.Tessler.Drivers;

namespace InfoSupport.Tessler.Adapters.Ajax
{
    public class JQueryAjaxStatusAdapter : IJavascriptAdapter
    {
        public bool IsActive(ITesslerWebDriver driver)
        {
            var activeString = driver.Js("return jQuery.active");

            return !activeString.Equals(0L);
        }
    }
}