using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dapengtest
{
    public class ClassPump
    {
		public string Name;

		public string AliasName;

		public float QatPWM100;

		public float PID_Kp;

		public float PID_Ki;

		public float PID_Kd;

		public bool Valid;

		public float Value;

		public short ValueInHz;

		public DateTime StartAtTime;

		public DateTime EndAtTime;

		public ClassPump()
		{
			AliasName = "保留";
		}
	}
}
