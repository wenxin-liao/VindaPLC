using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using log4net;
using Oracle.DataAccess.Client;
using PLCCommunicationComponent;
using log4net.Core;

namespace PLCDeviceMonitor
{
    /// <summary>
    /// communication util class
    /// </summary>
    public static class CommUtil
    {
        /// <summary>
        /// get data from device, return as string
        /// </summary>
        /// <param name="_DeviceName"></param>
        /// <param name="_DataSize"></param>
        /// <param name="_Com"></param>
        /// <returns></returns>
        public static String GetStringFromDevice(String _DeviceName, int _DataSize, PLCCom _Com)
        {
            sbyte[] buffer = new sbyte[_DataSize];
            Array.Clear(buffer, 0, _DataSize);

            int ret = _Com.ReadDeviceBlock(_DeviceName, buffer);
            if (0 != ret)
                throw new Exception(String.Format("从设备中读取数据失败. 错误码：0x{0:x8} [HEX]. 软元件地址：[{1}].", ret, _DeviceName));

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < _DataSize; ++i)
                result.Append((Char)buffer[i]);

            return result.ToString();
        }

        /// <summary>
        /// set data to device
        /// </summary>
        /// <param name="_Data"></param>
        /// <param name="_DeviceName"></param>
        /// <param name="_DataSize"></param>
        /// <param name="_Com"></param>
        public static void SetStringToDevice(String _Data, String _DeviceName, int _DataSize, PLCCom _Com)
        {
            sbyte[] buffer = new sbyte[_DataSize];
            Array.Clear(buffer, 0, _DataSize);

            int len = Math.Min(_Data.Length, _DataSize);
            for (int i = 0; i < _DataSize; ++i)
                buffer[i] = (sbyte)_Data[i];

            int ret = _Com.WriteDeviceBlock(_DeviceName, buffer);
            if (0 != ret)
                throw new Exception(String.Format("写入数据到设备失败. 错误码：0x{0:x8} [HEX]. 软元件地址：[{1}].", ret, _DeviceName));
        }

        /// <summary>
        /// get value of single device, retuen as short
        /// </summary>
        /// <param name="_DeviceName"></param>
        /// <param name="_Com"></param>
        /// <returns></returns>
        public static short GetDevice(String _DeviceName, PLCCom _Com)
        {
            short result = 0;
            int ret = _Com.GetDevice(_DeviceName, ref result);

            if (0 != ret)
                throw new Exception(String.Format("从设备中读取数据失败. 错误码：0x{0:x8} [HEX]. 软元件地址：[{1}].", ret, _DeviceName));

            return result;
        }

