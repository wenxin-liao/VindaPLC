using System;
using System.Text;
using PLCCommunicationComponent;

namespace PLCDeviceMonitor
{
    /// <summary>
    /// communication util class
    /// </summary>
    public static class CommUtil
    {
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

        public static short GetDevice(String _DeviceName, PLCCom _Com)
        {
            short result = 0;
            int ret = _Com.GetDevice(_DeviceName, ref result);

            if (0 != ret)
                throw new Exception(String.Format("从设备中读取数据失败. 错误码：0x{0:x8} [HEX]. 软元件地址：[{1}].", ret, _DeviceName));

            return result;
        }

        public static void SetDevice(short _Data, String _DeviceName, PLCCom _Com)
        {
            int ret = _Com.SetDevice(_DeviceName, _Data);

            if (0 != ret)
                throw new Exception(String.Format("写入数据到设备失败. 错误码：0x{0:x8} [HEX]. 软元件地址：[{1}].", ret, _DeviceName));
        }
    }
}
