﻿using InfoSupport.Tessler.Adapters.Ajax;
using InfoSupport.Tessler.Configuration;
using InfoSupport.Tessler.Exceptions;
using InfoSupport.Tessler.Selenium;
using InfoSupport.Tessler.Unity;
using InfoSupport.Tessler.Util;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace InfoSupport.Tessler.Drivers
{
    public class TesslerWebDriver : ITesslerWebDriver
    {
        private static bool inhibitExecution = false;
        private static List<JQuery> storedElements = new List<JQuery>();

        internal static bool InhibitExecution
        {
            get { return inhibitExecution; }
            set
            {
                if (!value)
                {
                    storedElements.Clear();
                }

                inhibitExecution = value;
            }
        }

        internal static List<JQuery> StoredElements
        {
            get
            {
                if (storedElements == null)
                {
                    storedElements = new List<JQuery>();
                }

                return storedElements;
            }
        }

        private IWebDriver driver;
        private bool isActive;

        public TesslerWebDriver()
        {
            this.driver = UnityInstance.Instance.Resolve<IWebDriverFactory>().BuildWebDriver();
            this.isActive = true;
        }

        public bool IsActive
        {
            get { return isActive; }
        }

        public void Close()
        {
            driver.Quit();
            isActive = false;
        }

        public string PageSource
        {
            get { return driver.PageSource; }
        }

        public string Title
        {
            get { return driver.Title; }
        }

        public string Url
        {
            get { return driver.Url; }
            set { driver.Url = value; }
        }

        public IWebDriver GetDriver()
        {
            return driver;
        }

        public Screenshot GetScreenshot()
        {
            return ((ITakesScreenshot)driver).GetScreenshot();
        }

        public IOptions Manage()
        {
            return driver.Manage();
        }

        public INavigation Navigate()
        {
            return driver.Navigate();
        }

        public void ClosePopups()
        {
            try
            {
                driver.SwitchTo().Alert().Dismiss();
            }
            catch { }
        }

        public void Wait()
        {
            Thread.Sleep(TimeSpan.FromSeconds(ConfigurationState.WaitTime));
        }

        public bool IsJQueryLoaded
        {
            get
            {
                try
                {
                    var result = Js("return typeof(jQuery)");

                    if (result.ToString() == "function") return true;
                }
                catch (UnhandledAlertException)
                {
                    // Automatically accept any pending alert to prevent this problem from happening.
                    driver.SwitchTo().Alert().Accept();

                    var result = Js("return typeof(jQuery)");
                    if (result.ToString() == "function") return true;
                }
                catch (Exception e)
                {
                    var message = string.Format("Error checking jQuery presence: {0}", e.Message);
                    Log.Fatal(message);
                    throw new TesslerWebDriverException(message);
                }

                return false;
            }
        }

        public void LoadJQuery()
        {
            if (ConfigurationState.AutoLoadJQuery)
            {
                // Check if jQuery is already loaded
                if (IsJQueryLoaded) return;

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                // Add a jQuery script element
                StringBuilder js = new StringBuilder();

                js.Append("var script = document.createElement('script');");
                js.AppendFormat("script.src = '{0}';", ConfigurationState.JQueryUrl);
                js.Append("script.type = 'text/javascript';");
                js.Append("document.getElementsByTagName('head')[0].appendChild(script);");

                Js(js.ToString());

                // Check that we can now run jQuery functions
                Retry.Create("Load jQuery", () =>
                {
                    return IsJQueryLoaded;
                })
                .OnSuccess(() =>
                {
                    stopwatch.Stop();

                    Log.InfoFormat("jQuery succesfully loaded in {0}ms", stopwatch.ElapsedMilliseconds);
                })
                .OnFail(() =>
                {
                    var message = "Could not manually load jQuery";
                    Log.Fatal(message);
                    throw new TesslerWebDriverException(message);
                })
                .SetInterval(TimeSpan.FromSeconds(0.2))
                .SetTimeout(TimeSpan.FromSeconds(4))
                .Start();
            }
        }

        public object Js(string javascript)
        {
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;

            if (js == null)
            {
                Log.Fatal("Driver cannot be used to execute javascript");
                throw new InvalidOperationException("Driver cannot be used to execute javascript");
            }

            return js.ExecuteScript(javascript);
        }

        public void WaitForAjax()
        {
            try
            {
                var jsAdapter = UnityInstance.Resolve<IJavascriptAdapter>();

                TimeSpan max = TimeSpan.FromSeconds(ConfigurationState.AjaxWaitTime);
                TimeSpan interval = TimeSpan.FromSeconds(ConfigurationState.AjaxWaitInterval);

                Retry.Create("Wait for Ajax", () => !jsAdapter.IsActive(this))
                    .SetInterval(interval)
                    .SetTimeout(max)
                    .Start();
            }
            catch (InvalidOperationException e)
            {
                Log.WarnFormat("Could not execute action WaitForAjax: {0} ({1})", e.Message, driver.Url);
            }
        }

        public IEnumerable<IJQueryElement> FindElements(JQuery by)
        {
            if (InhibitExecution)
            {
                storedElements.Add(by);

                return new List<IJQueryElement>() { new Mock<IJQueryElement>().Object };
            }

            IEnumerable<IWebElement> elements = null;

            // The driver will throw an exception if it cannot find any element, in which case we want to return null
            Retry.Create("Find element jQuery" + by.Selector, () =>
            {
                try
                {
                    elements = Js("return jQuery" + by.Selector + ".get()") as IEnumerable<IWebElement>;
                }
                catch { }

                return elements != null;
            })
            .AcceptAnyException()
            .SetInterval(TimeSpan.FromSeconds(TesslerConfiguration.NotVisibleWaitTime()))
            .SetTimeout(TimeSpan.FromSeconds(TesslerConfiguration.FindElementTimeout()))
            .Start()
            ;

            // If elements were found, wrap them in a JQueryElement collection
            return elements != null ? elements.Select(e => new JQueryElement(this, e, by)) : new List<JQueryElement>();
        }

        public bool IsVisible(JQuery by)
        {
            try
            {
                by = by.Filter(":visible");

                IEnumerable<IWebElement> elements = null;

                Retry.Create("IsVisible", () =>
                {
                    elements = Js("return jQuery" + by.Selector + ".get()") as IEnumerable<IWebElement>;

                    return true;
                })
                .AcceptAnyException()
                .SetInterval(TimeSpan.FromSeconds(TesslerConfiguration.AjaxWaitInterval()))
                .SetTimeout(TimeSpan.FromSeconds(TesslerConfiguration.NotVisibleWaitTime()))
                .Start();

                return elements.Count() > 0;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<IJQueryElement> WaitFor(JQuery by)
        {
            try
            {
                LoadJQuery();
                JQueryScriptExtensions.LoadExtensions();

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(ConfigurationState.FindElementTimeout));

                var result = wait.Until(d => FindElements(by));

                return result;
            }
            catch (WebDriverTimeoutException)
            {
                Log.FatalFormat("Could not find element(s) '{0}'", by.Selector);
                Assert.Fail("Could not find element(s) '{0}'", by.Selector);
                return null;
            }
            catch (Exception e)
            {
                Log.FatalFormat("Find element exception: '{0}'", e.Message);
                throw;
            }
        }
    }
}