        /// <summary>
        /// set value of single device
        /// </summary>
        /// <param name="_Data"></param>
        /// <param name="_DeviceName"></param>
        /// <param name="_Com"></param>
        public static void SetDevice(short _Data, String _DeviceName, PLCCom _Com)
        {
            int ret = _Com.SetDevice(_DeviceName, _Data);

            if (0 != ret)
                throw new Exception(String.Format("写入数据到设备失败. 错误码：0x{0:x8} [HEX]. 软元件地址：[{1}].", ret, _DeviceName));
        }
    }

    /// <summary>
    /// record class
    /// </summary>
    public class Record
    {
        public const int BOX_CODE_SIZE = 27;

        public short ReadMark = 0;

        public String BoxCodeDevice;
        public String AmountDevice;

        public String BoxCode = String.Empty;
        public short Amount = 0;

        /// <summary>
        /// contructor with given devices' name
        /// </summary>
        /// <param name="_plateCodeDN"></param>
        /// <param name="_BoxCode"></param>
        /// <param name="_AmountDN"></param>
        public Record(String _BoxCodeDevice, String _AmountDevice)
        {
            BoxCodeDevice = _BoxCodeDevice;
            AmountDevice = _AmountDevice;
        }

        /// <summary>
        /// get box code data from device
        /// </summary>
        /// <param name="_Com"></param>
        public void GetBoxCode(PLCCom _Com)
        {
            try
            {
                String tmpStr = CommUtil.GetStringFromDevice(BoxCodeDevice, BOX_CODE_SIZE, _Com);

                StringBuilder regularStr = new StringBuilder();
                foreach (Char c in tmpStr)
                    regularStr.Append(Char.IsLetterOrDigit(c) ? c : '#');

                BoxCode = regularStr.ToString();

                Log.Info(String.Format("箱码读取成功. 软元件地址：[{0}]. 箱码：[{1}].", BoxCodeDevice, BoxCode));
            }
            catch (System.Exception ex)
            {
                Log.Error("箱码读取失败.", ex);
                throw;
            }
        }

        /// <summary>
        /// get amount data from device
        /// </summary>
        /// <param name="_Com"></param>
        public void GetAmount(PLCCom _Com)
        {
            try
            {
                Amount = CommUtil.GetDevice(AmountDevice, _Com);
                Log.Info(String.Format("数量读取成功. 软元件地址：[{0}]. 数量：[{1}].", AmountDevice, Amount));
            }
            catch (System.Exception ex)
            {
                Log.Error("数量读取失败.", ex);
                throw;
            }
        }

        /// <summary>
        /// set box code to device
        /// </summary>
        /// <param name="_BoxCode"></param>
        /// <param name="_Com"></param>
        public void SetBoxCode(String _BoxCode, PLCCom _Com)
        {
            try
            {
                BoxCode = _BoxCode;

                CommUtil.SetStringToDevice(BoxCode, BoxCodeDevice, BOX_CODE_SIZE, _Com);
                Log.Info(String.Format("箱码写入成功. 软元件地址：[{0}]. 箱码：[{1}].", BoxCodeDevice, BoxCode));
            }
            catch (System.Exception ex)
            {
                Log.Error("箱码写入失败.", ex);
            }
        }

        /// <summary>
        /// set amount to device
        /// </summary>
        /// <param name="_Amount"></param>
        /// <param name="_Com"></param>
        public void SetAmount(short _Amount, PLCCom _Com)
        {
            try
            {
                Amount = _Amount;

                CommUtil.SetDevice(Amount, AmountDevice, _Com);
                Log.Info(String.Format("数量写入成功. 软元件地址：[{0}]. 数量：[{1}].", AmountDevice, Amount));
            }
            catch (System.Exception ex)
            {
                Log.Error("数量写入失败.", ex);
            }
        }

        public void ReadData(PLCCom _Com)
        {
            ReadMark = CommUtil.GetDevice(AmountDevice, _Com);
            if (ReadMark > 0)
            {
                GetBoxCode(_Com);
                GetAmount(_Com);
            }
        }
    }

    /// <summary>
    /// record line class
    /// </summary>
    public class RecordLine
    {
        public const int PLATE_CODE_SIZE = 8;

        public short ReadMark = 0;
        public bool Processed = true;

        public String PalletizerName;

        public String ReadMarkDevice;
        public String PlateCodeDevice;

        public String PlateCode = String.Empty;

        public List<Record> Records = new List<Record>();

        /// <summary>
        /// constructor with given device name
        /// </summary>
        /// <param name="_ReadMarkDN"></param>
        /// <param name="_DeviceName"></param>
        public RecordLine(String _PalletizerName, String _ReadMarkDevice, String _PlateCodeDevice)
        {
            PalletizerName = _PalletizerName;
            ReadMarkDevice = _ReadMarkDevice;
            PlateCodeDevice = _PlateCodeDevice;
        }

        /// <summary>
        /// add record to record line
        /// </summary>
        /// <param name="value"></param>
        public void AddRecord(Record value)
        {
            if (null != value)
                Records.Add(value);
        }

        public static List<RecordLine> ReadFromXmlFile(String _XmlFilename)
        {
            List<RecordLine> result = new List<RecordLine>();

            if (!String.IsNullOrEmpty(_XmlFilename))
            {
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(_XmlFilename);

                System.Xml.XmlNodeList lines = doc.GetElementsByTagName("RecordLine");
                foreach (System.Xml.XmlNode line in lines)
                {
                    System.Xml.XmlElement lineEle = line as System.Xml.XmlElement;
                    String palletizer = lineEle.GetAttribute("PalletizerName");
                    String readMarkDevice = lineEle.GetAttribute("ReadMarkDevice");
                    String plateCodeDevice = lineEle.GetAttribute("PlateCodeDevice");

                    System.Xml.XmlNodeList records = lineEle.GetElementsByTagName("Record");
                    if (records.Count > 0)
                    {
                        RecordLine recordLine = new RecordLine(palletizer, readMarkDevice, plateCodeDevice);
                        foreach (System.Xml.XmlNode record in records)
                        {
                            System.Xml.XmlElement recordEle = record as System.Xml.XmlElement;
                            String boxCodeDevice = recordEle.GetAttribute("BoxCodeDevice");
                            String amountDevice = recordEle.GetAttribute("AmountDevice");
                            recordLine.AddRecord(new Record(boxCodeDevice, amountDevice));
                        }
                        result.Add(recordLine);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// get plate code data from device
        /// </summary>
        /// <param name="_Com"></param>
        public void GetPlateCode(PLCCom _Com)
        {
            try
            {
                String tmpStr = CommUtil.GetStringFromDevice(PlateCodeDevice, PLATE_CODE_SIZE, _Com);

                StringBuilder regularStr = new StringBuilder();
                foreach (Char c in tmpStr)
                    regularStr.Append(Char.IsLetterOrDigit(c) ? c : '#');

                PlateCode = regularStr.ToString();

                Log.Info(String.Format("托盘码读取成功. 软元件地址：[{0}]. 托盘码：[{1}].", PlateCodeDevice, PlateCode));
            }
            catch (System.Exception ex)
            {
                Log.Error("托盘码读取失败.", ex);
                throw;
            }
        }

        /// <summary>
        /// set plate code to device
        /// </summary>
        /// <param name="_plateCode"></param>
        /// <param name="_Com"></param>
        public void SetPlateCode(String _PlateCode, PLCCom _Com)
        {
            try
            {
                PlateCode = _PlateCode;

                CommUtil.SetStringToDevice(PlateCode, PlateCodeDevice, PLATE_CODE_SIZE, _Com);
                Log.Info(String.Format("托盘码写入成功. 软元件地址：[{0}]. 托盘码：Data [{1}]", PlateCodeDevice, PlateCode));
            }
            catch (System.Exception ex)
            {
                Log.Error("托盘码写入失败.", ex);
            }
        }

        public void ReadData(PLCCom _Com)
        {
            try
            {
                ReadMark = CommUtil.GetDevice(ReadMarkDevice, _Com);
                if (0 != ReadMark)
                {
                    Log.Info(String.Format("检测读取标志已设置为 [{0}]. 开始读取数据.", ReadMark));

                    GetPlateCode(_Com);
                    foreach (Record record in Records)
                        record.ReadData(_Com);

                    Processed = false;
                }
            }
            catch (System.Exception ex)
            {
                Log.Error("数据读取失败.", ex);
            }
        }

        public void ResetReadMark(PLCCom _Com)
        {
            try
            {
                CommUtil.SetDevice(0, ReadMarkDevice, _Com);
                Log.Info("重设读取标志成功.");
            }
            catch (System.Exception ex)
            {
                Log.Error("重设读取标志失败.", ex);
            }
        }
    }

    /// <summary>
    /// data process class
    /// </summary>
    public class DataProcessor
    {
        log4net.Appender.IAppender m_BackupAppender;
        OracleConnection m_DBConnection;
        String m_MsgFormatter;
        bool m_ValidatePlateCode;
        bool m_ValidateBoxCode;
        bool m_ValidateRecordNum;
        int m_DBRetryInterval;
        int m_DBRetryTimes;

        /// <summary>
        /// initialize msg formatter
        /// </summary>
        /// <param name="_MsgFormatter"></param>
        private void InitializeMsgFromatter(string _MsgFormatter)
        {
            m_MsgFormatter = _MsgFormatter;

            m_MsgFormatter = m_MsgFormatter.Replace("%PalletizerName", "{0}");
            m_MsgFormatter = m_MsgFormatter.Replace("%PlateCode", "{1}");
            m_MsgFormatter = m_MsgFormatter.Replace("%BoxCode&Amount", "{2}");
        }

        /// <summary>
        /// intialize file writer
        /// </summary>
        /// <param name="_Filename"></param>
        void InitializeBackupFile(String _Filename)
        {
            try
            {
                if (!String.IsNullOrEmpty(_Filename) && null == m_BackupAppender)
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

                    m_BackupAppender = appender;
                }
            }
            catch (System.Exception ex)
            {
                m_BackupAppender = null;
                Log.Error("初始化备份文件失败.", ex);
            }
        }

        /// <summary>
        /// intialize db connection
        /// </summary>
        /// <param name="_DBConnectionString"></param>
        void InitializeDBConnection(String _DBConnectionString)
        {
            try
            {
                if (!String.IsNullOrEmpty(_DBConnectionString) && null == m_DBConnection)
                {
                    m_DBConnection = new OracleConnection(_DBConnectionString);
                    m_DBConnection.Open();

                    Log.Info(String.Format("初始化数据库连接成功. 数据库连接字：[{0}].", _DBConnectionString));
                }
            }
            catch (System.Exception ex)
            {
                m_DBConnection = null;
                Log.Error(String.Format("初始化数据库连接失败. 数据库连接字：[{0}].", _DBConnectionString), ex);
            }
        }

        /// <summary>
        /// initialize data processor
        /// </summary>
        /// <param name="_BKFilename"></param>
        /// <param name="_DBConnectionString"></param>
        public void Initialize(String _BKFilename, String _DBConnectionString, String _MsgFormatter, bool _ValidatePlateCode, bool _ValidateBoxCode, bool _ValidateRecordNum, int _DBRetryInterval, int _DBRetryTimes)
        {
            m_ValidatePlateCode = _ValidatePlateCode;
            m_ValidateBoxCode = _ValidateBoxCode;
            m_ValidateRecordNum = _ValidateRecordNum;

            m_DBRetryInterval = _DBRetryInterval;
            m_DBRetryTimes = _DBRetryTimes;

            InitializeMsgFromatter(_MsgFormatter);
            InitializeBackupFile(_BKFilename);
            InitializeDBConnection(_DBConnectionString);
        }

        private void ValidateData(RecordLine recordLine)
        {
            {
                bool hasInvalidChar = false;
                foreach (Char c in recordLine.PlateCode)
                    hasInvalidChar |= !Char.IsDigit(c);

                if (hasInvalidChar)
                {
                    String msg = "托盘码发现非法字符.";
                    if (m_ValidatePlateCode)
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
                    if (m_ValidateRecordNum)
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
                            hasInvalidChar |= !Char.IsDigit(c);

                if (hasInvalidChar)
                {
                    String msg = "货物箱码发现非法字符.";
                    if (m_ValidatePlateCode)
                        throw new Exception(msg);
                    else
                        Log.Warn(msg);
                }
            }
        }

        /// <summary>
        /// get formatted string from given device value
        /// </summary>
        public String GenDataMessage(RecordLine _RecordLine)
        {
            StringBuilder records = new StringBuilder();
            foreach (Record record in _RecordLine.Records)
                if (record.ReadMark > 0)
                    records.AppendFormat("{0}:{1};", record.BoxCode, record.Amount);

            String result = String.Format(m_MsgFormatter, _RecordLine.PalletizerName, _RecordLine.PlateCode, records.ToString());
            return result;
        }

        /// <summary>
        /// save data to file
        /// </summary>
        /// <param name="data"></param>
        private void SaveToFile(String data)
        {
            if (null != m_BackupAppender)
            {
                try
                {
                    LoggingEventData e = new LoggingEventData()
                    {
                        TimeStamp = DateTime.Now,
                        Message = data,
                    };
                    m_BackupAppender.DoAppend(new LoggingEvent(e));
                }
                catch (System.Exception ex)
                {
                    throw new Exception(String.Format("保存数据到备份文件失败. {0}", ex));
                }
            }
        }

        /// <summary>
        /// save data to db
        /// </summary>
        /// <param name="data"></param>
        private void SaveToDB(String data)
        {
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
                catch (System.Exception ex)
                {
                    Log.Warn(String.Format("第 {0} 次尝试保存数据到数据库失败.", retryTime));
                    if (++retryTime > m_DBRetryTimes)
                        throw new Exception(String.Format("保存数据到数据库失败. {0}", ex));

                    Thread.Sleep(m_DBRetryInterval);
                }
            }
        }

        /// <summary>
        /// threadpool callback func
        /// </summary>
        /// <param name="data"></param>
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

                Log.Info("数据处理.");
            }
            catch (System.Exception ex)
            {
                Log.Error("数据处理异常.", ex);
            }
        }
    }

    /// <summary>
    /// log class
    /// </summary>
    public static class Log
    {
        static ILog m_Logger = null;
        static List<log4net.Appender.IAppender> m_Appenders = new List<log4net.Appender.IAppender>();

        /// <summary>
        /// add custom appender
        /// </summary>
        /// <param name="appender"></param>
        public static void AddAppender(log4net.Appender.IAppender appender)
        {
            if (null != appender)
                m_Appenders.Add(appender);
        }

        /// <summary>
        /// add console appender
        /// </summary>
        /// <param name="patternStr"></param>
        public static void AddConsoleLogger(String patternStr)
        {
            if (String.IsNullOrEmpty(patternStr))
                patternStr = "%message%newline";

            log4net.Layout.PatternLayout layout = new log4net.Layout.PatternLayout(patternStr);
            log4net.Appender.ConsoleAppender appender = new log4net.Appender.ConsoleAppender();

            appender.Layout = layout;
            m_Appenders.Add(appender);
        }

        /// <summary>
        /// add rolling file appender
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="patternStr"></param>
        public static void AddFileLogger(String filename, String patternStr)
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

        /// <summary>
        /// open logger
        /// </summary>
        /// <param name="name"></param>
        public static void OpenLog(String name)
        {
            log4net.Config.BasicConfigurator.Configure(m_Appenders.ToArray());
            m_Logger = LogManager.GetLogger(String.Format("{0} [{1}]", name, System.Diagnostics.Process.GetCurrentProcess().Id));
        }

        /// <summary>
        /// debug msg
        /// </summary>
        /// <param name="msg"></param>
        public static void Debug(String msg)
        {
            if (null != m_Logger)
                m_Logger.Debug(msg);
        }

        /// <summary>
        /// debug msg & ex
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Debug(String msg, Exception ex)
        {
            if (null != m_Logger)
                m_Logger.Debug(msg, ex);
        }

        /// <summary>
        /// warn msg
        /// </summary>
        /// <param name="msg"></param>
        public static void Warn(String msg)
        {
            if (null != m_Logger)
                m_Logger.Warn(msg);
        }

        /// <summary>
        /// warn msg & ex
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Warn(String msg, Exception ex)
        {
            if (null != m_Logger)
                m_Logger.Warn(msg, ex);
        }

        /// <summary>
        /// error msg
        /// </summary>
        /// <param name="msg"></param>
        public static void Error(String msg)
        {
            if (null != m_Logger)
                m_Logger.Error(msg);
        }

        /// <summary>
        /// error msg & ex
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Error(String msg, Exception ex)
        {
            if (null != m_Logger)
                m_Logger.Error(msg, ex);
        }

        /// <summary>
        /// info msg
        /// </summary>
        /// <param name="msg"></param>
        public static void Info(String msg)
        {
            if (null != m_Logger)
                m_Logger.Info(msg);
        }

        /// <summary>
        /// info msg & ex
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Info(String msg, Exception ex)
        {
            if (null != m_Logger)
                m_Logger.Info(msg, ex);
        }

        /// <summary>
        /// fatal msg
        /// </summary>
        /// <param name="msg"></param>
        public static void Fatal(String msg)
        {
            if (null != m_Logger)
                m_Logger.Fatal(msg);
        }

        /// <summary>
        /// fatal msg & ex
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Fatal(String msg, Exception ex)
        {
            if (null != m_Logger)
                m_Logger.Fatal(msg, ex);
        }
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
    /// device monitor
    /// </summary>
    public class DeviceMonitor
    {
        Object m_IOLock;

        int m_MonitorInterval;
        List<Thread> m_MonitorThreads;
        CancellationTokenSource m_CancelToken;

        PLCCom m_Com;
        List<RecordLine> m_RecordLines;
        DataProcessor m_DataProcessor;

        /// <summary>
        /// private constructor
        /// </summary>
        DeviceMonitor()
        {
            m_IOLock = new Object();
            m_Com = new PLCCom(0);
            m_RecordLines = new List<RecordLine>();
            m_DataProcessor = new DataProcessor();
        }

        /// <summary>
        /// intialize communication component
        /// </summary>
        /// <param name="_LogicalStationNum"></param>
        void InitializeCommComponent(int _LogicalStationNum)
        {
            _LogicalStationNum = Math.Max(0, _LogicalStationNum);
            _LogicalStationNum = Math.Min(15, _LogicalStationNum);

            m_Com.SetLogicalStationNum(_LogicalStationNum);
            Log.Info(String.Format("初始化逻辑站号：{0}.", _LogicalStationNum));
        }

        /// <summary>
        /// initialize monitor interval
        /// </summary>
        /// <param name="_MonitorInterval"></param>
        void InitializeMonitorInterval(int _MonitorInterval)
        {
            _MonitorInterval = Math.Max(10, _MonitorInterval);

            m_MonitorInterval = _MonitorInterval;
            Log.Info(String.Format("初始化监测时间间隔：{0}.", _MonitorInterval));
        }

        /// <summary>
        /// initialize record lines from records configuration file
        /// </summary>
        /// <param name="_RecordConfigFilename"></param>
        void InitializeRecordLines(String _RecordConfigFilename)
        {
            try
            {
                m_RecordLines = RecordLine.ReadFromXmlFile(_RecordConfigFilename);
                Log.Info(String.Format("初始化码垛信息软元件配置成功. 已配置 {0} 条码垛线路.", m_RecordLines.Count));
            }
            catch (System.Exception ex)
            {
                Log.Error("初始化码垛信息软元件配置失败.", ex);
                throw;
            }
        }

        /// <summary>
        /// initialize data processor
        /// </summary>
        /// <param name="_BKFilename"></param>
        /// <param name="_DBConnectionString"></param>
        void InitializeDataProcessor(String _BKFilename, String _DBConnectionString, String _MsgFormatter, bool _ValidatePlateCode, bool _ValidateBoxCode, bool _ValidateRecordNum, int _DBRetryInterval, int _DBRetryTimes)
        {
            try
            {
                m_DataProcessor.Initialize(_BKFilename, _DBConnectionString, _MsgFormatter, _ValidatePlateCode, _ValidateBoxCode, _ValidateRecordNum, _DBRetryInterval, _DBRetryTimes);
            }
            catch (System.Exception ex)
            {
                Log.Error("初始化数据处理单元失败.", ex);
            }
        }

        /// <summary>
        /// start monitor
        /// </summary>
        public void Start()
        {
            if (null != m_MonitorThreads)
                return;

            int ret = m_Com.Open();
            if (0 == ret)
            {
                m_MonitorThreads = new List<Thread>();
                m_CancelToken = new CancellationTokenSource();

                foreach (RecordLine recordLine in m_RecordLines)
                {
                    Thread processThread = new Thread(new ParameterizedThreadStart(Monitor)) { IsBackground = true, Name = recordLine.PalletizerName };
                    m_MonitorThreads.Add(processThread);

                    processThread.Start(recordLine);
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

        /// <summary>
        /// stop monitor
        /// </summary>
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

        /// <summary>
        /// monitor thread callback func
        /// </summary>
        void Monitor(Object p)
        {
            RecordLine recordLine = p as RecordLine;
            if (null == recordLine)
                return;

            DateTime stamp;
            while (!m_CancelToken.IsCancellationRequested)
            {
                stamp = DateTime.Now;
                lock (m_IOLock)
                {
                    recordLine.ReadData(m_Com);
                }

                m_DataProcessor.ProcessData(recordLine);

                if (0 != recordLine.ReadMark && recordLine.Processed)
                {
                    lock (m_IOLock)
                    {
                        recordLine.ResetReadMark(m_Com);
                    }
                }

                int sleepInterval = (int)(m_MonitorInterval - (DateTime.Now - stamp).TotalMilliseconds);
                if (sleepInterval > 0)
                    Thread.Sleep(sleepInterval);
            }
        }

        /// <summary>
        /// get device monitor object by given configuration
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static DeviceMonitor GetPLCDeviceMonitor(DeviceMonitorConfig config)
        {
            if (null == config)
                throw new Exception("配置对象为空.");

            DeviceMonitor monitor = new DeviceMonitor();

            monitor.InitializeCommComponent(config.LogicalStationNum);
            monitor.InitializeMonitorInterval(config.MonitorInterval);
            monitor.InitializeRecordLines(config.RecordConfigFilename);
            monitor.InitializeDataProcessor(
                config.BackupFilename, 
                config.DBConnectionString, 
                config.MsgFormatter, 
                config.ValidatePlateCode, 
                config.ValidateBoxCode, 
                config.ValidateRecordNum, 
                config.DBRetryInterval, 
                config.DBRetryTimes);

            return monitor;
        }

        /// <summary>
        /// get communication component
        /// </summary>
        /// <returns></returns>
        public PLCCom GetCom()
        {
            return m_Com;
        }
    }
}