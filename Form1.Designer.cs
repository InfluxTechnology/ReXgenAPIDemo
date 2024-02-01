
namespace ReXgenAPIDemo
{
    partial class Form1
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
            this.btnSendConfiguration = new System.Windows.Forms.Button();
            this.dlgOpenRxc = new System.Windows.Forms.OpenFileDialog();
            this.btnReflash = new System.Windows.Forms.Button();
            this.dlgOpenReflash = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerReflash = new System.Windows.Forms.Timer(this.components);
            this.btnLogCount = new System.Windows.Forms.Button();
            this.lblLogCount = new System.Windows.Forms.Label();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.btnGetDateTime = new System.Windows.Forms.Button();
            this.btnSetDateTime = new System.Windows.Forms.Button();
            this.btnLiveData = new System.Windows.Forms.Button();
            this.btnGetFirmware = new System.Windows.Forms.Button();
            this.lblGetFirmware = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnConvertRXD = new System.Windows.Forms.Button();
            this.btnXMLToRXC = new System.Windows.Forms.Button();
            this.dlgOpenXML = new System.Windows.Forms.OpenFileDialog();
            this.dlgSaveRXC = new System.Windows.Forms.SaveFileDialog();
            this.dlgopenRXD = new System.Windows.Forms.OpenFileDialog();
            this.dlgSaveConvertedData = new System.Windows.Forms.SaveFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSendConfiguration
            // 
            this.btnSendConfiguration.Location = new System.Drawing.Point(26, 67);
            this.btnSendConfiguration.Name = "btnSendConfiguration";
            this.btnSendConfiguration.Size = new System.Drawing.Size(133, 23);
            this.btnSendConfiguration.TabIndex = 0;
            this.btnSendConfiguration.Text = "Send Configuration";
            this.btnSendConfiguration.UseVisualStyleBackColor = true;
            this.btnSendConfiguration.Click += new System.EventHandler(this.btnSendConfiguration_Click);
            // 
            // dlgOpenRxc
            // 
            this.dlgOpenRxc.FileName = "openFileDialog1";
            this.dlgOpenRxc.Filter = "(*.rxc)|*.rxc";
            // 
            // btnReflash
            // 
            this.btnReflash.Location = new System.Drawing.Point(26, 107);
            this.btnReflash.Name = "btnReflash";
            this.btnReflash.Size = new System.Drawing.Size(101, 23);
            this.btnReflash.TabIndex = 1;
            this.btnReflash.Text = "Reflash Logger";
            this.btnReflash.UseVisualStyleBackColor = true;
            this.btnReflash.Click += new System.EventHandler(this.btnReflash_Click);
            // 
            // dlgOpenReflash
            // 
            this.dlgOpenReflash.FileName = "dlgOpenReflash";
            this.dlgOpenReflash.Filter = "(*.bin)|*.bin";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(384, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // timerReflash
            // 
            this.timerReflash.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnLogCount
            // 
            this.btnLogCount.Location = new System.Drawing.Point(26, 149);
            this.btnLogCount.Name = "btnLogCount";
            this.btnLogCount.Size = new System.Drawing.Size(101, 23);
            this.btnLogCount.TabIndex = 3;
            this.btnLogCount.Text = "Get Log Count";
            this.btnLogCount.UseVisualStyleBackColor = true;
            this.btnLogCount.Click += new System.EventHandler(this.btnLogCount_Click);
            // 
            // lblLogCount
            // 
            this.lblLogCount.AutoSize = true;
            this.lblLogCount.Location = new System.Drawing.Point(159, 154);
            this.lblLogCount.Name = "lblLogCount";
            this.lblLogCount.Size = new System.Drawing.Size(0, 13);
            this.lblLogCount.TabIndex = 4;
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Location = new System.Drawing.Point(157, 192);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(0, 13);
            this.lblDateTime.TabIndex = 6;
            // 
            // btnGetDateTime
            // 
            this.btnGetDateTime.Location = new System.Drawing.Point(26, 187);
            this.btnGetDateTime.Name = "btnGetDateTime";
            this.btnGetDateTime.Size = new System.Drawing.Size(101, 23);
            this.btnGetDateTime.TabIndex = 5;
            this.btnGetDateTime.Text = "Get Date Time";
            this.btnGetDateTime.UseVisualStyleBackColor = true;
            this.btnGetDateTime.Click += new System.EventHandler(this.btnGetDateTime_Click);
            // 
            // btnSetDateTime
            // 
            this.btnSetDateTime.Location = new System.Drawing.Point(26, 229);
            this.btnSetDateTime.Name = "btnSetDateTime";
            this.btnSetDateTime.Size = new System.Drawing.Size(133, 23);
            this.btnSetDateTime.TabIndex = 7;
            this.btnSetDateTime.Text = "Set Current Date Time";
            this.btnSetDateTime.UseVisualStyleBackColor = true;
            this.btnSetDateTime.Click += new System.EventHandler(this.btnSetDateTime_Click);
            // 
            // btnLiveData
            // 
            this.btnLiveData.Location = new System.Drawing.Point(26, 381);
            this.btnLiveData.Name = "btnLiveData";
            this.btnLiveData.Size = new System.Drawing.Size(75, 23);
            this.btnLiveData.TabIndex = 8;
            this.btnLiveData.Text = "Live Data";
            this.btnLiveData.UseVisualStyleBackColor = true;
            this.btnLiveData.Click += new System.EventHandler(this.btnLiveData_Click);
            // 
            // btnGetFirmware
            // 
            this.btnGetFirmware.Location = new System.Drawing.Point(26, 275);
            this.btnGetFirmware.Name = "btnGetFirmware";
            this.btnGetFirmware.Size = new System.Drawing.Size(131, 23);
            this.btnGetFirmware.TabIndex = 9;
            this.btnGetFirmware.Text = "Get Firmware Version";
            this.btnGetFirmware.UseVisualStyleBackColor = true;
            this.btnGetFirmware.Click += new System.EventHandler(this.btnGetFirmware_Click);
            // 
            // lblGetFirmware
            // 
            this.lblGetFirmware.AutoSize = true;
            this.lblGetFirmware.Location = new System.Drawing.Point(176, 280);
            this.lblGetFirmware.Name = "lblGetFirmware";
            this.lblGetFirmware.Size = new System.Drawing.Size(0, 13);
            this.lblGetFirmware.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(26, 339);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 24);
            this.button1.TabIndex = 11;
            this.button1.Text = "Browse SD Card";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnConvertRXD
            // 
            this.btnConvertRXD.Location = new System.Drawing.Point(235, 22);
            this.btnConvertRXD.Name = "btnConvertRXD";
            this.btnConvertRXD.Size = new System.Drawing.Size(103, 23);
            this.btnConvertRXD.TabIndex = 13;
            this.btnConvertRXD.Text = "Convert RXD";
            this.btnConvertRXD.UseVisualStyleBackColor = true;
            this.btnConvertRXD.Click += new System.EventHandler(this.btnConvertRXD_Click);
            // 
            // btnXMLToRXC
            // 
            this.btnXMLToRXC.Location = new System.Drawing.Point(26, 22);
            this.btnXMLToRXC.Name = "btnXMLToRXC";
            this.btnXMLToRXC.Size = new System.Drawing.Size(91, 23);
            this.btnXMLToRXC.TabIndex = 12;
            this.btnXMLToRXC.Text = "XML to RXC";
            this.btnXMLToRXC.UseVisualStyleBackColor = true;
            this.btnXMLToRXC.Click += new System.EventHandler(this.btnXMLToRXC_Click);
            // 
            // dlgOpenXML
            // 
            this.dlgOpenXML.FileName = "openFileDialog1";
            this.dlgOpenXML.Filter = "*.xml|*.xml";
            // 
            // dlgSaveRXC
            // 
            this.dlgSaveRXC.Filter = "*.rxc|*.rxc";
            // 
            // dlgopenRXD
            // 
            this.dlgopenRXD.FileName = "openFileDialog2";
            this.dlgopenRXD.Filter = "*.rxd|*.rxd";
            // 
            // dlgSaveConvertedData
            // 
            this.dlgSaveConvertedData.Filter = "MDF Files|*.mf4|CSV Files|*.csv|BLF Files|*.blf|Matlab Files|*.mat|ASCII File|*.a" +
    "sc";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(235, 67);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "Format SD Card";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnConvertRXD);
            this.Controls.Add(this.btnXMLToRXC);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblGetFirmware);
            this.Controls.Add(this.btnGetFirmware);
            this.Controls.Add(this.btnLiveData);
            this.Controls.Add(this.btnSetDateTime);
            this.Controls.Add(this.lblDateTime);
            this.Controls.Add(this.btnGetDateTime);
            this.Controls.Add(this.lblLogCount);
            this.Controls.Add(this.btnLogCount);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnReflash);
            this.Controls.Add(this.btnSendConfiguration);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReXgen API Demo";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSendConfiguration;
        private System.Windows.Forms.OpenFileDialog dlgOpenRxc;
        private System.Windows.Forms.Button btnReflash;
        private System.Windows.Forms.OpenFileDialog dlgOpenReflash;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Timer timerReflash;
        private System.Windows.Forms.Button btnLogCount;
        private System.Windows.Forms.Label lblLogCount;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Button btnGetDateTime;
        private System.Windows.Forms.Button btnSetDateTime;
        private System.Windows.Forms.Button btnLiveData;
        private System.Windows.Forms.Button btnGetFirmware;
        private System.Windows.Forms.Label lblGetFirmware;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnConvertRXD;
        private System.Windows.Forms.Button btnXMLToRXC;
        private System.Windows.Forms.OpenFileDialog dlgOpenXML;
        private System.Windows.Forms.SaveFileDialog dlgSaveRXC;
        private System.Windows.Forms.OpenFileDialog dlgopenRXD;
        private System.Windows.Forms.SaveFileDialog dlgSaveConvertedData;
        private System.Windows.Forms.Button button2;
    }
}

