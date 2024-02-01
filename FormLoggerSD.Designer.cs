using System;

namespace InfluxApps.Forms
{
    partial class FormLoggerSD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLoggerSD));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip2 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnDownload = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnDownloadRXD = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDownloadMF4 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDownloadASC = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDownloadBLF = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDownloadTRC = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDownloadMAT = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDownloadCSV = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDownloadAdvanced = new System.Windows.Forms.ToolStripMenuItem();
            this.btnShowDatalog = new System.Windows.Forms.ToolStripButton();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFormatSD = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tbFind = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSDInfo = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dlgFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.gridFiles = new System.Windows.Forms.DataGridView();
            this.Locked = new System.Windows.Forms.DataGridViewImageColumn();
            this.ConfigName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlClient = new System.Windows.Forms.Panel();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridFiles)).BeginInit();
            this.pnlClient.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip2
            // 
            this.toolStrip2.ClickThrough = true;
            this.toolStrip2.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStrip2.Image = null;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefresh,
            this.btnDownload,
            this.toolStripDropDownButton1,
            this.btnShowDatalog,
            this.btnExport,
            this.toolStripSeparator1,
            this.btnFormatSD,
            this.toolStripSeparator3,
            this.tbFind,
            this.toolStripButton6});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Office12Mode = false;
            this.toolStrip2.ShowCaption = false;
            this.toolStrip2.ShowItemToolTips = true;
            this.toolStrip2.Size = new System.Drawing.Size(800, 31);
            this.toolStrip2.TabIndex = 6;
            this.toolStrip2.Text = "toolStrip2";
            this.toolStrip2.ThemeName = "Office2016White";
            this.toolStrip2.VisualStyle = Syncfusion.Windows.Forms.Tools.ToolStripExStyle.Office2016White;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(74, 28);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Enabled = false;
            this.btnDownload.Image = ((System.Drawing.Image)(resources.GetObject("btnDownload.Image")));
            this.btnDownload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(89, 28);
            this.btnDownload.Text = "Download";
            this.btnDownload.Visible = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDownloadRXD,
            this.btnDownloadMF4,
            this.btnDownloadASC,
            this.btnDownloadBLF,
            this.btnDownloadTRC,
            this.btnDownloadMAT,
            this.btnDownloadCSV,
            this.toolStripSeparator2,
            this.btnDownloadAdvanced});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(98, 28);
            this.toolStripDropDownButton1.Text = "Download";
            // 
            // btnDownloadRXD
            // 
            this.btnDownloadRXD.Name = "btnDownloadRXD";
            this.btnDownloadRXD.Size = new System.Drawing.Size(209, 22);
            this.btnDownloadRXD.Text = "ReX data (rxd)";
            this.btnDownloadRXD.Click += new System.EventHandler(this.btnDownloadRXD_Click);
            // 
            // btnDownloadMF4
            // 
            this.btnDownloadMF4.Name = "btnDownloadMF4";
            this.btnDownloadMF4.Size = new System.Drawing.Size(209, 22);
            this.btnDownloadMF4.Text = "ASAM MDF (mf4)";
            this.btnDownloadMF4.Click += new System.EventHandler(this.btnDownloadMF4_Click);
            // 
            // btnDownloadASC
            // 
            this.btnDownloadASC.Name = "btnDownloadASC";
            this.btnDownloadASC.Size = new System.Drawing.Size(209, 22);
            this.btnDownloadASC.Text = "ASCII Logging file (asc)";
            this.btnDownloadASC.Click += new System.EventHandler(this.btnDownloadASC_Click);
            // 
            // btnDownloadBLF
            // 
            this.btnDownloadBLF.Name = "btnDownloadBLF";
            this.btnDownloadBLF.Size = new System.Drawing.Size(209, 22);
            this.btnDownloadBLF.Text = "Vector binary frames (blf)";
            this.btnDownloadBLF.Click += new System.EventHandler(this.btnDownloadBLF_Click);
            // 
            // btnDownloadTRC
            // 
            this.btnDownloadTRC.Name = "btnDownloadTRC";
            this.btnDownloadTRC.Size = new System.Drawing.Size(209, 22);
            this.btnDownloadTRC.Text = "Peak Can Trace File (*.trc)";
            this.btnDownloadTRC.Click += new System.EventHandler(this.btnDownloadTRC_Click);
            // 
            // btnDownloadMAT
            // 
            this.btnDownloadMAT.Name = "btnDownloadMAT";
            this.btnDownloadMAT.Size = new System.Drawing.Size(209, 22);
            this.btnDownloadMAT.Text = "Matlab (mat)";
            this.btnDownloadMAT.Click += new System.EventHandler(this.btnDownloadMAT_Click);
            // 
            // btnDownloadCSV
            // 
            this.btnDownloadCSV.Name = "btnDownloadCSV";
            this.btnDownloadCSV.Size = new System.Drawing.Size(209, 22);
            this.btnDownloadCSV.Text = "Comma delimited (csv)";
            this.btnDownloadCSV.Click += new System.EventHandler(this.btnDownloadCSV_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(206, 6);
            // 
            // btnDownloadAdvanced
            // 
            this.btnDownloadAdvanced.Name = "btnDownloadAdvanced";
            this.btnDownloadAdvanced.Size = new System.Drawing.Size(209, 22);
            this.btnDownloadAdvanced.Text = "Advanced";
            this.btnDownloadAdvanced.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnShowDatalog
            // 
            this.btnShowDatalog.Enabled = false;
            this.btnShowDatalog.Image = ((System.Drawing.Image)(resources.GetObject("btnShowDatalog.Image")));
            this.btnShowDatalog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowDatalog.Name = "btnShowDatalog";
            this.btnShowDatalog.Size = new System.Drawing.Size(107, 28);
            this.btnShowDatalog.Text = "Show datalog";
            this.btnShowDatalog.Click += new System.EventHandler(this.btnShowDatalog_Click);
            // 
            // btnExport
            // 
            this.btnExport.Image = global::InfluxApps.Properties.Resources.save_24;
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(68, 28);
            this.btnExport.Text = "Export";
            this.btnExport.Visible = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // btnFormatSD
            // 
            this.btnFormatSD.Image = ((System.Drawing.Image)(resources.GetObject("btnFormatSD.Image")));
            this.btnFormatSD.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFormatSD.Name = "btnFormatSD";
            this.btnFormatSD.Size = new System.Drawing.Size(90, 28);
            this.btnFormatSD.Text = "Format SD";
            this.btnFormatSD.Click += new System.EventHandler(this.btnFormatSD_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // tbFind
            // 
            this.tbFind.Name = "tbFind";
            this.tbFind.Size = new System.Drawing.Size(150, 31);
            this.tbFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbFind_KeyPress);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(28, 28);
            this.toolStripButton6.Text = "toolStripButton6";
            this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // dlgSave
            // 
            this.dlgSave.DefaultExt = "ASAM MDF4 file (*.mf4)|*.mf4";
            this.dlgSave.Filter = "ASAM MDF4 file (*.mf4)|*.mf4|ReX data (*.rxd)|*.rxd";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblSDInfo);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 418);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 32);
            this.panel1.TabIndex = 7;
            // 
            // lblSDInfo
            // 
            this.lblSDInfo.BackColor = System.Drawing.Color.White;
            this.lblSDInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSDInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSDInfo.Location = new System.Drawing.Point(24, 0);
            this.lblSDInfo.Name = "lblSDInfo";
            this.lblSDInfo.Size = new System.Drawing.Size(776, 32);
            this.lblSDInfo.TabIndex = 1;
            this.lblSDInfo.Text = "SD Card Information: ";
            this.lblSDInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.pictureBox1.Size = new System.Drawing.Size(24, 32);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // gridFiles
            // 
            this.gridFiles.AllowUserToAddRows = false;
            this.gridFiles.AllowUserToDeleteRows = false;
            this.gridFiles.AllowUserToResizeRows = false;
            this.gridFiles.BackgroundColor = System.Drawing.Color.White;
            this.gridFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridFiles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Locked,
            this.ConfigName,
            this.colStartTime,
            this.colEndTime,
            this.colSize});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSeaGreen;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridFiles.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridFiles.EnableHeadersVisualStyles = false;
            this.gridFiles.GridColor = System.Drawing.Color.White;
            this.gridFiles.Location = new System.Drawing.Point(0, 0);
            this.gridFiles.Name = "gridFiles";
            this.gridFiles.ReadOnly = true;
            this.gridFiles.RowHeadersVisible = false;
            this.gridFiles.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.gridFiles.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridFiles.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gridFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridFiles.Size = new System.Drawing.Size(800, 387);
            this.gridFiles.TabIndex = 8;
            this.gridFiles.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridFiles_CellDoubleClick);
            this.gridFiles.SelectionChanged += new System.EventHandler(this.gridFiles_SelectionChanged);
            // 
            // Locked
            // 
            this.Locked.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Locked.DataPropertyName = "EncryptedImg";
            this.Locked.HeaderText = "";
            this.Locked.Name = "Locked";
            this.Locked.ReadOnly = true;
            this.Locked.Width = 30;
            // 
            // ConfigName
            // 
            this.ConfigName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ConfigName.DataPropertyName = "ConfigName";
            this.ConfigName.HeaderText = "Configuration name";
            this.ConfigName.Name = "ConfigName";
            this.ConfigName.ReadOnly = true;
            this.ConfigName.Width = 350;
            // 
            // colStartTime
            // 
            this.colStartTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colStartTime.DataPropertyName = "StartTimeString";
            this.colStartTime.HeaderText = "Start time";
            this.colStartTime.Name = "colStartTime";
            this.colStartTime.ReadOnly = true;
            this.colStartTime.Width = 150;
            // 
            // colEndTime
            // 
            this.colEndTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colEndTime.DataPropertyName = "EndTimeString";
            this.colEndTime.HeaderText = "End time";
            this.colEndTime.Name = "colEndTime";
            this.colEndTime.ReadOnly = true;
            this.colEndTime.Width = 150;
            // 
            // colSize
            // 
            this.colSize.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colSize.DataPropertyName = "FileSizeString";
            this.colSize.HeaderText = "Size";
            this.colSize.Name = "colSize";
            this.colSize.ReadOnly = true;
            // 
            // pnlClient
            // 
            this.pnlClient.Controls.Add(this.gridFiles);
            this.pnlClient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlClient.Location = new System.Drawing.Point(0, 31);
            this.pnlClient.Name = "pnlClient";
            this.pnlClient.Size = new System.Drawing.Size(800, 387);
            this.pnlClient.TabIndex = 9;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ConfigName";
            this.dataGridViewTextBoxColumn1.HeaderText = "Configuration name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 250;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "StartTimeString";
            this.dataGridViewTextBoxColumn2.HeaderText = "Start time";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "EndTimeString";
            this.dataGridViewTextBoxColumn3.HeaderText = "End time";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "FileSizeString";
            this.dataGridViewTextBoxColumn4.HeaderText = "Size";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // FormLoggerSD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlClient);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormLoggerSD";
            this.Text = "Internal Storage";
            this.Load += new System.EventHandler(this.FormLoggerSD_Load);
            this.Shown += new System.EventHandler(this.FormLoggerSD_Shown);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridFiles)).EndInit();
            this.pnlClient.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Syncfusion.Windows.Forms.Tools.ToolStripEx toolStrip2;
        private System.Windows.Forms.ToolStripButton btnDownload;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripTextBox tbFind;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.ToolStripButton btnFormatSD;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblSDInfo;
        private System.Windows.Forms.ToolStripButton btnShowDatalog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.FolderBrowserDialog dlgFolder;
        private System.Windows.Forms.DataGridView gridFiles;
        private System.Windows.Forms.ToolStripButton btnExport;
        private System.Windows.Forms.Panel pnlClient;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.ToolStripMenuItem btnDownloadRXD;
        private System.Windows.Forms.ToolStripMenuItem btnDownloadMF4;
        private System.Windows.Forms.ToolStripMenuItem btnDownloadASC;
        private System.Windows.Forms.ToolStripMenuItem btnDownloadBLF;
        private System.Windows.Forms.ToolStripMenuItem btnDownloadMAT;
        private System.Windows.Forms.ToolStripMenuItem btnDownloadCSV;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem btnDownloadAdvanced;
        private System.Windows.Forms.DataGridViewImageColumn Locked;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConfigName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEndTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSize;
        private System.Windows.Forms.ToolStripMenuItem btnDownloadTRC;
    }
}