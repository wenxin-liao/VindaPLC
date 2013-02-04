// PLCCommunicationComponent.h

#pragma once

using namespace System;
using namespace System::Collections::Generic;
using namespace System::Runtime::InteropServices;

namespace PLCCommunicationComponent {
    /*
    * plc device communication util class
    */
	public ref class PLCCom
	{
        //<------------------------data members------------------------>
        const static int BUFFER_SIZE = 128;
        ACTMULTILib::ActEasyIFClass ^m_Com;

    public:
        //<---------------------------interface--------------------------->
        /*
        * constructor with logical station num
        */
		PLCCom( int logicalStationNum )
        {
            logicalStationNum = System::Math::Min(logicalStationNum, 65535);
            logicalStationNum = System::Math::Max(logicalStationNum, 0);

            m_Com = gcnew ACTMULTILib::ActEasyIFClass();
            m_Com->ActLogicalStationNumber = logicalStationNum;
        }
        /*
        * open communication link
        */
        int Open()
        {
            return m_Com->Open();
        }
        /*
        * close communication link
        */
        int Close()
        {
            return m_Com->Close();
        }
        /*
        * set logical station num
        * range 0-15
        */
        int SetLogicalStationNum( int _LogicalStationNum )
        {
            _LogicalStationNum = Math::Max(0, _LogicalStationNum);
            _LogicalStationNum = Math::Min(15, _LogicalStationNum);

            m_Com->ActLogicalStationNumber = _LogicalStationNum;
            return 0;
        }
        /*
        * get device value
        */
        int GetDevice( String ^deviceName, short %data )
        {
            return m_Com->GetDevice2(deviceName, data);
        }
        /*
        * set device value
        */
        int SetDevice( String ^deviceName, short data )
        {
            return m_Com->SetDevice2(deviceName, data);
        }
        /*
        * get device block
        */
        int ReadDeviceBlock( String ^deviceName , array<char> ^data)
        {
            if ( data->Length > BUFFER_SIZE )
                return -1;

            char buffer[BUFFER_SIZE] = {0};

            int deviceNum = (int)System::Math::Ceiling(data->Length/2.0);
            int ret = m_Com->ReadDeviceBlock2( deviceName, deviceNum, (short %)buffer );

            for (int i = 0; i<data->Length; ++i)
                data[i] = buffer[i];

            return ret;
        }
        /*
        * set device block
        */
        int WriteDeviceBlock( String ^deviceName, array<char> ^data)
        {
            if ( data->Length > BUFFER_SIZE )
                return -1;

            char buffer[BUFFER_SIZE] = {0};

            for (int i = 0; i < data->Length; ++i)
                buffer[i] = data[i];

            int deviceNum = (int)System::Math::Ceiling(data->Length/2.0);
            int ret = m_Com->WriteDeviceBlock2(deviceName, deviceNum, (short %)buffer);

            return ret;
        }
	};
}
