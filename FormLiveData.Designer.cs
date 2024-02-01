
namespace ReXgenAPIDemo
{
    partial class FormLiveData
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gridTrace = new System.Windows.Forms.DataGridView();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ident = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Flags = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DLC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnStartLive = new System.Windows.Forms.Button();
            this.btnStopLive = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridTrace)).BeginInit();
            this.SuspendLayout();
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
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridTrace.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.gridTrace.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Time,
            this.Bus,
            this.Ident,
            this.Flags,
            this.DLC,
            this.Data});
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.LightSeaGreen;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridTrace.DefaultCellStyle = dataGridViewCellStyle15;
            this.gridTrace.EnableHeadersVisualStyles = false;
            this.gridTrace.GridColor = System.Drawing.SystemColors.ControlLight;
            this.gridTrace.Location = new System.Drawing.Point(160, 35);
            this.gridTrace.Name = "gridTrace";
            this.gridTrace.ReadOnly = true;
            this.gridTrace.RowHeadersVisible = false;
            this.gridTrace.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.LightSeaGreen;
            this.gridTrace.RowsDefaultCellStyle = dataGridViewCellStyle16;
            this.gridTrace.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTrace.Size = new System.Drawing.Size(628, 403);
            this.gridTrace.TabIndex = 9;
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
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Data.DefaultCellStyle = dataGridViewCellStyle14;
            this.Data.FillWeight = 6.651387F;
            this.Data.HeaderText = "Data";
            this.Data.Name = "Data";
            this.Data.ReadOnly = true;
            // 
            // btnStartLive
            // 
            this.btnStartLive.Location = new System.Drawing.Point(21, 70);
            this.btnStartLive.Name = "btnStartLive";
            this.btnStartLive.Size = new System.Drawing.Size(101, 23);
            this.btnStartLive.TabIndex = 10;
            this.btnStartLive.Text = "Start Live Data";
            this.btnStartLive.UseVisualStyleBackColor = true;
            this.btnStartLive.Click += new System.EventHandler(this.btnStartLive_Click);
            // 
            // btnStopLive
            // 
            this.btnStopLive.Location = new System.Drawing.Point(21, 116);
            this.btnStopLive.Name = "btnStopLive";
            this.btnStopLive.Size = new System.Drawing.Size(101, 23);
            this.btnStopLive.TabIndex = 11;
            this.btnStopLive.Text = "Stop Live Data";
            this.btnStopLive.UseVisualStyleBackColor = true;
            this.btnStopLive.Click += new System.EventHandler(this.btnStopLive_Click);
            // 
            // FormLiveData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnStopLive);
            this.Controls.Add(this.btnStartLive);
            this.Controls.Add(this.gridTrace);
            this.Name = "FormLiveData";
            this.Text = "Live Data";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLiveData_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.gridTrace)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridTrace;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ident;
        private System.Windows.Forms.DataGridViewTextBoxColumn Flags;
        private System.Windows.Forms.DataGridViewTextBoxColumn DLC;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data;
        private System.Windows.Forms.Button btnStartLive;
        private System.Windows.Forms.Button btnStopLive;
    }
}