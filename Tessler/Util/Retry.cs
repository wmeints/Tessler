using System;
using System.Collections.Generic;
using System.Threading;

namespace InfoSupport.Tessler.Util
{
    public class Retry
    {
        private Func<bool> action;

        private Action onSuccess;
        private Action onFail;

        private TimeSpan interval = TimeSpan.Zero;
        private TimeSpan timeout = TimeSpan.Zero;

        private List<Type> exceptions;

        private Retry(Func<bool> action)
        {
            this.action = action;
            this.exceptions = new List<Type>();
        }

        public Retry AddException<TException>()
            where TException : Exception
        {
            exceptions.Add(typeof(TException));

            return this;
        }

        public Retry OnFail(Action action)
        {
            onFail = action;

            return this;
        }

        public Retry OnSuccess(Action action)
        {
            onSuccess = action;

            return this;
        }

        public Retry SetInterval(TimeSpan interval)
        {
            this.interval = interval;

            return this;
        }

        public Retry SetTimeout(TimeSpan timeout)
        {
            this.timeout = timeout;

            return this;
        }

        public void Start()
        {
            TimeSpan timeSpan = new TimeSpan();

            do
            {
                try
                {
                    if (action())
                    {
                        if (onSuccess != null) onSuccess();
                        return;
                    }
                }
                catch (Exception e)
                {
                    if (!exceptions.Contains(e.GetType()))
                    {
                        if (onFail != null) onFail();
                        throw;
                    }
                }

                Thread.Sleep(interval);
                timeSpan = timeSpan.Add(interval);
            } while (timeSpan < timeout);

            try
            {
                if (action())
                {
                    if (onSuccess != null) onSuccess();
                    return;
                }
            }
            catch
            {
                onFail();

                throw;
            }

            if (onFail != null) onFail();
        }

        public static Retry Create(Func<bool> action)
        {
            return new Retry(action);
        }
    }
}