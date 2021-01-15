using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace dapengtest
{
	class ClassHardware
	{

		public enum HWisCtrledBy
		{
			GIZWIT,
			PC,
			NONE
		}

		public enum THREAD_CONTROLED_BY
		{
			MAINFORM,
			MCUEMULATOR,
			DEBUGMB
		}

		public string Name;

		public string AliasName;

		public HWisCtrledBy ControlledBy;

		public short nSSs;

		public short nSVs;

		public short nPumps;

		public ClassUnit Unitg510;

		public ClassUnit Unit1;

		public ClassUnit Unit2;

		public ClassUnit Unit3;

		public ClassUnit[] UnitSoil;

		public ClassUnit Unit10;

		public ClassUnit Unit11;

		public ClassUnit Unit12;

		public ClassUnit Unit13;

		public ClassUnit Unit14;

		public ClassUnit Unit15;

		public ClassUnit Unitair485_I1;

		public ClassUnit Unitair485_I2;

		public ClassUnit Unitair485_I3;

		public ClassUnit Unitair485_I4;

		public ClassUnit Unitair485_I5;

		public ClassUnit Unitair485_I6;

		public ClassUnit Unitair485_O1;

		public ClassMB MB232;

		public ClassMB MB485;

		public Thread mb232thread;

		public Thread mb485thread;

		public bool ThreadEnabled;

		public THREAD_CONTROLED_BY ThreadControledBy;

		public int CommThread485Secs;

		public int CommThread485Cnt;

		public int CommThread232Secs;

		public int CommThread232Cnt;

		public ClassHardware()
		{
			nSSs = 55;
			nSVs = 6;
			nPumps = 2;
			Unitg510 = new ClassUnit();
			Unit1 = new ClassUnit();
			Unit2 = new ClassUnit();
			Unit3 = new ClassUnit();
			checked
			{
				UnitSoil = new ClassUnit[nSVs - 1 + 1];
				Unit10 = new ClassUnit();
				Unit11 = new ClassUnit();
				Unit12 = new ClassUnit();
				Unit13 = new ClassUnit();
				Unit14 = new ClassUnit();
				Unit15 = new ClassUnit();
				Unitair485_I1 = new ClassUnit();
				Unitair485_I2 = new ClassUnit();
				Unitair485_I3 = new ClassUnit();
				Unitair485_I4 = new ClassUnit();
				Unitair485_I5 = new ClassUnit();
				Unitair485_I6 = new ClassUnit();
				Unitair485_O1 = new ClassUnit();
				MB232 = new ClassMB("COM10", 9600, 8, Parity.None, StopBits.One, 180, 180);
				MB485 = new ClassMB("COM2", 9600, 8, Parity.None, StopBits.One, 180, 180);
				AliasName = "硬件接口对象";
				MB232.Name = "MB232";
				MB485.Name = "MB485";
				//MB232.Open();
				MB485.Open();
				ClassUnit unitg = Unitg510;
				unitg.AliasName = "远程通信和基础控制单元";
				unitg.MB = MB232;
				unitg.MBAddress = 16;
				unitg.SV[0].AliasName = "园区1电磁阀";
				unitg.SV[1].AliasName = "园区2电磁阀";
				unitg.SV[2].AliasName = "水泵电磁阀";
				unitg.SV[3].AliasName = "肥泵电磁阀";
				unitg.MCU.AliasName = "远程通信和基础控制器";
				unitg = null;
				ClassUnit unitair485_O = Unitair485_O1;
				unitair485_O.AliasName = "485型室外大气温湿度采集单元.1";
				unitair485_O.MB = MB485;
				unitair485_O.MBAddress = 128;
				unitair485_O.Sensor[0].IDinDB = 3;
				unitair485_O.Sensor[0].GetSettingsFromDB();
				unitair485_O.Sensor[1].IDinDB = 2;
				unitair485_O.Sensor[1].GetSettingsFromDB();
				unitair485_O = null;
				ClassUnit unitair485_I = Unitair485_I1;
				unitair485_I.AliasName = "485型区内大气温湿度采集单元.1";
				unitair485_I.MB = MB485;
				unitair485_I.MBAddress = 129;
				unitair485_I.Sensor[0].IDinDB = 1;
				unitair485_I.Sensor[0].GetSettingsFromDB();
				unitair485_I.Sensor[1].IDinDB = 0;
				unitair485_I.Sensor[1].GetSettingsFromDB();
				unitair485_I = null;
				ClassUnit unitair485_I2 = Unitair485_I2;
				unitair485_I2.AliasName = "485型区内大气温湿度采集单元.2";
				unitair485_I2.MB = MB485;
				unitair485_I2.MBAddress = 130;
				unitair485_I2.Sensor[0].IDinDB = 61;
				unitair485_I2.Sensor[0].GetSettingsFromDB();
				unitair485_I2.Sensor[1].IDinDB = 60;
				unitair485_I2.Sensor[1].GetSettingsFromDB();
				unitair485_I2 = null;
				ClassUnit unitair485_I3 = Unitair485_I3;
				unitair485_I3.AliasName = "485型区内大气温湿度采集单元.3";
				unitair485_I3.MB = MB485;
				unitair485_I3.MBAddress = 131;
				unitair485_I3.Sensor[0].IDinDB = 63;
				unitair485_I3.Sensor[0].GetSettingsFromDB();
				unitair485_I3.Sensor[1].IDinDB = 62;
				unitair485_I3.Sensor[1].GetSettingsFromDB();
				unitair485_I3 = null;
				ClassUnit unitair485_I4 = Unitair485_I4;
				unitair485_I4.AliasName = "485型区内大气温湿度采集单元.4";
				unitair485_I4.MB = MB485;
				unitair485_I4.MBAddress = 132;
				unitair485_I4.Sensor[0].IDinDB = 65;
				unitair485_I4.Sensor[0].GetSettingsFromDB();
				unitair485_I4.Sensor[1].IDinDB = 64;
				unitair485_I4.Sensor[1].GetSettingsFromDB();
				unitair485_I4 = null;
				ClassUnit unitair485_I5 = Unitair485_I5;
				unitair485_I5.AliasName = "485型区内大气温湿度采集单元.5";
				unitair485_I5.MB = MB485;
				unitair485_I5.MBAddress = 133;
				unitair485_I5.Sensor[0].IDinDB = 67;
				unitair485_I5.Sensor[0].GetSettingsFromDB();
				unitair485_I5.Sensor[1].IDinDB = 66;
				unitair485_I5.Sensor[1].GetSettingsFromDB();
				unitair485_I5 = null;
				ClassUnit unitair485_I6 = Unitair485_I6;
				unitair485_I6.AliasName = "485型区内大气温湿度采集单元.6";
				unitair485_I6.MB = MB485;
				unitair485_I6.MBAddress = 134;
				unitair485_I6.Sensor[0].IDinDB = 69;
				unitair485_I6.Sensor[0].GetSettingsFromDB();
				unitair485_I6.Sensor[1].IDinDB = 68;
				unitair485_I6.Sensor[1].GetSettingsFromDB();
				unitair485_I6 = null;
				ClassUnit unit = Unit1;
				unit.AliasName = "泵控单元";
				unit.MB = MB485;
				unit.MBAddress = 1;
				unit.Sensor[0].IDinDB = 6;
				unit.Sensor[0].GetSettingsFromDB();
				unit.Sensor[2].IDinDB = 4;
				unit.Sensor[2].GetSettingsFromDB();
				unit.Sensor[3].IDinDB = 5;
				unit.Sensor[3].GetSettingsFromDB();
				unit.Pump[1].AliasName = "'肥泵";
				unit = null;
				ClassUnit unit2 = Unit2;
				unit2.AliasName = "PH/EC采集单元/电磁阀控制单元1";
				unit2.MB = MB485;
				unit2.MBAddress = 2;
				unit2.Sensor[0].IDinDB = 12;
				unit2.Sensor[0].GetSettingsFromDB();
				unit2.Sensor[0].ValFormat = "0.0";
				unit2.Sensor[1].IDinDB = 13;
				unit2.Sensor[1].GetSettingsFromDB();
				unit2.Sensor[1].ValFormat = "0.00";
				unit2.SV[0].AliasName = "园区3电磁阀";
				unit2.SV[1].AliasName = "园区4电磁阀";
				unit2.SV[2].AliasName = "园区5电磁阀";
				unit2.SV[3].AliasName = "园区6电磁阀";
				unit2 = null;
				ClassUnit unit3 = Unit3;
				unit3.AliasName = "区内CO2/光照度采集单元1/电磁阀控制单元2";
				unit3.MB = MB485;
				unit3.MBAddress = 3;
				unit3.Sensor[2].IDinDB = 8;
				unit3.Sensor[2].GetSettingsFromDB();
				unit3.Sensor[2].ValFormat = "0";
				unit3.Sensor[3].IDinDB = 11;
				unit3.Sensor[3].GetSettingsFromDB();
				unit3.Sensor[3].ValFormat = "0";
				unit3.SV[0].AliasName = "声光报警电磁阀";
				unit3 = null;
				int num = UnitSoil.Length - 1;
				int num2 = 0;
				while (true)
				{
					int num3 = num2;
					int num4 = num;
					if (num3 > num4)
					{
						break;
					}
					UnitSoil[num2] = new ClassUnit();
					ClassUnit classUnit = UnitSoil[num2];
					classUnit.AliasName = "园区" + Convert.ToString(1 + num2) + "土壤墒情采集单元";//123
					classUnit.MB = MB485;
					classUnit.MBAddress = (byte)(4 + num2);
					classUnit.Sensor[0].IDinDB = (short)(16 + 4 * num2);
					classUnit.Sensor[0].GetSettingsFromDB();
					classUnit.Sensor[1].IDinDB = (short)(17 + 4 * num2);
					classUnit.Sensor[1].GetSettingsFromDB();
					classUnit.Sensor[3].IDinDB = (short)(19 + 4 * num2);
					classUnit.Sensor[3].ValFormat = "0";
					classUnit.Sensor[3].GetSettingsFromDB();
					classUnit = null;
					num2++;
				}
				ClassUnit unit4 = Unit10;
				unit4.AliasName = "区内CO2/光照度采集单元2";
				unit4.MB = MB485;
				unit4.MBAddress = 10;
				unit4.Sensor[2].IDinDB = 9;
				unit4.Sensor[2].GetSettingsFromDB();
				unit4.Sensor[2].ValFormat = "0";
				unit4.Sensor[3].IDinDB = 40;
				unit4.Sensor[3].GetSettingsFromDB();
				unit4.Sensor[3].ValFormat = "0";
				unit4.SV[0].AliasName = "声光报警电磁阀";
				unit4 = null;
				ClassUnit unit5 = Unit11;
				unit5.AliasName = "区内CO2/光照度采集单元3";
				unit5.MB = MB485;
				unit5.MBAddress = 11;
				unit5.Sensor[2].IDinDB = 41;
				unit5.Sensor[2].GetSettingsFromDB();
				unit5.Sensor[2].ValFormat = "0";
				unit5.Sensor[3].IDinDB = 42;
				unit5.Sensor[3].GetSettingsFromDB();
				unit5.Sensor[3].ValFormat = "0";
				unit5.SV[0].AliasName = "声光报警电磁阀";
				unit5 = null;
				ClassUnit unit6 = Unit12;
				unit6.AliasName = "区内CO2/光照度采集单元4";
				unit6.MB = MB485;
				unit6.MBAddress = 12;
				unit6.Sensor[2].IDinDB = 43;
				unit6.Sensor[2].GetSettingsFromDB();
				unit6.Sensor[2].ValFormat = "0";
				unit6.Sensor[3].IDinDB = 44;
				unit6.Sensor[3].GetSettingsFromDB();
				unit6.Sensor[3].ValFormat = "0";
				unit6.SV[0].AliasName = "声光报警电磁阀";
				unit6 = null;
				ClassUnit unit7 = Unit13;
				unit7.AliasName = "区内CO2/光照度采集单元5";
				unit7.MB = MB485;
				unit7.MBAddress = 13;
				unit7.Sensor[2].IDinDB = 45;
				unit7.Sensor[2].GetSettingsFromDB();
				unit7.Sensor[2].ValFormat = "0";
				unit7.Sensor[3].IDinDB = 46;
				unit7.Sensor[3].GetSettingsFromDB();
				unit7.Sensor[3].ValFormat = "0";
				unit7.SV[0].AliasName = "声光报警电磁阀";
				unit7 = null;
				ClassUnit unit8 = Unit14;
				unit8.AliasName = "区内CO2/光照度采集单元6/电磁阀控制单元3";
				unit8.MB = MB485;
				unit8.MBAddress = 14;
				unit8.Sensor[2].IDinDB = 9;
				unit8.Sensor[2].GetSettingsFromDB();
				unit8.Sensor[2].ValFormat = "0";
				unit8.Sensor[3].IDinDB = 48;
				unit8.Sensor[3].GetSettingsFromDB();
				unit8.Sensor[3].ValFormat = "0";
				unit8.SV[0].AliasName = "外遮光帘电磁阀";
				unit8.SV[1].AliasName = "内遮光帘电磁阀";
				unit8.SV[2].AliasName = "保温被电磁阀";
				unit8.SV[3].AliasName = "补光灯电磁阀";
				unit8.SV[4].AliasName = "水墙电磁阀";
				unit8.SV[5].AliasName = "加热器电磁阀";
				unit8.SV[6].AliasName = "细水雾电磁阀";
				unit8.SV[7].AliasName = "CO2发生器电磁阀";
				unit8 = null;
				ClassUnit unit9 = Unit15;
				unit9.AliasName = "电磁阀控制单元4";
				unit9.MB = MB485;
				unit9.MBAddress = 15;
				unit9.Sensor[3].IDinDB = 59;
				unit9.Sensor[3].GetSettingsFromDB();
				unit9.Sensor[3].ValFormat = "0";
				unit9.SV[0].AliasName = "环流风机1#";
				unit9.SV[1].AliasName = "环流风机2#";
				unit9.SV[4].AliasName = "放风机1#";
				unit9.SV[5].AliasName = "放风机2#";
				unit9.SV[6].AliasName = "放风机3#";
				unit9 = null;
				mb232thread = new Thread(ExchangeingInfo_232Unit_PC);
				mb485thread = new Thread(ExchangeingInfo_485Units_PC);
			}
		}

		public void StartCommThreads()
		{
			ThreadEnabled = true;
			//mb232thread.Start();
			mb485thread.Start();
		}

		public void StopCommThreads()
		{
			ThreadEnabled = false;
			//mb232thread.Join();
			mb485thread.Join();
		}

		public void ExchangeingInfo_485Units_PC()
		{
			checked
			{
				while (ThreadEnabled)
				{
					DateTime now = DateAndTime.Now;
								Unit1.RefreshSensorsValue();
								//ModuleGlobal.g_gizwit.SensorVals[4] = (short)Math.Round((double)Unit1.Sensor[0].Value / 0.1);
								//ModuleGlobal.g_gizwit.SensorVals[6] = (short)Math.Round((double)(Unit1.Sensor[2].Value * 60f) / 0.1);
								//ModuleGlobal.g_gizwit.SensorVals[7] = (short)Math.Round((double)(Unit1.Sensor[3].Value * 60f) / 0.1);
								Thread.Sleep(45);
								Unit2.RefreshSensorsValue();
								//ModuleGlobal.g_gizwit.SensorVals[8] = (short)Math.Round((double)Unit2.Sensor[0].Value / 0.1);
								//ModuleGlobal.g_gizwit.SensorVals[9] = (short)Math.Round((double)Unit2.Sensor[1].Value / 0.01);
								Thread.Sleep(45);
								Unitair485_I1.RefreshSensorsValue();
								Thread.Sleep(45);
								Unitair485_I2.RefreshSensorsValue();
								Thread.Sleep(45);
								Unitair485_I3.RefreshSensorsValue();
								Thread.Sleep(45);
								Unitair485_I4.RefreshSensorsValue();
								Thread.Sleep(45);
								Unitair485_I5.RefreshSensorsValue();
								Thread.Sleep(45);
								Unitair485_I6.RefreshSensorsValue();
								Thread.Sleep(45);
								//ModuleGlobal.g_gizwit.SensorVals[0] = (short)Math.Round((double)ModuleGlobal.g_tairautoH.AVGVal / 0.1);
								//ModuleGlobal.g_gizwit.SensorVals[1] = (short)Math.Round((double)ModuleGlobal.g_hairauto.AVGVal / 0.1);
								Unit3.RefreshSensorsValue();
								Thread.Sleep(45);
								//ModuleGlobal.g_gizwit.SensorVals[13] = (short)Math.Round(Unit3.Sensor[2].Value / 10f);
								Unit10.RefreshSensorsValue();
								Thread.Sleep(45);
								Unit11.RefreshSensorsValue();
								Thread.Sleep(45);
								Unit12.RefreshSensorsValue();
								Thread.Sleep(45);
								Unit13.RefreshSensorsValue();
								Thread.Sleep(45);
								Unit14.RefreshSensorsValue();
								Thread.Sleep(45);
								short num15 = (short)(UnitSoil.Length - 1);
								short num = 0;
								while (true)
								{
									short num16 = num;
									short num3 = num15;
									if (num16 > num3)
									{
										break;
									}
									UnitSoil[num].RefreshSensorsValue();
									//ModuleGlobal.g_gizwit.SensorVals[16 + 4 * num] = (short)Math.Round((double)UnitSoil[num].Sensor[0].Value / 0.1);
									//ModuleGlobal.g_gizwit.SensorVals[17 + 4 * num] = (short)Math.Round((double)UnitSoil[num].Sensor[1].Value / 0.1);
									//ModuleGlobal.g_gizwit.SensorVals[19 + 4 * num] = (short)Math.Round(UnitSoil[num].Sensor[3].Value / 1f);
									Thread.Sleep(45);
									num = (short)unchecked(num + 1);
								}
								Unitair485_O1.RefreshSensorsValue();
								//ModuleGlobal.g_gizwit.SensorVals[10] = (short)Math.Round((double)Unitair485_O1.Sensor[1].Value / 0.1);
								//ModuleGlobal.g_gizwit.SensorVals[11] = (short)Math.Round((double)Unitair485_O1.Sensor[0].Value / 0.1);
								Thread.Sleep(45);
								Unit14.RefreshSensorsValue();
								//ModuleGlobal.g_gizwit.SensorVals[12] = (short)Math.Round(ModuleGlobal.g_co2auto.AVGVal / 1f);
								Thread.Sleep(45);
								Unit15.RefreshSensorsValue();
								//ModuleGlobal.g_gizwit.SensorVals[15] = (short)Math.Round(Unit15.Sensor[3].Value / 10f);
								Thread.Sleep(45);
                                Unit2.ExecuteSVs();
                                Thread.Sleep(45);
                                Unit3.ExecuteSVs();
                                Thread.Sleep(45);
                                Unit14.ExecuteSVs();
                                Thread.Sleep(45);
                                Unit15.ExecuteSVs();
                                Thread.Sleep(45);
                                Unit1.ExecutePumpQ();
                                Thread.Sleep(45);
                                //break;

                               Application.DoEvents();
                   
                            DateTime now2 = DateAndTime.Now;
					CommThread485Secs = (int)DateAndTime.DateDiff((DateInterval)9, now, now2, (FirstDayOfWeek)1, (FirstWeekOfYear)1);
					if (CommThread485Cnt >= 10)
					{
						CommThread485Cnt = 0;
					}
					else
					{
						CommThread485Cnt++;
					}
				}
			}
		}

        public void ExchangeingInfo_232Unit_PC()
        {
            checked
            {
                while (ThreadEnabled)
                {
                    DateTime now = DateAndTime.Now;
                    switch (ThreadControledBy)
                    {
                        case THREAD_CONTROLED_BY.MAINFORM:
                            {
                                ClassUnit unitg = Unitg510;
                                unitg.ExecuteSVs();
                                Thread.Sleep(300);
                                //unitg.RefreshFromGizwitsToPCButtonValues();
                                Thread.Sleep(46);
                                //unitg.RefreshFromPCtoGizwitSensorValues();
                                Thread.Sleep(300);
                                unitg = null;
                                break;
                            }
                      
                            Application.DoEvents();
                            break;
                      
                    }
                    DateTime now2 = DateAndTime.Now;
                    CommThread232Secs = (int)DateAndTime.DateDiff((DateInterval)9, now, now2, (FirstDayOfWeek)1, (FirstWeekOfYear)1);
                    if (CommThread232Cnt >= 10)
                    {
                        CommThread232Cnt = 0;
                    }
                    else
                    {
                        CommThread232Cnt++;
                    }
                }
            }
        }

    }
}
