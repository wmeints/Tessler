﻿using InfoSupport.Tessler.Adapters.Ajax;
using InfoSupport.Tessler.Configuration;
using InfoSupport.Tessler.Selenium;
using InfoSupport.Tessler.Unity;
using InfoSupport.Tessler.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using Moq;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

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

        public void LoadJQuery()
        {
            // Check if jQuery is already loaded
            try
            {
                var result = Js("return typeof(jQuery)");

                if (result.ToString() == "function") return;
            }
            catch (Exception e)
            {
                Log.WarnFormat("Error checking jQuery presence: {0}", e.Message);
                return;
            }

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
            Retry.Create(() =>
            {
                return Js("return typeof(jQuery)").ToString() == "function";
            })
            .OnSuccess(() =>
            {
                stopwatch.Stop();

                Log.InfoFormat("jQuery succesfully loaded in {0}ms", stopwatch.ElapsedMilliseconds);
            })
            .OnFail(() =>
            {
                Log.Warn("Could not manually load jQuery");
            })
            .SetInterval(TimeSpan.FromSeconds(0.2))
            .SetTimeout(TimeSpan.FromSeconds(4))
            .Start();
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

                Retry.Create(() => !jsAdapter.IsActive(this))
                    .SetInterval(interval)
                    .SetTimeout(max)
                    .Start();
            }
            catch (InvalidOperationException e)
            {
                Log.WarnFormat("Could not execute action WaitForAjax: {0} ({1})", e.Message, driver.Url);
            }
        }

        public IEnumerable<JQueryElement> FindElements(JQuery by)
        {
            if (InhibitExecution)
            {
                storedElements.Add(by);

                return new List<JQueryElement>() { new JQueryElement(this, new Mock<IWebElement>().Object, by) };
            }

            IEnumerable<IWebElement> elements = null;

            // The driver will throw an exception if it cannot find any element, in which case we want to return null
            try
            {
                elements = Js("return jQuery" + by.Selector + ".get()") as IEnumerable<IWebElement>;
            }
            catch (Exception e)
            {

            }

            // If elements were found, wrap them in a JQueryElement collection
            return elements != null ? elements.Select(e => new JQueryElement(this, e, by)) : null;
        }

        public bool IsVisible(JQuery by)
        {
            try
            {
                by = by.Filter(":visible");

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(ConfigurationState.NotVisibleWaitTime));
                wait.Until(d => FindElements(by));

                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<JQueryElement> WaitFor(JQuery by)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(ConfigurationState.FindElementTimeout));

                var result = wait.Until(d => FindElements(by));

                return result;
            }
            catch (WebDriverTimeoutException e)
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

        public void SetDialogLeavePage(bool expected = false)
        {
            InitializeSetTesslerData();

            var sb = new StringBuilder();
            sb.Append("window.onbeforeunload = function () {");
            //sb.Append("window.setTesslerData('leavepage-message', window.onbeforeunload());");

            // Throw error if not expected
            if (!expected)
            {
                sb.Append("throw Error('An unexpected Leave Page Dialog openend.');");
            }

            sb.Append("}");

            Js(sb.ToString());
        }

        public void SetDialogAlert(bool expected = false)
        {
            InitializeSetTesslerData();

            var sb = new StringBuilder();
            sb.Append("alert = function (text) { window.setTesslerData('alert-message', text);");

            // Throw error if not expected
            if (!expected)
            {
                sb.Append("throw Error('An unexpected Alert Dialog openend.');");
            }
        
            sb.Append("}");

            Js(sb.ToString());
        }

        public string GetDialogAlertMessage()
        {
            InitializeGetTesslerData();
            
            var message = Js("window.getTesslerState('alert-message')");
            return message != null ? message.ToString() : null;
        }

        public void SetDialogConfirm(bool? result = null)
        {
            InitializeSetTesslerData();

            var sb = new StringBuilder();
            sb.Append("confirm = function (text) { window.setTesslerData('confirm-message', text);");
            

            // Either return the given result value or throw error if not expected
            if(result.HasValue)
            {
                sb.Append("return " + (result.Value ? "true" : "false") + "; }");
            }
            else
            {
                sb.Append("throw Error('An unexpected Confirm Dialog openend.');");
            }

            sb.Append("}");

            Js(sb.ToString());
        }

        public string GetDialogConfirmMessage()
        {
            InitializeGetTesslerData();

            var message = Js("window.getTesslerState('confirm-message')");
            return message != null ? message.ToString() : null;
        }

        public void ClearDialogMessages()
        {
            var sb = new StringBuilder();

            sb.Append("if(window.sessionStorage) {");
            sb.Append("window.sessionStorage.removeItem('tessler-alert-message');");
            sb.Append("window.sessionStorage.removeItem('tessler-confirm-message');");
            sb.Append("} else if(window.tessler) {");
            sb.Append("window.tessler['alert-message'] = null;");
            sb.Append("window.tessler['confirm-message'] = null;");
            sb.Append("}");

            Js(sb.ToString());
        }
        
        private void InitializeSetTesslerData()
        {
            var sb = new StringBuilder();
            sb.Append("window.setTesslerData = window.setTesslerData || function(key, data) {");
            sb.Append("if(window.sessionStorage) {");
            sb.Append("window.sessionStorage.setItem('tessler-' + key, data);");
            sb.Append("} else {");
            sb.Append("window.tessler = window.tessler || new Array();");
            sb.Append("window.tessler[key] = data;");
            sb.Append("}");
            sb.Append("};");

            Js(sb.ToString());
        }

        private void InitializeGetTesslerData()
        {
            var sb = new StringBuilder();
            sb.Append("window.getTesslerData = window.getTesslerData || function(key) {");
            sb.Append("if(window.sessionStorage) {");
            sb.Append("return window.sessionStorage.getItem('tessler-' + key);");
            sb.Append("} else {");
            sb.Append("window.tessler = window.tessler || new Array();");
            sb.Append("return window.tessler[key];");
            sb.Append("}");
            sb.Append("};");

            Js(sb.ToString());
        }
    }
}