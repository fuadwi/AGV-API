using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modbus.Device;
using Newtonsoft.Json;

namespace Dispatch_Read
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ushort lastCall = 0;
        private void Button1_Click(object sender, EventArgs e)
        {
            modbusCom.PortName = "COM5"; //com port
            modbusCom.BaudRate = 9600; // baudrate modbus
            modbusCom.DataBits = 8;
            modbusCom.Parity = Parity.None;
            modbusCom.StopBits = StopBits.One;
            modbusCom.Open();
            modbusWorker.RunWorkerAsync();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (!modbusWorker.IsBusy) modbusWorker.CancelAsync();
        }
        private void callAPI(string daisha, string location)
        {
            var APIContent = new System.Net.Http.StringContent("login=admin&password=r4pyd&model=agv.checkpoint&values= {\"daisha\": \""+daisha+"\", \"location\": \""+location+"\"}", Encoding.UTF8, "application/x-www-form-urlencoded");
            var client = new System.Net.Http.HttpClient();
            var response = client.PostAsync("https://hppm-wms.herokuapp.com/api/create", APIContent).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;
                dynamic json = JsonConvert.DeserializeObject(responseString);
                Console.WriteLine(json);
            }
        }
            private void ModbusWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ModbusSerialMaster ms = ModbusSerialMaster.CreateRtu(modbusCom);
            while (true)
            {
                ushort[] result = ms.ReadHoldingRegisters(1, 1, 10); //read register 1-10 di slave 1
                label1.Text = "EMEGENCY : " +result[1].ToString(); //read to label 
                if(result[1] != lastCall)
                {
                    lastCall = result[1];
                    callAPI("DAISHA " + result[1].ToString(), "Location " + result[1].ToString());
                }
                Thread.Sleep(50);

            }
        }
    }
}
