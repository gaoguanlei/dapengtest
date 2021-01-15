using HZH_Controls.Forms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace dapengtest
{

    public partial class Form1 : Form
    {
        // ClassUnit unit = Unit1;
        ClassHardware ClassHardware = new ClassHardware();
        ClassUnit ClassUnit = new ClassUnit();
         
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            ClassHardware.StartCommThreads();
            //timer1.Interval=1000
            ////ClassHardware.Unit3.RefreshSensorsValue();
            //if (ClassHardware.Unit3.Sensor[2].Valid==true) 
            //{
            //    textBox1.AppendText(ClassHardware.Unit3.Sensor[2].Value.ToString());
            //}
            ////textBox1.AppendText ( ClassHardware.Unit3.Sensor[2].Value.ToString());

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 100;
            
           // MySqlConnection ml = new MySqlConnection();
            //MB485 = new ClassMB("COM9", 9600, 8, Parity.None, StopBits.One, 180, 180);
            //Thread.Sleep(500);
            //MB485.COMx.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClassSensor classSensor = new ClassSensor();
            classSensor.IDinDB = 6;
            classSensor.GetSettingsFromDB();
            //textBox1.Text = System.Text.Encoding.UTF8.GetString(ClassHardware.MB485.rcvADU);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ClassHardware.Unit3.Sensor[2].Valid == true)
            {
                textBox1.AppendText(ClassHardware.Unit3.Sensor[2].Value.ToString());
            }
            
        }
    }
}
