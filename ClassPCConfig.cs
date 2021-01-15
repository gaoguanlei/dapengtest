using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dapengtest
{
    class ClassPCConfig
    {
		public string Name;

		public string MAC;

		public bool WithZoneCtrl;

		public bool WithEnvCtrl;

		public bool WithFlowMeterDsp;

		public bool WithMixPHECDsp;

		public bool WithAtmspTHDsp;

		public bool WithSoilTHDsp;

		public byte nSVs;

		public byte nFerTanks;

		public bool WithFerTankSel;

		public bool WithQcCtrl;

		public bool WithQkCtrl;

		public bool WithZoneAICtrl;

		public bool WithEnvAICtrl;

		public bool WithIoT;

		public string IoTConnStr;

		public int IoTTerminalID;

		public string IoTGetIPUrl;

		public ClassPCConfig()
		{
			nFerTanks = 3;
			IoTGetIPUrl = "http://www.taobao.com/help/getip.php";
		}
	}
}
