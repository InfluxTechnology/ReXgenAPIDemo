using InfluxShared.Objects;
using RXD.Base;
using RXD.Blocks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using USBDll;

namespace ReXgenAPIDemo
{
    public partial class FormLive : Form
    {
        internal const int LiveBlockSize = 512;
        internal const int maxLiveBlocks = 1;

        bool LiveDataStarted = false;
        BinRXD LiveDataRxd;
        Dictionary<string, ushort> SupportedChannels = new Dictionary<string, ushort>();

        public FormLive()
        {
            InitializeComponent();
            ResetSupportedChannels();
        }

        void ResetSupportedChannels()
        {
            SupportedChannels.Clear();
            SupportedChannels["CAN0"] = 0;
            SupportedChannels["CAN1"] = 0;
            SupportedChannels["CAN2"] = 0;
            SupportedChannels["CAN3"] = 0;
            SupportedChannels["LIN0"] = 0;
            SupportedChannels["LIN1"] = 1;
        }

        public bool GetLiveDataChannelsUID()
        {
            try
            {
                ResetSupportedChannels();

                string tempDir = Path.Combine(Path.GetTempPath(), "ReXgenAPIDemo");
                Directory.CreateDirectory(tempDir);

                string liveBin = Path.Combine(tempDir, "live.bin");
                if (File.Exists(liveBin))
                    File.Delete(liveBin);

                USBDllComm.GetActiveConfiguration(liveBin);

                if (!File.Exists(liveBin))
                    return false;

                LiveDataRxd = BinRXD.Load(liveBin);
                if (LiveDataRxd == null || LiveDataRxd.Empty)
                    return false;

                MapSupportedChannels(liveBin);
                return true;
            }
            catch
            {
                return false;
            }
        }

        void MapSupportedChannels(string liveBin)
        {
            byte[] rxc = File.ReadAllBytes(liveBin);
            int currentPos = 0;
            ushort canInt0Uid = 0;
            ushort canInt1Uid = 0;
            ushort canInt2Uid = 0;
            ushort canInt3Uid = 0;
            ushort linInt0Uid = 0;
            ushort linInt1Uid = 0;

            while (currentPos + 20 < rxc.Length)
            {
                ushort blockType = (ushort)((rxc[currentPos + 1] << 8) + rxc[currentPos]);
                ushort blockSize = (ushort)((rxc[currentPos + 5] << 8) + rxc[currentPos + 4]);
                ushort uid = (ushort)((rxc[currentPos + 7] << 8) + rxc[currentPos + 6]);
                if (blockSize == 0)
                    break;

                if (blockType == 2)
                {
                    byte canphys = rxc[currentPos + 9];
                    if (canphys == 0)
                        canInt0Uid = uid;
                    else if (canphys == 1)
                        canInt1Uid = uid;
                    else if (canphys == 2)
                        canInt2Uid = uid;
                    else if (canphys == 3)
                        canInt3Uid = uid;
                }
                else if (blockType == 32)
                {
                    byte linphys = rxc[currentPos + 9];
                    if (linphys == 0)
                        linInt0Uid = uid;
                    else if (linphys == 1)
                        linInt1Uid = uid;
                }

                currentPos += blockSize;
            }

            currentPos = 0;
            while (currentPos + 20 < rxc.Length)
            {
                ushort blockType = (ushort)((rxc[currentPos + 1] << 8) + rxc[currentPos]);
                ushort blockSize = (ushort)((rxc[currentPos + 5] << 8) + rxc[currentPos + 4]);
                if (blockSize == 0)
                    break;

                if (blockType == 3)
                {
                    ushort uid = (ushort)((rxc[currentPos + 7] << 8) + rxc[currentPos + 6]);
                    byte interfaceID = rxc[currentPos + 19];
                    if (interfaceID == canInt0Uid)
                        SupportedChannels["CAN0"] = uid;
                    else if (interfaceID == canInt1Uid)
                        SupportedChannels["CAN1"] = uid;
                    else if (interfaceID == canInt2Uid)
                        SupportedChannels["CAN2"] = uid;
                    else if (interfaceID == canInt3Uid)
                        SupportedChannels["CAN3"] = uid;
                    else if (interfaceID == linInt0Uid)
                        SupportedChannels["LIN0"] = uid;
                    else if (interfaceID == linInt1Uid)
                        SupportedChannels["LIN1"] = uid;
                }

                currentPos += blockSize;
            }
        }

