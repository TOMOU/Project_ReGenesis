using UnityEngine;

public static class Logger
{
    public static Constant.LOGLEVEL logLevel = Constant.LOGLEVEL.ALL;

    private static bool IsEnable(Constant.LOGLEVEL level)
    {
        if (level <= logLevel)
            return true;

        return false;
    }

    private static void LogInternal(Constant.LOGLEVEL level, string format, params object[] args)
    {
        if (IsEnable(level) == false)
            return;

        switch (level)
        {
            case Constant.LOGLEVEL.LOG:
                Debug.LogFormat(format, args);
                break;

            case Constant.LOGLEVEL.WARNING:
                Debug.LogWarningFormat(format, args);
                break;

            case Constant.LOGLEVEL.ERROR:
                Debug.LogErrorFormat(format, args);
                break;
        }
    }

    private static void LogInternal(Constant.LOGLEVEL level, Object context, string format, params object[] args)
    {
        if (IsEnable(level) == false)
            return;

        switch (level)
        {
            case Constant.LOGLEVEL.LOG:
                Debug.LogFormat(context, format, args);
                break;

            case Constant.LOGLEVEL.WARNING:
                Debug.LogWarningFormat(context, format, args);
                break;

            case Constant.LOGLEVEL.ERROR:
                Debug.LogErrorFormat(context, format, args);
                break;
        }
    }

    #region Log
    public static void Log(object message)
    {
        LogInternal(Constant.LOGLEVEL.LOG, "{0}", message);
    }

    public static void Log(object message, Object context)
    {
        LogInternal(Constant.LOGLEVEL.LOG, context, "{0}", message);
    }

    public static void LogFormat(string format, params object[] args)
    {
        LogInternal(Constant.LOGLEVEL.LOG, format, args);
    }

    public static void LogFormat(Object context, string format, params object[] args)
    {
        LogInternal(Constant.LOGLEVEL.LOG, context, format, args);
    }
    #endregion

    #region LogWarning
    public static void LogWarning(object message)
    {
        LogInternal(Constant.LOGLEVEL.WARNING, "{0}", message);
    }

    public static void LogWarning(object message, Object context)
    {
        LogInternal(Constant.LOGLEVEL.WARNING, context, "{0}", message);
    }

    public static void LogWarningFormat(string format, params object[] args)
    {
        LogInternal(Constant.LOGLEVEL.WARNING, format, args);
    }

    public static void LogWarningFormat(Object context, string format, params object[] args)
    {
        LogInternal(Constant.LOGLEVEL.WARNING, context, format, args);
    }
    #endregion

    #region LogError
    public static void LogError(object message)
    {
        LogInternal(Constant.LOGLEVEL.ERROR, "{0}", message);
    }

    public static void LogError(object message, Object context)
    {
        LogInternal(Constant.LOGLEVEL.ERROR, context, "{0}", message);
    }

    public static void LogErrorFormat(string format, params object[] args)
    {
        LogInternal(Constant.LOGLEVEL.ERROR, format, args);
    }

    public static void LogErrorFormat(Object context, string format, params object[] args)
    {
        LogInternal(Constant.LOGLEVEL.ERROR, context, format, args);
    }
    #endregion
}