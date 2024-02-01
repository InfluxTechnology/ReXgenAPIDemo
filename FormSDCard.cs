using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using USBDll;

namespace ReXgenAPIDemo
{
    public partial class FormSDCard : Form
    {
        class LogDetails
        {
            public string tmpFileName;
            public uint StartSector;
            public uint EndSector;
            public uint LogSize;
            public bool isEncrypted;
            public string FileSizeString { get; set; }

            public string outFileName;

            public string ConfigName { get; set; }

            public DateTime StartTime;
            public string StartTimeString => StartTime.ToString("yyyy-MM-dd HH:mm:ss");

            public DateTime EndTime;
            public string EndTimeString => EndTime.ToString("yyyy-MM-dd HH:mm:ss");

            public LogDetails()
            {
                FileSizeString = "static";
            }
        }

        List<LogDetails> SDContent;
        byte ActivePartitionID = 0;

        public FormSDCard()
        {
            InitializeComponent();
            GetSDFileInfo();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            GetSDFileInfo();
        }

        private void GetSDFileInfo()
        {
            SDContent = new List<LogDetails>();

            UsbDllWrapper.GetRexgenInfo(out byte NumOfPartitions, out uint sn);
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

                SDContent.Add(new LogDetails()
                {
                    StartTime = DateTimeOffset.FromUnixTimeSeconds(StartLogDateTime).DateTime,
                    EndTime = dtStartLogDateTime = DateTimeOffset.FromUnixTimeSeconds(EndLogDateTime).DateTime,
                    StartSector = LogStartDataSector,
                    EndSector = LogEndDataSector,
                    LogSize = LogDataSize,
                    ConfigName = string.Format("{0}_{1}_{2}", structname, sn.ToString("0000000"),
                    DateTimeOffset.FromUnixTimeSeconds(StartLogDateTime).DateTime.ToString("yyyyMMdd_HHmmss")) + suffix,
                    isEncrypted = isEncrypted,
                    FileSizeString = LogDataSize.ToString()
                }); 
            }
            gridFiles.DataSource = SDContent;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (gridFiles.SelectedRows.Count == 0)
                return;

            LogDetails log = gridFiles.SelectedRows[0].DataBoundItem as LogDetails;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                USBDllComm.SDRequestSendData(saveFileDialog1.FileName, log.StartSector, log.EndSector);
            }
        }
    }
}
