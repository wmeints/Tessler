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
using System.IO;
using System.Collections.Generic;

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

            // Register Page objects
            GetAllPageObjects().ToList().ForEach(po =>
            {
                TesslerState.RegisterPageObject(po);
            });

            JQueryScriptExtensions.Add("jQuery.expr[':'].equals = function(a, i, m) { var $a = $(a); return ($a.text() == m[3]); }");
        }

        private static IEnumerable<Type> GetAllPageObjects()
        {
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var currentAssembly = Assembly.GetExecutingAssembly();

            // Ensure alle references assemblies are loaded
            var loadedPaths = loadedAssemblies.Where(a => !a.IsDynamic).Select(a => a.Location).ToArray();
            var referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
            var toLoad = referencedPaths.Where(r => !loadedPaths.Contains(r, StringComparer.InvariantCultureIgnoreCase)).ToList();
            toLoad.ForEach(path => loadedAssemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(path))));

            return loadedAssemblies
                .Where(a => a.FullName.IndexOf("PageObject", StringComparison.OrdinalIgnoreCase) != -1)
                .Where(a => a.GetReferencedAssemblies().Any(r => r.ToString() == currentAssembly.GetName().ToString()))
                .SelectMany(a => a.GetTypes())
                .Where(a => a.IsSubclassOf(typeof(TesslerObject)))
            ;
        }
        
        internal static void InitializeStandAlone()
        {
            UnityInstance.Instance.AddNewExtension<UnityConfiguration>();
        }
    }
}