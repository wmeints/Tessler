using InfoSupport.Tessler.Drivers;

namespace InfoSupport.Tessler.Adapters.Ajax
{
    /// <summary>
    /// Interface for a javascript adapter, used to poll the browser for pending ajax requests
    /// </summary>
    public interface IJavascriptAdapter
    {
        bool IsActive(ITesslerWebDriver driver);
    }
}