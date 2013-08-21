using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoSupport.Tessler.UnitTest.Mock;
using InfoSupport.Tessler.Unity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InfoSupport.Tessler.Drivers;
using Moq;

namespace InfoSupport.Tessler.UnitTest
{
    [TestClass]
    public class UnityConfiguration : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.Configure<Interception>()
                .AddPolicy("InterceptionPolicy")
                .AddMatchingRule<AnyMatchingRule>()
                .AddCallHandler<CallHandler>();
            
            Container.RegisterType<PageObjectMock>(new ContainerControlledLifetimeManager());

            Container.RegisterType<StubPageObjectA>(new ContainerControlledLifetimeManager());
            Container.RegisterType<StubPageObjectB>(new ContainerControlledLifetimeManager());

            Container.RegisterType<StubParentPageObject>(new ContainerControlledLifetimeManager());
            Container.RegisterType<StubChildPageObject>(new ContainerControlledLifetimeManager());
            Container.RegisterType<StubSubChildPageObject>(new ContainerControlledLifetimeManager());

            Container.RegisterType<StubPageObjectAChild>(new ContainerControlledLifetimeManager());
            Container.RegisterType<StubPageObjectASubChild>(new ContainerControlledLifetimeManager());

            Container.RegisterType<StubScopeObject>(new ContainerControlledLifetimeManager());
            Container.RegisterType<StubChildScopeObject>(new ContainerControlledLifetimeManager());
            Container.RegisterType<StubPageChildScopeObject>(new ContainerControlledLifetimeManager());

            Container.Configure<Interception>().SetInterceptorFor<PageObjectMock>(new TransparentProxyInterceptor());
        }

        [AssemblyInitialize]
        public static void Configuration(TestContext context)
        {
            var container = UnityInstance.Instance;

            container.AddNewExtension<Interception>();
            container.AddNewExtension<UnityConfiguration>();
        }
    }
}
