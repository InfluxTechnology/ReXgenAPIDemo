using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using USBDll;
using RxLibrary;
using System.Runtime.InteropServices;

namespace ReXgenAPIDemo
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            bool connected = USBDllComm.Connected;
        }

        private void btnSendConfiguration_Click(object sender, EventArgs e)
        {
            if (dlgOpenRxc.ShowDialog() == DialogResult.OK)
            {
                byte Res;
                short Status;
                if (USBDllComm.Connected)  //Check if the logger is connected
                {
                    Res = UsbDllWrapper.SendConfiguration((dlgOpenRxc.FileName + '\0').ToCharArray(), out Status);
                    if (Res == 0)
                    {
                        if (Status != 0)
                            MessageBox.Show("Bad Config");
                        MessageBox.Show("Logger Configured Successfully");
                    }
                    else
                        MessageBox.Show("Communication Error");
                }                
            }
        }

        private void btnReflash_Click(object sender, EventArgs e)
        {
            if (dlgOpenReflash.ShowDialog() == DialogResult.OK)
            {
                if (USBDllComm.Connected)
                {
                    timerReflash.Start();
                    //Using timer to start the reflash as Async() method
                }
            }
        }

        async Task<bool> Reflash(string fwFileName)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                lblStatus.Text = "Reflash procedure started...";
                byte res = USBDllComm.ReflashFile(fwFileName, out Int16 Status);
                bool error = res == 1 && Status != 0;
                bool connected = false;
                do
                {
                    await Task.Delay(1000);
                    lblStatus.Text = "Logger is updating. Waiting to reconnect...";
                    connected = UsbDllWrapper.DeviceIsReady() > 0;
                    if (connected)
                    {
                        await Task.Delay(1000);
                        lblStatus.Text = "";
                        MessageBox.Show("Reflash completed successfully");
                    }

                }
                while (!connected);
                string msg = error ? "Reflash error " + Status.ToString() : "Reflash Completed!";
                lblStatus.Text = msg;
                return !error;
            }
            catch (Exception e)
            {
                //LogMessage("Uncaught exception");
                //LogMessage(e.Message);
                return false;
            }
            finally
            {
                Cursor.Current = Cursors.Default;

            }
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            timerReflash.Enabled = false;
            await Reflash(dlgOpenReflash.FileName);
            Close();
        }

        private void btnLogCount_Click(object sender, EventArgs e)
        {
            byte NumOfPartitions = 0;
            UInt32 SN = 0;
            if (USBDllComm.Connected)
            {
                UsbDllWrapper.GetRexgenInfo(out NumOfPartitions, out SN);
                //if (NumOfPartitions > 0)
                    lblLogCount.Text = USBDllComm.SDLogCount(0).ToString();
            }
        }

        private void btnGetDateTime_Click(object sender, EventArgs e)
        {
            if (USBDllComm.Connected)
                lblDateTime.Text = USBDllComm.GetDateTime().ToString();
        }

        private void btnSetDateTime_Click(object sender, EventArgs e)
        {
            uint unixTimestamp;
            unixTimestamp = (UInt32)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
            if (USBDllComm.Connected)
                UsbDllWrapper.SetDateTime(unixTimestamp);
        }

        private void btnLiveData_Click(object sender, EventArgs e)
        {
            FormLive frmLive = new FormLive();
            frmLive.ShowDialog();

            
        }        

        private void btnGetFirmware_Click(object sender, EventArgs e)
        {
            if (USBDllComm.Connected)
                lblGetFirmware.Text = USBDllComm.GetFirmware();
        }

        private void btnXMLToRXC_Click(object sender, EventArgs e)
        {
            if (dlgOpenXML.ShowDialog() != DialogResult.OK)
                return;
            if (dlgSaveRXC.ShowDialog() != DialogResult.OK)
                return;
            RxLib.XmlToRxc(dlgOpenXML.FileName, dlgSaveRXC.FileName);
        }

        private void btnConvertRXD_Click(object sender, EventArgs e)
        {
            if (dlgopenRXD.ShowDialog() != DialogResult.OK)
                return;
            if (dlgSaveConvertedData.ShowDialog() != DialogResult.OK)
                return;
            RxLib.ConvertData(dlgopenRXD.FileName, dlgSaveConvertedData.FileName);
            MessageBox.Show(RxLib.LastConvertStatus());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormSDCard frmSD = new FormSDCard();
            frmSD.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            USBDllComm.SDFormat(1, 0, null, out short Status);
            if (Status == 0)
            {
                MessageBox.Show("Format Complete!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
