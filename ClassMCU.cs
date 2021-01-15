using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dapengtest
{
    public class ClassMCU
    {
		public string Name;

		public string AliasName;

		public bool AinValid;

		public short[] Ain;

		public bool DinValid;

		public bool[] Din;

		public bool DoutValid;

		public bool[] Dout;

		public bool PWMoutValid;

		public short[] PWMout;

		public ClassMCU()
		{
			Ain = new short[4];
			Din = new bool[4];
			Dout = new bool[8];
			PWMout = new short[2];
		}
	}
}
