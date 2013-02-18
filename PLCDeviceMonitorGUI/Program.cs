using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace PLCDeviceMonitorGUI
{
    static class Program
    {
        static Mutex SingleAppMutex = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool isCreated;
            SingleAppMutex = new Mutex(true, "PLCDeviceMonitorGUI", out isCreated);

            if (Properties.Settings.Default.SingleInstance && isCreated)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else
                MessageBox.Show("无法启动程序，另一个程序实例正在运行. \n改变程序运行设置以允许多个实例，或者关闭当前正在运行的实例.", "启动失败");
        }
    }
}
