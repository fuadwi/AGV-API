using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace ModbusTestClient
{
    public partial class FormMainScreen : Form
    {
        private static int _rowHeightOffset = 2;
        private static int _columnWidthOffset = 3;
        private System.UInt16[] _registerData = new System.UInt16[65600];
        private byte[] _messageReceived = new byte[8];
        private int _startRegister = 0;
        private byte _slaveAddress = 0;
        private int _slaveDelay = 0;
        
        public FormMainScreen()
        {
            InitializeComponent();
            prepareTable();
            fillPortList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //dataGridView1.Rows.Add();
        }

        

        private void prepareTable()
        {
            for (int i = 0; i < 15; i++)
            {
                if (i < 14) dataGridViewRegisters.Rows.Add();
                dataGridViewRegisters.Rows[i].Cells[0].Value = i.ToString() + " (0x" + i.ToString("X") + ")";
                dataGridViewRegisters.Rows[i].Cells[1].Value = 0;
            }
            dataGridViewRegisters.Width = dataGridViewRegisters.Columns[0].Width + dataGridViewRegisters.Columns[1].Width + _columnWidthOffset;
            dataGridViewRegisters.Height = (dataGridViewRegisters.Rows.Count + 1) * dataGridViewRegisters.Rows[0].Height + _rowHeightOffset;
        }

        private void shiftTable()
        {
            for (int i = 0; i < 15; i++)
            {
                dataGridViewRegisters.Rows[i].Cells[0].Value = (i + _startRegister).ToString() + " (0x" + (i + _startRegister).ToString("X") + ")";
            }
        }

        private void dataGridViewRegisters_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                try
                {
                    System.UInt16 tempUint = 0;
                    tempUint = Convert.ToUInt16(e.FormattedValue);
                    _registerData[_startRegister + e.RowIndex] = tempUint;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.ToString());
                    dataGridViewRegisters.Rows[e.RowIndex].ErrorText = "Input is not a valid 16 bit number";
                    dataGridViewRegisters.Rows[e.RowIndex].Cells[1].Value = 0;
                }
            }
        }

        private void buttonGoToRegister_Click(object sender, EventArgs e)
        {
            int tempInt = 0;
            try
            {
                tempInt = Convert.ToInt32(textBoxRegisterStart.Text);
                _startRegister = tempInt;
                shiftTable();

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
                textBoxRegisterStart.Text = "0";
            }
        }

        private void fillPortList()
        {
            foreach (String port in SerialPort.GetPortNames())
            {
                comboBoxSerialPorts.Items.Add(port);
            }
            comboBoxSerialPorts.SelectedIndex = 0;
        }

        private void textBoxSlaveDelay_Validating(object sender, CancelEventArgs e)
        {
            int tempInt = 0;
            try
            {
                tempInt = Convert.ToInt32(textBoxSlaveDelay.Text);
                if (tempInt < 0)
                {
                    tempInt = 0;
                    textBoxSlaveDelay.Text = "0";
                }

                _slaveDelay = tempInt;

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
                textBoxSlaveDelay.Text = "0";
            }
        }

        private void textBoxSlaveAddress_Validating(object sender, CancelEventArgs e)
        {
            int tempInt = 0;
            byte tempByte = 0;
            try
            {
                tempInt = Convert.ToInt32(textBoxSlaveAddress.Text);
                if (tempInt > 255)
                {
                    tempInt = 255;
                    textBoxSlaveAddress.Text = "255";
                }
                tempByte = Convert.ToByte(tempInt);
                _slaveAddress = tempByte;

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
                textBoxSlaveAddress.Text = "1";
            }
        }



        private void radioButtonOn_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonOn.Checked)
            {
                groupBoxConnParameters.Enabled = false;
                _serialPort.PortName = Convert.ToString(comboBoxSerialPorts.SelectedItem);
                if (radioButtonParityEven.Checked)
                    _serialPort.Parity = Parity.Even;
                else if (radioButtonParityNone.Checked)
                    _serialPort.Parity = Parity.None;
                else if (radioButtonParityOdd.Checked)
                    _serialPort.Parity = Parity.Odd;

                if (radioButtonDataBits7.Checked)
                    _serialPort.DataBits = 7;
                else if (radioButtonDataBits8.Checked)
                    _serialPort.DataBits = 8;

                if (radioButtonStopBit1.Checked)
                    _serialPort.StopBits = StopBits.One;
                if (radioButtonStopBit2.Checked)
                    _serialPort.StopBits = StopBits.Two;

                _serialPort.BaudRate = Convert.ToInt32(textBoxBaud.Text);

                try
                {
                    _serialPort.Open();
                    backgroundWorker1.RunWorkerAsync();
                }
                catch (Exception ex)
                {
                    radioButtonOff.Checked = true;
                }

            }
            else
            {
                backgroundWorker1.CancelAsync();
                groupBoxConnParameters.Enabled = true;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            while (!worker.CancellationPending)
            {
                if (_serialPort.BytesToRead == 8)
                {                    
                    _serialPort.Read(_messageReceived, 0, 8);
                    addLog(createLogStr(ref _messageReceived), LogType.RX, worker);
                    if (_messageReceived[0] == _slaveAddress && _messageReceived[1] == 3)
                    {
                        if (CRCStuff.checkCRC(ref _messageReceived, 8))
                        {
                            byte[] messageToSend = createRespondMessage();
                            System.Threading.Thread.Sleep(_slaveDelay);
                            _serialPort.Write(messageToSend, 0, messageToSend.Length);
                            addLog(createLogStr(ref messageToSend), LogType.TX, worker);                            
                        }
                        else
                        {
                            addLog("", LogType.CRC_ERR, worker);
                        }
                    }
                }
                else
                {
                    _serialPort.DiscardInBuffer();
                }
            }
            _serialPort.Close();
            e.Cancel = true;
            
        }

        private byte[] createRespondMessage()
        {
            int numberOfPoints = 0;
            int bytesToSend = 0;
            int startAddress = 0;
            numberOfPoints = (_messageReceived[4] << 8) | _messageReceived[5];
            bytesToSend = 5 * numberOfPoints + 2;
            byte[] respondMessage = new byte[bytesToSend];
            respondMessage[0] = _slaveAddress;
            respondMessage[1] = 3;
            respondMessage[2] = Convert.ToByte(2 * numberOfPoints);
            startAddress = (_messageReceived[2] << 8) | _messageReceived[3];
            int j = 0;
            for (int i = 0; i < numberOfPoints; i++)
            {
                respondMessage[i + j + 3] = Convert.ToByte((_registerData[startAddress + i] >> 8) & 0xff);
                respondMessage[i + j + 4] = Convert.ToByte(_registerData[startAddress + i] & 0xff);
                j++;
            }
            byte[] crcCalculation = CRCStuff.calculateCRC(ref respondMessage, bytesToSend - 2);
            respondMessage[bytesToSend - 2] = crcCalculation[0];
            respondMessage[bytesToSend - 1] = crcCalculation[1];
            return respondMessage;
        }

        private void addLog(String log, LogType logType, BackgroundWorker worker)
        {
            DateTime now = DateTime.Now;
            String tmpStr = "";
            switch (logType)
            {
                case LogType.CRC_ERR:
                    tmpStr = ">" + now.ToLongTimeString() + ">CRC Check Failed";                    
                    break;
                case LogType.RX:
                    tmpStr = ">" + now.ToLongTimeString() + ">RX:" + log;                    
                    break;
                case LogType.TX:
                    tmpStr = ">" + now.ToLongTimeString() + ">TX:" + log;                   
                    break;
            }
            worker.ReportProgress(0, tmpStr);
        }

        private String createLogStr(ref byte[] message)
        {
            String tmpStr = "";
            foreach (byte oneByte in message)
            {
                String byteString = oneByte.ToString("X");
                if (byteString.Length == 1)
                    byteString = "0" + byteString;
                    
                tmpStr = tmpStr + byteString + " ";
            }
            return tmpStr;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            listBoxCommLog.Items.Add(e.UserState);
        }
        enum LogType
        {
            RX,
            TX,
            CRC_ERR
        }

        private void GroupBoxSlaveParameters_Enter(object sender, EventArgs e)
        {

        }
    }
}