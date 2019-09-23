namespace ModbusTestClient
{
    partial class FormMainScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBoxSlaveParameters = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSlaveDelay = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSlaveAddress = new System.Windows.Forms.TextBox();
            this.groupBoxConnParameters = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridViewRegisters = new System.Windows.Forms.DataGridView();
            this.ColumnRegisterNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnRegisterValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxRegisterStart = new System.Windows.Forms.TextBox();
            this.buttonGoToRegister = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.listBoxCommLog = new System.Windows.Forms.ListBox();
            this._serialPort = new System.IO.Ports.SerialPort(this.components);
            this.comboBoxSerialPorts = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxBaud = new System.Windows.Forms.TextBox();
            this.groupBoxDataBits = new System.Windows.Forms.GroupBox();
            this.radioButtonDataBits7 = new System.Windows.Forms.RadioButton();
            this.radioButtonDataBits8 = new System.Windows.Forms.RadioButton();
            this.groupBoxStopBits = new System.Windows.Forms.GroupBox();
            this.radioButtonStopBit2 = new System.Windows.Forms.RadioButton();
            this.radioButtonStopBit1 = new System.Windows.Forms.RadioButton();
            this.groupBoxParity = new System.Windows.Forms.GroupBox();
            this.radioButtonParityEven = new System.Windows.Forms.RadioButton();
            this.radioButtonParityNone = new System.Windows.Forms.RadioButton();
            this.radioButtonParityOdd = new System.Windows.Forms.RadioButton();
            this.radioButtonOn = new System.Windows.Forms.RadioButton();
            this.radioButtonOff = new System.Windows.Forms.RadioButton();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBoxSlaveParameters.SuspendLayout();
            this.groupBoxConnParameters.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRegisters)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBoxDataBits.SuspendLayout();
            this.groupBoxStopBits.SuspendLayout();
            this.groupBoxParity.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxSlaveParameters
            // 
            this.groupBoxSlaveParameters.Controls.Add(this.radioButtonOff);
            this.groupBoxSlaveParameters.Controls.Add(this.radioButtonOn);
            this.groupBoxSlaveParameters.Controls.Add(this.label2);
            this.groupBoxSlaveParameters.Controls.Add(this.textBoxSlaveDelay);
            this.groupBoxSlaveParameters.Controls.Add(this.label1);
            this.groupBoxSlaveParameters.Controls.Add(this.textBoxSlaveAddress);
            this.groupBoxSlaveParameters.Location = new System.Drawing.Point(247, 12);
            this.groupBoxSlaveParameters.Name = "groupBoxSlaveParameters";
            this.groupBoxSlaveParameters.Size = new System.Drawing.Size(422, 100);
            this.groupBoxSlaveParameters.TabIndex = 8;
            this.groupBoxSlaveParameters.TabStop = false;
            this.groupBoxSlaveParameters.Text = "Slave Parameters";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(146, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Slave Delay (ms) =";
            // 
            // textBoxSlaveDelay
            // 
            this.textBoxSlaveDelay.Location = new System.Drawing.Point(244, 56);
            this.textBoxSlaveDelay.Name = "textBoxSlaveDelay";
            this.textBoxSlaveDelay.Size = new System.Drawing.Size(50, 20);
            this.textBoxSlaveDelay.TabIndex = 10;
            this.textBoxSlaveDelay.Text = "0";
            this.textBoxSlaveDelay.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxSlaveDelay_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(157, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Slave Address =";
            // 
            // textBoxSlaveAddress
            // 
            this.textBoxSlaveAddress.Location = new System.Drawing.Point(244, 31);
            this.textBoxSlaveAddress.Name = "textBoxSlaveAddress";
            this.textBoxSlaveAddress.Size = new System.Drawing.Size(50, 20);
            this.textBoxSlaveAddress.TabIndex = 8;
            this.textBoxSlaveAddress.Text = "1";
            this.textBoxSlaveAddress.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxSlaveAddress_Validating);
            // 
            // groupBoxConnParameters
            // 
            this.groupBoxConnParameters.Controls.Add(this.groupBoxParity);
            this.groupBoxConnParameters.Controls.Add(this.groupBoxStopBits);
            this.groupBoxConnParameters.Controls.Add(this.groupBoxDataBits);
            this.groupBoxConnParameters.Controls.Add(this.label5);
            this.groupBoxConnParameters.Controls.Add(this.textBoxBaud);
            this.groupBoxConnParameters.Controls.Add(this.label4);
            this.groupBoxConnParameters.Controls.Add(this.comboBoxSerialPorts);
            this.groupBoxConnParameters.Location = new System.Drawing.Point(247, 117);
            this.groupBoxConnParameters.Name = "groupBoxConnParameters";
            this.groupBoxConnParameters.Size = new System.Drawing.Size(422, 144);
            this.groupBoxConnParameters.TabIndex = 9;
            this.groupBoxConnParameters.TabStop = false;
            this.groupBoxConnParameters.Text = "Connection Parameters";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonGoToRegister);
            this.groupBox3.Controls.Add(this.dataGridViewRegisters);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.textBoxRegisterStart);
            this.groupBox3.Location = new System.Drawing.Point(9, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(221, 422);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Edit Holding Registers";
            // 
            // dataGridViewRegisters
            // 
            this.dataGridViewRegisters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRegisters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnRegisterNo,
            this.ColumnRegisterValue});
            this.dataGridViewRegisters.Location = new System.Drawing.Point(9, 63);
            this.dataGridViewRegisters.Name = "dataGridViewRegisters";
            this.dataGridViewRegisters.RowHeadersVisible = false;
            this.dataGridViewRegisters.Size = new System.Drawing.Size(211, 378);
            this.dataGridViewRegisters.TabIndex = 12;
            this.dataGridViewRegisters.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewRegisters_CellValidating);
            // 
            // ColumnRegisterNo
            // 
            this.ColumnRegisterNo.HeaderText = "Register No";
            this.ColumnRegisterNo.Name = "ColumnRegisterNo";
            // 
            // ColumnRegisterValue
            // 
            this.ColumnRegisterValue.HeaderText = "Register Value";
            this.ColumnRegisterValue.Name = "ColumnRegisterValue";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Go to Register No ->";
            // 
            // textBoxRegisterStart
            // 
            this.textBoxRegisterStart.Location = new System.Drawing.Point(113, 26);
            this.textBoxRegisterStart.Name = "textBoxRegisterStart";
            this.textBoxRegisterStart.Size = new System.Drawing.Size(52, 20);
            this.textBoxRegisterStart.TabIndex = 10;
            this.textBoxRegisterStart.Text = "0";
            // 
            // buttonGoToRegister
            // 
            this.buttonGoToRegister.Location = new System.Drawing.Point(177, 24);
            this.buttonGoToRegister.Name = "buttonGoToRegister";
            this.buttonGoToRegister.Size = new System.Drawing.Size(37, 23);
            this.buttonGoToRegister.TabIndex = 13;
            this.buttonGoToRegister.Text = "OK";
            this.buttonGoToRegister.UseVisualStyleBackColor = true;
            this.buttonGoToRegister.Click += new System.EventHandler(this.buttonGoToRegister_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.listBoxCommLog);
            this.groupBox4.Location = new System.Drawing.Point(247, 267);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(422, 167);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Communication Log";
            // 
            // listBoxCommLog
            // 
            this.listBoxCommLog.BackColor = System.Drawing.Color.Black;
            this.listBoxCommLog.ForeColor = System.Drawing.Color.LimeGreen;
            this.listBoxCommLog.FormattingEnabled = true;
            this.listBoxCommLog.Location = new System.Drawing.Point(14, 19);
            this.listBoxCommLog.Name = "listBoxCommLog";
            this.listBoxCommLog.Size = new System.Drawing.Size(396, 134);
            this.listBoxCommLog.TabIndex = 3;
            // 
            // _serialPort
            // 
            this._serialPort.PortName = "COM6";
            // 
            // comboBoxSerialPorts
            // 
            this.comboBoxSerialPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSerialPorts.FormattingEnabled = true;
            this.comboBoxSerialPorts.Location = new System.Drawing.Point(80, 48);
            this.comboBoxSerialPorts.Name = "comboBoxSerialPorts";
            this.comboBoxSerialPorts.Size = new System.Drawing.Size(58, 21);
            this.comboBoxSerialPorts.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Port Name =";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Baud =";
            // 
            // textBoxBaud
            // 
            this.textBoxBaud.Location = new System.Drawing.Point(80, 81);
            this.textBoxBaud.Name = "textBoxBaud";
            this.textBoxBaud.Size = new System.Drawing.Size(58, 20);
            this.textBoxBaud.TabIndex = 11;
            this.textBoxBaud.Text = "9600";
            // 
            // groupBoxDataBits
            // 
            this.groupBoxDataBits.Controls.Add(this.radioButtonDataBits8);
            this.groupBoxDataBits.Controls.Add(this.radioButtonDataBits7);
            this.groupBoxDataBits.Location = new System.Drawing.Point(244, 19);
            this.groupBoxDataBits.Name = "groupBoxDataBits";
            this.groupBoxDataBits.Size = new System.Drawing.Size(80, 103);
            this.groupBoxDataBits.TabIndex = 18;
            this.groupBoxDataBits.TabStop = false;
            this.groupBoxDataBits.Text = "Data Bits";
            // 
            // radioButtonDataBits7
            // 
            this.radioButtonDataBits7.AutoSize = true;
            this.radioButtonDataBits7.Location = new System.Drawing.Point(15, 29);
            this.radioButtonDataBits7.Name = "radioButtonDataBits7";
            this.radioButtonDataBits7.Size = new System.Drawing.Size(50, 17);
            this.radioButtonDataBits7.TabIndex = 0;
            this.radioButtonDataBits7.Text = "7 bits";
            this.radioButtonDataBits7.UseVisualStyleBackColor = true;
            // 
            // radioButtonDataBits8
            // 
            this.radioButtonDataBits8.AutoSize = true;
            this.radioButtonDataBits8.Checked = true;
            this.radioButtonDataBits8.Location = new System.Drawing.Point(15, 61);
            this.radioButtonDataBits8.Name = "radioButtonDataBits8";
            this.radioButtonDataBits8.Size = new System.Drawing.Size(50, 17);
            this.radioButtonDataBits8.TabIndex = 1;
            this.radioButtonDataBits8.TabStop = true;
            this.radioButtonDataBits8.Text = "8 bits";
            this.radioButtonDataBits8.UseVisualStyleBackColor = true;
            // 
            // groupBoxStopBits
            // 
            this.groupBoxStopBits.Controls.Add(this.radioButtonStopBit2);
            this.groupBoxStopBits.Controls.Add(this.radioButtonStopBit1);
            this.groupBoxStopBits.Location = new System.Drawing.Point(330, 19);
            this.groupBoxStopBits.Name = "groupBoxStopBits";
            this.groupBoxStopBits.Size = new System.Drawing.Size(80, 103);
            this.groupBoxStopBits.TabIndex = 19;
            this.groupBoxStopBits.TabStop = false;
            this.groupBoxStopBits.Text = "Stop Bits";
            // 
            // radioButtonStopBit2
            // 
            this.radioButtonStopBit2.AutoSize = true;
            this.radioButtonStopBit2.Location = new System.Drawing.Point(15, 61);
            this.radioButtonStopBit2.Name = "radioButtonStopBit2";
            this.radioButtonStopBit2.Size = new System.Drawing.Size(50, 17);
            this.radioButtonStopBit2.TabIndex = 1;
            this.radioButtonStopBit2.Text = "2 bits";
            this.radioButtonStopBit2.UseVisualStyleBackColor = true;
            // 
            // radioButtonStopBit1
            // 
            this.radioButtonStopBit1.AutoSize = true;
            this.radioButtonStopBit1.Checked = true;
            this.radioButtonStopBit1.Location = new System.Drawing.Point(15, 29);
            this.radioButtonStopBit1.Name = "radioButtonStopBit1";
            this.radioButtonStopBit1.Size = new System.Drawing.Size(45, 17);
            this.radioButtonStopBit1.TabIndex = 0;
            this.radioButtonStopBit1.TabStop = true;
            this.radioButtonStopBit1.Text = "1 bit";
            this.radioButtonStopBit1.UseVisualStyleBackColor = true;
            // 
            // groupBoxParity
            // 
            this.groupBoxParity.Controls.Add(this.radioButtonParityOdd);
            this.groupBoxParity.Controls.Add(this.radioButtonParityEven);
            this.groupBoxParity.Controls.Add(this.radioButtonParityNone);
            this.groupBoxParity.Location = new System.Drawing.Point(158, 19);
            this.groupBoxParity.Name = "groupBoxParity";
            this.groupBoxParity.Size = new System.Drawing.Size(80, 103);
            this.groupBoxParity.TabIndex = 20;
            this.groupBoxParity.TabStop = false;
            this.groupBoxParity.Text = "Parity";
            // 
            // radioButtonParityEven
            // 
            this.radioButtonParityEven.AutoSize = true;
            this.radioButtonParityEven.Location = new System.Drawing.Point(15, 69);
            this.radioButtonParityEven.Name = "radioButtonParityEven";
            this.radioButtonParityEven.Size = new System.Drawing.Size(50, 17);
            this.radioButtonParityEven.TabIndex = 1;
            this.radioButtonParityEven.Text = "Even";
            this.radioButtonParityEven.UseVisualStyleBackColor = true;
            // 
            // radioButtonParityNone
            // 
            this.radioButtonParityNone.AutoSize = true;
            this.radioButtonParityNone.Checked = true;
            this.radioButtonParityNone.Location = new System.Drawing.Point(15, 24);
            this.radioButtonParityNone.Name = "radioButtonParityNone";
            this.radioButtonParityNone.Size = new System.Drawing.Size(51, 17);
            this.radioButtonParityNone.TabIndex = 0;
            this.radioButtonParityNone.TabStop = true;
            this.radioButtonParityNone.Text = "None";
            this.radioButtonParityNone.UseVisualStyleBackColor = true;
            // 
            // radioButtonParityOdd
            // 
            this.radioButtonParityOdd.AutoSize = true;
            this.radioButtonParityOdd.Location = new System.Drawing.Point(15, 47);
            this.radioButtonParityOdd.Name = "radioButtonParityOdd";
            this.radioButtonParityOdd.Size = new System.Drawing.Size(45, 17);
            this.radioButtonParityOdd.TabIndex = 2;
            this.radioButtonParityOdd.Text = "Odd";
            this.radioButtonParityOdd.UseVisualStyleBackColor = true;
            // 
            // radioButtonOn
            // 
            this.radioButtonOn.AutoSize = true;
            this.radioButtonOn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonOn.ForeColor = System.Drawing.Color.DarkGreen;
            this.radioButtonOn.Location = new System.Drawing.Point(27, 27);
            this.radioButtonOn.Name = "radioButtonOn";
            this.radioButtonOn.Size = new System.Drawing.Size(50, 24);
            this.radioButtonOn.TabIndex = 12;
            this.radioButtonOn.Text = "On";
            this.radioButtonOn.UseVisualStyleBackColor = true;
            this.radioButtonOn.CheckedChanged += new System.EventHandler(this.radioButtonOn_CheckedChanged);
            // 
            // radioButtonOff
            // 
            this.radioButtonOff.AutoSize = true;
            this.radioButtonOff.Checked = true;
            this.radioButtonOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonOff.ForeColor = System.Drawing.Color.Red;
            this.radioButtonOff.Location = new System.Drawing.Point(27, 57);
            this.radioButtonOff.Name = "radioButtonOff";
            this.radioButtonOff.Size = new System.Drawing.Size(52, 24);
            this.radioButtonOff.TabIndex = 13;
            this.radioButtonOff.TabStop = true;
            this.radioButtonOff.Text = "Off";
            this.radioButtonOff.UseVisualStyleBackColor = true;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // FormMainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 456);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBoxConnParameters);
            this.Controls.Add(this.groupBoxSlaveParameters);
            this.Name = "FormMainScreen";
            this.Text = "Modbus Software Slave";
            this.groupBoxSlaveParameters.ResumeLayout(false);
            this.groupBoxSlaveParameters.PerformLayout();
            this.groupBoxConnParameters.ResumeLayout(false);
            this.groupBoxConnParameters.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRegisters)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBoxDataBits.ResumeLayout(false);
            this.groupBoxDataBits.PerformLayout();
            this.groupBoxStopBits.ResumeLayout(false);
            this.groupBoxStopBits.PerformLayout();
            this.groupBoxParity.ResumeLayout(false);
            this.groupBoxParity.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxSlaveParameters;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSlaveDelay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSlaveAddress;
        private System.Windows.Forms.GroupBox groupBoxConnParameters;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxRegisterStart;
        private System.Windows.Forms.DataGridView dataGridViewRegisters;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnRegisterNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnRegisterValue;
        private System.Windows.Forms.Button buttonGoToRegister;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListBox listBoxCommLog;
        private System.IO.Ports.SerialPort _serialPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxSerialPorts;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxBaud;
        private System.Windows.Forms.GroupBox groupBoxDataBits;
        private System.Windows.Forms.GroupBox groupBoxStopBits;
        private System.Windows.Forms.RadioButton radioButtonStopBit2;
        private System.Windows.Forms.RadioButton radioButtonStopBit1;
        private System.Windows.Forms.RadioButton radioButtonDataBits8;
        private System.Windows.Forms.RadioButton radioButtonDataBits7;
        private System.Windows.Forms.GroupBox groupBoxParity;
        private System.Windows.Forms.RadioButton radioButtonParityEven;
        private System.Windows.Forms.RadioButton radioButtonParityNone;
        private System.Windows.Forms.RadioButton radioButtonParityOdd;
        private System.Windows.Forms.RadioButton radioButtonOff;
        private System.Windows.Forms.RadioButton radioButtonOn;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

