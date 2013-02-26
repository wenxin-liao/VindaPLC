using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using log4net.Appender;
using log4net.Core;
using Oracle.DataAccess.Client;
using PLCCommunicationComponent;

namespace PLCDeviceMonitor
{
    public class MonitorTask
    {
        public DataProcessor dataProcessor;
        public RecordLine recordLine;
    }

    /// <summary>
    /// device monitor config class
    /// </summary>
    public class DeviceMonitorConfig
    {
        public int LogicalStationNum = 0;
        public int MonitorInterval = 500;
        public String RecordConfigFilename = null;
        public String DBConnectionString = null;
        public String BackupFilename = null;
        public String MsgFormatter = String.Empty;
        public String LogFilename = null;
        public String LogFormatter = null;
        public bool ValidatePlateCode = true;
        public bool ValidateBoxCode = false;
        public bool ValidateRecordNum = true;
        public int DBRetryInterval = 1000;
        public int DBRetryTimes = 3;
    }

    /// <summary>
    /// data process class
    /// </summary>
    public class DataProcessor
    {
        static IAppender BackupAppender;
        static String DBConnectionString;
        static String MsgFormatter;
        static bool ValidatePlateCode;
        static bool ValidateBoxCode;
        static bool ValidateRecordNum;
        static int DBRetryInterval;
        static int DBRetryTimes;

        OracleConnection m_DBConnection;

        static public void Initialize(DeviceMonitorConfig _Config)
        {
            ValidatePlateCode = _Config.ValidatePlateCode;
            ValidateBoxCode = _Config.ValidateBoxCode;
            ValidateRecordNum = _Config.ValidateRecordNum;

            DBRetryInterval = _Config.DBRetryInterval;
            DBRetryTimes = _Config.DBRetryTimes;

            DBConnectionString = _Config.DBConnectionString;

            InitializeMsgFromatter(_Config.MsgFormatter);
            InitializeBackupFile(_Config.BackupFilename);
        }

        static private void InitializeMsgFromatter(string _MsgFormatter)
        {
            MsgFormatter = _MsgFormatter;

            MsgFormatter = MsgFormatter.Replace("%PalletizerName", "{0}");
            MsgFormatter = MsgFormatter.Replace("%PlateCode", "{1}");
            MsgFormatter = MsgFormatter.Replace("%BoxCode&Amount", "{2}");
        }

        static private void InitializeBackupFile(String _Filename)
        {
            try
            {
                if (!String.IsNullOrEmpty(_Filename) && null == BackupAppender)
                {
                    String patternStr = "%date : %message%newline";

                    log4net.Layout.PatternLayout layout = new log4net.Layout.PatternLayout(patternStr);

                    log4net.Appender.RollingFileAppender appender = new log4net.Appender.RollingFileAppender();
                    appender.RollingStyle = log4net.Appender.RollingFileAppender.RollingMode.Date;
                    appender.Layout = layout;
                    appender.File = _Filename;
                    appender.DatePattern = "\"_\"yyyyMMdd\".txt\"";
                    appender.StaticLogFileName = false;
                    appender.ActivateOptions();

                    BackupAppender = appender;
                }
            }
            catch (System.Exception ex)
            {
                BackupAppender = null;
                Log.Error("初始化备份文件失败.", ex);
            }
        }

        static private void ValidateData(RecordLine recordLine)
        {
            {
                bool hasInvalidChar = false;
                foreach (Char c in recordLine.PlateCode)
                    hasInvalidChar |= !Char.IsDigit(c);

                if (hasInvalidChar)
                {
                    String msg = "托盘码发现非法字符.";
                    if (ValidatePlateCode)
                        throw new Exception(msg);
                    else
                        Log.Warn(msg);
                }
            }

            {
                bool allEmpty = true;
                foreach (Record record in recordLine.Records)
                    allEmpty &= record.ReadMark == 0;

                if (allEmpty)
                {
                    String msg = "货物数量为 0.";
                    if (ValidateRecordNum)
                        throw new Exception(msg);
                    else
                        Log.Warn(msg);
                }
            }

            {
                bool hasInvalidChar = false;
                foreach (Record record in recordLine.Records)
                    if (record.ReadMark != 0)
                        foreach (Char c in record.BoxCode)
                            hasInvalidChar |= !Char.IsLetterOrDigit(c);

                if (hasInvalidChar)
                {
                    String msg = "货物箱码发现非法字符.";
                    if (ValidatePlateCode)
                        throw new Exception(msg);
                    else
                        Log.Warn(msg);
                }
            }
        }

