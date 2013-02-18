using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Core;

namespace PLCDeviceMonitorGUI
{
    public class ControlAppender : log4net.Appender.IAppender
    {
        ILogControl m_LogControl;
        String m_Name;

        public ControlAppender(ILogControl control)
        {
            m_LogControl = control;
            m_Name = "ControlAppender";
        }

        public void Close()
        {
            m_LogControl = null;
        }

        public void DoAppend(LoggingEvent loggingEvent)
        {
            if (null != m_LogControl)
                m_LogControl.DoLog(loggingEvent);
        }

        public String Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }
    }

    public interface ILogControl
    {
        void DoLog(LoggingEvent loggingEvent);
    }
}
