using InfoSupport.Tessler.Drivers;
using InfoSupport.Tessler.UnitTest.Mock;
using InfoSupport.Tessler.Unity;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace InfoSupport.Tessler.UnitTest.Core
{
    [TestClass]
    public class ResolveEventsTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            var webDriver = new Mock<ITesslerWebDriver>();

            UnityInstance.Instance.RegisterInstance<ITesslerWebDriver>(webDriver.Object);

            UnityInstance.Resolve<StubPageObjectA>().ResetCounters();
            UnityInstance.Resolve<StubPageObjectB>().ResetCounters();
            UnityInstance.Resolve<StubParentPageObject>().ResetCounters();
            UnityInstance.Resolve<StubChildPageObject>().ResetCounters();
            UnityInstance.Resolve<StubSubChildPageObject>().ResetCounters();
            UnityInstance.Resolve<StubScopeObject>().ResetCounters();
            UnityInstance.Resolve<StubChildScopeObject>().ResetCounters();
            UnityInstance.Resolve<StubPageChildScopeObject>().ResetCounters();
            UnityInstance.Resolve<StubPageObjectAChild>().ResetCounters();
            UnityInstance.Resolve<StubPageObjectASubChild>().ResetCounters();
        }

        [TestMethod]
        [TestCategory("ResolveEvents")]
        public void PageObjectAToPageObjectB()
        {
            //  A              B
            // |========| --> |========|
            //         L       E

            var from = UnityInstance.Resolve<StubPageObjectA>();

            var to = from.DoResolve<StubPageObjectB>();

            Assert.AreEqual(0, from.OnEnterCalls);
            Assert.AreEqual(1, from.OnLeaveCalls);
            Assert.AreEqual(1, to.OnEnterCalls);
            Assert.AreEqual(0, to.OnLeaveCalls);
        }

        [TestMethod]
        [TestCategory("ResolveEvents")]
        public void ParentObjectToChildObjectTest()
        {
            //  P
            // |================|
            //       |    C
            //       +-> |======|
            //            E

            var parent = UnityInstance.Resolve<StubParentPageObject>();

            var child = parent.DoResolve<StubChildPageObject>();

            Assert.AreEqual(0, parent.OnEnterCalls);
            Assert.AreEqual(0, parent.OnLeaveCalls);
            Assert.AreEqual(1, child.OnEnterCalls);
            Assert.AreEqual(0, child.OnLeaveCalls);
        }

        [TestMethod]
        [TestCategory("ResolveEvents")]
        public void ChildObjectToParentObjectTest()
        {
            //  P
            // |================|
            //  C        |
            // |======| -+
            //       L

            var child = UnityInstance.Resolve<StubChildPageObject>();

            var parent = child.DoResolve<StubParentPageObject>();

            Assert.AreEqual(0, child.OnEnterCalls);
            Assert.AreEqual(1, child.OnLeaveCalls);
            Assert.AreEqual(0, parent.OnEnterCalls);
            Assert.AreEqual(0, parent.OnLeaveCalls);
        }

        [TestMethod]
        [TestCategory("ResolveEvents")]
        public void ParentToChildToSubChild()
        {
            //  P
            // |====================|
            //      |
            //      +->|============|
            //          E |
            //            +->|======|
            //                E

            var parent = UnityInstance.Resolve<StubParentPageObject>();

            var child = parent.DoResolve<StubChildPageObject>();

            var subchild = child.DoResolve<StubSubChildPageObject>();

            Assert.AreEqual(0, parent.OnEnterCalls);
            Assert.AreEqual(0, parent.OnLeaveCalls);
            Assert.AreEqual(1, child.OnEnterCalls);
            Assert.AreEqual(0, child.OnLeaveCalls);
            Assert.AreEqual(1, subchild.OnEnterCalls);
            Assert.AreEqual(0, subchild.OnLeaveCalls);
        }

        [TestMethod]
        [TestCategory("ResolveEvents")]
        public void SubChildToChildToParent()
        {
            //  P
            // |====================|
            //                |
            // |============|-+
            //          |  L
            // |======|-+
            //       L

            var subchild = UnityInstance.Resolve<StubSubChildPageObject>();

            var child = subchild.DoResolve<StubChildPageObject>();

            var parent = child.DoResolve<StubParentPageObject>();

            Assert.AreEqual(0, subchild.OnEnterCalls);
            Assert.AreEqual(1, subchild.OnLeaveCalls);
            Assert.AreEqual(0, child.OnEnterCalls);
            Assert.AreEqual(1, child.OnLeaveCalls);
            Assert.AreEqual(0, parent.OnEnterCalls);
            Assert.AreEqual(0, parent.OnLeaveCalls);
        }

        [TestMethod]
        [TestCategory("ResolveEvents")]
        public void ChildObjectToUnrelatedObject()
        {
            //  P
            // |====================| |====================|
            //  C   |              L   E |
            //      +->|============|----+
            //          E          L

            var parent = UnityInstance.Resolve<StubParentPageObject>();

            var child = parent.DoResolve<StubChildPageObject>();

            var to = child.DoResolve<StubPageObjectB>();

            Assert.AreEqual(0, parent.OnEnterCalls);
            Assert.AreEqual(1, parent.OnLeaveCalls);
            Assert.AreEqual(1, child.OnEnterCalls);
            Assert.AreEqual(1, child.OnLeaveCalls);
            Assert.AreEqual(1, to.OnEnterCalls);
            Assert.AreEqual(0, to.OnLeaveCalls);
        }

        [TestMethod]
        [TestCategory("ResolveEvents")]
        public void SubChildObjectToUnrelatedObject()
        {
            //  P
            // |====================| |====================|
            //      |   C          L   E |
            //      +->|============|    |
            //          E   |      L     |
            //              +->|====|----+
            //                  E  L

            var parent = UnityInstance.Resolve<StubParentPageObject>();

            var child = parent.DoResolve<StubChildPageObject>();

            var subchild = child.DoResolve<StubSubChildPageObject>();

            var to = subchild.DoResolve<StubPageObjectB>();

            Assert.AreEqual(0, parent.OnEnterCalls);
            Assert.AreEqual(1, parent.OnLeaveCalls);
            Assert.AreEqual(1, child.OnEnterCalls);
            Assert.AreEqual(1, child.OnLeaveCalls);
            Assert.AreEqual(1, subchild.OnEnterCalls);
            Assert.AreEqual(1, subchild.OnLeaveCalls);
            Assert.AreEqual(1, to.OnEnterCalls);
            Assert.AreEqual(0, to.OnLeaveCalls);
        }

        [TestMethod]
        [TestCategory("ResolveEvents")]
        public void ParentObjectToScopeObjectTest()
        {
            //  P
            // |================|
            //       |    C
            //       +-> |======|
            //            E

            var parent = UnityInstance.Resolve<StubParentPageObject>();

            var scope = parent.DoResolve<StubScopeObject>();

            Assert.AreEqual(0, parent.OnEnterCalls);
            Assert.AreEqual(0, parent.OnLeaveCalls);
        }

        [TestMethod]
        [TestCategory("ResolveEvents")]
        public void ScopeObjectToParentObject()
        {
            //  P
            // |================|
            //  C        |
            // |======| -+
            //       L

            var scope = UnityInstance.Resolve<StubScopeObject>();

            var parent = scope.DoResolve<StubParentPageObject>();

            Assert.AreEqual(0, parent.OnEnterCalls);
            Assert.AreEqual(0, parent.OnLeaveCalls);
        }

        [TestMethod]
        [TestCategory("ResolveEvents")]
        public void ChildPageObjectToUnrelatedChildPageObject()
        {
            //  P
            // |====================| |====================|
            //      |   C          L   E
            //      +->|============| |============|
            //          E   |      L   E          L
            //              +->|====|>|====|
            //                  E  L   E  L

            var parentA = UnityInstance.Resolve<StubParentPageObject>();

            var childA = parentA.DoResolve<StubChildPageObject>();

            var subChildA = childA.DoResolve<StubSubChildPageObject>();

            var subChildB = subChildA.DoResolve<StubPageObjectASubChild>();

            Assert.AreEqual(0, parentA.OnEnterCalls);
            Assert.AreEqual(1, parentA.OnLeaveCalls);
            Assert.AreEqual(1, childA.OnEnterCalls);
            Assert.AreEqual(1, childA.OnLeaveCalls);
            Assert.AreEqual(1, subChildA.OnEnterCalls);
            Assert.AreEqual(1, subChildA.OnLeaveCalls);
            Assert.AreEqual(1, subChildB.OnEnterCalls);

            var childB = subChildB.DoResolve<StubPageObjectAChild>();

            Assert.AreEqual(1, subChildB.OnLeaveCalls);
            Assert.AreEqual(1, childB.OnEnterCalls);

            var parentB = childB.DoResolve<StubPageObjectA>();

            Assert.AreEqual(1, childB.OnLeaveCalls);
            Assert.AreEqual(1, parentB.OnEnterCalls);
            Assert.AreEqual(0, parentB.OnLeaveCalls);
        }
    }
}
