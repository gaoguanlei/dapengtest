using HZH_Controls.Forms;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace dapengtest
{
    public class ClassMB
    {
		public enum Result
		{
			SUCCESS,
			PORTCLOSE,
			TIMEOUT,
			CRCERROR,
			UNKNOWNERROR
		}
		public string Name;

		public string AliasName;

		public SerialPort COMx;

		public bool IsOpen;

		public byte[] sndADU;

		public int nsndByte;

		public byte[] rcvADU;

		public int nrcvByte;
		public ClassMB(string PortName, short BaudRate, short DataBits, Parity Parity, StopBits StopBits, short ReadTimeOut, short WriteTimeOut)
		{
			COMx = new SerialPort();
			sndADU = new byte[256];
			rcvADU = new byte[256];
			SerialPort cOMx = COMx;
			cOMx.PortName = PortName;
			cOMx.BaudRate = BaudRate;
			cOMx.DataBits = DataBits;
			cOMx.Parity = Parity;
			cOMx.StopBits = StopBits;
			cOMx.ReadTimeout = ReadTimeOut;
			cOMx.WriteTimeout = WriteTimeOut;
			cOMx = null;
		}
		public void Open()
		{
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			try
			{
				COMx.Open();
				COMx.DiscardInBuffer();
				COMx.DiscardOutBuffer();
				IsOpen = true;
			}
			catch (Exception ex)
			{
				IsOpen = false;
				//Interaction.MsgBox((object)("打开串口" + COMx.PortName + "失败！"), (MsgBoxStyle)0, (object)null);
				FrmDialog.ShowDialog((System.Windows.Forms.IWin32Window)this,"打开串口" + COMx.PortName + "失败！","提示");
			}
		}
		public void Close()
		{
			try
			{
				COMx.DiscardInBuffer();
				COMx.DiscardOutBuffer();
				COMx.Close();
				IsOpen = false;
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				
			}
		}
		public Result ReadCoils(ref bool[] Coils, byte SlaveAddress, short StartAddress, short nCoils)//读线圈
		{
			checked
			{
				if (COMx.IsOpen)
				{
					sndADU[0] = SlaveAddress;
					sndADU[1] = 1;
					sndADU[2] = (byte)unchecked((short)(StartAddress >> 8));
					sndADU[3] = (byte)(StartAddress & 0xFF);
					sndADU[4] = (byte)unchecked((short)(nCoils >> 8));
					sndADU[5] = (byte)(nCoils & 0xFF);
					ushort num = Get_CRC16(sndADU, 6);
					sndADU[6] = (byte)(num & 0xFF);
					sndADU[7] = (byte)unchecked((ushort)((uint)num >> 8));
					nsndByte = 8;
					try
					{
						COMx.Write(sndADU, 0, nsndByte);
					}
					catch (Exception ex)
					{
						Result result = Result.CRCERROR;
						return result;
					}
					try
					{
						rcvADU[0] = (byte)COMx.ReadByte();
						rcvADU[1] = (byte)COMx.ReadByte();
						rcvADU[2] = (byte)COMx.ReadByte();
						int num2 = unchecked((int)rcvADU[2]) - 1;
						int num3 = 0;
						while (true)
						{
							int num4 = num3;
							int num5 = num2;
							if (num4 > num5)
							{
								break;
							}
							rcvADU[3 + num3] = (byte)COMx.ReadByte();
							num3++;
						}
						num = Get_CRC16(rcvADU, (byte)(3 + num3));
						rcvADU[3 + num3] = (byte)COMx.ReadByte();
						rcvADU[3 + num3 + 1] = (byte)COMx.ReadByte();
						nrcvByte = 3 + num3 + 2;
						COMx.DiscardInBuffer();
						COMx.DiscardOutBuffer();
						byte b = (byte)(num & 0xFF);
						byte b2 = (byte)unchecked((ushort)((uint)num >> 8));
						if ((b == rcvADU[nrcvByte - 2]) & (b2 == rcvADU[nrcvByte - 1]))
						{
							int num6 = rcvADU[2];
							byte[] array = new byte[num6 + 1];
							int num7 = num6 - 1;
							num3 = 0;
							while (true)
							{
								int num8 = num3;
								int num5 = num7;
								if (num8 > num5)
								{
									break;
								}
								array[num3] = rcvADU[3 + num3];
								num3++;
							}
							int num9 = nCoils - 1;
							num3 = 0;
							while (true)
							{
								int num10 = num3;
								int num5 = num9;
								if (num10 > num5)
								{
									break;
								}
								Coils[num3] = unchecked(array[num3 / 8] & (1 << num3 % 8)) != 0;
								num3++;
							}
							return Result.SUCCESS;
						}
						return Result.CRCERROR;
					}
					catch (Exception ex3)
					{
						nrcvByte = 0;
						Result result = Result.TIMEOUT;
						return result;
					}
				}
				return Result.PORTCLOSE;
			}
		}
		public Result ReadHoldingRegs(ref short[] Holds, byte SlaveAddress, short StartAddress, short nHolds)//读保持寄存器
		{
			//Discarded unreachable code: IL_00c7, IL_0215, IL_021d, IL_023a, IL_0241, IL_024a
			checked
			{
				if (COMx.IsOpen)
				{
					sndADU[0] = SlaveAddress;
					sndADU[1] = 3;
					sndADU[2] = (byte)unchecked((short)(StartAddress >> 8));
					sndADU[3] = (byte)(StartAddress & 0xFF);
					sndADU[4] = (byte)unchecked((short)(nHolds >> 8));
					sndADU[5] = (byte)(nHolds & 0xFF);
					ushort num = Get_CRC16(sndADU, 6);
					sndADU[6] = (byte)(num & 0xFF);
					sndADU[7] = (byte)unchecked((ushort)((uint)num >> 8));
					nsndByte = 8;
					try
					{
						COMx.Write(sndADU, 0, nsndByte);
					}
					catch (Exception ex)
					{
						Result result = Result.CRCERROR;
					}
					try
					{
						rcvADU[0] = (byte)COMx.ReadByte();
						rcvADU[1] = (byte)COMx.ReadByte();
						rcvADU[2] = (byte)COMx.ReadByte();
						int num2 = unchecked((int)rcvADU[2]) - 1;
						int num3 = 0;
						byte b = default(byte);
						while (true)
						{
							int num4 = num3;
							int num5 = num2;
							if (num4 > num5)
							{
								break;
							}
							rcvADU[3 + num3] = (byte)COMx.ReadByte();
							unchecked
							{
								if (num3 % 2 == 1)
								{
									Holds[num3 / 2] = (short)((short)(b << 8) | rcvADU[checked(3 + num3)]);
								}
								else
								{
									b = rcvADU[checked(3 + num3)];
								}
							}
							num3++;
						}
						num = Get_CRC16(rcvADU, (byte)(3 + num3));
						rcvADU[3 + num3 + 1] = (byte)COMx.ReadByte();
						rcvADU[3 + num3 + 2] = (byte)COMx.ReadByte();
						nrcvByte = 3 + num3 + 3;
						COMx.DiscardInBuffer();
						COMx.DiscardOutBuffer();
						byte b2 = (byte)(num & 0xFF);
						byte b3 = (byte)unchecked((ushort)((uint)num >> 8));
						if ((b2 == rcvADU[nrcvByte - 2]) & (b3 == rcvADU[nrcvByte - 1]))
						{
							return Result.SUCCESS;
						}
						return Result.CRCERROR;
					}
					catch (Exception ex3)
					{
						
						nrcvByte = 0;
						Result result = Result.TIMEOUT;
						return result;
					}
				}
				return Result.PORTCLOSE;
			}
		}
		public Result ReadInputRegs(ref short[] Inputs, byte SlaveAddress, short StartAddress, short nInputs)//读输入寄存器（0x04）
		{
			checked
			{
				if (COMx.IsOpen)
				{
					sndADU[0] = SlaveAddress;
					sndADU[1] = 4;
					sndADU[2] = (byte)unchecked((short)(StartAddress >> 8));
					sndADU[3] = (byte)(StartAddress & 0xFF);
					sndADU[4] = (byte)unchecked((short)(nInputs >> 8));
					sndADU[5] = (byte)(nInputs & 0xFF);
					ushort num = Get_CRC16(sndADU, 6);
					sndADU[6] = (byte)(num & 0xFF);
					sndADU[7] = (byte)unchecked((ushort)((uint)num >> 8));
					nsndByte = 8;
					try
					{
						COMx.Write(sndADU, 0, nsndByte);
					}
					catch (Exception ex)
					{
						Result result = Result.CRCERROR;
						return result;
					}
					try
					{
						rcvADU[0] = (byte)COMx.ReadByte();
						rcvADU[1] = (byte)COMx.ReadByte();
						rcvADU[2] = (byte)COMx.ReadByte();
						if ((rcvADU[2] > 0) & (rcvADU[2] < 125))
						{
							int num2 = unchecked((int)rcvADU[2]) - 1;
							int num3 = 0;
							byte b = default(byte);
							while (true)
							{
								int num4 = num3;  //循环开始值
								int num5 = num2;  //循环结束值
								if (num4 > num5)
								{
									break;
								}
								rcvADU[3 + num3] = (byte)COMx.ReadByte();
								unchecked
								{
									if (num3 % 2 == 1)
									{
										Inputs[num3 / 2] = (short)((short)(b << 8) | rcvADU[checked(3 + num3)]);
									}
									else
									{
										b = rcvADU[checked(3 + num3)];
									}
								}
								num3++;
							}
							num = Get_CRC16(rcvADU, (byte)(3 + num3));
							rcvADU[3 + num3 + 1] = (byte)COMx.ReadByte();
							rcvADU[3 + num3 + 2] = (byte)COMx.ReadByte();
							nrcvByte = 3 + num3 + 3;
							COMx.DiscardInBuffer();
							COMx.DiscardOutBuffer();
							byte b2 = (byte)(num & 0xFF);
							byte b3 = (byte)unchecked((ushort)((uint)num >> 8));
							if ((b2 == rcvADU[nrcvByte - 2]) & (b3 == rcvADU[nrcvByte - 1]))
							{
								return Result.SUCCESS;
							}
							return Result.CRCERROR;
						}
						nrcvByte = 0;
						return Result.UNKNOWNERROR;
					}
					catch (Exception ex3)
					{
						nrcvByte = 0;
						Result result = Result.TIMEOUT;
						return result;
					}
				}
				else
                {
					Console.WriteLine("串口未打开");
                }
				return Result.PORTCLOSE;
			}
		}
		public Result WriteSingleHoldingReg(byte SlaveAddress, short StartAddress, short HoldValue)//写单个寄存器
		{
			//Discarded unreachable code: IL_00c4, IL_01ee, IL_01f5, IL_0211, IL_0218, IL_0220
			checked
			{
				if (COMx.IsOpen)
				{
					sndADU[0] = SlaveAddress;
					sndADU[1] = 6;
					sndADU[2] = (byte)unchecked((short)(StartAddress >> 8));
					sndADU[3] = (byte)(StartAddress & 0xFF);
					sndADU[4] = (byte)unchecked((short)(HoldValue >> 8));
					sndADU[5] = (byte)(HoldValue & 0xFF);
					ushort num = Get_CRC16(sndADU, 6);
					sndADU[6] = (byte)(num & 0xFF);
					sndADU[7] = (byte)unchecked((ushort)((uint)num >> 8));
					nsndByte = 8;
					try
					{
						COMx.Write(sndADU, 0, nsndByte);
					}
					catch (Exception ex)
					{
						
						Result result = Result.CRCERROR;
						
						return result;
					}
					try
					{
						rcvADU[0] = (byte)COMx.ReadByte();
						rcvADU[1] = (byte)COMx.ReadByte();
						rcvADU[2] = (byte)COMx.ReadByte();
						rcvADU[3] = (byte)COMx.ReadByte();
						rcvADU[4] = (byte)COMx.ReadByte();
						rcvADU[5] = (byte)COMx.ReadByte();
						num = Get_CRC16(rcvADU, 6);
						rcvADU[6] = (byte)COMx.ReadByte();
						rcvADU[7] = (byte)COMx.ReadByte();
						nrcvByte = 8;
						COMx.DiscardInBuffer();
						COMx.DiscardOutBuffer();
						num = Get_CRC16(rcvADU, (byte)(nrcvByte - 2));
						byte b = (byte)(num & 0xFF);
						byte b2 = (byte)unchecked((ushort)((uint)num >> 8));
						if ((b == rcvADU[nrcvByte - 2]) & (b2 == rcvADU[nrcvByte - 1]))
						{
							return Result.SUCCESS;
						}
						return Result.CRCERROR;
					}
					catch (Exception ex3)
					{
						nrcvByte = 0;
						Result result = Result.TIMEOUT;
						return result;
					}
				}
				return Result.PORTCLOSE;
			}
		}
		public Result WriteMultiHoldingRegs(byte SlaveAddress, short StartAddress, short nHolds, short[] HoldsValue)//写多个寄存器
		{
			//Discarded unreachable code: IL_0135, IL_0260, IL_0268, IL_0285, IL_028c, IL_0295
			checked
			{
				if (COMx.IsOpen)
				{
					nsndByte = 7 + nHolds * 2 + 2;
					sndADU[0] = SlaveAddress;
					sndADU[1] = 16;
					sndADU[2] = (byte)unchecked((short)(StartAddress >> 8));
					sndADU[3] = (byte)(StartAddress & 0xFF);
					sndADU[4] = (byte)unchecked((short)(nHolds >> 8));
					sndADU[5] = (byte)(nHolds & 0xFF);
					sndADU[6] = (byte)(nHolds * 2);
					byte b = (byte)(nHolds - 1);
					byte b2 = 0;
					while (true)
					{
						byte num = b2;
						byte b3 = b;
						if (unchecked((uint)num > (uint)b3))
						{
							break;
						}
						sndADU[7 + unchecked((int)b2) * 2] = BitConverter.GetBytes(HoldsValue[b2])[1];
						sndADU[8 + unchecked((int)b2) * 2] = BitConverter.GetBytes(HoldsValue[b2])[0];
						b2 = (byte)unchecked((uint)(b2 + 1));
					}
					ushort num2 = Get_CRC16(sndADU, (byte)(nsndByte - 2));
					sndADU[nsndByte - 2] = (byte)(num2 & 0xFF);
					sndADU[nsndByte - 1] = (byte)unchecked((ushort)((uint)num2 >> 8));
					try
					{
						COMx.Write(sndADU, 0, nsndByte);
					}
					catch (Exception ex)
					{
						
						Result result = Result.CRCERROR;
						return result;
					}
					try
					{
						rcvADU[0] = (byte)COMx.ReadByte();
						rcvADU[1] = (byte)COMx.ReadByte();
						rcvADU[2] = (byte)COMx.ReadByte();
						rcvADU[3] = (byte)COMx.ReadByte();
						rcvADU[4] = (byte)COMx.ReadByte();
						rcvADU[5] = (byte)COMx.ReadByte();
						num2 = Get_CRC16(rcvADU, 6);
						rcvADU[6] = (byte)COMx.ReadByte();
						rcvADU[7] = (byte)COMx.ReadByte();
						nrcvByte = 8;
						COMx.DiscardInBuffer();
						COMx.DiscardOutBuffer();
						num2 = Get_CRC16(rcvADU, (byte)(nrcvByte - 2));
						byte b4 = (byte)(num2 & 0xFF);
						byte b5 = (byte)unchecked((ushort)((uint)num2 >> 8));
						if ((b4 == rcvADU[nrcvByte - 2]) & (b5 == rcvADU[nrcvByte - 1]))
						{
							return Result.SUCCESS;
						}
						return Result.CRCERROR;
					}
					catch (Exception ex3)
					{
						Exception ex4 = ex3;
						nrcvByte = 0;
						Result result = Result.TIMEOUT;
						return result;
					}
				}
				return Result.PORTCLOSE;
			}
		}
		public Result WriteSingleCoil(byte SlaveAddress, short StartAddress, bool CoilValue)//写单个线圈
		{
			//Discarded unreachable code: IL_00da, IL_01ee, IL_01f5, IL_0211, IL_0218, IL_0220
			checked
			{
				if (COMx.IsOpen)
				{
					sndADU[0] = SlaveAddress;
					sndADU[1] = 5;
					sndADU[2] = (byte)unchecked((short)(StartAddress >> 8));
					sndADU[3] = (byte)(StartAddress & 0xFF);
					if (CoilValue)
					{
						sndADU[4] = byte.MaxValue;
						sndADU[5] = 0;
					}
					else
					{
						sndADU[4] = 0;
						sndADU[5] = 0;
					}
					ushort num = Get_CRC16(sndADU, 6);
					sndADU[6] = (byte)(num & 0xFF);
					sndADU[7] = (byte)unchecked((ushort)((uint)num >> 8));
					nsndByte = 8;
					try
					{
						COMx.Write(sndADU, 0, nsndByte);
					}
					catch (Exception ex)
					{
						Result result = Result.CRCERROR;
						return result;
					}
					try
					{
						rcvADU[0] = (byte)COMx.ReadByte();
						rcvADU[1] = (byte)COMx.ReadByte();
						rcvADU[2] = (byte)COMx.ReadByte();
						rcvADU[3] = (byte)COMx.ReadByte();
						rcvADU[4] = (byte)COMx.ReadByte();
						rcvADU[5] = (byte)COMx.ReadByte();
						num = Get_CRC16(rcvADU, 6);
						rcvADU[6] = (byte)COMx.ReadByte();
						rcvADU[7] = (byte)COMx.ReadByte();
						nrcvByte = 8;
						COMx.DiscardInBuffer();
						COMx.DiscardOutBuffer();
						byte b = (byte)(num & 0xFF);
						byte b2 = (byte)unchecked((ushort)((uint)num >> 8));
						if ((b == rcvADU[nrcvByte - 2]) & (b2 == rcvADU[nrcvByte - 1]))
						{
							return Result.SUCCESS;
						}
						return Result.CRCERROR;
					}
					catch (Exception ex3)
					{
						
						nrcvByte = 0;
						Result result = Result.TIMEOUT;
						return result;
					}
				}
				return Result.PORTCLOSE;
			}
		}
		public Result WriteMultiCoils(byte SlaveAddress, short StartAddress, short nCoils, bool[] CoilsValue)//写多个线圈
		{
			//Discarded unreachable code: IL_016c, IL_0297, IL_029f, IL_02bc, IL_02c3, IL_02cc
			checked
			{
				if (COMx.IsOpen)
				{
					byte b = (byte)(unchecked(checked(nCoils - 1) / 8) + 1);
					byte[] array = new byte[unchecked((int)b) + 1];
					byte b2 = (byte)(nCoils - 1);
					byte b3 = 0;
					while (true)
					{
						byte num = b3;
						byte b4 = b2;
						if (unchecked((uint)num > (uint)b4))
						{
							break;
						}
						if (CoilsValue[b3])
						{
							array[unchecked((int)b3 / 8)] = (byte)unchecked(array[(int)b3 / 8] | (1 << (int)b3 % 8));
						}
						b3 = (byte)unchecked((uint)(b3 + 1));
					}
					nsndByte = 7 + unchecked((int)b) + 2;
					sndADU[0] = SlaveAddress;
					sndADU[1] = 15;
					sndADU[2] = (byte)unchecked((short)(StartAddress >> 8));
					sndADU[3] = (byte)(StartAddress & 0xFF);
					sndADU[4] = (byte)unchecked((short)(nCoils >> 8));
					sndADU[5] = (byte)(nCoils & 0xFF);
					sndADU[6] = b;
					byte b5 = (byte)(unchecked((int)b) - 1);
					b3 = 0;
					while (true)
					{
						byte num2 = b3;
						byte b4 = b5;
						if (unchecked((uint)num2 > (uint)b4))
						{
							break;
						}
						sndADU[7 + unchecked((int)b3)] = array[b3];
						b3 = (byte)unchecked((uint)(b3 + 1));
					}
					ushort num3 = Get_CRC16(sndADU, (byte)(nsndByte - 2));
					sndADU[nsndByte - 2] = (byte)(num3 & 0xFF);
					sndADU[nsndByte - 1] = (byte)unchecked((ushort)((uint)num3 >> 8));
					try
					{
						COMx.Write(sndADU, 0, nsndByte);
					}
					catch (Exception ex)
					{
						
						Result result = Result.CRCERROR;
						
						return result;
					}
					try
					{
						rcvADU[0] = (byte)COMx.ReadByte();
						rcvADU[1] = (byte)COMx.ReadByte();
						rcvADU[2] = (byte)COMx.ReadByte();
						rcvADU[3] = (byte)COMx.ReadByte();
						rcvADU[4] = (byte)COMx.ReadByte();
						rcvADU[5] = (byte)COMx.ReadByte();
						num3 = Get_CRC16(rcvADU, 6);
						rcvADU[6] = (byte)COMx.ReadByte();
						rcvADU[7] = (byte)COMx.ReadByte();
						nrcvByte = 8;
						COMx.DiscardInBuffer();
						COMx.DiscardOutBuffer();
						num3 = Get_CRC16(rcvADU, (byte)(nrcvByte - 2));
						byte b6 = (byte)(num3 & 0xFF);
						byte b7 = (byte)unchecked((ushort)((uint)num3 >> 8));
						if ((b6 == rcvADU[nrcvByte - 2]) & (b7 == rcvADU[nrcvByte - 1]))
						{
							return Result.SUCCESS;
						}
						return Result.CRCERROR;
					}
					catch (Exception ex3)
					{
						
						nrcvByte = 0;
						Result result = Result.TIMEOUT;
						
						return result;
					}
				}
				return Result.PORTCLOSE;
			}
		}

		private ushort Get_CRC16(byte[] Crc16_num, byte nLength)
		{
			ushort num = ushort.MaxValue;
			checked
			{
				ushort num2 = (ushort)(unchecked((int)nLength) - 1);
				ushort num3 = 0;
				while (true)
				{
					ushort num4 = num3;
					ushort num5 = num2;
					unchecked
					{
						if ((uint)num4 > (uint)num5)
						{
							break;
						}
						num = (ushort)(num ^ Crc16_num[num3]);
						ushort num6 = 0;
						ushort num7;
						do
						{
							if ((num & 1) > 0)
							{
								num = (ushort)((uint)num >> 1);
								num = checked((ushort)(num ^ 0xA001));
							}
							else
							{
								num = (ushort)((uint)num >> 1);
							}
							checked
							{
								num6 = (ushort)unchecked((uint)(num6 + 1));
								num7 = num6;
								num5 = 7;
							}
						}
						while ((uint)num7 <= (uint)num5);
					}
					num3 = (ushort)unchecked((uint)(num3 + 1));
				}
				return num;
			}
		}

	}
}
