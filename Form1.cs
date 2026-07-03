using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using RxLibrary;
using USBDll;

namespace ReXgenAPIDemo
{
    public partial class Form1 : Form
    {
        private delegate byte ReflashAction(string fileName, out short status);

        public Form1()
        {
            InitializeComponent();
            bool connected = USBDllComm.Connected;
        }

        private void btnSendConfiguration_Click(object sender, EventArgs e)
        {
            if (dlgOpenRxc.ShowDialog() == DialogResult.OK)
            {
                if (USBDllComm.Connected)
                {
                    ConfigureResult result = USBDllComm.Reconfigure(dlgOpenRxc.FileName);
                    if (result == ConfigureResult.Ok)
                        MessageBox.Show("Logger Configured Successfully");
                    else if (result == ConfigureResult.BadConfig)
                        MessageBox.Show("Bad Config");
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
                    timerReflash.Start();
            }
        }

        async Task<bool> Reflash(string fwFileName)
        {
            return await RunReflash(fwFileName, USBDllComm.ReflashFile, "Logger");
        }

        async Task<bool> RunReflash(string fwFileName, ReflashAction reflashAction, string targetName)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                lblStatus.Text = $"{targetName} reflash procedure started...";
                byte res = reflashAction(fwFileName, out short status);
                bool error = res == 1 && status != 0;
                bool connected = false;
                do
                {
                    await Task.Delay(1000);
                    lblStatus.Text = $"{targetName} is updating. Waiting to reconnect...";
                    connected = UsbDllWrapper.DeviceIsReady() > 0;
                    if (connected)
                    {
                        await Task.Delay(1000);
                        lblStatus.Text = "";
                        MessageBox.Show($"{targetName} reflash completed successfully");
                    }
                }
                while (!connected);

                lblStatus.Text = error ? $"{targetName} reflash error {status}" : $"{targetName} reflash completed!";
                return !error;
            }
            catch (Exception)
            {
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
            if (USBDllComm.Connected)
            {
                USBDllComm.GetRexgenInfo(out byte numOfPartitions, out uint sn);
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
            uint unixTimestamp = (uint)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
            if (USBDllComm.Connected)
                USBDllComm.SetDateTime(unixTimestamp);
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
            USBDllComm.SDFormat(1, 0, null, out short status);
            if (status == 0)
                MessageBox.Show("Format Complete!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLockStatus_Click(object sender, EventArgs e)
        {
            int lockState = USBDllComm.IsDeviceLocked();
            lblLockStatus.Text = lockState switch
            {
                0 => "Unlocked",
                1 => "Locked",
                _ => "Unavailable"
            };
        }

        private void btnMicroType_Click(object sender, EventArgs e)
        {
            lblMicroType.Text = USBDllComm.GetMicroType().ToString();
        }

        private void btnSetAESKey_Click(object sender, EventArgs e)
        {
            if (!USBDllComm.Connected)
                return;

            if (!TryParseHexString(txtAESKey.Text, out byte[] key))
            {
                MessageBox.Show("Enter the AES key as hex bytes, for example: 01 02 03 04", "Invalid AES Key");
                return;
            }

            byte result = USBDllComm.SetAESKey(key);
            if (result == 0)
                MessageBox.Show("AES key sent successfully.", "Set AES Key");
            else
                MessageBox.Show($"Set AES Key failed with code {result}.", "Set AES Key");
        }

        private static bool TryParseHexString(string text, out byte[] data)
        {
            data = Array.Empty<byte>();
            if (string.IsNullOrWhiteSpace(text))
                return false;

            string cleaned = new string(text.Where(c => !char.IsWhiteSpace(c) && c != '-' && c != ':').ToArray());
            if (cleaned.Length == 0 || cleaned.Length % 2 != 0)
                return false;

            List<byte> bytes = new List<byte>(cleaned.Length / 2);
            for (int i = 0; i < cleaned.Length; i += 2)
            {
                if (!byte.TryParse(cleaned.Substring(i, 2), System.Globalization.NumberStyles.HexNumber, null, out byte value))
                    return false;

                bytes.Add(value);
            }

            data = bytes.ToArray();
            return true;
        }
    }
}