        private void tmrLiveData_Tick(object sender, EventArgs e)
        {
            try
            {
                tmrLiveData.Stop();
                byte[] liveBuffer = new byte[maxLiveBlocks * LiveBlockSize];
                GCHandle handle = GCHandle.Alloc(liveBuffer, GCHandleType.Pinned);
                try
                {
                    if (UsbDllWrapper.GetLiveData(0, handle.AddrOfPinnedObject()) > 0)
                        return;

                    if (liveBuffer[0] == 0 && liveBuffer[1] == 0)
                        return;

                    PrintLiveData(liveBuffer);
                }
                finally
                {
                    handle.Free();
                }
            }
            finally
            {
                if (LiveDataStarted)
                    tmrLiveData.Start();
                else
                    UsbDllWrapper.StopLiveData(0);
            }
        }

        private void PrintLiveData(byte[] livebuffer)
        {
            ushort blockSize = (ushort)((livebuffer[1] << 8) + livebuffer[0]);
            ushort pos = 2;
            if (blockSize > 21)
                blockSize = 21;

            while (pos < blockSize * 24 - 24)
            {
                ushort uid = (ushort)((livebuffer[pos + 1] << 8) + livebuffer[pos]);
                byte dlc = livebuffer[pos + 3];
                uint timestamp = (uint)((livebuffer[pos + 7] << 24) + (livebuffer[pos + 6] << 16) + (livebuffer[pos + 5] << 8) + livebuffer[pos + 4]);
                double timeInSeconds = (double)timestamp / 100000;

                if (LiveDataRxd.ContainsKey(uid))
                {
                    BinBase linkedBin = (BinBase)LiveDataRxd[uid];
                    int row = gridTrace.Rows.Add();
                    gridTrace[0, row].Value = timeInSeconds.ToString("0.000000");
                    gridTrace[4, row].Value = dlc.ToString();

                    if (linkedBin.RecType == RecordType.CanTrace)
                    {
                        uint canID = (uint)((livebuffer[pos + 11] << 24) + (livebuffer[pos + 10] << 16) + (livebuffer[pos + 9] << 8) + livebuffer[pos + 8]);
                        string busName = SupportedChannels.FirstOrDefault(x => x.Value == uid).Key;
                        gridTrace[1, row].Value = string.IsNullOrEmpty(busName) ? "CAN" : busName;
                        gridTrace[2, row].Value = "0x" + canID.ToString("X2");
                        gridTrace[3, row].Value = livebuffer[pos + 12].ToString();

                        string data = "";
                        for (int i = 0; i < dlc; i++)
                            data += livebuffer[i + pos + 13].ToString("X2") + " ";

                        gridTrace[5, row].Value = data;
                        pos += (ushort)(13 + dlc);
                    }
                    else if (linkedBin.RecType == RecordType.LinTrace)
                    {
                        uint linID = livebuffer[pos + 7];
                        gridTrace[1, row].Value = linkedBin.Name;
                        gridTrace[2, row].Value = "0x" + linID.ToString("X2");
                        gridTrace[3, row].Value = livebuffer[pos + 12].ToString();

                        string data = "";
                        for (int i = 0; i < dlc; i++)
                            data += livebuffer[i + pos + 13].ToString("X2") + " ";

                        gridTrace[5, row].Value = data;
                        pos += (ushort)(13 + dlc);
                    }
                    else
                    {
                        gridTrace[1, row].Value = linkedBin.Name;
                        byte[] data = new byte[dlc];
                        for (int i = 0; i < dlc; i++)
                            data[i] = livebuffer[i + pos + 8];

                        pos += (ushort)(8 + dlc);
                        var bindata = linkedBin.DataDescriptor.CreateBinaryData();
                        if (bindata.ExtractHex(data, out BinaryData.HexStruct hex))
                            gridTrace[5, row].Value = bindata.CalcValue(ref hex);
                        else
                            gridTrace[5, row].Value = BitConverter.ToString(data);
                    }
                }
                else
                {
                    pos += (ushort)(13 + dlc);
                }

                if (gridTrace.RowCount > 0)
                    gridTrace.FirstDisplayedScrollingRowIndex = gridTrace.RowCount - 1;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (GetLiveDataChannelsUID())
            {
                if (UsbDllWrapper.InitLiveData(1) != 0)
                {
                    MessageBox.Show("Unable to start live data.");
                    return;
                }

                LiveDataStarted = true;
                tmrLiveData.Start();
            }
            else
            {
                MessageBox.Show("Unable to load live data structure from the device.");
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            LiveDataStarted = false;
        }
    }
}
