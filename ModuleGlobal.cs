using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dapengtest
{
    class ModuleGlobal
    {
		public enum Events
		{
			None,
			EvSysSettings,
			EvExitSys
		}

		public static ClassValidPC g_thispc = new ClassValidPC();

		public static ClassSreenResolution g_screen = new ClassSreenResolution();

		public static ClassDB g_db = new ClassDB();


		public static ClassHardware hw = new ClassHardware();

		public static byte nSVs;

		public static byte nSSs;

		public static byte nZones;

		public static Events ev;

        //public static ClassGizwits g_gizwit;

        //public static ClassConflictJudger1 conflictjudger;

        //public static MainForm frmMain;

        //public static FormEnviroment frmEnv;

        //public static FormMCUEmulator frmMCUEmu1;

        //public static FormMCUEmulator frmMCUEmu2;

        //public static FormIOT g_frmIoT;

        //public static string g_imgpath_qrgiz;

        //public static ClassIRRtimely irrimed;

        //public static ClassFERTimely ferimed;

        //public static ClassTimeWork g_timework;

        //public static ClassIRRAUTO irrauto;

        //public static ClassFERAUTO ferauto;

        //public static ClassTairAuto_Low g_tairautoL;

        //public static ClassTairAuto_High g_tairautoH;

        //public static ClassLUXAuto g_luxauto;

        //public static ClassSupLUX g_suplux;

        //public static ClassHairAuto g_hairauto;

        //public static ClassCO2Auto g_co2auto;

        //public static ClassJudgerHW g_jdgr2hw;

        //static ModuleGlobal()
        //{
        //    checked
        //    {
        //        nSVs = (byte)hw.nSVs;
        //        nSSs = (byte)hw.nSSs;
        //        nZones = nSVs;
        //        g_gizwit = new ClassGizwits();
        //        conflictjudger = new ClassConflictJudger1();
        //        frmMain = new MainForm();
        //        frmEnv = new FormEnviroment();
        //        g_imgpath_qrgiz = "img\\qrgiz.png";
        //        irrimed = new ClassIRRtimely();
        //        ferimed = new ClassFERTimely();
        //        g_timework = new ClassTimeWork();
        //        irrauto = new ClassIRRAUTO();
        //        ferauto = new ClassFERAUTO();
        //        g_tairautoL = new ClassTairAuto_Low();
        //        g_tairautoH = new ClassTairAuto_High();
        //        g_luxauto = new ClassLUXAuto();
        //        g_suplux = new ClassSupLUX();
        //        g_hairauto = new ClassHairAuto();
        //        g_co2auto = new ClassCO2Auto();
        //        g_jdgr2hw = new ClassJudgerHW();
        //    }
        //}

        //public static void Main()
        //{
        //	MyProject.Forms.MDIParent.ShowDialog();
        //}
    }
}
