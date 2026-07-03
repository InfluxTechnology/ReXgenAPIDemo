using InfluxShared.Generic;
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace USBDll
{
    public enum ConfigureResult : byte
    {
        Ok,
        Fail,
        BadConfig
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct MMC_INFO
    {
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 6)]
        public byte[] Type;
        public byte Version;
        public ushort BlockSize;
        public uint FullSize;
        public uint FreeSpace;
    }

    public static class USBDllComm
    {
        static byte Version = 0;
        public static byte PartitionCount = 0;
        public static string Firmware = "";
        public static string SerialNumber = "";
        public static string ActiveConfiguration = "";

        public static bool Connected
        {
            get
            {
                bool ok = UsbDllWrapper.DeviceIsReady() > 0 ? true : UsbDllWrapper.DeviceIsReady() > 0;
                if (!ok)
                    return false;

                UpdateVersionInfo();
                return true;
            }
        }

        public static string SerialNumberFull { get; private set; }

        static void UpdateVersionInfo()
        {
            Version = 0;
            PartitionCount = 0;
            Firmware = GetFirmware();

            string[] fw = Firmware.Split('.');
            if (fw.Length < 2)
                return;

            string versionPrefix = fw[0] + "." + fw[1].PadLeft(3, '0');
            if (string.Compare(versionPrefix, "1.042", StringComparison.Ordinal) >= 0)
                Version = 1;
            if (string.Compare(fw[0], "2", StringComparison.Ordinal) >= 0)
                Version = 2;
        }

        public static string GetSerialNumber()
        {
            return "";
        }

        public static byte Reflash(out uint Status) => UsbDllWrapper.DPReflash(out Status);

        public static byte SDFormat(ushort FormatType, byte PartitionsCount, byte[] PartitionSizes, out short Status)
        {
            byte res = 0;
            Status = 0;

            if (Version == 0 || PartitionsCount == 0)
                res = UsbDllWrapper.FormatSDCard(FormatType, out Status);
            else if (Version >= 1)
            {
                using (PinObj poSizes = new PinObj(PartitionSizes))
                    res = UsbDllWrapper.FormatSDCardPartitions(FormatType, PartitionsCount, poSizes, out Status);
            }
            else
                return 0;

            if (Status == 0)
                PartitionCount = PartitionsCount;

            return res;
        }

        public static ushort SDLogCount(byte partition)
        {
            if (Version >= 1 && PartitionCount > 0)
                return UsbDllWrapper.GetPartitionLogCount(partition);

            return UsbDllWrapper.GetLogCount();
        }

        public static byte SDLogInfo(
            byte Partition,
            ushort LogNumber,
            out uint StartLogDateTime, out uint EndLogDateTime,
            out uint LoggingTimeStart, out uint LoggingTimeEnd,
            out uint LogStartDataSector, out uint LogEndDataSector,
            out uint LogDataSize, out Guid GUID, out string FileName, out bool isEncrypted)
        {
            byte[] bGuid;
            byte[] bFileName;
            using (PinObj poStartLogDateTime = new PinObj(StartLogDateTime = 0))
            using (PinObj poEndLogDateTime = new PinObj(EndLogDateTime = 0))
            using (PinObj poLoggingTimeStart = new PinObj(LoggingTimeStart = 0))
            using (PinObj poLoggingTimeEnd = new PinObj(LoggingTimeEnd = 0))
            using (PinObj poLogStartDataSector = new PinObj(LogStartDataSector = 0))
            using (PinObj poLogEndDataSector = new PinObj(LogEndDataSector = 0))
            using (PinObj poLogDataSize = new PinObj(LogDataSize = 0))
            using (PinObj poGUID = new PinObj(bGuid = new byte[16]))
            using (PinObj poFileName = new PinObj(bFileName = new byte[256]))
            using (PinObj poIsEncrypted = new PinObj((byte)0))
            {
                byte res = 0;
                if (Version >= 1 && PartitionCount > 0)
                    res = UsbDllWrapper.GetPartitionLogInfo(Partition, LogNumber, poStartLogDateTime, poEndLogDateTime, poLoggingTimeStart, poLoggingTimeEnd, poLogStartDataSector, poLogEndDataSector, poLogDataSize, poGUID, poFileName, poIsEncrypted);
                else
                    res = UsbDllWrapper.GetSDLogInfo(LogNumber, poStartLogDateTime, poEndLogDateTime, poLoggingTimeStart, poLoggingTimeEnd, poLogStartDataSector, poLogEndDataSector, poLogDataSize, poGUID, poFileName, poIsEncrypted);

                GUID = new Guid(bGuid);
                FileName = poFileName;
                StartLogDateTime = poStartLogDateTime;
                EndLogDateTime = poEndLogDateTime;
                LoggingTimeStart = poLoggingTimeStart;
                LoggingTimeEnd = poLoggingTimeEnd;
                LogStartDataSector = poLogStartDataSector;
                LogEndDataSector = poLogEndDataSector;
                LogDataSize = poLogDataSize;
                isEncrypted = poIsEncrypted == 1;
                return res;
            }
        }

        public static ConfigureResult Reconfigure(string FileName)
        {
            byte res = UsbDllWrapper.DPReconfigureFile(FileName, out short status);
            if (res != 0)
                return ConfigureResult.Fail;

            return status != 0 ? ConfigureResult.BadConfig : ConfigureResult.Ok;
        }

        public static byte ReflashFile(string FileName, out short Status) => UsbDllWrapper.DPReflashFile((FileName + '\0').ToCharArray(), out Status);

        public static byte ReflashWiFi(string FileName, out short Status) => UsbDllWrapper.DPReflashWiFi((FileName + '\0').ToCharArray(), out Status);

        public static byte GetDateTime(out uint DateTimeUNIX) => UsbDllWrapper.DPGetDateTime(out DateTimeUNIX);

        public static byte SetDateTime(uint DateTimeUNIX) => UsbDllWrapper.DPSetDateTime(DateTimeUNIX);

        public static byte SDRequestSendData(string FileName, uint StartSector, uint EndSector)
        {
            using (PinObj poFileName = new PinObj(FileName + '\0'))
                return UsbDllWrapper.DPSDRequestSendData(poFileName, StartSector, EndSector);
        }

        public static byte SDCurrentSector(out uint CurrentSector) => UsbDllWrapper.DPSDCurrentSector(out CurrentSector);

        public static byte SDStopSendData() => UsbDllWrapper.DPSDStopSendData();

        public static byte GetMMCInfo(out MMC_INFO mmc)
        {
            using (PinObj po = new PinObj(new byte[Marshal.SizeOf(typeof(MMC_INFO))]))
            {
                byte res = UsbDllWrapper.DPGetMMCInfo(po);
                mmc = Marshal.PtrToStructure<MMC_INFO>(po);
                return res;
            }
        }

        public static byte GetConfigInfo(out Guid ConfigurationGuid, out uint ConfigurationSize)
        {
            byte[] guid = new byte[16];
            byte res = UsbDllWrapper.DPUSBGetConfigInfo(guid, out ConfigurationSize);
            ConfigurationGuid = new Guid(guid);
            return res;
        }

        public static byte GetActiveConfiguration(string FileName) => UsbDllWrapper.DPConfigFileData((FileName + '\0').ToCharArray());

        public static bool GetActiveConfigurationInfo(out string name, out Guid guid, out uint size)
        {
            byte[] guidActive = new byte[16];
            UsbDllWrapper.DPUSBGetConfigInfo(guidActive, out uint configSize);

            short logCount = (short)SDLogCount(0);
            if (logCount > 0)
            {
                for (int i = logCount - 1; i >= 0; i--)
                {
                    SDLogInfo(
                        0,
                        (ushort)i,
                        out uint StartLogDateTime, out uint EndLogDateTime,
                        out uint LoggingTimeStart, out uint LoggingTimeEnd,
                        out uint LogStartDataSector, out uint LogEndDataSector, out uint LogDataSize,
                        out Guid logGuid, out string logName, out bool isEncrypted);

                    if (logGuid.Equals(new Guid(guidActive)))
                    {
                        name = logName;
                        guid = logGuid;
                        size = LogDataSize;
                        return true;
                    }
                }
            }

            name = "";
            guid = Guid.Empty;
            size = 0;
            return false;
        }

        public static string GetFirmware()
        {
            string[] firmwareReleaseType = new string[4] { "", "A", "B", "RC" };
            _ = UsbDllWrapper.DPGetFirmwareVersion(out ushort majorVersion, out byte minVersion, out byte branchVersion, out byte fwType);
            return $"{majorVersion}.{minVersion}.{branchVersion} {firmwareReleaseType[fwType]}";
        }

        public static string GetFirmwareWiFi()
        {
            for (byte req = 0; req < 3; req++)
            {
                _ = UsbDllWrapper.DPGetFirmwareVersionWiFi(out ushort majorVersion, out byte minVersion, out byte branchVersion, out byte fwType);
                if (majorVersion > 0 || minVersion > 0 || branchVersion > 0)
                    return $"{majorVersion}.{minVersion}.{branchVersion}";

                Thread.Sleep(1000);
            }

            return "0.0.0";
        }

        public static byte GetMicroType()
        {
            byte microType = 0;
            if (Connected && Version > 1)
                _ = UsbDllWrapper.GetMicroType(out microType);

            return microType;
        }

        public static DateTime GetDateTime()
        {
            _ = UsbDllWrapper.DPGetDateTime(out uint unixDateTime);
            return DateTimeOffset.FromUnixTimeSeconds(unixDateTime).DateTime;
        }

        public static byte GetEEPROMPageFormat(byte PageNum, out byte[] PageFormat)
        {
            using (PinObj po = new PinObj(new byte[128]))
            {
                byte res = UsbDllWrapper.GetEEPROMPageFormat(PageNum, po);
                PageFormat = po;
                return res;
            }
        }

        public static byte GetHWSettings(out byte[] data)
        {
            using (PinObj po = new PinObj(new byte[128]))
            {
                byte res = UsbDllWrapper.DPGetHWSettings(po);
                data = po;
                return res;
            }
        }

        public static byte SendHWSettings(byte PageNum, byte offset, byte size, byte[] page_data)
        {
            using (PinObj po = new PinObj(page_data))
                return UsbDllWrapper.DPSetHWSettings(PageNum, offset, size, po);
        }

        public static byte StoreEEPROMHWSettings(byte PageNum, byte CRC) => UsbDllWrapper.StoreEEPROMHWSettings(PageNum, CRC);

        public static byte SendSerialNumber(byte size, byte[] Data)
        {
            using (PinObj po = new PinObj(Data))
                return UsbDllWrapper.DPSetSerialNumber(size, po);
        }

        public static byte DPGetSerialNumber(out byte[] Data)
        {
            using (PinObj po = new PinObj(new byte[128]))
            {
                byte result = UsbDllWrapper.DPGetSerialNumber(po);
                Data = po;
                return result;
            }
        }

        public static byte GetRexgenInfo(out byte NumOfPartitions, out uint SN)
        {
            NumOfPartitions = 0;
            SN = 0;

            if (Version == 0)
                UpdateVersionInfo();

            if (Version == 0)
                return 0;

            return UsbDllWrapper.DPGetRexgenInfo(out NumOfPartitions, out SN);
        }

        public static byte SendStreamData(byte Channel, byte[] StreamData, uint StrreamDataSize)
        {
            if (StreamData == null)
                throw new ArgumentNullException(nameof(StreamData));

            return UsbDllWrapper.DPUSBSendStreamData(Channel, StreamData, StrreamDataSize);
        }

        public static byte[] GetEncryptedKey()
        {
            using (PinObj po = new PinObj(new byte[128]))
            {
                byte res = UsbDllWrapper.GetEncryptedKey(po);
                if (res != 0)
                    return null;

                return po;
            }
        }

        public static bool ConfirmDecrytpedKey(byte[] Key)
        {
            using (PinObj po = new PinObj(Key))
            {
                _ = UsbDllWrapper.ConfirmDecrytpedKey(po, out byte status);
                return status == 0;
            }
        }

        public static byte SetAESKey(byte[] key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            using (PinObj po = new PinObj(key))
                return UsbDllWrapper.SetAESKey(po);
        }

        public static int IsDeviceLocked()
        {
            if (UsbDllWrapper.DeviceIsReady() == 0)
                return -1;

            byte res = UsbDllWrapper.IsDeviceLocked(out byte status);
            if (res != 0)
                return -1;

            return status;
        }

        public static byte StopProcessData() => UsbDllWrapper.DPUSBStopProcessData();

        public static byte StartDiagnosticMode() => UsbDllWrapper.DPDiagModeStart();

        public static byte StopDiagnosticMode() => UsbDllWrapper.DPDiagModeStop();

        public static byte GetDiagnosticInfo(out byte[] data)
        {
            using (PinObj po = new PinObj(new byte[128]))
            {
                byte res = UsbDllWrapper.DPGetDiagInfo(po);
                data = po;
                return res;
            }
        }
    }
}
