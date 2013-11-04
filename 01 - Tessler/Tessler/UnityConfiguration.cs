using System;
using System.Linq;
using System.Reflection;
using InfoSupport.Tessler.Adapters.Ajax;
using InfoSupport.Tessler.Core;
using InfoSupport.Tessler.Drivers;
using InfoSupport.Tessler.Screenshots;
using InfoSupport.Tessler.Selenium;
using InfoSupport.Tessler.Unity;
using log4net;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace InfoSupport.Tessler
{
    public sealed class UnityConfiguration : UnityContainerExtension
    {
        protected override void Initialize()
        {
            // Logging
            Container.RegisterInstance<ILog>(LogManager.GetLogger(typeof(TesslerState)));

            // Interception
            Container.AddNewExtension<Interception>();
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

            // Page objects
            var currentAssembly = Assembly.GetAssembly(typeof(TesslerObject));
            var pageObjects = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a != currentAssembly)
                .SelectMany(a => a.GetTypes())
                .Where(a => a.IsSubclassOf(typeof(TesslerObject)))
                .ToList()
            ;

            pageObjects.ForEach(po =>
            {
                Container.RegisterType(po, new ContainerControlledLifetimeManager());

                Container.Configure<Interception>().SetInterceptorFor(po, new TransparentProxyInterceptor());
            });

            JQueryScriptExtensions.Add("jQuery.expr[':'].equals = function(a, i, m) { var $a = $(a); return ($a.text() == m[3]); }");
        }

        private static bool isInitialized;

        internal static void InitializeStandAlone()
        {
            if (!isInitialized)
            {
                var container = UnityInstance.Instance;

                container.AddNewExtension<UnityConfiguration>();
            }
        }
    }
}