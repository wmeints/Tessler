using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace InfoSupport.Tessler.UnitTest.Mock
{
    public interface IWebDriverMock : IWebDriver, IJavaScriptExecutor, ITakesScreenshot
    {

    }
}
