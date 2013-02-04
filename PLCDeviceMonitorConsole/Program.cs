using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using PLCDeviceMonitor;
using System.Threading;
using PLCCommunicationComponent;

namespace PLCDeviceMonitorConsole
{
    class Program
    {
        //void Test1(PLCCom);

        static void Main(string[] args)
        {
            try
            {
                int LogicalStationNum = Convert.ToInt32(ConfigurationManager.AppSettings["LogicalStationNum"]);
                Log.AddConsoleLogger(ConfigurationManager.AppSettings["LogFormatter"]);
                Log.OpenLog("PLC-Console");

                PLCCom com = new PLCCom(LogicalStationNum);
                int ret = com.Open();
                if (0 != ret)
                    throw new Exception("Open com fail.");

                RecordLine line1 = new RecordLine("MD1", "D01001", "D01002");
                Record record1_1 = new Record("D01012", "D01026");
                Record record1_2 = new Record("D01032", "D01046");

                RecordLine line2 = new RecordLine("MD2", "D01101", "D01102");
                Record record2_1 = new Record("D01112", "D01126");
                Record record2_2 = new Record("D01132", "D01146");

                RecordLine line3 = new RecordLine("MD3", "D01201", "D01202");
                Record record3_1 = new Record("D01212", "D01226");
                Record record3_2 = new Record("D01232", "D01246");

                RecordLine line4 = new RecordLine("MD4", "D01301", "D01302");
                Record record4_1 = new Record("D01312", "D01326");
                Record record4_2 = new Record("D01332", "D01346");


                if (false)
                {
                    Console.WriteLine("Test read mark.");
                    while (true)
                    {
                        short lineMark1 = CommUtil.GetDevice(line1.ReadMarkDevice, com);
                        if (lineMark1 != 0)
                            Console.WriteLine(String.Format("{2},{3} -- {0}'s read mark is set to {1}.", line1.PalletizerName, lineMark1, DateTime.Now, DateTime.Now.Millisecond));

                        short lineMark2 = CommUtil.GetDevice(line2.ReadMarkDevice, com);
                        if (lineMark2 != 0)
                            Console.WriteLine(String.Format("{2},{3} -- {0}'s read mark is set to {1}.", line2.PalletizerName, lineMark2, DateTime.Now, DateTime.Now.Millisecond));

                        short lineMark3 = CommUtil.GetDevice(line3.ReadMarkDevice, com);
                        if (lineMark3 != 0)
                            Console.WriteLine(String.Format("{2},{3} -- {0}'s read mark is set to {1}.", line3.PalletizerName, lineMark3, DateTime.Now, DateTime.Now.Millisecond));

                        short lineMark4 = CommUtil.GetDevice(line4.ReadMarkDevice, com);
                        if (lineMark4 != 0)
                            Console.WriteLine(String.Format("{2},{3} -- {0}'s read mark is set to {1}.", line4.PalletizerName, lineMark4, DateTime.Now, DateTime.Now.Millisecond));
                    }
                }
                else
                {
                    while (true)
                    {
                        Console.Write("\nPush enter to write data.");
                        Console.ReadLine();

                        line1.SetPlateCode("11111111", com);
                        record1_1.SetBoxCode("1A0000000000000000000000000", com);
                        record1_1.SetAmount(11, com);
                        record1_2.SetBoxCode("1B0000000000000000000000000", com);
                        record1_2.SetAmount(12, com);
                        CommUtil.SetDevice(1, line1.ReadMarkDevice, com);

                        line2.SetPlateCode("22222222", com);
                        record2_1.SetBoxCode("2A0000000000000000000000000", com);
                        record2_1.SetAmount(21, com);
                        record2_2.SetBoxCode("2B0000000000000000000000000", com);
                        record2_2.SetAmount(22, com);
                        CommUtil.SetDevice(1, line2.ReadMarkDevice, com);

                        line3.SetPlateCode("33333333", com);
                        record3_1.SetBoxCode("3A0000000000000000000000000", com);
                        record3_1.SetAmount(31, com);
                        record3_2.SetBoxCode("3B0000000000000000000000000", com);
                        record3_2.SetAmount(32, com);
                        CommUtil.SetDevice(1, line3.ReadMarkDevice, com);

                        line4.SetPlateCode("44444444", com);
                        record4_1.SetBoxCode("4A0000000000000000000000000", com);
                        record4_1.SetAmount(41, com);
                        record4_2.SetBoxCode("4B0000000000000000000000000", com);
                        record4_2.SetAmount(42, com);
                        CommUtil.SetDevice(1, line4.ReadMarkDevice, com);
                    }
                }

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
