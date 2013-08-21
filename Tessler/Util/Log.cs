using System;
using InfoSupport.Tessler.Unity;
using log4net;

namespace InfoSupport.Tessler.Util
{
    public static class Log
    {
        private const string TesslerLogPrefix = "[Tessler] ";

        public static void Debug(object message)
        {
            GetLogger().Debug(TesslerLogPrefix + message);
        }

        public static void Debug(object message, Exception exception)
        {
            GetLogger().Debug(TesslerLogPrefix + message, exception);
        }

        public static void Debug(string format, params object[] args)
        {
            GetLogger().Debug(TesslerLogPrefix + string.Format(format, args));
        }

        public static void Error(object message)
        {
            GetLogger().Error(TesslerLogPrefix + message);
        }

        public static void Error(object message, Exception exception)
        {
            GetLogger().Error(TesslerLogPrefix + message, exception);
        }

        public static void ErrorFormat(string format, params object[] args)
        {
            GetLogger().Error(TesslerLogPrefix + string.Format(format, args));
        }

        public static void Fatal(object message)
        {
            GetLogger().Fatal(TesslerLogPrefix + message);
        }

        public static void Fatal(object message, Exception exception)
        {
            GetLogger().Fatal(TesslerLogPrefix + message, exception);
        }

        public static void FatalFormat(string format, params object[] args)
        {
            GetLogger().Fatal(TesslerLogPrefix + string.Format(format, args));
        }

        public static void Info(object message)
        {
            GetLogger().Info(TesslerLogPrefix + message);
        }

        public static void Info(object message, Exception exception)
        {
            GetLogger().Info(TesslerLogPrefix + message, exception);
        }

        public static void InfoFormat(string format, params object[] args)
        {
            GetLogger().Info(TesslerLogPrefix + string.Format(format, args));
        }

        public static void Warn(object message)
        {
            GetLogger().Warn(TesslerLogPrefix + message);
        }

        public static void Warn(object message, Exception exception)
        {
            GetLogger().Warn(TesslerLogPrefix + message, exception);
        }

        public static void WarnFormat(string format, params object[] args)
        {
            GetLogger().Warn(TesslerLogPrefix + string.Format(format, args));
        }

        public static ILog GetLogger()
        {
            return UnityInstance.Resolve<ILog>();
        }
    }
}