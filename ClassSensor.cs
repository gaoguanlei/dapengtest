using HZH_Controls.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dapengtest
{
    public class ClassSensor
    {
		public enum SensorType
		{
			Volt,
			mAmp,
			RS485
		}

		public string Name;

		public string AliasName;

		public SensorType Type;

		public short IDinDB;

		public string XUnit;

		public float XMin;

		public float XMax;

		public string YUnit;

		public float YMin;

		public float YMax;

		public float x0;

		public float y0;

		public float x1;

		public float y1;

		public bool Valid;

		public short RawValue;

		public float Value;

		public float AccValue;

		public string ValFormat;

		public bool AL;

		public float LAL;

		public float HAL;

		public string LALTIPS;

		public string HALTIPS;

		public bool ALARM;

		public ClassDB wtf = new ClassDB();


		public void GetSettingsFromDB()
		{
    //        if (!wtf.IsConnect)
    //        {
				//FrmDialog.ShowDialog((System.Windows.Forms.IWin32Window)this, "数据库对象尚未实例化，不能进行任何数据库操作！", "警告！");
				////Interaction.MsgBox((object)"数据库对象尚未实例化，不能进行任何数据库操作！", (MsgBoxStyle)0, (object)null);
    //        }
    //        else
    //        {
                wtf.ReadSetting_Sensor(ref IDinDB, ref AliasName, ref XUnit, ref XMin, ref XMax, ref YUnit, ref YMin, ref YMax, ref x0, ref y0, ref x1, ref y1, ref AL, ref LAL, ref HAL, ref LALTIPS, ref HALTIPS);
				Console.WriteLine(AliasName);
        //    }
        }

		public void RefreshValue(short RawVal)
		{
			//rawvalue = rawval;
			Value = y0 + ((float)RawValue - x0) * (y1 - y0) / (x1 - x0);
		}
	}
}
