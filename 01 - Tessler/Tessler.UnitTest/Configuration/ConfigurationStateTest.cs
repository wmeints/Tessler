using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InfoSupport.Tessler.Core;
using InfoSupport.Tessler.Configuration;
using InfoSupport.Tessler.Adapters.Ajax;

namespace InfoSupport.Tessler.UnitTest.Configuration
{
    [TestClass]
    public class ConfigurationStateTest
    {
        public class JavascriptAdapter1 : IJavascriptAdapter
        {
            public bool IsActive(Tessler.Drivers.ITesslerWebDriver driver)
            {
                throw new NotImplementedException();
            }
        }

        public class JavascriptAdapter2 : IJavascriptAdapter
        {
            public bool IsActive(Tessler.Drivers.ITesslerWebDriver driver)
            {
                throw new NotImplementedException();
            }
        }

        [TestMethod]
        public void SaveStateTest()
        {
            var ajaxWaitInterval = 1f;
            var ajaxWaitTime = 2f;
            var autoLoadJQuery = true;
            var browser = Browser.Firefox;
            var browserProfile = "profile1";
            var dateFormat = "dd/mm/yyyy";
            var findElementTimeout = 3f;
            var jqueryUrl = "jQueryUrl1";
            var takeScreenshot = TakeScreenshot.Always;
            var maximizeBrowser = true;
            var notVisibleWaitTime = 4f;
            var recycleBrowser = true;
            var resetDatabase = true;
            var screenshotsPath = "path1";
            var stripNamespace = "namespace1";
            var waitTime = 5f;
            var websiteUrl = "websiteUrl1";

            var ajaxWaitInterval2 = 10f;
            var ajaxWaitTime2 = 20f;
            var autoLoadJQuery2 = false;
            var browser2 = Browser.InternetExplorer;
            var browserProfile2 = "profile2";
            var dateFormat2 = "yyyy/mm/dd";
            var findElementTimeout2 = 30f;
            var jqueryUrl2 = "jqueryUrl2";
            var takeScreenshot2 = TakeScreenshot.Never;
            var maximizeBrowser2 = false;
            var notVisibleWaitTime2 = 40f;
            var recycleBrowser2 = false;
            var resetDatabase2 = false;
            var screenshotsPath2 = "path2";
            var stripNamespace2 = "namespace2";
            var waitTime2 = 50f;
            var websiteUrl2 = "websiteUrl2";

            var config = TesslerState.Configure();

            config
                .SetAjaxWaitInterval(1)
                .SetAjaxWaitTime(2)
                .SetAutoLoadJQuery(true)
                .SetBrowser(Browser.Firefox)
                .SetBrowserProfile("profile1")
                .SetDateFormat(dateFormat)
                .SetFindElementTimeout(findElementTimeout)
                .SetJavascriptAdapter<JavascriptAdapter1>()
                .SetJQueryUrl(jqueryUrl)
                .SetMaximizeBrowser(maximizeBrowser)
                .SetNotVisibleWaitTime(notVisibleWaitTime)
                .SetRecycleBrowser(recycleBrowser)
                .SetResetDatabase(resetDatabase)
                .SetScreenshotsPath(screenshotsPath)
                .SetStripNamespace(stripNamespace)
                .SetTakeScreenshot(takeScreenshot)
                .SetWaitTime(waitTime)
                .SetWebsiteUrl(websiteUrl)
                .SaveState()
            ;

            var delta = 0.1f;
            Assert.AreEqual(ConfigurationState.AjaxWaitInterval, ajaxWaitInterval, delta);
            Assert.AreEqual(ConfigurationState.AjaxWaitTime, ajaxWaitTime, delta);
            Assert.AreEqual(ConfigurationState.AutoLoadJQuery, autoLoadJQuery);
            Assert.AreEqual(ConfigurationState.Browser, browser);
            Assert.AreEqual(ConfigurationState.BrowserProfile, browserProfile);
            Assert.AreEqual(ConfigurationState.DateFormat, dateFormat);
            Assert.AreEqual(ConfigurationState.FindElementTimeout, findElementTimeout);
            Assert.AreEqual(ConfigurationState.JQueryUrl, jqueryUrl);
            Assert.AreEqual(ConfigurationState.MaximizeBrowser, maximizeBrowser);
            Assert.AreEqual(ConfigurationState.NotVisibleWaitTime, notVisibleWaitTime, delta);
            Assert.AreEqual(ConfigurationState.RecycleBrowser, recycleBrowser);
            Assert.AreEqual(ConfigurationState.ResetDatabase, resetDatabase);
            Assert.AreEqual(ConfigurationState.ScreenshotsPath, screenshotsPath);
            Assert.AreEqual(ConfigurationState.StripNamespace, stripNamespace);
            Assert.AreEqual(ConfigurationState.TakeScreenshot, takeScreenshot);
            Assert.AreEqual(ConfigurationState.WaitTime, waitTime);
            Assert.AreEqual(ConfigurationState.WebsiteUrl, websiteUrl);

            TesslerState.Configure()
                .SetAjaxWaitInterval(ajaxWaitInterval2)
                .SetAjaxWaitTime(ajaxWaitTime2)
                .SetAutoLoadJQuery(autoLoadJQuery2)
                .SetBrowser(browser2)
                .SetBrowserProfile(browserProfile2)
                .SetDateFormat(dateFormat2)
                .SetFindElementTimeout(findElementTimeout2)
                .SetJavascriptAdapter<JavascriptAdapter2>()
                .SetJQueryUrl(jqueryUrl2)
                .SetMaximizeBrowser(maximizeBrowser2)
                .SetNotVisibleWaitTime(notVisibleWaitTime2)
                .SetRecycleBrowser(recycleBrowser2)
                .SetResetDatabase(resetDatabase2)
                .SetScreenshotsPath(screenshotsPath2)
                .SetStripNamespace(stripNamespace2)
                .SetTakeScreenshot(takeScreenshot2)
                .SetWaitTime(waitTime2)
                .SetWebsiteUrl(websiteUrl2)
            ;

            Assert.AreEqual(ConfigurationState.AjaxWaitInterval, ajaxWaitInterval2, delta);
            Assert.AreEqual(ConfigurationState.AjaxWaitTime, ajaxWaitTime2, delta);
            Assert.AreEqual(ConfigurationState.AutoLoadJQuery, autoLoadJQuery2);
            Assert.AreEqual(ConfigurationState.Browser, browser2);
            Assert.AreEqual(ConfigurationState.BrowserProfile, browserProfile2);
            Assert.AreEqual(ConfigurationState.DateFormat, dateFormat2);
            Assert.AreEqual(ConfigurationState.FindElementTimeout, findElementTimeout2);
            Assert.AreEqual(ConfigurationState.JQueryUrl, jqueryUrl2);
            Assert.AreEqual(ConfigurationState.MaximizeBrowser, maximizeBrowser2);
            Assert.AreEqual(ConfigurationState.NotVisibleWaitTime, notVisibleWaitTime2);
            Assert.AreEqual(ConfigurationState.RecycleBrowser, recycleBrowser2);
            Assert.AreEqual(ConfigurationState.ResetDatabase, resetDatabase2);
            Assert.AreEqual(ConfigurationState.ScreenshotsPath, screenshotsPath2);
            Assert.AreEqual(ConfigurationState.StripNamespace, stripNamespace2);
            Assert.AreEqual(ConfigurationState.TakeScreenshot, takeScreenshot2);
            Assert.AreEqual(ConfigurationState.WaitTime, waitTime2);
            Assert.AreEqual(ConfigurationState.WebsiteUrl, websiteUrl2);

            TesslerState.Configure().RestoreState();

            Assert.AreEqual(ConfigurationState.AjaxWaitInterval, ajaxWaitInterval, delta);
            Assert.AreEqual(ConfigurationState.AjaxWaitTime, ajaxWaitTime, delta);
            Assert.AreEqual(ConfigurationState.AutoLoadJQuery, autoLoadJQuery);
            Assert.AreEqual(ConfigurationState.Browser, browser);
            Assert.AreEqual(ConfigurationState.BrowserProfile, browserProfile);
            Assert.AreEqual(ConfigurationState.DateFormat, dateFormat);
            Assert.AreEqual(ConfigurationState.FindElementTimeout, findElementTimeout, delta);
            Assert.AreEqual(ConfigurationState.JQueryUrl, jqueryUrl);
            Assert.AreEqual(ConfigurationState.MaximizeBrowser, maximizeBrowser);
            Assert.AreEqual(ConfigurationState.NotVisibleWaitTime, notVisibleWaitTime, delta);
            Assert.AreEqual(ConfigurationState.RecycleBrowser, recycleBrowser);
            Assert.AreEqual(ConfigurationState.ResetDatabase, resetDatabase);
            Assert.AreEqual(ConfigurationState.ScreenshotsPath, screenshotsPath);
            Assert.AreEqual(ConfigurationState.StripNamespace, stripNamespace);
            Assert.AreEqual(ConfigurationState.TakeScreenshot, takeScreenshot);
            Assert.AreEqual(ConfigurationState.WaitTime, waitTime, delta);
            Assert.AreEqual(ConfigurationState.WebsiteUrl, websiteUrl);

            TesslerState.Configure()
                .LoadFromAppConfig()
                .SaveState()
            ;
        }
    }
}
