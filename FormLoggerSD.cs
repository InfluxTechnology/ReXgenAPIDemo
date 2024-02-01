using Influx.Shared.Helpers;
using InfluxApps.Objects;
using InfluxShared.FileObjects;
using InfluxShared.Helpers;
using MatlabFile.Base;
using MDF4xx.IO;
using RXD.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using USBDll;

namespace InfluxApps.Forms
{
    public partial class FormLoggerSD : Form
    {
        const string FileNamePattern = "{0}_{1}_{2}";
        const string FileStartLogPattern = "yyyy-MM-dd HH:mm:ss";
        public static List<string> PartitionName = new List<string>() { "Master", "Backup 1", "Backup 2", "Backup 3" };

        class LogDetails
        {
            public string tmpFileName;
            public uint StartSector;
            public uint EndSector;
            public uint LogSize;
            public bool isEncrypted;
            public string FileSizeString => LogSize.ToFormatedFileSize();

            public string outFileName;

            public string ConfigName { get; set; }

            public DateTime StartTime;
            public string StartTimeString => StartTime.ToString(FileStartLogPattern);

            public DateTime EndTime;
            public string EndTimeString => EndTime.ToString(FileStartLogPattern);
        }

        class SDPartition
        {
            public List<LogDetails> sdDatalogs;
            public BindingSource bs = new BindingSource();
            public DataGridView dg;
        }

        TabControl tabPartitions = null;
        ExportDbcCollection expSignals = null;
        SDPartition[] Partitions = null;

        LogDetails ActiveLog = null;
        List<LogDetails> SelectedDatalogs;
        int DatalogIndex = 0;

        ProgressHandler pHandler = null;

        bool MergedExport;
        List<UInt16> expChannels;
        bool expFullDateTime;
        DataHelper.FileType expFileType;

        public FormLoggerSD()
        {
            InitializeComponent();

            btnDownloadRXD.Tag = BinRXD.Filter;
            btnDownloadCSV.Tag = DoubleDataCollection.Filter;
            InitPartitions();
        }

        void InitPartitions(bool ReInit = false)
        {
            if (ReInit)
            {
                pnlClient.Controls.Add(gridFiles);
                if (tabPartitions is not null)
                {
                    pnlClient.Controls.Remove(tabPartitions);
                    tabPartitions = null;
                }
            }

            USBDllComm.GetRexgenInfo(out USBDllComm.PartitionCount, out uint _);

            byte PartitionCount = USBDllComm.PartitionCount;
            if (PartitionCount < 1)
                PartitionCount = 1;

            Partitions = new SDPartition[PartitionCount];

            if (PartitionCount > 1)
            {
                tabPartitions = new TabControl();
                pnlClient.Controls.Add(tabPartitions);
                tabPartitions.Dock = DockStyle.Fill;
                tabPartitions.TabPages.Add(PartitionName[0]);
                tabPartitions.TabPages[0].Controls.Add(gridFiles);
            }
            else
                tabPartitions = null;

            for (int i = 0; i < PartitionCount; i++)
            {
                Partitions[i] = new SDPartition();
                if (i == 0)
                    Partitions[i].dg = gridFiles;
                else
                {
                    Partitions[i].dg = CloneDataGrid(gridFiles);// (DataGridView)CloneControl(gridFiles);
                    //copyControl(gridFiles, Partitions[i].dg);
                    if (tabPartitions.TabCount <= i)
                        tabPartitions.TabPages.Add(PartitionName[i]);
                    tabPartitions.TabPages[i].Controls.Add(Partitions[i].dg);
                    Partitions[i].dg.Show();
                }

                Partitions[i].dg.AutoGenerateColumns = false;
                Partitions[i].bs.DataSource = typeof(LogDetails);
                Partitions[i].dg.DataSource = Partitions[i].bs;
                Partitions[i].dg.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                Partitions[i].bs.DataSource = Partitions[i].sdDatalogs;
            }
            if (tabPartitions != null)
            {
                tabPartitions.SelectedIndex = 0;
                tabPartitions.SelectedIndexChanged += (object sender, EventArgs e) => { Refresh(); }; 
                //gridFiles_SelectionChanged;
            }
        }

