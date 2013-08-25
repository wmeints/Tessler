using System;
using System.Configuration;
using System.IO;
using InfoSupport.Tessler.Adapters.Ajax;
using InfoSupport.Tessler.Adapters.Database;
using InfoSupport.Tessler.Configuration;
using InfoSupport.Tessler.Drivers;
using InfoSupport.Tessler.Screenshots;
using InfoSupport.Tessler.Selenium;
using InfoSupport.Tessler.Unity;
using InfoSupport.Tessler.Util;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;

namespace InfoSupport.Tessler
{
    public sealed class UnityConfiguration : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.Configure<Interception>()
                .AddPolicy("InterceptionPolicy")
                .AddMatchingRule<AnyMatchingRule>()
                .AddCallHandler<CallHandler>();

            // WebDriver
            Container.RegisterType<IWebDriverFactory, WebDriverFactory>();
            Container.RegisterType<ITesslerWebDriver, TesslerWebDriver>();

            // Screenshot manager
            Container.RegisterType<IScreenshotManager, ScreenshotManager>(new ContainerControlledLifetimeManager());
            
            if (!Container.IsRegistered<IJavascriptAdapter>())
            {
                Container.RegisterType<IJavascriptAdapter, JQueryAjaxStatusAdapter>();
            }

            JQueryScriptExtensions.Add("jQuery.expr[':'].equals = function(a, i, m) { var $a = $(a); return ($a.text() == m[3]); }");
        }
    }
}