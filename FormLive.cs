using InfluxShared.Objects;
using RXD.Base;
using RXD.Blocks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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
			SupportedChannels.Add("CAN0", 0);
            SupportedChannels.Add("CAN1", 0);
            SupportedChannels.Add("CAN2", 0);
            SupportedChannels.Add("CAN3", 0);
            SupportedChannels.Add("LIN0", 0);
            SupportedChannels.Add("LIN1", 1);
        }

		public bool GetLiveDataChannelsUID()   //read configuration and get the uid for all supported channels
		{
			string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "active.rxc");
			USBDllComm.GetActiveConfiguration(fileName);
			LiveDataRxd = BinRXD.Load(fileName);
			

            if (File.Exists(fileName))
            {
				byte[] rxc = File.ReadAllBytes(fileName);
				int currentPos = 0;
				ushort blockType;
				ushort blockSize;
				ushort uid;
				ushort canInt0Uid = 0;   //the uid of the CAN interface block
				ushort canInt1Uid = 0;
                ushort canInt2Uid = 0;
                ushort canInt3Uid = 0;
                ushort linInt0Uid = 0;  //the uid of the LIN interface block
                ushort linInt1Uid = 0;
                while (currentPos < rxc.Length)
                {
					blockType = (ushort)((rxc[currentPos + 1] << 8) + rxc[currentPos]);
					blockSize = (ushort)((rxc[currentPos + 5] << 8) + rxc[currentPos + 4]);
					uid = (ushort)((rxc[currentPos + 7] << 8) + rxc[currentPos + 6]);
					if (blockType == 2) //CAN Interface
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
					else if (blockType == 32) //LIN Interface
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
				while (currentPos < rxc.Length)
				{
					blockType = (ushort)((rxc[currentPos + 1] << 8) + rxc[currentPos]);
					blockSize = (ushort)((rxc[currentPos + 5] << 8) + rxc[currentPos + 4]);
					uid = (ushort)((rxc[currentPos + 7] << 8) + rxc[currentPos + 6]);
					if (blockType == 3) //CAN Message
					{
						uint identStart = (uint)((rxc[currentPos + 11] << 24) + (rxc[currentPos + 10] << 16) + (rxc[currentPos + 9] << 8) + rxc[currentPos + 8]);
						uint identEnd = (uint)((rxc[currentPos + 15] << 24) + (rxc[currentPos + 14] << 16) + (rxc[currentPos + 13] << 8) + rxc[currentPos + 12]);
						if (identStart == 0 && identEnd == 0xFFFFFFFF)
                        {
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
					}
					currentPos += blockSize;
				}
				return true;
			}	
			return false;
		}

		private void tmrLiveData_Tick(object sender, EventArgs e)
        {
            try
            {
                tmrLiveData.Stop();
                byte[] liveBuffer = new byte[maxLiveBlocks * LiveBlockSize];
                GCHandle handle = GCHandle.Alloc(liveBuffer, GCHandleType.Pinned);
                if (UsbDllWrapper.GetLiveData(0, handle.AddrOfPinnedObject()) > 0)
                    return;
                PrintLiveData(liveBuffer);
                handle.Free();
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

            ushort blockSize = 0;
            ushort pos = 2;
            blockSize = (ushort)((livebuffer[1] << 8) + livebuffer[0]);
            if (blockSize > 21)
                blockSize = 21; //Maximum 21 messages per request

            while (pos < blockSize * 24 - 24)
            {
                //2 bytes UID
                ushort uid = (ushort)((livebuffer[pos + 1] << 8) + livebuffer[pos + 0]);
                //1 byte Information size
                byte infSize = livebuffer[pos + 2];
                //1 byte DLC
                byte dlc = livebuffer[pos + 3];
                //4 bytes Timestamp
                uint timestamp = (uint)((livebuffer[pos + 7] << 24) + (livebuffer[pos + 6] << 16) + (livebuffer[pos + 5] << 8) + livebuffer[pos + 4]);
                double timeInSeconds = (double)timestamp / 100000;
				if (LiveDataRxd.ContainsKey(uid))
				{
                    BinBase linkedBin = (BinBase)LiveDataRxd[uid];
					
                    int row = gridTrace.Rows.Add();
					gridTrace[0, row].Value = timeInSeconds.ToString("0.000000");
					gridTrace[4, row].Value = dlc.ToString();
					if (linkedBin.RecType == RecordType.CanTrace)  //CAN bus
                    {
						uint canID = (uint)((livebuffer[pos + 11] << 24) + (livebuffer[pos + 10] << 16) + (livebuffer[pos + 9] << 8) + livebuffer[pos + 8]);
						gridTrace[1, row].Value = SupportedChannels.Where(x=>x.Value == uid).ToList()[0].Key;
						gridTrace[2, row].Value = "0x" + canID.ToString("X2");
						gridTrace[3, row].Value = livebuffer[pos + 12].ToString();
						string data = "";
						for (int i = 0; i < dlc; i++)
						{
							data += livebuffer[i + pos + 13].ToString("X2") + " ";
						}
						gridTrace[5, row].Value = data;
						pos += (ushort)(13 + dlc);
					}
                    else if (linkedBin.RecType == RecordType.LinTrace)  //LIN bus
                    {
                        uint linID = livebuffer[pos + 7];
                        gridTrace[1, row].Value = linkedBin.Name;
                        gridTrace[2, row].Value = "0x" + linID.ToString("X2");
                        gridTrace[3, row].Value = livebuffer[pos + 12].ToString();
                        string data = "";
                        for (int i = 0; i < dlc; i++)
                        {
                            data += livebuffer[i + pos + 13].ToString("X2") + " ";
                        }
                        gridTrace[5, row].Value = data;
                        pos += (ushort)(13 + dlc);
                    }
                    else  //Accelerometer or Gyro
                    {
						gridTrace[1, row].Value = linkedBin.Name; 

                        byte[] data = new byte[dlc];
						for (int i = 0; i < dlc; i++)
						{
                            data[i] += livebuffer[i + pos + 8];
						}
						gridTrace[5, row].Value = data;
						pos += (ushort)(8 + dlc);
                        var desc = linkedBin.DataDescriptor;
                        var bindata = linkedBin.DataDescriptor.CreateBinaryData();
                        if (bindata.ExtractHex(data, out BinaryData.HexStruct hex))
                        {
                            var value = bindata.CalcValue(ref hex);
                            gridTrace[5, row].Value = value;
                        }
                    }	
				}
				else
					pos += (ushort)(13 + dlc);
				if (gridTrace.RowCount > 0)
					gridTrace.FirstDisplayedScrollingRowIndex = gridTrace.RowCount - 1;
			}
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
			if (GetLiveDataChannelsUID())
            {
				UsbDllWrapper.InitLiveData(0); //Prepare logger for lve data transfer
				LiveDataStarted = true;
				tmrLiveData.Start();
			}            
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            LiveDataStarted = false;
            
        }
    }
}