        SDPartition ActivePartition => Partitions[tabPartitions is null ? 0 : tabPartitions.SelectedIndex];
        byte ActivePartitionID => (byte)(tabPartitions is null ? 0 : tabPartitions.SelectedIndex);

        DataGridView CloneDataGrid(DataGridView mainDataGridView)
        {
            Control CloneControl(Control srcCtl)
            {
                var cloned = Activator.CreateInstance(srcCtl.GetType()) as Control;
                var binding = BindingFlags.Public | BindingFlags.Instance;
                foreach (PropertyInfo prop in srcCtl.GetType().GetProperties(binding))
                {
                    if (IsClonable(prop))
                    {
                        object val = prop.GetValue(srcCtl);
                        prop.SetValue(cloned, val, null);
                    }
                }

                foreach (Control ctl in srcCtl.Controls)
                {
                    cloned.Controls.Add(CloneControl(ctl));
                }

                return cloned;
            }

            bool IsClonable(PropertyInfo prop)
            {
                var browsableAttr = prop.GetCustomAttribute(typeof(BrowsableAttribute), true) as BrowsableAttribute;
                var editorBrowsableAttr = prop.GetCustomAttribute(typeof(EditorBrowsableAttribute), true) as EditorBrowsableAttribute;

                return prop.CanWrite
                    && (browsableAttr == null || browsableAttr.Browsable == true)
                    && (editorBrowsableAttr == null || editorBrowsableAttr.State != EditorBrowsableState.Advanced);
            }

            DataGridView cloneDataGridView = (DataGridView)CloneControl(mainDataGridView);

            if (cloneDataGridView.Columns.Count == 0)
            {
                foreach (DataGridViewColumn datagrid in mainDataGridView.Columns)
                {
                    cloneDataGridView.Columns.Add(datagrid.Clone() as DataGridViewColumn);
                }
            }

            DataGridViewRow dataRow = new DataGridViewRow();

            for (int i = 0; i < mainDataGridView.Rows.Count; i++)
            {
                dataRow = (DataGridViewRow)mainDataGridView.Rows[i].Clone();
                int Index = 0;
                foreach (DataGridViewCell cell in mainDataGridView.Rows[i].Cells)
                {
                    dataRow.Cells[Index].Value = cell.Value;
                    Index++;
                }
                cloneDataGridView.Rows.Add(dataRow);
            }
            cloneDataGridView.AllowUserToAddRows = false;
            cloneDataGridView.Refresh();


            return cloneDataGridView;
        }

