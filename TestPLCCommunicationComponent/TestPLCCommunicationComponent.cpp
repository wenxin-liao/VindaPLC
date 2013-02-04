#include "stdafx.h"

using namespace System;
using namespace System::Text;
using namespace System::Collections::Generic;
using namespace	Microsoft::VisualStudio::TestTools::UnitTesting;
using namespace PLCCommunicationComponent;

namespace TestPLCCommunicationComponent
{
	[TestClass]
	public ref class TestPLCCommunicationComponent
	{
        PLCCom ^com;
	public: 
        TestPLCCommunicationComponent()
        {
            com = gcnew PLCCom(1);
            com->Open();
        }

        ~TestPLCCommunicationComponent()
        {
            com->Close();
        }

        [TestMethod]
        void TestGetSetDevice()
        {
            short setData = 5;
            short getData = 0;

            Assert::AreEqual(0, com->SetDevice("D1", setData), "Set device fail.");
            Assert::AreEqual(0, com->GetDevice("D1", (short %)getData), "Get device fail.");
            Assert::AreEqual(setData, getData, "Get data and set data are not equal.");
        }

        [TestMethod]
        void TestGetSetDeviceBlock()
        {
            String ^temp = "adfad2f3g4fsaleadfwlkesalkcaoiewncnadlialdskasdkfajsdf";

            array<char> ^setData = gcnew array<char>(temp->Length);
            array<char> ^getData = gcnew array<char>(temp->Length);

            for (int i = 0; i<temp->Length; ++i)
                setData[i] = (char)temp[i];

            Assert::AreEqual(0, com->WriteDeviceBlock("D1", setData), "Set device fail.");
            Assert::AreEqual(0, com->ReadDeviceBlock("D1", getData), "Get device fail.");
            
            for (int i = 0; i<temp->Length; ++i)
                Assert::AreEqual(setData[i], getData[i], "Get data and set data are not equal.");
        }
	};
}
