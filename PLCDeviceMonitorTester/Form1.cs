using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PLCCommunicationComponent;
using PLCDeviceMonitor;

namespace PLCDeviceMonitorTester
{
    public partial class MainFrom : Form
    {
        PLCCom m_PLCCom = null;
        bool m_Connected = false;

        public MainFrom()
        {
            InitializeComponent();
        }

        private void ConnButton_Click(object sender, EventArgs e)
        {
            try
            {
                m_PLCCom = new PLCCom(int.Parse(LogicStationNum.Text));
                int ret = m_PLCCom.Open();
                if (0 == ret)
                {
                    LinkStatus.Text = "连通";
                    LogicStationNum.Enabled = false;
                    ConnButton.Enabled = false;
                    ReadButton.Enabled = true;
                    WriteButton.Enabled = true;
                    m_Connected = true;
                }
                else
                    throw new Exception(String.Format("连接失败. 0x{0:x8} [HEX]", ret));
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "错误");
            }
        }

        private void ReadButton_Click(object sender, EventArgs e)
        {
            if (!m_Connected)
                return;

            String DeviceName = DeviceAddr.Text;
            int DeviceSize;
            if (!int.TryParse(DeviceNum.Text, out DeviceSize))
            {
                DeviceSize = 1;
                DeviceNum.Text = DeviceSize.ToString();
            }

            try
            {
                String data = CommUtil.GetStringFromDevice(DeviceName, DeviceSize * 2, m_PLCCom);
                StringBuilder msg = new StringBuilder();
                foreach (Char c in data)
                {
                    msg.Append((short)(c));
                    msg.Append(" ");
                }
                DeviceData.Text = msg.ToString();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "错误");
            }
        }

        private void WriteButton_Click(object sender, EventArgs e)
        {
            if (!m_Connected)
                return;

            String DeviceName = DeviceAddr.Text;
            String[] values = DeviceData.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder data = new StringBuilder();

            try
            {
                foreach (String v in values)
                {
                    byte c = byte.Parse(v);
                    data.Append((Char)(c));
                }

                String dataStr = data.ToString();
                CommUtil.SetStringToDevice(dataStr, DeviceName, dataStr.Length, m_PLCCom);

                MessageBox.Show("写入成功", "信息");    
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "错误");           	
            }
        }
    }
}
