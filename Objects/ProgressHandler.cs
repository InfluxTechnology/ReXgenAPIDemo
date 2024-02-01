using System;
using System.IO;
using System.Windows.Forms;

namespace InfluxApps.Objects
{
    public interface IFormProgress : IDisposable
    {
        public ProgressBar pBar { get; }
        public Label pStatus { get; }
        public Label pFilesProgress { get; }
        public Label pFileInfo { get; }
        public EventHandler OnAbort { get; set; }
        public bool AllowCancel { set; }
        public bool isCanceled { get; set; }
    }

    public class ProgressHandler : IDisposable
    {
        public EventHandler OnAbort { get => pForm.OnAbort; set => pForm.OnAbort = value; }

        public IFormProgress pForm = null;
        public ProgressBar pBar = null;
        public Label pStatus = null;
        public Label pFilesProgress = null;
        public Label pFileInfo = null;

        int StartPos = 0;
        int EndPos = 100;

        int FileIndex = 0;
        int TotalFiles = 1;

        public ProgressHandler(IFormProgress pForm, int TotalFiles = 0)
        {
            this.pForm = pForm;
            this.pBar = pForm.pBar;
            this.pStatus = pForm.pStatus;
            this.pFilesProgress = pForm.pFilesProgress;
            this.pFileInfo = pForm.pFileInfo;
            this.TotalFiles = TotalFiles;
        }

        public void InitProgress(int StartVal, int EndVal, string FileName)
        {
            pBar.Minimum = StartPos = StartVal;
            pBar.Maximum = EndPos = EndVal;
            if (pFileInfo is not null)
                pFileInfo.Text = $"Downloading file: {Path.GetFileName(FileName)}";
            FileIndex++;
            if (pFilesProgress is not null)
                pFilesProgress.Text = $"Files completed: {FileIndex - 1} of {TotalFiles}";
        }

        #region destructors
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ProgressHandler()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion

        public void SetProgress(int value, string status = null)
        {
            if (value < 0)
                value = EndPos + value;
            if (value < StartPos || value > EndPos)
                return;

            if (pStatus is not null && status != null)
                pStatus.Text = status;
            pBar.Value = value;
            //Update();
        }

    }
}
