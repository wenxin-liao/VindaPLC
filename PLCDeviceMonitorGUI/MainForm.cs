using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using log4net.Core;
using PLCDeviceMonitor;
using PLCDeviceMonitorLogDatabase;
using System.Collections.Generic;
using System.ComponentModel;

namespace PLCDeviceMonitorGUI
{
    /// <summary>
    /// main form class
    /// </summary>
    public partial class MainForm : Form, ILogControl
    {
        PLCDeviceMonitor.DeviceMonitorConfig config;
        PLCDeviceMonitor.DeviceMonitor monitor;
        Object logLock = new Object();

        String logFilename;
        String logFormatter;

        BackgroundWorker showLogWorker;
        BackgroundWorker initializeWorker;
        BackgroundWorker startWorker;

        ShareQueue<LoggingEvent> logEventQueue;

        /// <summary>
        /// constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            InitializeGUI();

            initializeWorker.RunWorkerAsync();
        }

        /// <summary>
        /// implement interface of ILogControl
        /// </summary>
        public void DoLog(LoggingEvent loggingEvent)
        {
            logEventQueue.Enqueue(loggingEvent);
        }

        private void InitializeGUI()
        {
            this.Text = Properties.Settings.Default.Title;
            Thread.CurrentThread.Name = "Main";

            logEventQueue = new ShareQueue<LoggingEvent>();

            initializeWorker = new BackgroundWorker();
            initializeWorker.DoWork += new DoWorkEventHandler(InitializeMonitor);
            initializeWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(InitializeComplete);

            startWorker = new BackgroundWorker();
            startWorker.DoWork += new DoWorkEventHandler(StartMonitor);
            startWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(StartComplete);

            showLogWorker = new BackgroundWorker();
            showLogWorker.DoWork += new DoWorkEventHandler(ShowLog);
            showLogWorker.RunWorkerAsync();
        }

        /// <summary>
        /// initialize config
        /// </summary>
        private void InitializeConfiguration()
        {
            config = new PLCDeviceMonitor.DeviceMonitorConfig();

            config.LogicalStationNum = Properties.Settings.Default.LogicalStationNum;
            config.MonitorInterval = Properties.Settings.Default.MoniterInterval;

            config.RecordConfigFilename = Properties.Settings.Default.RecordConfigFilename;

            config.BackupFilename = Properties.Settings.Default.BackupFilename;
            config.DBConnectionString = Properties.Settings.Default.OracleDB;

            config.DBRetryInterval = Properties.Settings.Default.DBRetryInterval;
            config.DBRetryTimes = Properties.Settings.Default.DBRetryTimes;

            config.MsgFormatter = Properties.Settings.Default.MsgFormatter;

            config.ValidatePlateCode = Properties.Settings.Default.ValidatePlateCode;
            config.ValidateBoxCode = Properties.Settings.Default.ValidateBoxCode;
            config.ValidateRecordNum = Properties.Settings.Default.ValidateRecordNum;

            config.LogFilename = Properties.Settings.Default.LogFilename;
            config.LogFormatter = Properties.Settings.Default.LogFormatter;
        }

        /// <summary>
        /// initialize logger
        /// </summary>
        private void InitializeLogger()
        {
            logFilename = Properties.Settings.Default.LogFilename;
            logFormatter = Properties.Settings.Default.LogFormatter;

            PLCDeviceMonitor.Log.AddFileLogger(logFilename, logFormatter);
            PLCDeviceMonitor.Log.AddAppender(new ControlAppender(this));

            PLCDeviceMonitor.Log.OpenLog("PLC-GUI");
        }

