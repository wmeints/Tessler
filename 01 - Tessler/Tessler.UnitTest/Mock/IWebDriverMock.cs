using OpenQA.Selenium;

namespace InfoSupport.Tessler.UnitTest.Mock
{
    public interface IWebDriverMock : IWebDriver, IJavaScriptExecutor, ITakesScreenshot
    {

    }
}
