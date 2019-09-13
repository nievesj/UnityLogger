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
        private static LogType _logLevel;

        public static LogType LogLevel
        {
            get => _logLevel;
            set
            {
                _logLevel = value;
                Debug.unityLogger.logEnabled = LogLevel != LogType.None;
            }
        }

        public static void Log(object message, string hexColor = "#acafb2")
        {
            if ((LogLevel == LogType.All || LogLevel == LogType.Message) && LogLevel != LogType.None)
                Log(message, LogType.Message, hexColor);
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
                Log("", LogType.Exception, "", exception);
        }

        private static void Log(object message, LogType logType, string hexColor = "#acafb2", Exception exception = null)
        {
            if (Debug.unityLogger.logEnabled)
            {
                switch (logType)
                {
                    case LogType.Message:
                        Debug.Log(Debug.isDebugBuild ? $"<color={hexColor}>{message}</color>\n" : message);
                        break;
                    case LogType.Warning:
                        Debug.LogWarning(Debug.isDebugBuild ? $"<color=#ADFF2F>{message}</color>\n" : message);
                        break;
                    case LogType.Error:
                        Debug.LogError(Debug.isDebugBuild ? $"<color=#FF0000>{message}</color>\n" : message);
                        break;
                    case LogType.Assertion:
                        Debug.LogAssertion(Debug.isDebugBuild ? $"<color=#FF0000>{message}</color>\n" : message);
                        break;
                    case LogType.Exception:
                        Debug.LogException(exception);
                        break;
                }
            }
        }
    }
}