using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLCCommunicationComponent;

namespace PLCDeviceMonitor
{
    /// <summary>
    /// record class
    /// </summary>
    public class Record
    {
        public const int BOX_CODE_SIZE = 27;
        public String BoxCodeDevice;
        public String AmountDevice;
        public String BoxCode = String.Empty;
        public short Amount = 0;
        public short ReadMark = 0;

        public Record(String _BoxCodeDevice, String _AmountDevice)
        {
            BoxCodeDevice = _BoxCodeDevice;
            AmountDevice = _AmountDevice;
        }

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

        public RecordLine(String _PalletizerName, String _ReadMarkDevice, String _PlateCodeDevice)
        {
            PalletizerName = _PalletizerName;
            ReadMarkDevice = _ReadMarkDevice;
            PlateCodeDevice = _PlateCodeDevice;
        }

        public void AddRecord(Record value)
        {
            if (null != value)
                Records.Add(value);
        }

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
    }
}
