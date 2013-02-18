using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PLCDeviceMonitorLogDatabase
{
    public static class PLCDeviceMonitorLogDatabaseDriver
    {
        static PLCDeviceMonitorLogDatabaseAccessorDataContext DataContext = new PLCDeviceMonitorLogDatabaseAccessorDataContext();

        public static void InsertLogEvent(LogEvent e)
        {
            DataContext.LogEvents.InsertOnSubmit(e);
            DataContext.SubmitChanges();
        }
    }
}
