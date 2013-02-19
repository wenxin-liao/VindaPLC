using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PLCDeviceMonitorLogDatabase
{
    static public class PLCDeviceMonitorLogDatabaseDriver
    {
        static PLCDeviceMonitorLogDatabaseAccessorDataContext DataContext = new PLCDeviceMonitorLogDatabaseAccessorDataContext();

        static public void InsertLogEvent(LogEvent e)
        {
            DataContext.LogEvents.InsertOnSubmit(e);
            DataContext.SubmitChanges();
        }
    }
}
