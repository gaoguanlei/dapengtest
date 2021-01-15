using HZH_Controls.Forms;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Windows.Forms;

namespace dapengtest
{
    public class ClassDB
    {
		static string connectStr = "server=47.240.70.213;port=3306;database=HSWJ;user=root;password=FQIufyPG";
		public static MySqlConnection conn = new MySqlConnection(connectStr);
		public static bool IsConnect;

		private bool busy;
		public ClassDB()
		{
			try
            {
				if(!IsConnect)
				{
					conn.Open();
					IsConnect = true;
				}
            }
            catch
            {
				IsConnect = false;
            }
		}
		public void ReadSetting_Sensor(ref short id, ref string aliasname, ref string xunit, ref float xmin, ref float xmax, ref string yunit, ref float ymin, ref float ymax, ref float x0, ref float y0, ref float x1, ref float y1, ref bool AL, ref float LAL, ref float HAL, ref string LALTIPS, ref string HALTIPS)
		{
			MySqlCommand mySqlCommand = new MySqlCommand();
			//SQLiteCommand sQLiteCommand = new SQLiteCommand();
			busy = true;
			string sql = "SELECT id,aliasname,xunit,xmin,xmax,yunit,ymin,ymax,x0,y0,x1,y1,AL,LAL,HAL,LALTIPS,HALTIPS FROM sensors WHERE id=" + Convert.ToString((int)id);
			using (MySqlCommand cmd = new MySqlCommand(sql, conn))
			{
				// 读取数据
				MySqlDataReader reader = cmd.ExecuteReader();

				if (reader.HasRows)
				{
					reader.Read();
					id = reader.GetInt16(0);
					aliasname = reader.GetString(1);
					xunit = reader.GetString(2);
					xmin = reader.GetFloat(3);
					xmax = reader.GetFloat(4);
					yunit = reader.GetString(5);
					ymin = reader.GetFloat(6);
					ymax = reader.GetFloat(7);
					x0 = reader.GetFloat(8);
					y0 = reader.GetFloat(9);
					x1 = reader.GetFloat(10);
					y1 = reader.GetFloat(11);
					AL = reader.GetBoolean(12);
					LAL = reader.GetFloat(13);
					HAL = reader.GetFloat(14);
					LALTIPS = reader.GetString(15);
					HALTIPS = reader.GetString(16);
				}
				else
				{
					//Interaction.MsgBox((object)("在数据库中查询编号为" + Conversions.ToString((int)id) + "的传感器失败！"), (MsgBoxStyle)0, (object)null);
					FrmDialog.ShowDialog((IWin32Window)this, "在数据库中查询编号为" + Convert.ToString((int)id) + "的传感器失败！", "警告！");
				}
				reader.Close();
				reader = null;
				busy = false;
				//sQLiteCommand = null;
				mySqlCommand = null;
				//sQLiteDataReader = null;
				reader = null;
			}
		}
	}
}