        static private String GenDataMessage(RecordLine _RecordLine)
        {
            StringBuilder records = new StringBuilder();
            foreach (Record record in _RecordLine.Records)
                if (record.ReadMark > 0)
                    records.AppendFormat("{0}:{1};", record.BoxCode, record.Amount);

            String result = String.Format(MsgFormatter, _RecordLine.PalletizerName, _RecordLine.PlateCode, records.ToString());
            return result;
        }

        static private void SaveToFile(String data)
        {
            if (null != BackupAppender)
            {
                try
                {
                    LoggingEventData e = new LoggingEventData()
                    {
                        TimeStamp = DateTime.Now,
                        Message = data,
                    };
                    BackupAppender.DoAppend(new LoggingEvent(e));
                }
                catch (System.Exception ex)
                {
                    throw new Exception(String.Format("保存数据到备份文件失败. {0}", ex));
                }
            }
        }

        public void InitializeDBConnection()
        {
            try
            {
                if (!String.IsNullOrEmpty(DBConnectionString) && m_DBConnection == null)
                {
                    m_DBConnection = new OracleConnection(DBConnectionString);
                    m_DBConnection.Open();
                }
            }
            catch (System.Exception)
            {
                m_DBConnection = null;
                Log.Error(String.Format("初始化数据库连接失败. 数据库连接字：[{0}].", DBConnectionString));
                throw;
            }
        }

        public void ProcessData(RecordLine recordLine)
        {
            if (null == recordLine || 0 == recordLine.ReadMark)
                return;

            try
            {
                ValidateData(recordLine);
                String dataStr = GenDataMessage(recordLine);

                Log.Info(String.Format("生成数据报文：[{0}].", dataStr));

                SaveToDB(dataStr);
                SaveToFile(dataStr);

                recordLine.Processed = true;

                Log.Info("数据处理完成.");
            }
            catch (System.Exception ex)
            {
                Log.Error("数据处理异常.", ex);
            }
        }

        private void SaveToDB(String data)
        {
            InitializeDBConnection();
            if (null == m_DBConnection)
                return;

            OracleCommand cmd = m_DBConnection.CreateCommand();
            cmd.CommandText = "apps.cux_wms_warehouse_processor.process_request";
            cmd.CommandType = CommandType.StoredProcedure;

            OracleParameter retParam = cmd.Parameters.Add("ret", OracleDbType.Varchar2, ParameterDirection.Output);
            OracleParameter statusParam = cmd.Parameters.Add("status", OracleDbType.Varchar2, ParameterDirection.Output);
            OracleParameter dataParam = cmd.Parameters.Add("data", OracleDbType.Varchar2, data, ParameterDirection.Input);

            retParam.Size = 32;
            statusParam.Size = 2048;

            int retryTime = 1;
            while (true)
            {
                try
                {
                    cmd.ExecuteNonQuery();
                    break;
                }
                catch (System.Exception)
                {
                    Log.Warn(String.Format("第 {0} 次尝试保存数据到数据库失败.", retryTime));
                    if (++retryTime > DBRetryTimes)
                    {
                        m_DBConnection.Dispose();
                        m_DBConnection = null;

                        Log.Error("保存数据到数据库失败，连接将于下一次访问时被重置.");
                        throw;
                    }

                    Thread.Sleep(DBRetryInterval);
                }
            }
        }
    }

    /// <summary>
    /// device monitor
    /// </summary>
    public class DeviceMonitor
    {
        DeviceMonitorConfig m_Config;
        PLCCom m_Com;
        Object m_IOLock;
        CancellationTokenSource m_CancelToken;
        List<Thread> m_MonitorThreads;
        List<RecordLine> m_RecordLines;
        List<DataProcessor> m_DataProcessors;

        DeviceMonitor()
        {
            m_IOLock = new Object();
            m_Com = new PLCCom(0);
            m_RecordLines = new List<RecordLine>();
            m_DataProcessors = new List<DataProcessor>();
        }

