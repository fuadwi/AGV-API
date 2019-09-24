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
using System.Threading;

namespace Dispatching_API
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }
        private Modbus.Data.DataStore dataStore;
        private static HttpListener listener;
        private static string url;
        private static volatile int dataRoute = 0;
        private int oldDataRoute = 0; 
        Form configFormVar = new configForm();
        private delegate void PrintToLog(String log);
       
        private void handleNewConfiguration(object Sender, ConfigurationEvent e)
        {

        }
        [Obsolete]
        private void Form1_Load(object sender, EventArgs e)
        {
            url = System.Configuration.ConfigurationSettings.AppSettings["listenHost"] + ":" + System.Configuration.ConfigurationSettings.AppSettings["listenPort"] + "/";
            Console.WriteLine("URL = {0}", url);
            dataStore = Modbus.Data.DataStoreFactory.CreateDefaultDataStore();
            dataStore.HoldingRegisters[102] = 0;
        }
        public void formConfigClosed()
        {

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

            //if the form is minimized  
            //hide it from the task bar  
            //and show the system tray icon (represented by the NotifyIcon control)  
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(100, "INFINITI 4.0", "The Program Running in System Tray", ToolTipIcon.Info);
            }
        }

        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }
        private void printLog(String log)
        {
            if (listBox2.InvokeRequired)
            {
                var d = new PrintToLog(printLog);
                listBox2.Invoke(d, new object[] { log });
            }
            else
            {
                if (listBox2.Items.Count > 10) listBox2.Items.Clear();
                listBox2.Items.Add(log);
            }
             
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
                timHandleCaller.Enabled = true;
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(100, "INFINITI 4.0", "AGV DISPATCHING API SERVICE STARTED!", ToolTipIcon.Info);
                notifyIcon.Visible = false;
            }
            catch(IOException SE)
            {
                MessageBox.Show("COM PORT NOT READY!", "!! E R R O R !!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(UnauthorizedAccessException HLE)
            {
                MessageBox.Show("API SERVER PORT NOT AVAILABLE!", "!! E R R O R !!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                if(modbusCom.IsOpen) modbusCom.Close();
                if (!modbusWorker.IsBusy) modbusWorker.CancelAsync();
                if (!apiWorker.IsBusy) apiWorker.CancelAsync();
                modbusCom.Close();
                btnStartService.Enabled = true;
                btnStopService.Enabled = false;
                lblStatus.Text = "STOP";
                lblStatus.BackColor = Color.Red;

            }
        }
        private void ModbusWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                ModbusSerialSlave ms = ModbusSerialSlave.CreateRtu(1, modbusCom);
                ms.DataStore = dataStore;
                ms.Listen();
            }
            catch
            {

            }
        }

        private void BtnStopService_Click(object sender, EventArgs e)
        {
            try
            {
                timHandleCaller.Enabled = false;
                listener.Stop();
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
            if (configFormVar.IsDisposed) configFormVar = new configForm();
            configFormVar.Show();
            configFormVar.Focus();

        }
        private async Task HandleIncomingConnections()
        {
            bool runServer = true;

            // While a user hasn't visited the `shutdown` url, keep on handling requests
            while (!apiWorker.CancellationPending)
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
                PrintToLog logging = printLog;
                logging.Invoke("RX<-"+req.RawUrl);
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
                        dataRoute = route;
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

                logging.Invoke("TX->" + Encoding.UTF8.GetString(data));
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
                try
                {
                    // Create a Http server and start listening for incoming connections
                    listener = new HttpListener();
                    listener.Prefixes.Add(url);
                    listener.Start();
                    if (apiWorker.CancellationPending)
                    {
                        listener.Stop();
                        break;
                    }
                    Console.WriteLine("Listening for connections on {0}", url);
                    // Handle requests
                    Task listenTask = HandleIncomingConnections();
                    listenTask.GetAwaiter().GetResult();
                    // Close the listener
                    listener.Close();
                }
                catch(Exception Ee)
                {
                    Console.WriteLine("ERROR MSG = {0}", Ee.Message);
                    break;
                }
            }
        }

        private void TimHandleCaller_Tick(object sender, EventArgs e)
        {
            if (oldDataRoute != dataRoute)
            {
                dataStore.HoldingRegisters[102] = (UInt16)dataRoute;
                dataStore.CoilDiscretes[2] = true;
                oldDataRoute = dataRoute;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do you want to Exit?\n\tYes = Exit\n\tNo = Hide", "Hide or Exit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            if (dialog == DialogResult.No)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
                Hide();
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(100, "INFINITI 4.0", "The Program Running in System Tray", ToolTipIcon.Info);

            }
            else if(dialog == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }
    }
}