        private void FormLoggerSD_Shown(object sender, EventArgs e)
        {
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            { 
                case Keys.F5:
                    btnRefresh_Click(null, null);
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void FormLoggerSD_Load(object sender, EventArgs e)
        {
            GetSDCardFiles();
            GetSDCardInformation();
        }

        private void GetSDCardInformation()
        {
            ulong free_space = 0, full_size = 0;
            string type = "";
            if (USBDllComm.GetMMCInfo(out MMC_INFO mmc) == 0)
            {
                if (mmc.FullSize > 0)
                {
                    full_size = mmc.FullSize;
                    full_size *= 512;
                    full_size /= (1024 * 1024); 
                }
                if (mmc.FreeSpace > 0)
                {
                    free_space = (ulong)(mmc.FreeSpace) * 512;
                    free_space /= (1024 * 1024);
                }
                
                {
                    for (int i = 0; i < 5; i++)
                        type += ((char)mmc.Type[i]);
                    if (type == "IS016")
                        type = "EMMC";
                }
                lblSDInfo.Text = "Version : " + mmc.Version.ToString() + "     Type: "+type.ToString()+ "     Block Size: " + mmc.BlockSize.ToString() +
                    "    Free Space: " + free_space.ToString()+ " / " + full_size.ToString() + " MB";
                /*richTextBox1.AppendText("Block Size   = " + mmc.BlockSize.ToString() + "\n");
                richTextBox1.AppendText("Full Size      = " + mmc.FullSize.ToString() + " Blocks,( " + full_size.ToString() + " MB)\n");
                richTextBox1.AppendText("Free space = " + mmc.FreeSpace.ToString() + " Blocks,( " + free_space.ToString() + " MB)\n");
                richTextBox1.AppendText("\n");*/

            }
            else
                lblSDInfo.Text = ("Get MMC Info failed");

        }

        private void GetSDCardFiles()
        {
            ActivePartition.sdDatalogs = new List<LogDetails>();

            ushort LogNumber;
            DateTime dtStartLogDateTime;
            ushort LogCount = USBDllComm.SDLogCount(ActivePartitionID);
            string suffix = "";
            if (ActivePartitionID > 0)
                suffix = "_BK" + ActivePartitionID.ToString();
            for (ushort i = 0; i < LogCount; i++)
            {
                LogNumber = i;
                USBDllComm.SDLogInfo(
                    ActivePartitionID,
                    LogNumber,
                    out uint StartLogDateTime, out uint EndLogDateTime,
                    out uint LoggingTimeStart, out uint LoggingTimeEnd,
                    out uint LogStartDataSector, out uint LogEndDataSector, out uint LogDataSize,
                    out Guid GUID, out string structname, out bool isEncrypted
                );

                ActivePartition.sdDatalogs.Add(new LogDetails()
                {
                    StartTime = DateTimeOffset.FromUnixTimeSeconds(StartLogDateTime).DateTime,
                    EndTime = dtStartLogDateTime = DateTimeOffset.FromUnixTimeSeconds(EndLogDateTime).DateTime,
                    StartSector = LogStartDataSector,
                    EndSector = LogEndDataSector,
                    LogSize = LogDataSize,
                    ConfigName = string.Format(FileNamePattern, structname,
                    DateTimeOffset.FromUnixTimeSeconds(StartLogDateTime).DateTime.ToString("yyyyMMdd_HHmmss")) + suffix,
                    isEncrypted = isEncrypted
                }); ;
            }
            ActivePartition.bs.DataSource = ActivePartition.sdDatalogs;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            
        }

        private void tbFind_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        bool isActiveLog(LogDetails log) => ActiveLog is null ? false : log.StartSector == ActiveLog.StartSector;

        void UpdateActiveLog()
        {
            ActiveLog = null;

            short LogCount = (short)USBDllComm.SDLogCount(ActivePartitionID);
            if (LogCount == 0)
                return;

            USBDllComm.SDLogInfo(ActivePartitionID, (ushort)(LogCount - 1),
                   out uint StartLogDateTime, out uint EndLogDateTime,
                   out uint LoggingTimeStart, out uint LoggingTimeEnd,
                   out uint LogStartDataSector, out uint LogEndDataSector, out uint LogDataSize,
                   out Guid GUID, out string structname, out bool isEncrypted
               );

            DateTime dtStartLogDateTime;
            ActiveLog = new LogDetails()
            {
                StartTime = DateTimeOffset.FromUnixTimeSeconds(StartLogDateTime).DateTime,
                EndTime = dtStartLogDateTime = DateTimeOffset.FromUnixTimeSeconds(EndLogDateTime).DateTime,
                StartSector = LogStartDataSector,
                EndSector = LogEndDataSector,
                LogSize = LogDataSize,
                ConfigName = string.Format(FileNamePattern, structname, 
                DateTimeOffset.FromUnixTimeSeconds(StartLogDateTime).DateTime.ToString("yyyyMMdd_HHmmss"))
            };
        }

        void SetLastPrebufferAsInvalid(string logName)
        {
            using (var stream = new FileStream(logName, FileMode.Append))
                stream.Write(new byte[0x200], 0, 0x200);
        }

        async Task<bool> Download(LogDetails log)
        {
            List<Task> tasks = new List<Task>();
            bool DownloadCanceled = false;
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            async void AbortDownload(object sender, EventArgs e)
            {
                DownloadCanceled = true;
                USBDllComm.SDStopSendData();
                tokenSource.Cancel();
                Task.WhenAll(tasks).Wait();
            }

            if (pHandler is not null)
                pHandler.OnAbort = AbortDownload;
            //if (pHandler is not null && pHandler.pForm is FormProgress)
                //((FormProgress)pHandler.pForm).OnAbort += AbortDownload;

            tasks.Add(Task.Run(() =>
            {
                USBDllComm.SDRequestSendData(log.tmpFileName, log.StartSector, log.EndSector);
                tokenSource.Cancel();
            }));

            tasks.Add(Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        //USBDllComm.DPSDCurrentSector(out uint CurrentSector);
                        //Invoke(new Action(() => fprog.SetProgress((int)CurrentSector)));
                        if (pHandler is not null && !pHandler.pForm.isCanceled)
                        {
                            Int64 flength = new FileInfo(log.tmpFileName).Length;
                            string status = "Downloaded " + ((UInt32)flength).ToFormatedFileSize() + " from " + ((log.EndSector - log.StartSector) * 512).ToFormatedFileSize();
                            Invoke(new Action(() => pHandler.SetProgress((int)(flength / 512), status)));
                        }
                    }
                    catch { }
                    finally
                    {
                        if (!token.IsCancellationRequested)
                            await Task.Delay(1000);
                    }
                }
            }, token));

