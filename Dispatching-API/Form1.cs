using Code4Bugs.Utils.IO;
using Code4Bugs.Utils.IO.Modbus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modbus.Device;
using System.Net;
using System.IO;

namespace Dispatching_API
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private Modbus.Data.DataStore dataStore;
        private static HttpListener listener;
        private static string url;

        [Obsolete]
        private void Form1_Load(object sender, EventArgs e)
        {
            url = System.Configuration.ConfigurationSettings.AppSettings["listenHost"] + ":" + System.Configuration.ConfigurationSettings.AppSettings["listenPort"] + "/";
            Console.WriteLine("URL = {0}", url);
            dataStore = Modbus.Data.DataStoreFactory.CreateDefaultDataStore();
            dataStore.HoldingRegisters[102] = 22;
        }


        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {

        }

        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        [Obsolete]
        private void BtnStartService_Click(object sender, EventArgs e)
        {
            try
            {
                modbusCom.PortName = System.Configuration.ConfigurationSettings.AppSettings["comPort"];
                modbusCom.BaudRate = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["comBaud"]);
                modbusCom.DataBits = 8;
                modbusCom.Parity = Parity.None;
                modbusCom.StopBits = StopBits.One;
                modbusCom.Open();
                modbusWorker.RunWorkerAsync();
                btnStartService.Enabled = false;
                btnStopService.Enabled = true;
                lblStatus.Text = "RUNNING";
                lblStatus.BackColor = Color.Green;
                apiWorker.RunWorkerAsync();

                
            }
            catch
            {
                if(modbusCom.IsOpen) modbusCom.Close();
            }
        }

        private void ModbusWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ModbusSerialSlave ms = ModbusSerialSlave.CreateRtu(1, modbusCom);
            ms.DataStore = dataStore;
            ms.Listen();
        }

        private void BtnStopService_Click(object sender, EventArgs e)
        {
            try
            {
                if (!modbusWorker.IsBusy) modbusWorker.CancelAsync();
                if (!apiWorker.IsBusy) apiWorker.CancelAsync();
                modbusCom.Close();
                btnStartService.Enabled = true;
                btnStopService.Enabled = false;
                lblStatus.Text = "STOP";
                lblStatus.BackColor = Color.Red;
            }
            catch
            {

            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }
        public static async Task HandleIncomingConnections()
        {
            bool runServer = true;

            // While a user hasn't visited the `shutdown` url, keep on handling requests
            while (runServer)
            {
                // Will wait here until we hear from a connection
                HttpListenerContext ctx = await listener.GetContextAsync();

                // Peel out the requests and response objects
                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;
                //CORS - KIRIM DATA REPLY LANGSUNG KE WMS
                if (req.HttpMethod == "OPTIONS")
                {
                    resp.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept, X-Requested-With");
                    resp.AddHeader("Access-Control-Allow-Methods", "GET, POST");
                    resp.AddHeader("Access-Control-Max-Age", "1728000");
                }
                resp.AppendHeader("Access-Control-Allow-Origin", "*");

                // Make sure we don't increment the page views counter if `favicon.ico` is requested
                if (req.Url.AbsolutePath != "/favicon.ico")
                {
                    Console.WriteLine(req.Url.ToString());
                    Console.WriteLine(req.HttpMethod);
                    Console.WriteLine(req.UserHostName);
                    Console.WriteLine("Request to route " + req.QueryString["route"]);
                    Console.WriteLine();
                }

                // Write the response info
                string disableSubmit = !runServer ? "disabled" : "";
                byte[] data;
                //data = Encoding.UTF8.GetBytes("Casun AGV API");
                //Console.WriteLine(req.QueryString["route"]);
                if (String.IsNullOrEmpty(req.QueryString["route"]))
                {
                    data = Encoding.UTF8.GetBytes("Casun AGV API");
                }
                else
                {
                    try
                    {
                        int route = Convert.ToInt32(req.QueryString["route"]);
                        
                        data = Encoding.UTF8.GetBytes("Accepted, route=" + route.ToString());
                    }
                    catch
                    {
                        data = Encoding.UTF8.GetBytes("Wrong data type");
                    }
                }
                resp.ContentType = "text/html";
                resp.ContentEncoding = Encoding.UTF8;
                resp.ContentLength64 = data.LongLength;

                // Write out to the response stream (asynchronously), then close it
                await resp.OutputStream.WriteAsync(data, 0, data.Length);
                resp.Close();
            }
        }
        private static byte[] IconToBytes(Icon icon)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                icon.Save(ms);
                return ms.ToArray();
            }
        }

        private static Icon BytesToIcon(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return new Icon(ms);
            }
        }
        private void ApiWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                // Create a Http server and start listening for incoming connections
                listener = new HttpListener();
                listener.Prefixes.Add(url);
                listener.Start();
                Console.WriteLine("Listening for connections on {0}", url);

                // Handle requests
                Task listenTask = HandleIncomingConnections();
                listenTask.GetAwaiter().GetResult();

                // Close the listener
                listener.Close();
            }
        }
    }
}