        void InitializeCommComponent()
        {
            m_Config.LogicalStationNum = Math.Max(0, m_Config.LogicalStationNum);
            m_Config.LogicalStationNum = Math.Min(15, m_Config.LogicalStationNum);

            m_Com.SetLogicalStationNum(m_Config.LogicalStationNum);
            Log.Info(String.Format("初始化逻辑站号：{0}.", m_Config.LogicalStationNum));
        }

        void InitializeMonitorInterval()
        {
            m_Config.MonitorInterval = Math.Max(10, m_Config.MonitorInterval);
            Log.Info(String.Format("初始化监测时间间隔：{0}.", m_Config.MonitorInterval));
        }

        void InitializeRecordLines()
        {
            try
            {
                m_RecordLines = RecordLine.ReadFromXmlFile(m_Config.RecordConfigFilename);
                Log.Info(String.Format("初始化码垛信息软元件配置成功. 已配置 {0} 条码垛线路.", m_RecordLines.Count));
            }
            catch (System.Exception ex)
            {
                Log.Error("初始化码垛信息软元件配置失败.", ex);
                throw;
            }
        }

        void InitializeDataProcessors()
        {
            try
            {
                DataProcessor.Initialize(m_Config);

                for (int i = 0; i < m_RecordLines.Count; ++i)
                    m_DataProcessors.Add(new DataProcessor());

                foreach (DataProcessor dp in m_DataProcessors)
                    dp.InitializeDBConnection();

                Log.Info("初始化数据处理单元组成功,");
            }
            catch (System.Exception ex)
            {
                Log.Error("初始化数据处理单元失败.", ex);
            }
        }

        void Monitor(Object p)
        {
            MonitorTask monitorTask = p as MonitorTask;
            if (null == monitorTask)
                return;

            while (!m_CancelToken.IsCancellationRequested)
            {
                lock (m_IOLock)
                {
                    monitorTask.recordLine.ReadData(m_Com);
                }

                monitorTask.dataProcessor.ProcessData(monitorTask.recordLine);

                if (0 != monitorTask.recordLine.ReadMark && monitorTask.recordLine.Processed)
                {
                    lock (m_IOLock)
                    {
                        monitorTask.recordLine.ResetReadMark(m_Com);
                    }
                }
                Thread.Sleep(m_Config.MonitorInterval);
            }
        }

        public void Start()
        {
            if (null != m_MonitorThreads)
                return;

            int ret = m_Com.Open();
            if (0 == ret)
            {
                m_MonitorThreads = new List<Thread>();
                m_CancelToken = new CancellationTokenSource();

                for (int i = 0; i < m_RecordLines.Count; ++i )
                {
                    Thread processThread = new Thread(new ParameterizedThreadStart(Monitor)) { IsBackground = true, Name = m_RecordLines[i].PalletizerName };
                    m_MonitorThreads.Add(processThread);

                    MonitorTask task = new MonitorTask() { recordLine = m_RecordLines[i], dataProcessor = m_DataProcessors[i] };
                    processThread.Start(task);
                }
                Log.Info("设备监听已启动.");
            }
            else
            {
                String msg = String.Format("打开设备读写链路失败. 错误码：0x{0:x8} [HEX].", ret);
                Log.Error(msg);
                throw new Exception(msg);
            }
        }

        public void Stop()
        {
            if (null != m_CancelToken && null != m_MonitorThreads)
            {
                m_CancelToken.Cancel();
                foreach (Thread processThread in m_MonitorThreads)
                    processThread.Join();

                m_CancelToken = null;
                m_MonitorThreads = null;

                m_Com.Close();

                Log.Info("工作线程已退出，监听已停止.");
            }
        }

        public static DeviceMonitor GenInstance(DeviceMonitorConfig config)
        {
            if (null == config)
                throw new Exception("配置对象为空.");

            DeviceMonitor monitor = new DeviceMonitor();
            monitor.m_Config = config;

            monitor.InitializeCommComponent();
            monitor.InitializeMonitorInterval();
            monitor.InitializeRecordLines();
            monitor.InitializeDataProcessors();

            return monitor;
        }
    }
}