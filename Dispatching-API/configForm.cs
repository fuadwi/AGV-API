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

namespace Dispatching_API
{
    public partial class configForm : Form
    {
        
        public configForm()
        {
            InitializeComponent();
        }
        private ConfigurationEvent configFormClose { get; set; }

        [Obsolete]
        private void ConfigForm_Load(object sender, EventArgs e)
        {
            textBox7.Focus();
            ScanPort();
            LoadConfiguration();
        }

        [Obsolete]
        private void LoadConfiguration()
        {
            comboBox1.Text = System.Configuration.ConfigurationSettings.AppSettings["comPort"]; 
            textBox1.Text = System.Configuration.ConfigurationSettings.AppSettings["comBaud"];
            textBox2.Text = System.Configuration.ConfigurationSettings.AppSettings["listenHost"];
            textBox3.Text = System.Configuration.ConfigurationSettings.AppSettings["listenPort"];
            textBox4.Text = System.Configuration.ConfigurationSettings.AppSettings["uname"];
            textBox5.PasswordChar = '*';
            textBox6.PasswordChar = '*';
            textBox5.Text = System.Configuration.ConfigurationSettings.AppSettings["passwd"];
            checkBox1.Checked = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["autoStart"]);
            checkBox2.Checked = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["autoMinimize"]);
            textBox7.Focus();
        }
        [Obsolete]
        private void saveConfiguration()
        {
            System.Configuration.ConfigurationSettings.AppSettings["comPort"] = comboBox1.Text;
            System.Configuration.ConfigurationSettings.AppSettings["comBaud"] = textBox1.Text;
            System.Configuration.ConfigurationSettings.AppSettings["listenHost"] = textBox2.Text;
            System.Configuration.ConfigurationSettings.AppSettings["listenPort"] = textBox3.Text;
            System.Configuration.ConfigurationSettings.AppSettings["uname"] = textBox4.Text;
            System.Configuration.ConfigurationSettings.AppSettings["passwd"] = textBox5.Text;
            System.Configuration.ConfigurationSettings.AppSettings["autoStart"] = checkBox1.Checked ? "true" : "false";
            System.Configuration.ConfigurationSettings.AppSettings["autoMinimize"] = checkBox2.Checked ? "true" : "false";
        }
        private void saveProgram()
        {
            //configFormClose += ConfigurationEvent()
        }
        private void restartProgram()
        {
            //System.Diagnostics.Process.Start(Application.ExecutablePath); // to start new instance of application
            //this.Close(); //to turn off current app
        }
        private void ScanPort()
        {
            try
            {
                string[] ports = SerialPort.GetPortNames();

                // Display each port name to the console.
                foreach (string port in ports)
                {
                    comboBox1.Items.Add(port);
                }
            }
            catch
            {

            }
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            ScanPort();
        }

        private void ConfigForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            restartProgram();
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if(textBox7.Text == textBox4.Text && textBox6.Text == textBox5.Text)
            {
                //MessageBox.Show("")
                panel1.Visible = false;
            }
            else
            {
                MessageBox.Show("Username/Password Wrong!", "!! E R R O R !!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
