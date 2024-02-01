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
        public static extern byte GetFirmwareVersionExt(out ushort MajorVersion, out byte MinVersion, out byte BranchVersion, out byte FwType);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte DPGetSerialNumber(StringBuilder SerialNumber);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte DPReflash(out uint Status);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte FormatSDCard(ushort FormatType, out short Status);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte FormatSDCardPartitions(ushort FormatType, byte NumOfPartition, IntPtr PartitionInfo, out short Status);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt16 GetLogCount();

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt16 GetPartitionLogCount(byte partition);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte GetSDLogInfo(
            ushort LogNumber,
            IntPtr StartLogDateTime, IntPtr EndLogDateTime,
            IntPtr LoggingTimeStart, IntPtr LoggingTimeEnd,
            IntPtr LogStartDataSector, IntPtr LogEndDataSector,
            IntPtr LogDataSize, IntPtr GUID, IntPtr FileName, IntPtr isEncrypted);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte GetPartitionLogInfo(byte partition,
            ushort LogNumber,
            IntPtr StartLogDateTime, IntPtr EndLogDateTime,
            IntPtr LoggingTimeStart, IntPtr LoggingTimeEnd,
            IntPtr LogStartDataSector, IntPtr LogEndDataSector,
            IntPtr LogDataSize, IntPtr GUID, IntPtr FileName, IntPtr isEncrypted);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte SendConfiguration(char[] FileName, out short Status);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte ReflashLogger(char[] FileName, out short Status);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 GetDateTime();

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte SetDateTime(uint DateTimeUNIX);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte GetLogData(IntPtr FileName, uint StartSector, uint EndSector);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte DPSDCurrentSector(out uint CurrentSector);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte DPSDStopSendData();

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte DPGetMMCInfo(IntPtr mmc);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte InitLiveData(byte Channel);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte StopLiveData(byte Channel);

        [DllImport(DllFile, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte GetLiveData(byte Channel, IntPtr LiveData);

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
        public extern static byte GetRexgenInfo(out byte NumOfPartitions, out UInt32 SN);

    }
}
