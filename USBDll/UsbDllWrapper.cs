using System;
using System.Runtime.InteropServices;
using System.Text;

namespace USBDll
{
    public static class UsbDllWrapper
    {
        const string DllFile = "RGUSBdrv.dll";

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte DeviceIsReady();

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte DPGetFirmwareVersion(out ushort MajorVersion, out byte MinVersion, out byte BranchVersion, out byte FwType);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte DPGetSerialNumber(StringBuilder SerialNumber);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte DPReflash(out uint Status);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte DPSDFormat(ushort FormatType, out short Status);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte DPPartitionFormat(ushort FormatType, byte NumOfPartition, IntPtr PartitionInfo, out short Status);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte DPSDLogCount(IntPtr LogCount);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte DPPartitionLogCount(byte partition, IntPtr LogCount);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte DPSDLogInfo(
            ushort LogNumber,
            IntPtr StartLogDateTime, IntPtr EndLogDateTime,
            IntPtr LoggingTimeStart, IntPtr LoggingTimeEnd,
            IntPtr LogStartDataSector, IntPtr LogEndDataSector,
            IntPtr LogDataSize, IntPtr GUID, IntPtr FileName);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte DPPartitionLogInfo(byte partition,
            ushort LogNumber,
            IntPtr StartLogDateTime, IntPtr EndLogDateTime,
            IntPtr LoggingTimeStart, IntPtr LoggingTimeEnd,
            IntPtr LogStartDataSector, IntPtr LogEndDataSector,
            IntPtr LogDataSize, IntPtr GUID, IntPtr FileName);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte DPReconfigureFile(char[] FileName, out short Status);//,   out IntPtr FileName);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte DPReflashFile(char[] FileName, out short Status);//,   out IntPtr FileName);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte DPGetDateTime(out uint DateTimeUNIX);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte DPSetDateTime(uint DateTimeUNIX);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte DPSDRequestSendData(IntPtr FileName, uint StartSector, uint EndSector);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte DPSDCurrentSector(out uint CurrentSector);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte DPSDStopSendData();

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte DPGetMMCInfo(IntPtr mmc);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte DPUSBStartLiveData(byte Channel);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte DPUSBStopLiveData(byte Channel);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte DPUSBGetLiveData(byte Channel, IntPtr LiveData);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte DPUSBGetConfigInfo(byte[] ConfigGuid, out UInt32 ConfigSize);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte DPConfigFileData(char[] FileName);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte GetEEPROMPageFormat(byte PageNum, IntPtr PageFormat);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public extern static byte DPGetHWSettings(IntPtr data);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public extern static byte DPSetHWSettings(byte PageNum, byte offset, byte size, IntPtr page_data);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte StoreEEPROMHWSettings(byte PageNum, byte CRC);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public extern static byte DPSetSerialNumber(byte size, IntPtr Data);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public extern static byte DPGetSerialNumber(IntPtr Data);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public extern static byte DPGetRexgenInfo(out byte NumOfPartitions, out UInt32 SN);

    }
}
