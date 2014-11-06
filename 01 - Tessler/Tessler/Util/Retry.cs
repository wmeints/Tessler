using System;
using System.Collections.Generic;
using System.Threading;

namespace InfoSupport.Tessler.Util
{
    public class Retry
    {
        public string name;
        private Func<bool> action;

        private Action onSuccess;
        private Action onFail;

        private TimeSpan interval = TimeSpan.Zero;
        private TimeSpan timeout = TimeSpan.Zero;

        private bool anyException;
        private List<Type> exceptions;

        private Retry(string name, Func<bool> action)
        {
            this.name = name;
            this.action = action;
            this.exceptions = new List<Type>();
        }

        public Retry AcceptAnyException()
        {
            anyException = true;

            return this;
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
            var start = DateTime.Now;
            var end = start.Add(timeout);
            int count = 0;

            do
            {
                count++;

                try
                {
                    Log.Info(string.Format("Executing task '{0}', attempt {1}...", name, count));
                    if (action())
                    {
                        if (onSuccess != null) onSuccess();
                        return;
                    }
                    else
                    {
                        Log.Warn(string.Format("Task '{0}' attempt {1} failed...", name, count));
                    }
                }
                catch (Exception e)
                {
                    if (!anyException && !exceptions.Contains(e.GetType()))
                    {
                        if (onFail != null) onFail();
                        throw;
                    }
                    else
                    {
                        Log.Warn(string.Format("Task '{0}' attempt {1} failed after exception thrown: {2}", name, count, e.Message));
                    }
                }
                Thread.Sleep(interval);
            } while (DateTime.Now < end);

            try
            {
                Log.Warn(string.Format("Executing task '{0}' one last time after {1} attempts...", name, count));
                if (action())
                {
                    if (onSuccess != null) onSuccess();
                    return;
                }
            }
            catch
            {
                Log.Error(string.Format("Task '{0}' failed", name));
                if(onFail != null) onFail();

                throw;
            }

            if (onFail != null) onFail();
        }

        public static Retry Create(string name, Func<bool> action)
        {
            return new Retry(name, action);
        }
    }
}