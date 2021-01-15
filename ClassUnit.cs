using HZH_Controls.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace dapengtest
{
	public class ClassUnit
    {
		public enum Result
		{
			FAIL,
			SUCCESS
		}

		public string Name;

		public string AliasName;

		public ClassMB MB;

		public byte MBAddress;

		public ClassMCU MCU;

		public ClassSensor[] Sensor;

		public ClassSV[] SV;

		public ClassPump[] Pump;

		public ClassSB[] SB;

		public Result RefreshSensorsValueResult;

		public Result RefreshFromPCtoGizwitButtonValuesResult;

		public Result ExecuteSVsResult;

		public Result RefreshFromGizwitsToPCButtonValuesResult;

		public Result ExecutePumpQResult;

		public Result RefreshFromGizwitToPCResult;

		public Result RefreshFromPCtoGizwitResult;

		public Result ResetGizwitZoneBtnEvFlagResult;

		public ClassUnit()
		{
			Sensor = new ClassSensor[4];
			SV = new ClassSV[8];
			Pump = new ClassPump[2];
			SB = new ClassSB[4];
			MCU = new ClassMCU();
			checked
			{
				int num = Sensor.Length - 1;
				int num2 = 0;
				while (true)
				{
					int num3 = num2;
					int num4 = num;
					if (num3 > num4)
					{
						break;
					}
					Sensor[num2] = new ClassSensor();
					num2++;
				}
				int num5 = SV.Length - 1;
				num2 = 0;
				while (true)
				{
					int num6 = num2;
					int num4 = num5;
					if (num6 > num4)
					{
						break;
					}
					SV[num2] = new ClassSV();
					num2++;
				}
				int num7 = Pump.Length - 1;
				num2 = 0;
				while (true)
				{
					int num8 = num2;
					int num4 = num7;
					if (num8 > num4)
					{
						break;
					}
					Pump[num2] = new ClassPump();
					num2++;
				}
				int num9 = SB.Length - 1;
				num2 = 0;
				while (true)
				{
					int num10 = num2;
					int num4 = num9;
					if (num10 <= num4)
					{
						SB[num2] = new ClassSB();
						num2++;
						continue;
					}
					break;
				}
			}
		}


		public Result RefreshSensorsValue()//刷新传感器的的值
		{

            checked
            {
				if (MB == null)
				{
					//MessageBox.Show("没有实例化MB通讯对象，不能更新" + AliasName + "传感器值！", "警告！");
					FrmDialog.ShowDialog((IWin32Window)this, "没有实例化MB通讯对象，不能更新" + AliasName + "传感器值！", "警告！");
				}
				else if (MB.ReadInputRegs(ref MCU.Ain, MBAddress, 0, 4) == ClassMB.Result.SUCCESS)
				{
					int num = Sensor.Length - 1;
					int num2 = 0;
					while (true)
					{
						int num3 = num2;
						int num4 = num;
						if (num3 > num4)
						{
							break;
						}
						Sensor[num2].RawValue = MCU.Ain[num2];
						Sensor[num2].RefreshValue(MCU.Ain[num2]);
						Sensor[num2].Valid = true;
						num2++;
					}
					RefreshSensorsValueResult = Result.SUCCESS;
				}
				else
				{
					int num5 = Sensor.Length - 1;
					int num2 = 0;
					while (true)
					{
						int num6 = num2;
						int num4 = num5;
						if (num6 > num4)
						{
							break;
						}
						Sensor[num2].Valid = false;
						num2++;
					}
					RefreshSensorsValueResult = Result.FAIL;
				}
				Result result = default(Result);
				return result;
			}
		}

        public Result ExecuteSVs()
        {
            //IL_0027: Unknown result type (might be due to invalid IL or missing references)
            checked
            {
                if (MB == null)
                {

                    FrmDialog.ShowDialog((IWin32Window)this, "没有实例化MB通讯对象，不能更新" + AliasName + "电磁阀值！", "警告！");
                }
                else
                {
                    int num = SV.Length - 1;
                    int num2 = 0;
                    while (true)
                    {
                        int num3 = num2;
                        int num4 = num;
                        if (num3 > num4)
                        {
                            break;
                        }
                        MCU.Dout[num2] = SV[num2].Value;
                        num2++;
                    }
                    if (MB.WriteMultiCoils(MBAddress, 8, (short)SV.Length, MCU.Dout) == ClassMB.Result.SUCCESS)
                    {
                        int num5 = SV.Length - 1;
                        num2 = 0;
                        while (true)
                        {
                            int num6 = num2;
                            int num4 = num5;
                            if (num6 > num4)
                            {
                                break;
                            }
                            SV[num2].Valid = true;
                            num2++;
                        }
                        ExecuteSVsResult = Result.SUCCESS;
                    }
                    else
                    {
                        int num7 = SV.Length - 1;
                        num2 = 0;
                        while (true)
                        {
                            int num8 = num2;
                            int num4 = num7;
                            if (num8 > num4)
                            {
                                break;
                            }
                            SV[num2].Valid = false;
                            num2++;
                        }
                        ExecuteSVsResult = Result.FAIL;
                    }
                }
                Result result = default(Result);
                return result;
            }
        }

        public Result RefreshSBsValues()
        {
            //IL_0027: Unknown result type (might be due to invalid IL or missing references)
            checked
            {
                if (MB == null)
                {
                    //Interaction.MsgBox((object)("没有实例化MB通讯对象，不能更新" + AliasName + "开关按钮值！"), (MsgBoxStyle)0, (object)null);
                    FrmDialog.ShowDialog((IWin32Window)this, "没有实例化MB通讯对象，不能更新" + AliasName + "开关按钮值！", "警告！");
                }
                else if (MB.ReadCoils(ref MCU.Din, MBAddress, 0, 4) == ClassMB.Result.SUCCESS)
                {
                    int num = SB.Length - 1;
                    int num2 = 0;
                    while (true)
                    {
                        int num3 = num2;
                        int num4 = num;
                        if (num3 > num4)
                        {
                            break;
                        }
                        SB[num2].Value = MCU.Din[num2];
                        SB[num2].Valid = true;
                        num2++;
                    }
                    RefreshFromGizwitsToPCButtonValuesResult = Result.SUCCESS;
                }
                else
                {
                    int num5 = SB.Length - 1;
                    int num2 = 0;
                    while (true)
                    {
                        int num6 = num2;
                        int num4 = num5;
                        if (num6 > num4)
                        {
                            break;
                        }
                        SB[num2].Valid = false;
                        num2++;
                    }
                    RefreshFromGizwitsToPCButtonValuesResult = Result.FAIL;
                }
                Result result = default(Result);
                return result;
            }
        }

        //public Result ResetGizwitZoneBtnEvFlag()
        //{
        //	//IL_0025: Unknown result type (might be due to invalid IL or missing references)
        //	if (MB == null)
        //	{
        //		Interaction.MsgBox((object)("没有实例化MB通讯对象，不能复位" + AliasName + "机智云开关按钮值！"), (MsgBoxStyle)0, (object)null);
        //	}
        //	else if (MB.WriteSingleCoil(MBAddress, 7, CoilValue: false) == ClassMB.Result.SUCCESS)
        //	{
        //		ModuleGlobal.g_gizwit.ButtonEvent = false;
        //		ResetGizwitZoneBtnEvFlagResult = Result.SUCCESS;
        //	}
        //	else
        //	{
        //		ResetGizwitZoneBtnEvFlagResult = Result.FAIL;
        //	}
        //	Result result = default(Result);
        //	return result;
        //}

        //public Result RefreshFromGizwitsToPCButtonValues()
        //{
        //	//IL_0036: Unknown result type (might be due to invalid IL or missing references)
        //	bool[] Coils = new bool[4];
        //	bool[] Coils2 = new bool[16];
        //	if (MB == null)
        //	{
        //		Interaction.MsgBox((object)("没有实例化MB通讯对象，不能更新" + AliasName + "机智云开关按钮值！"), (MsgBoxStyle)0, (object)null);
        //	}
        //	else
        //	{
        //		ClassGizwits g_gizwit = ModuleGlobal.g_gizwit;
        //		if (MB.ReadCoils(ref Coils, MBAddress, 4, 4) == ClassMB.Result.SUCCESS)
        //		{
        //			g_gizwit.ButtonValid = true;
        //			g_gizwit.ButtonIrr = Coils[0];
        //			g_gizwit.ButtonFer = Coils[1];
        //			RefreshFromGizwitsToPCButtonValuesResult = Result.SUCCESS;
        //		}
        //		else
        //		{
        //			g_gizwit.ButtonValid = false;
        //			RefreshFromGizwitsToPCButtonValuesResult = Result.FAIL;
        //		}
        //		Thread.Sleep(46);
        //		if (MB.ReadCoils(ref Coils2, MBAddress, 16, 16) == ClassMB.Result.SUCCESS)
        //		{
        //			g_gizwit.ButtonValid = true;
        //			g_gizwit.ButtonShldOUFld = Coils2[0];
        //			g_gizwit.ButtonShldOFld = Coils2[1];
        //			g_gizwit.ButtonShldIUFld = Coils2[2];
        //			g_gizwit.ButtonShldIFld = Coils2[3];
        //			g_gizwit.ButtonWrmKprUFld = Coils2[4];
        //			g_gizwit.ButtonWrmKprFld = Coils2[5];
        //			g_gizwit.ButtonSupLUX = Coils2[6];
        //			g_gizwit.ButtonWW = Coils2[7];
        //			g_gizwit.ButtonHeater = Coils2[8];
        //			g_gizwit.ButtonWM = Coils2[9];
        //			g_gizwit.ButtonCO2 = Coils2[10];
        //			g_gizwit.ButtonLoopFan1 = Coils2[11];
        //			g_gizwit.ButtonLoopFan2 = Coils2[12];
        //			g_gizwit.ButtonVent1 = Coils2[13];
        //			g_gizwit.ButtonVent2 = Coils2[14];
        //			g_gizwit.ButtonVent3 = Coils2[15];
        //			RefreshFromGizwitsToPCButtonValuesResult = Result.SUCCESS;
        //		}
        //		else
        //		{
        //			g_gizwit.ButtonValid = false;
        //			RefreshFromGizwitsToPCButtonValuesResult = Result.FAIL;
        //		}
        //		g_gizwit = null;
        //	}
        //	Result result = default(Result);
        //	return result;
        //}

        //public Result InformHWisCtronledByWhom(ClassHardware.HWisCtrledBy ControledBy)
        //{
        //    //Discarded unreachable code: IL_0046
        //    //IL_0027: Unknown result type (might be due to invalid IL or missing references)

        //    if (MB == null)
        //    {
        //        //Interaction.MsgBox((object)("没有实例化MB通讯对象，不能更新" + AliasName + "泵值！"), (MsgBoxStyle)0, (object)null);
        //        FrmDialog.ShowDialog((IWin32Window)this, "没有实例化MB通讯对象，不能更新" + AliasName + "泵值！", "警告！");

        //    }
        //    else
        //    {
        //        short holdValue = default(short);
        //        switch (ControledBy)
        //        {
        //            case ClassHardware.HWisCtrledBy.GIZWIT:
        //                holdValue = 0;
        //                break;
        //        }
        //        if (MB.WriteSingleHoldingReg(MBAddress, 39, holdValue) == ClassMB.Result.SUCCESS)
        //        {
        //            Pump[1].Valid = true;
        //            ExecutePumpQResult = Result.SUCCESS;
        //        }
        //        else
        //        {
        //            Pump[1].Valid = false;
        //            ExecutePumpQResult = Result.FAIL;
        //        }
        //    }
        //    Result result = default(Result);
        //    return result;
        //}

        public Result ExecutePumpQ()
        {
            //IL_002c: Unknown result type (might be due to invalid IL or missing references)
            short[] array = new short[8];
            checked
            {
                if (MB == null)
                {
                    //Interaction.MsgBox((object)("没有实例化MB通讯对象，不能更新" + AliasName + "泵值！"), (MsgBoxStyle)0, (object)null);
                    FrmDialog.ShowDialog((IWin32Window)this, "没有实例化MB通讯对象，不能更新" + AliasName + "泵值！","警告");
                }
                else
                {
                    array[4] = Pump[1].ValueInHz;
                    array[5] = (short)Math.Round(Pump[1].PID_Kp * 100f);
                    array[6] = (short)Math.Round(Pump[1].PID_Ki * 100f);
                    array[7] = (short)Math.Round(Pump[1].PID_Kd * 100f);
                    if (MB.WriteMultiHoldingRegs(MBAddress, 0, (short)array.Length, array) == ClassMB.Result.SUCCESS)
                    {
                        Pump[1].Valid = true;
                        ExecutePumpQResult = Result.SUCCESS;
                    }
                    else
                    {
                        Pump[1].Valid = false;
                        ExecutePumpQResult = Result.FAIL;
                    }
                }
                Result result = default(Result);
                return result;
            }
        }

        //public Result RefreshFromGizwitToPC()
        //{
        //	//IL_0025: Unknown result type (might be due to invalid IL or missing references)
        //	if (MB == null)
        //	{
        //		Interaction.MsgBox((object)("没有实例化MB通讯对象，不能下载" + AliasName + "机智云通讯数据！"), (MsgBoxStyle)0, (object)null);
        //	}
        //	else if (MB.ReadHoldingRegs(ref ModuleGlobal.g_gizwit.SensorVals, MBAddress, 0, checked((short)ModuleGlobal.g_gizwit.SensorVals.Length)) == ClassMB.Result.SUCCESS)
        //	{
        //		RefreshFromGizwitToPCResult = Result.SUCCESS;
        //	}
        //	else
        //	{
        //		RefreshFromGizwitToPCResult = Result.FAIL;
        //	}
        //	Result result = default(Result);
        //	return result;
        //}

        //public Result RefreshFromPCtoGizwitSensorValues()
        //{
        //	//IL_002f: Unknown result type (might be due to invalid IL or missing references)
        //	short[] array = new short[16];
        //	checked
        //	{
        //		if (MB == null)
        //		{
        //			Interaction.MsgBox((object)("没有实例化MB通讯对象，不能上传" + AliasName + "机智云通讯数据！"), (MsgBoxStyle)0, (object)null);
        //		}
        //		else
        //		{
        //			ModuleGlobal.g_gizwit.SensorVals[2] = ModuleGlobal.g_gizwit.ZonesWorkState;
        //			ModuleGlobal.g_gizwit.SensorVals[3] = ModuleGlobal.g_gizwit.EnvWorkState;
        //			int num = unchecked(ModuleGlobal.g_gizwit.SensorVals.Length / 16) - 1;
        //			int num2 = 0;
        //			while (true)
        //			{
        //				int num3 = num2;
        //				int num4 = num;
        //				if (num3 > num4)
        //				{
        //					break;
        //				}
        //				int num5 = 0;
        //				int num6;
        //				do
        //				{
        //					array[num5] = ModuleGlobal.g_gizwit.SensorVals[16 * num2 + num5];
        //					num5++;
        //					num6 = num5;
        //					num4 = 15;
        //				}
        //				while (num6 <= num4);
        //				if (MB.WriteMultiHoldingRegs(MBAddress, (short)(16 * num2), 16, array) == ClassMB.Result.SUCCESS)
        //				{
        //					RefreshFromPCtoGizwitResult = Result.SUCCESS;
        //				}
        //				else
        //				{
        //					RefreshFromPCtoGizwitResult = Result.FAIL;
        //				}
        //				Thread.Sleep(300);
        //				num2++;
        //			}
        //			if (unchecked(ModuleGlobal.g_gizwit.SensorVals.Length % 16) > 0)
        //			{
        //				int num7 = unchecked(ModuleGlobal.g_gizwit.SensorVals.Length % 16) - 1;
        //				int num5 = 0;
        //				while (true)
        //				{
        //					int num8 = num5;
        //					int num4 = num7;
        //					if (num8 > num4)
        //					{
        //						break;
        //					}
        //					array[num5] = ModuleGlobal.g_gizwit.SensorVals[16 * num2 + num5];
        //					num5++;
        //				}
        //				if (MB.WriteMultiHoldingRegs(MBAddress, (short)(16 * num2), (short)num5, array) == ClassMB.Result.SUCCESS)
        //				{
        //					RefreshFromPCtoGizwitResult = Result.SUCCESS;
        //				}
        //				else
        //				{
        //					RefreshFromPCtoGizwitResult = Result.FAIL;
        //				}
        //				Thread.Sleep(300);
        //			}
        //		}
        //		Result result = default(Result);
        //		return result;
        //	}
        //}
    }

}