        void ShowLog(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (true)
                {
                    LoggingEvent loggingEvent = logEventQueue.Dequeue();

                    LogEvent item = new LogEvent()
                    {
                        Time = loggingEvent.TimeStamp,
                        Level = loggingEvent.Level.ToString(),
                        ThreadName = loggingEvent.ThreadName,
                        Msg = loggingEvent.MessageObject.ToString(),
                        Exception = loggingEvent.ExceptionObject == null ? null : loggingEvent.ExceptionObject.ToString(),
                    };

                    //PLCDeviceMonitorLogDatabaseDriver.InsertLogEvent(item);

                    String msg = String.Format("{0} - {1},{2:D3} - {3} - T[{4}] - [ {5} ] {6}\n",
                        loggingEvent.LoggerName,
                        loggingEvent.TimeStamp,
                        loggingEvent.TimeStamp.Millisecond,
                        loggingEvent.Level,
                        loggingEvent.ThreadName,
                        loggingEvent.MessageObject,
                        loggingEvent.ExceptionObject);

                    Color color = Color.Black;
                    switch (loggingEvent.Level.Name)
                    {
                        case "DEBUG":
                        case "INFO":
                            color = Color.Navy;
                            break;
                        case "WARN":
                            color = Color.DarkGoldenrod;
                            break;
                        case "ERROR":
                        case "FATAL":
                            color = Color.Red;
                            break;
                    }

                    MethodInvoker invoker = () =>
                    {
                        int start = LogTextBox.Text.Length;
                        LogTextBox.AppendText(msg);
                        LogTextBox.Select(start, msg.Length);
                        LogTextBox.SelectionColor = color;
                        LogTextBox.SelectionLength = 0;
                    };

                    Invoke(invoker);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void InitializeMonitor(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.Name = "Main";

            InitializeLogger();
            Log.Info("初始化日志系统成功.");

            InitializeConfiguration();
            Log.Info("初始化配置对象成功.");

            monitor = DeviceMonitor.GenInstance(config);
        }

        void InitializeComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (InvokeRequired)
            {
                MethodInvoker invoker = () => InitializeComplete(sender, e);
                Invoke(invoker);
                return;
            }

            StartMonitorButton.Enabled = true;
            if (!String.IsNullOrEmpty(config.BackupFilename))
                BackupFileButton.Enabled = true;

            if (!String.IsNullOrEmpty(logFilename))
                LogFileButton.Enabled = true;
        }

        void StartMonitor(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.Name = "Main";

            try
            {
                monitor.Start();
                e.Result = true;
            }
            catch (System.Exception ex)
            {
                Log.Error("设备监控启动失败.", ex);
                e.Result = false;
            }
        }

        void StartComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (InvokeRequired)
            {
                MethodInvoker invoker = () => StartComplete(sender, e);
                Invoke(invoker);
                return;
            }

            if (null != e.Result)
            {
                if ((bool)(e.Result))
                    StopMonitorButton.Enabled = true;
                else
                    StartMonitorButton.Enabled = true;
            }
        }

        /// <summary>
        /// event handler of start monitor button click
        /// start monitor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartMonitorButton_Click(object sender, EventArgs e)
        {
            StartMonitorButton.Enabled = false;
            startWorker.RunWorkerAsync();
        }

        /// <summary>
        /// event handler of stop monitor button click
        /// stop monitor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopMonitorButton_Click(object sender, EventArgs e)
        {
            try
            {
                StopMonitorButton.Enabled = false;
                monitor.Stop();
                StartMonitorButton.Enabled = true;
            }
            catch (System.Exception ex)
            {
                StopMonitorButton.Enabled = true;
                Log.Error("Stop monitor fail.", ex);
            }
        }

        /// <summary>
        /// event handler of back file button click
        /// open back file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackupFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(config.BackupFilename))
                    System.Diagnostics.Process.Start(Path.GetFullPath(Path.GetDirectoryName(config.BackupFilename)));
            }
            catch (System.Exception ex)
            {
            	Log.Error("Open backup file fail.", ex);
            }
        }

        /// <summary>
        /// event handler of log file button click
        /// open dir where the log file save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(logFilename))
                    System.Diagnostics.Process.Start(Path.GetFullPath(Path.GetDirectoryName(logFilename)));
            }
            catch (System.Exception ex)
            {
                Log.Error("Open log file fail.", ex);
            }
        }

        /// <summary>
        /// event handler of closing form
        /// stop monitor before exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (StopMonitorButton.Enabled)
                StopMonitorButton_Click(null, null);

            logEventQueue.Shutdown();
        }
    }
}
