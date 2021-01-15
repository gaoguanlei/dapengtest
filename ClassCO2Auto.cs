using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dapengtest
{
    class ClassCO2Auto
    {
		public enum EStatus
		{
			IDLE,
			CNTDWN,
			WORKING
		}

		public string Name;

		public string Msg;

		public bool Activate;

		public bool Enable;

		public EStatus Status;

		public float SecsCntDwn;

		public float CO2High;

		public float CO2Low;

		public ClassSensor[] ss;

		public bool AVGValid;

		public float AVGVal;

		public bool CO2GenSVVal;

		public bool[] LoopFansSVVal;

		public ClassCO2Auto()
		{
			ss = new ClassSensor[2];
			LoopFansSVVal = new bool[2];
		}

		public void RecoverDefault()
		{
			Activate = false;
			Enable = false;
			Status = EStatus.IDLE;
		}

		public void RefreshAVGVal()
		{
			AVGVal = 0f;
			short num = 0;
			checked
			{
				int num2 = ss.Length - 1;
				int num3 = 0;
				while (true)
				{
					int num4 = num3;
					int num5 = num2;
					if (num4 > num5)
					{
						break;
					}
					if (ss[num3].Valid & ((float)ss[num3].RawValue >= ss[num3].XMin) & ((float)ss[num3].RawValue <= ss[num3].XMax))
					{
						AVGVal += ss[num3].Value;
						num = (short)(num + 1);
					}
					num3++;
				}
				if (num > 0)
				{
					AVGValid = true;
					AVGVal /= num;
				}
				else
				{
					AVGValid = false;
				}
			}
		}

		public void PollAutoCtrl()
		{
			checked
			{
				short num2;
				if (AVGValid)
				{
					if (AVGVal < CO2Low)
					{
						CO2GenSVVal = true;
						short num = (short)(LoopFansSVVal.Length - 1);
						num2 = 0;
						while (true)
						{
							short num3 = num2;
							short num4 = num;
							if (num3 <= num4)
							{
								LoopFansSVVal[num2] = true;
								num2 = (short)unchecked(num2 + 1);
								continue;
							}
							break;
						}
					}
					else
					{
						if (!(AVGVal > CO2High))
						{
							return;
						}
						CO2GenSVVal = false;
						short num5 = (short)(LoopFansSVVal.Length - 1);
						num2 = 0;
						while (true)
						{
							short num6 = num2;
							short num4 = num5;
							if (num6 <= num4)
							{
								LoopFansSVVal[num2] = false;
								num2 = (short)unchecked(num2 + 1);
								continue;
							}
							break;
						}
					}
					return;
				}
				CO2GenSVVal = false;
				short num7 = (short)(LoopFansSVVal.Length - 1);
				num2 = 0;
				while (true)
				{
					short num8 = num2;
					short num4 = num7;
					if (num8 <= num4)
					{
						LoopFansSVVal[num2] = false;
						num2 = (short)unchecked(num2 + 1);
						continue;
					}
					break;
				}
			}
		}
	}
}
