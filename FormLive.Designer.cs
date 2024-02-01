namespace ReXgenAPIDemo
{
    partial class FormLive
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLive));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.tmrLiveData = new System.Windows.Forms.Timer(this.components);
            this.gridTrace = new System.Windows.Forms.DataGridView();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ident = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Flags = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DLC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTrace)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(51, 22);
            this.toolStripButton1.Text = "Start";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(51, 22);
            this.toolStripButton2.Text = "Stop";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // tmrLiveData
            // 
            this.tmrLiveData.Interval = 10;
            this.tmrLiveData.Tick += new System.EventHandler(this.tmrLiveData_Tick);
            // 
            // gridTrace
            // 
            this.gridTrace.AllowUserToAddRows = false;
            this.gridTrace.AllowUserToDeleteRows = false;
            this.gridTrace.AllowUserToOrderColumns = true;
            this.gridTrace.AllowUserToResizeRows = false;
            this.gridTrace.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridTrace.BackgroundColor = System.Drawing.Color.White;
            this.gridTrace.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridTrace.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridTrace.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Time,
            this.Bus,
            this.Ident,
            this.Flags,
            this.DLC,
            this.Data});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSeaGreen;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridTrace.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridTrace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTrace.EnableHeadersVisualStyles = false;
            this.gridTrace.GridColor = System.Drawing.SystemColors.ControlLight;
            this.gridTrace.Location = new System.Drawing.Point(0, 25);
            this.gridTrace.Name = "gridTrace";
            this.gridTrace.ReadOnly = true;
            this.gridTrace.RowHeadersVisible = false;
            this.gridTrace.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightSeaGreen;
            this.gridTrace.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gridTrace.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTrace.ShowCellErrors = false;
            this.gridTrace.ShowCellToolTips = false;
            this.gridTrace.ShowEditingIcon = false;
            this.gridTrace.ShowRowErrors = false;
            this.gridTrace.Size = new System.Drawing.Size(800, 425);
            this.gridTrace.TabIndex = 7;
            // 
            // Time
            // 
            this.Time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Time.DataPropertyName = "Timestamp";
            this.Time.FillWeight = 113.4199F;
            this.Time.HeaderText = "Time";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            this.Time.Width = 120;
            // 
            // Bus
            // 
            this.Bus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Bus.DataPropertyName = "BusChannel";
            this.Bus.FillWeight = 74.58418F;
            this.Bus.HeaderText = "Bus";
            this.Bus.Name = "Bus";
            this.Bus.ReadOnly = true;
            this.Bus.Width = 60;
            // 
            // Ident
            // 
            this.Ident.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Ident.DataPropertyName = "CanID";
            this.Ident.FillWeight = 139.7873F;
            this.Ident.HeaderText = "Ident";
            this.Ident.Name = "Ident";
            this.Ident.ReadOnly = true;
            this.Ident.Width = 85;
            // 
            // Flags
            // 
            this.Flags.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Flags.DataPropertyName = "Flags";
            this.Flags.FillWeight = 158.9582F;
            this.Flags.HeaderText = "Flags";
            this.Flags.Name = "Flags";
            this.Flags.ReadOnly = true;
            this.Flags.Width = 60;
            // 
            // DLC
            // 
            this.DLC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DLC.DataPropertyName = "DLC";
            this.DLC.FillWeight = 106.599F;
            this.DLC.HeaderText = "DLC";
            this.DLC.Name = "DLC";
            this.DLC.ReadOnly = true;
            this.DLC.Width = 45;
            // 
            // Data
            // 
            this.Data.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Data.DataPropertyName = "Data";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Data.DefaultCellStyle = dataGridViewCellStyle2;
            this.Data.FillWeight = 6.651387F;
            this.Data.HeaderText = "Data";
            this.Data.Name = "Data";
            this.Data.ReadOnly = true;
            // 
            // FormLive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gridTrace);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormLive";
            this.Text = "FormLive";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTrace)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Timer tmrLiveData;
        private System.Windows.Forms.DataGridView gridTrace;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ident;
        private System.Windows.Forms.DataGridViewTextBoxColumn Flags;
        private System.Windows.Forms.DataGridViewTextBoxColumn DLC;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data;
    }
}