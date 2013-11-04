using OpenQA.Selenium;

namespace InfoSupport.Tessler.Drivers
{
    public interface IWebDriverFactory
    {
        IWebDriver BuildWebDriver();
    }
}
