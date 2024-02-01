using RXD.Base;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using USBDll;

namespace ReXgenAPIDemo
{
    public partial class FormLiveData : Form
    {
        BinRXD liveRxd = null;

        public FormLiveData()
        {
            InitializeComponent();
        }

        private void btnStopLive_Click(object sender, EventArgs e)
        {
            UsbDllWrapper.StopLiveData(0);
        }

        private void FormLiveData_FormClosing(object sender, FormClosingEventArgs e)
        {
            UsbDllWrapper.StopLiveData(0);
        }

        private async void btnStartLive_Click(object sender, EventArgs e)
        {
            bool GetLiveDataStructure()
            {
                try
                {
                    string liveBin = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Temp", "live.bin");
                    if (File.Exists(liveBin))
                        File.Delete(liveBin);

                    string configName = USBDllComm.GetActiveConfiguration();                                                                                                                                                                                                                                                                                                                                                                               
                    liveRxd = new BinRXD(liveBin, DateTime.Now);
                    return !liveRxd.Empty;
                }
                catch
                {
                    return false;
                }
            }

         /*   if (!btnLiveData.Checked)
            {
                if (!GetLiveDataStructure())
                    return;

                btnLiveData.Checked = true;
                btnSave.Enabled = false;
                if (TraceData is null)
                {
                    TraceData = new TraceCollection();
                    bs.DataSource = TraceData;
                    bs.ResetBindings(false);
                }

                Task.Run(async () => await StartCapturing());
            }
            else
            {
                await Task.Run(async () => await OscilloscopeSetup.StopLiveData());
                btnLiveData.Checked = false;
            }*/
        }
    }
}