            try
            {
                await Task.WhenAll(tasks);

                if (isActiveLog(log))
                    SetLastPrebufferAsInvalid(log.tmpFileName);

            }
            catch 
            {
                return false;
            }
            finally
            {
                if (pHandler is not null)
                    pHandler.OnAbort = null;
            }

            return (!DownloadCanceled);
        }

        async Task DownloadNext()
        {
            LogDetails log = SelectedDatalogs[DatalogIndex];

            UInt32 SectorCount = log.EndSector - log.StartSector;
            if (pHandler is not null)
            {
                if (pHandler.pForm.isCanceled)
                    return;

                pHandler.InitProgress(0, (int)SectorCount, log.outFileName);
                (pHandler.pForm as Form).Update();
                pHandler.pBar.Update();

                var taskProgress = (pHandler.pForm as Form).ShowDialogAsync();
            }

            DatalogIndex++;
            if (await Download(log) && (!MergedExport || (MergedExport && DatalogIndex == SelectedDatalogs.Count)))
            {
                if (pHandler is not null && pHandler.pForm.isCanceled)
                    return;

                if (MergedExport)
                    log = MergeFiles();

                if (log is not null)
                {
                    if (expFileType.Filter == BinRXD.Filter || expFileType.Filter == BinRXD.EncryptedFilter)
                    {
                        if (pHandler is not null)
                        {
                            pHandler.SetProgress((int)SectorCount, "Downloaded...");
                            (pHandler.pForm as Form).Update();
                        }
                    }                    
                }
            }

            if (DatalogIndex < SelectedDatalogs.Count)
                await DownloadNext();
        }

        LogDetails MergeFiles()
        {
            var rxFiles = SelectedDatalogs.Select(x => new { bin = BinRXD.Load(x.tmpFileName), info = x }).Where(b => b.bin is not null).ToList();
            if (rxFiles.Count == 0)
                return null;
            rxFiles[0].bin.OffsetTimestamps(0);
            for (int i = 1; i < rxFiles.Count; i++)
            {
                rxFiles[i].bin.OffsetTimestamps((Int64)(rxFiles[i].bin.DatalogStartTime - rxFiles[0].bin.DatalogStartTime).TotalMilliseconds);
                FileStreamHelper.Append(rxFiles[i].bin.FileName, rxFiles[0].bin.FileName, (Int64)rxFiles[i].bin.DataOffset);
            }


            /*LogDetails log1 = SelectedDatalogs[0];
            using (BinRXD rxd = BinRXD.Load(log1.tmpFileName, log1.StartTime))
                if (rxd is not null)
                    rxd.OffsetTimestamps(0);
            for (int i = 1; i < SelectedDatalogs.Count; i++)
            {
                LogDetails log = SelectedDatalogs[i];
                using (BinRXD rxd = BinRXD.Load(log.tmpFileName, log.StartTime))
                    if (rxd is not null)
                    {
                        rxd.OffsetTimestamps((Int64)(log.StartTime - log1.StartTime).TotalMilliseconds);
                        FileStreamHelper.Append(rxd.FileName, log1.tmpFileName, (Int64)rxd.DataOffset);
                    }
            }*/
            string ext = Path.GetExtension(rxFiles[0].info.outFileName);
            if (ext.Equals(Path.GetExtension(BinRXD.Extension), StringComparison.OrdinalIgnoreCase))
                File.Copy(rxFiles[0].info.tmpFileName, rxFiles[0].info.outFileName, true);
            //string tmpFileName = Path.Combine(Path.GetDirectoryName(log.tmpFileName), "Merged" + BinRXD.Extension);

            rxFiles[0].info.tmpFileName = Path.ChangeExtension(rxFiles[0].info.tmpFileName, BinRXD.Extension);
            return rxFiles[0].info;
        }

        void SelectLogToDownload(LogDetails log, string outputFileName)
        {
            string tmpFileName = Path.Combine(PathHelper.TempPath, log.ConfigName + (log.isEncrypted ? BinRXD.EncryptedExtension : BinRXD.Extension));
            log.outFileName = outputFileName;
            bool isRxd = Path.GetExtension(log.outFileName).Equals(Path.GetExtension(BinRXD.Extension), StringComparison.OrdinalIgnoreCase) ||
               Path.GetExtension(log.outFileName).Equals(Path.GetExtension(BinRXD.EncryptedExtension), StringComparison.OrdinalIgnoreCase);
            log.tmpFileName = isRxd && !MergedExport ? log.outFileName : tmpFileName;

            SelectedDatalogs.Add(log);
        }

        private async void btnDownload_Click(object sender, EventArgs e)
        {
            if (ActivePartition.dg.SelectedRows.Count == 0)
                return;

            string TargetFilter = BinRXD.Filter;
            if (sender is not null && sender is ToolStripMenuItem && (sender as ToolStripMenuItem).Tag is not null)
                TargetFilter = (string)(sender as ToolStripMenuItem).Tag;

            expSignals = null;
            await OscilloscopeSetup.StopLiveData();

            MergedExport = false;
            SelectedDatalogs = new List<LogDetails>();

            if (ActivePartition.dg.SelectedRows.Count == 1)
            {
                LogDetails log = ActivePartition.sdDatalogs[ActivePartition.dg.SelectedRows[0].Index];
                /*dlgSave.Filter = BinRXD.Filter + "|" + MDF.Filter;
                dlgSave.FilterIndex = 2;
                var ftypes = new string[] {
                    DoubleDataCollection.Filter,
                    Matlab.Filter,
                    ASC.Filter,
                    BLF.Filter,
                    MDF.Filter,
                    BinRXD.Filter,
                };*/

                if (log.isEncrypted && TargetFilter == BinRXD.Filter)
                    TargetFilter = BinRXD.EncryptedFilter;
                var ftypes = new string[] { TargetFilter };

                dlgSave.Filter = string.Join("|", ftypes);
                dlgSave.FilterIndex = 1;
                dlgSave.FileName = log.ConfigName;
                if (dlgSave.ShowDialog() != DialogResult.OK)
                    return;

                expFileType = DataHelper.FileTypeCollection.FirstOrDefault(f => f.Filter == ftypes[dlgSave.FilterIndex - 1]);
                if (expFileType is null)
                    expFileType = DataHelper.FileTypeCollection.FirstOrDefault(f => f.Filter == BinRXD.Filter);
                SelectLogToDownload(log, dlgSave.FileName);
            }
            else
            {
                if (dlgFolder.ShowDialog() != DialogResult.OK)
                    return;

                expFileType = DataHelper.FileTypeCollection.FirstOrDefault(f => f.Filter == TargetFilter);

                for (int i = 0; i < ActivePartition.dg.SelectedRows.Count; i++)
                {
                    LogDetails log = ActivePartition.sdDatalogs[ActivePartition.dg.SelectedRows[i].Index];
                    SelectLogToDownload(log, Path.Combine(dlgFolder.SelectedPath, log.ConfigName + ((log.isEncrypted && expFileType.Filter == BinRXD.Filter) ? BinRXD.EncryptedExtension : expFileType.Extension)));
                }
            }

            UpdateActiveLog();
            DatalogIndex = 0;
            using (IFormProgress frm = (SelectedDatalogs.Count == 1) ? new FormProgress() : new FormDownload())
            using (pHandler = new ProgressHandler(frm, SelectedDatalogs.Count))
            {
                (frm as Form).ShowDialogAsync();
                await DownloadNext();
            }
            pHandler = null;
        }

        private void btnFormatSD_Click(object sender, EventArgs e)
        {
            FormSDFormat format = new FormSDFormat();
            if (Globals.VersionType == VersionType.Bladon)
            {
                if (format.ShowDialog() == DialogResult.OK)
                {
                    short Status;
                    if (format.cbPartitions.Checked)
                    {
                        FormPartitions frmPartitions = new FormPartitions();
                        if (frmPartitions.ShowDialog() == DialogResult.Cancel)
                            return;

                        Cursor.Current = Cursors.WaitCursor;
                        USBDllComm.SDFormat(1, (byte)frmPartitions.PartitionSizes.Length, frmPartitions.PartitionSizes, out Status);
                    }
                    else
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        USBDllComm.SDFormat(1, 0, null, out Status);
                    }
                    Task.Delay(5000).Wait();
                    if (Status == 0)
                    {
                        MessageBox.Show("Format Complete!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        InitPartitions(true);
                        Refresh();
                    }
                    else
                        MessageBox.Show("Application was unable to complete the format!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cursor.Current = Cursors.Default;
                }
            }
            else if (MessageBox.Show("Are you sure you want to format the logger SD card?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;
                USBDllComm.SDFormat(1, 0, null, out short Status);
                Task.Delay(5000).Wait();
                if (Status == 0)
                {
                    MessageBox.Show("Format Complete!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    InitPartitions(true);
                    Refresh();
                }
                else
                    MessageBox.Show("Application was unable to complete the format!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cursor.Current = Cursors.Default;
            }
            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            btnDownload.Enabled = false;
            btnShowDatalog.Enabled = false;
            GetSDCardFiles();
            GetSDCardInformation();
            UpdateButtonsStatus();
        }

        void UpdateButtonsStatus()
        {
            btnShowDatalog.Enabled = btnDownload.Enabled = ActivePartition is not null && ActivePartition.dg.SelectedRows.Count > 0;
        }

        private void gridFiles_SelectionChanged(object sender, EventArgs e)
        {
            UpdateButtonsStatus();
        }

        private async void btnShowDatalog_Click(object sender, EventArgs e)
        {
            ShowToScope();
        }

        private async void gridFiles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowToScope();
        }

        async void ShowToScope()
        {
            if (ActivePartition.dg.SelectedRows.Count > 0)
            {
                await OscilloscopeSetup.StopLiveData();

                MergedExport = false;
                expFileType = DataHelper.FileTypeCollection.FirstOrDefault(ft => ft.Filter == BinRXD.Filter);
                SelectedDatalogs = new List<LogDetails>();
                UpdateActiveLog();

                for (int i = 0; i < ActivePartition.dg.SelectedRows.Count; i++)
                {
                    LogDetails log = ActivePartition.sdDatalogs[ActivePartition.dg.SelectedRows[i].Index];
                    SelectLogToDownload(log, Path.Combine(PathHelper.TempPath, log.ConfigName + (log.isEncrypted ? BinRXD.EncryptedExtension : BinRXD.Extension)));
                }

                DatalogIndex = 0;
                using (IFormProgress frm = (SelectedDatalogs.Count == 1) ? new FormProgress() : new FormDownload())
                using (pHandler = new ProgressHandler(frm, SelectedDatalogs.Count))
                {
                    (frm as Form).ShowDialogAsync();
                    await DownloadNext();
                }
                if (pHandler.pForm.isCanceled)
                {
                    pHandler = null;
                    return;
                }
                pHandler = null;

                await Task.Delay(200);
                for (int i = 0; i < SelectedDatalogs.Count; i++)
                {
                    FormRXD rxd = new FormRXD(SelectedDatalogs[i].outFileName);
                    Invoke(new Action(async () =>
                    {
                        await Task.Delay(200);
                        Globals.FormMain.DockNewRXD(rxd);
                        rxd.busconfig.RestoreConfiguration(Globals.MainProject.BusChannelsConfig);
                    }));
                }
            }
        }

        private async void btnExport_Click(object sender, EventArgs e)
        {
            if (ActivePartition.dg.SelectedRows.Count == 0)
                return;

            expSignals = Globals.MainProject.BusChannelsConfig.GetExportCollection();
            await OscilloscopeSetup.StopLiveData();

            SelectedDatalogs = new List<LogDetails>();
            for (int i = 0; i < ActivePartition.dg.SelectedRows.Count; i++)
                SelectedDatalogs.Add(ActivePartition.sdDatalogs[ActivePartition.dg.SelectedRows[i].Index]);

            using (FormExportOptions frmExport = new FormExportOptions()
            {
                FilesFromDisk = false,
                InputFiles = SelectedDatalogs.Select(l => l.ConfigName + BinRXD.Extension).ToList(),
                dbChannels = Globals.MainProject.BusChannelsConfig,
                /*OutputFileTypes = new List<string>()
                {
                    DoubleDataCollection.Filter,
                    Matlab.Filter,
                    ASC.Filter,
                    BLF.Filter,
                    MDF.Filter,
                    BinRXD.Filter,
                },*/
            })
            {

                if (frmExport.ShowDialog() != DialogResult.OK)
                    return;

                Globals.UpdateLibraryIcons();
                MergedExport = frmExport.TargetMerge;
                expFullDateTime = frmExport.TargetFullDateTime;
                expChannels = frmExport.TargetChanels;
                expSignals = Globals.MainProject.BusChannelsConfig.GetExportCollection();
                expFileType = frmExport.TargetType;

                SelectedDatalogs = new List<LogDetails>();

                for (int i = 0; i < ActivePartition.dg.SelectedRows.Count; i++)
                {
                    LogDetails log = ActivePartition.sdDatalogs[ActivePartition.dg.SelectedRows[i].Index];
                    string outFileName = (frmExport.InputFileCount == 1) || MergedExport ?
                        frmExport.TargetPath :
                        Path.Combine(frmExport.TargetPath, log.ConfigName + expFileType.Extension);

                    SelectLogToDownload(log, outFileName);
                }
            }
            SelectedDatalogs = SelectedDatalogs.OrderBy(x => x.StartTime).ToList();

            UpdateActiveLog();
            DatalogIndex = 0;
            using (IFormProgress frm = (SelectedDatalogs.Count == 1) ? new FormProgress() : new FormDownload())
            using (pHandler = new ProgressHandler(frm, SelectedDatalogs.Count))
            {
                (frm as Form).ShowDialogAsync();
                await DownloadNext();
            }
            pHandler = null;
        }

        private void btnDownloadRXD_Click(object sender, EventArgs e)
        {
            btnDownload_Click(sender, e);
        }

        private void btnDownloadMF4_Click(object sender, EventArgs e)
        {
            btnDownload_Click(sender, e);
        }

        private void btnDownloadASC_Click(object sender, EventArgs e)
        {
            btnDownload_Click(sender, e);
        }

        private void btnDownloadBLF_Click(object sender, EventArgs e)
        {
            btnDownload_Click(sender, e);
        }

        private void btnDownloadMAT_Click(object sender, EventArgs e)
        {
            btnDownload_Click(sender, e);
        }

        private void btnDownloadCSV_Click(object sender, EventArgs e)
        {
            btnDownload_Click(sender, e);
        }

        private void btnDownloadTRC_Click(object sender, EventArgs e)
        {
            btnDownload_Click(sender, e);
        }
    }
}
