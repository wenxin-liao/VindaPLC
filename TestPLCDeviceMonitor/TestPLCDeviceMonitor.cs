using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PLCDeviceMonitor;

namespace TestPLCDeviceMonitor
{
    [TestClass]
    public class TestPLCDeviceMonitor
    {
        static DeviceMonitorConfig config = new DeviceMonitorConfig();

        public TestPLCDeviceMonitor()
        {
            String patternStr = "%logger - %date - %level - T[%thread] - [ %message ] %exception%newline";
            String filename = @"log\PLC-Test-Log";
            Log.AddFileLogger(filename, patternStr);

            config.BackupFilename = "backup.txt";
            config.DBConnectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.101.233)(PORT=1524))(CONNECT_DATA=(SERVICE_NAME=test)));User Id=cuxwms;Password=cuxwms;";
            config.LogicalStationNum = 1;
            config.MonitorInterval = 500;
            config.RecordConfigFilename = @"D:\Vincent\My Documents\Projects\VindaPLC\PLCDeviceMonitor\Records.xml";
        }

        [TestMethod]
        public void TestConfig()
        {
            try
            {
                PLCDeviceMonitor.DeviceMonitor monitor = PLCDeviceMonitor.DeviceMonitor.GenInstance(config);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
