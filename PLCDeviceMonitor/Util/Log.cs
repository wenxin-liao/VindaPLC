using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace PLCDeviceMonitor
{
    /// <summary>
    /// log class
    /// </summary>
    public static class Log
    {
        static ILog m_Logger = null;
        static List<log4net.Appender.IAppender> m_Appenders = new List<log4net.Appender.IAppender>();

        static public void AddAppender(log4net.Appender.IAppender appender)
        {
            if (null != appender)
                m_Appenders.Add(appender);
        }

        static public void AddConsoleLogger(String patternStr)
        {
            if (String.IsNullOrEmpty(patternStr))
                patternStr = "%message%newline";

            log4net.Layout.PatternLayout layout = new log4net.Layout.PatternLayout(patternStr);
            log4net.Appender.ConsoleAppender appender = new log4net.Appender.ConsoleAppender();

            appender.Layout = layout;
            m_Appenders.Add(appender);
        }

        static public void AddFileLogger(String filename, String patternStr)
        {
            if (!String.IsNullOrEmpty(filename))
            {
                if (String.IsNullOrEmpty(patternStr))
                    patternStr = "%message%newline";

                log4net.Layout.PatternLayout layout = new log4net.Layout.PatternLayout(patternStr);

                log4net.Appender.RollingFileAppender appender = new log4net.Appender.RollingFileAppender();
                appender.RollingStyle = log4net.Appender.RollingFileAppender.RollingMode.Date;
                appender.Layout = layout;
                appender.File = filename;
                appender.DatePattern = "\"_\"yyyyMMdd\".txt\"";
                appender.StaticLogFileName = false;
                appender.ActivateOptions();

                m_Appenders.Add(appender);
            }
        }

        static public void OpenLog(String name)
        {
            log4net.Config.BasicConfigurator.Configure(m_Appenders.ToArray());
            m_Logger = LogManager.GetLogger(String.Format("{0} [{1}]", name, System.Diagnostics.Process.GetCurrentProcess().Id));
        }

        static public void Debug(String msg)
        {
            if (null != m_Logger)
                m_Logger.Debug(msg);
        }

        static public void Debug(String msg, Exception ex)
        {
            if (null != m_Logger)
                m_Logger.Debug(msg, ex);
        }

        static public void Warn(String msg)
        {
            if (null != m_Logger)
                m_Logger.Warn(msg);
        }

        static public void Warn(String msg, Exception ex)
        {
            if (null != m_Logger)
                m_Logger.Warn(msg, ex);
        }

        static public void Error(String msg)
        {
            if (null != m_Logger)
                m_Logger.Error(msg);
        }

        static public void Error(String msg, Exception ex)
        {
            if (null != m_Logger)
                m_Logger.Error(msg, ex);
        }

        static public void Info(String msg)
        {
            if (null != m_Logger)
                m_Logger.Info(msg);
        }

        static public void Info(String msg, Exception ex)
        {
            if (null != m_Logger)
                m_Logger.Info(msg, ex);
        }

        static public void Fatal(String msg)
        {
            if (null != m_Logger)
                m_Logger.Fatal(msg);
        }

        static public void Fatal(String msg, Exception ex)
        {
            if (null != m_Logger)
                m_Logger.Fatal(msg, ex);
        }
    }
}
