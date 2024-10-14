using ReGenesis.Enums.System;
using UnityEngine;

public static class Logger
{
    public static LOGLEVEL logLevel = LOGLEVEL.ALL;

    private static bool IsEnable(LOGLEVEL level)
    {
        if (level <= logLevel)
            return true;

        return false;
    }

    private static void LogInternal(LOGLEVEL level, string format, params object[] args)
    {
        if (IsEnable(level) == false)
            return;

        switch (level)
        {
            case LOGLEVEL.LOG:
                Debug.LogFormat(format, args);
                break;

            case LOGLEVEL.WARNING:
                Debug.LogWarningFormat(format, args);
                break;

            case LOGLEVEL.ERROR:
                Debug.LogErrorFormat(format, args);
                break;
        }
    }

    private static void LogInternal(LOGLEVEL level, Object context, string format, params object[] args)
    {
        if (IsEnable(level) == false)
            return;

        switch (level)
        {
            case LOGLEVEL.LOG:
                Debug.LogFormat(context, format, args);
                break;

            case LOGLEVEL.WARNING:
                Debug.LogWarningFormat(context, format, args);
                break;

            case LOGLEVEL.ERROR:
                Debug.LogErrorFormat(context, format, args);
                break;
        }
    }

    #region Log
    public static void Log(object message)
    {
        LogInternal(LOGLEVEL.LOG, "{0}", message);
    }

    public static void Log(object message, Object context)
    {
        LogInternal(LOGLEVEL.LOG, context, "{0}", message);
    }

    public static void LogFormat(string format, params object[] args)
    {
        LogInternal(LOGLEVEL.LOG, format, args);
    }

    public static void LogFormat(Object context, string format, params object[] args)
    {
        LogInternal(LOGLEVEL.LOG, context, format, args);
    }
    #endregion

    #region LogWarning
    public static void LogWarning(object message)
    {
        LogInternal(LOGLEVEL.WARNING, "{0}", message);
    }

    public static void LogWarning(object message, Object context)
    {
        LogInternal(LOGLEVEL.WARNING, context, "{0}", message);
    }

    public static void LogWarningFormat(string format, params object[] args)
    {
        LogInternal(LOGLEVEL.WARNING, format, args);
    }

    public static void LogWarningFormat(Object context, string format, params object[] args)
    {
        LogInternal(LOGLEVEL.WARNING, context, format, args);
    }
    #endregion

    #region LogError
    public static void LogError(object message)
    {
        LogInternal(LOGLEVEL.ERROR, "{0}", message);
    }

    public static void LogError(object message, Object context)
    {
        LogInternal(LOGLEVEL.ERROR, context, "{0}", message);
    }

    public static void LogErrorFormat(string format, params object[] args)
    {
        LogInternal(LOGLEVEL.ERROR, format, args);
    }

    public static void LogErrorFormat(Object context, string format, params object[] args)
    {
        LogInternal(LOGLEVEL.ERROR, context, format, args);
    }
    #endregion
}