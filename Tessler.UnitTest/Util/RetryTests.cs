﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InfoSupport.Tessler.Util;
using System.Diagnostics;

namespace InfoSupport.Tessler.UnitTest.Util
{
    [TestClass]
    public class RetryTests
    {
        [TestMethod]
        public void RetryTest()
        {
            bool isCalled = false;

            Retry.Create(() =>
            {
                isCalled = true;
                return true;
            })
            .SetInterval(TimeSpan.FromSeconds(0.1))
            .SetTimeout(TimeSpan.FromSeconds(1))
            .Start();

            Assert.IsTrue(isCalled);
        }

        [TestMethod]
        public void RetryOnSuccessTest()
        {
            bool isCalled = false;

            Retry.Create(() => true)
            .OnSuccess(() => isCalled = true)
            .SetInterval(TimeSpan.FromSeconds(0.1))
            .SetTimeout(TimeSpan.FromSeconds(1))
            .Start();

            Assert.IsTrue(isCalled);
        }

        [TestMethod]
        public void RetryOnFailTest()
        {
            bool isCalled = false;

            Retry.Create(() => false)
            .OnFail(() => isCalled = true)
            .SetInterval(TimeSpan.FromSeconds(0.1))
            .SetTimeout(TimeSpan.FromSeconds(0.2))
            .Start();

            Assert.IsTrue(isCalled);
        }

        [TestMethod]
        public void RetryTimesTest()
        {
            int times = 0;
            bool isCalled = false;

            Retry.Create(() => ++times == 10)
            .OnSuccess(() => isCalled = true)
            .SetInterval(TimeSpan.FromSeconds(0.01))
            .SetTimeout(TimeSpan.FromSeconds(0.1))
            .Start();

            Assert.AreEqual(10, times);
            Assert.IsTrue(isCalled);
        }

        [TestMethod]
        public void RetryAllowedExceptionTest()
        {
            int times = 0;
            bool isCalled = false;

            Retry.Create(() =>
            {
                if(++times == 10) return true;

                throw new InvalidOperationException();
            })
            .AddException<InvalidOperationException>()
            .OnSuccess(() => isCalled = true)
            .SetInterval(TimeSpan.FromSeconds(0.01))
            .SetTimeout(TimeSpan.FromSeconds(0.1))
            .Start();

            Assert.AreEqual(times, 10);
            Assert.IsTrue(isCalled);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RetryDisallowedExceptionTest()
        {
            Retry.Create(() =>
            {
                throw new InvalidOperationException();
            })
            .Start();
        }

        [TestMethod]
        public void RetryTimeoutTest()
        {
            int times = 0;
            bool isCalled = false;

            Retry.Create(() =>
            {
                times++;

                return false;
            })
            .OnFail(() => isCalled = true)
            .SetInterval(TimeSpan.FromSeconds(0.01))
            .SetTimeout(TimeSpan.FromSeconds(0.1))
            .Start();

            Assert.AreEqual(11, times);
            Assert.IsTrue(isCalled);
        }

        [TestMethod]
        public void RetryTimeoutExceptionTest()
        {
            int times = 0;
            bool isCalled = false;
            bool isThrown = false;

            try
            {
                Retry.Create(() =>
                {
                    times++;

                    throw new InvalidOperationException();
                })
                .AddException<InvalidOperationException>()
                .OnFail(() => isCalled = true)
                .SetInterval(TimeSpan.FromSeconds(0.01))
                .SetTimeout(TimeSpan.FromSeconds(0.1))
                .Start();
            }
            catch (InvalidOperationException)
            {
                isThrown = true;
            }

            Assert.AreEqual(11, times);
            Assert.IsTrue(isCalled);
            Assert.IsTrue(isThrown);
        }

        [TestMethod]
        public void RetryTimingsTest()
        {
            int times = 0;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Retry.Create(() =>
            {
                times++;

                return false;
            })
            .SetInterval(TimeSpan.FromSeconds(0.1))
            .SetTimeout(TimeSpan.FromSeconds(0.5))
            .Start();

            stopwatch.Stop();

            Assert.AreEqual(6, times);
            Assert.IsTrue(stopwatch.ElapsedMilliseconds > 400);
        }
    }
}
