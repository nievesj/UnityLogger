using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace UnityLogger
{
    public enum LogType
    {
        All,
        None,
        Message,
        Warning,
        Error,
        Assertion,
        Exception
    }

    public static class Logger
    {
        public static LogType LogLevel { get; set; }

        public static void Log(object message)
        {
            if ((LogLevel == LogType.All || LogLevel == LogType.Message) && LogLevel != LogType.None)
                Log(message, LogType.Message);
        }

        public static void LogWarning(object message)
        {
            if ((LogLevel == LogType.All || LogLevel == LogType.Warning) && LogLevel != LogType.None)
                Log(message, LogType.Warning);
        }

        public static void LogError(object message)
        {
            if ((LogLevel == LogType.All || LogLevel == LogType.Error) && LogLevel != LogType.None)
                Log(message, LogType.Error);
        }

        [Conditional("UNITY_ASSERTIONS")]
        public static void LogAssertion(object message)
        {
            if ((LogLevel == LogType.All || LogLevel == LogType.Assertion) && LogLevel != LogType.None)
                Log(message, LogType.Assertion);
        }

        public static void LogException(Exception exception)
        {
            if ((LogLevel == LogType.All || LogLevel == LogType.Exception) && LogLevel != LogType.None)
                Log("", LogType.Exception, exception);
        }

        private static void Log(object message, LogType logType, Exception exception = null)
        {
            if (Debug.unityLogger.logEnabled)
                switch (logType)
                {
                    case LogType.Message:
                        Debug.Log(message);
                        break;
                    case LogType.Warning:
                        Debug.LogWarning($"<color={"#FFFF00"}>{message}</color>");
                        break;
                    case LogType.Error:
                        Debug.LogError($"<color={"#FF0000"}>{message}</color>");
                        break;
                    case LogType.Assertion:
                        Debug.LogAssertion($"<color={"#FF0000"}>{message}</color>");
                        break;
                    case LogType.Exception:
                        Debug.LogException(exception);
                        break;
                }
        }
    }
}