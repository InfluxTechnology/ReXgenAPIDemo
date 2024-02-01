using InfluxShared.Generic;
using ReXconfig;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace USBDll
{
    public enum ConfigureResult : byte
    {
        Ok, Fail, BadConfig
    };

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

        static USBDllComm()
        {
        }

        public static bool Connected
        {
            get
            {
                bool ok = UsbDllWrapper.DeviceIsReady() > 0 ? true : UsbDllWrapper.DeviceIsReady() > 0;
                //Firmware = ok ? GetFirmware() : "";
                if (ok)
                    Firmware = GetFirmware();
                //SerialNumber = ok ? GetSerialNumber() : "";
                Version = 0;
                PartitionCount = 0;
                if (ok)
                {
                    var fw = Firmware.Split('.');
                    if (fw != null && fw.Length > 1)
                        if (int.TryParse(fw[1], out int fwmin) && fwmin >= 42)
                        {
                            Version = 1;

                        }

                    Rexgen rex = new Rexgen();
                    rex.ReadSerial();
                    SerialNumberFull = rex.SNLong;
                    if (rex.SNLong.Contains("REX"))
                    {
                        SerialNumberFull = rex.SNLong;
                        SerialNumber = rex.Number.ToString("0000000");
                    }
                    else
                    {
                        SerialNumberFull = "     Serial Number: Not set";

                    }

                    /*GetRexgenInfo(out PartitionCount, out uint sn);
                    SerialNumber = sn.ToString("0000000");*/
                }
                ActiveConfiguration = ok ? GetActiveConfiguration() : "";

                return ok;
            }
        }

        public static string SerialNumberFull { get; private set; }

        public static string GetSerialNumber()
        {
            byte Res;
            //Res = DPGetSerialNumber(out byte[] serial);   //New implementation
            return ""; //serial.ToString();
        }

        public static byte Reflash(out uint Status) => UsbDllWrapper.DPReflash(out Status);

        public static byte SDFormat(ushort FormatType, byte PartitionsCount, byte[] PartitionSizes, out short Status)
        {
            byte res = 0;
            Status = 0;

            if (Version == 0 || PartitionsCount == 0)
                res = UsbDllWrapper.DPSDFormat(FormatType, out Status);
            else if (Version == 1)
                using (PinObj poSizes = new PinObj(PartitionSizes))
                    res = UsbDllWrapper.DPPartitionFormat(FormatType, PartitionsCount, poSizes, out Status);
            else
                return 0;

            if (Status == 0)
                PartitionCount = PartitionsCount;
            //GetRexgenInfo(out USBDllComm.PartitionCount, out uint _);

            return res;
        }

        public static short SDLogCount(byte partition)
        {
            using (PinObj poLogCount = new PinObj(new short()))
            {
                if (Version == 1 && PartitionCount > 0)
                    UsbDllWrapper.DPPartitionLogCount(partition, poLogCount);
                else
                    UsbDllWrapper.DPSDLogCount(poLogCount);

                return poLogCount.Object<short>();
            }
        }

        public static byte SDLogInfo(
            byte Partition,
            ushort LogNumber,
            out uint StartLogDateTime, out uint EndLogDateTime,
            out uint LoggingTimeStart, out uint LoggingTimeEnd,
            out uint LogStartDataSector, out uint LogEndDataSector,
            out uint LogDataSize, out Guid GUID, out string FileName)
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
            {
                byte res = 0;
                if (Version == 1 && PartitionCount > 0)
                    res = UsbDllWrapper.DPPartitionLogInfo(Partition, LogNumber, poStartLogDateTime, poEndLogDateTime, poLoggingTimeStart, poLoggingTimeEnd, poLogStartDataSector, poLogEndDataSector, poLogDataSize, poGUID, poFileName);
                else
                    res = UsbDllWrapper.DPSDLogInfo(LogNumber, poStartLogDateTime, poEndLogDateTime, poLoggingTimeStart, poLoggingTimeEnd, poLogStartDataSector, poLogEndDataSector, poLogDataSize, poGUID, poFileName);

                GUID = new Guid(bGuid);
                FileName = poFileName;
                StartLogDateTime = poStartLogDateTime;
                EndLogDateTime = poEndLogDateTime;
                LoggingTimeStart = poLoggingTimeStart;
                LoggingTimeEnd = poLoggingTimeEnd;
                LogStartDataSector = poLogStartDataSector;
                LogEndDataSector = poLogEndDataSector;
                LogDataSize = poLogDataSize;
                return res;
            }
        }

        public static ConfigureResult Reconfigure(string FileName)
        {
            byte Res;
            short Status;

            Res = UsbDllWrapper.DPReconfigureFile((FileName + '\0').ToCharArray(), out Status);
            if (Res == 1)
            {
                if (Status != 0)
                    return ConfigureResult.BadConfig;  //Configuration Error
                return ConfigureResult.Ok;
            }
            else
                return ConfigureResult.Fail;
        }

        public static byte ReflashFile(string FileName, out short Status) => UsbDllWrapper.DPReflashFile((FileName + '\0').ToCharArray(), out Status);

        public static byte GetDateTime(out uint DateTimeUNIX) => UsbDllWrapper.DPGetDateTime(out DateTimeUNIX);

        public static byte SetDateTime(uint DateTimeUNIX) => UsbDllWrapper.DPSetDateTime(DateTimeUNIX);

        public static byte SDRequestSendData(string FileName, uint StartSector, uint EndSector)
        {
            using (PinObj poFileName = new PinObj(Encoding.ASCII.GetBytes(FileName + '\0')))
                return UsbDllWrapper.DPSDRequestSendData((IntPtr)poFileName, StartSector, EndSector);
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

        public static byte GetConfigInfo(out Guid ConfigurationGuid, out UInt32 ConfigurationSize)
        {
            var guid = new byte[16];
            byte res = UsbDllWrapper.DPUSBGetConfigInfo(guid, out ConfigurationSize);
            ConfigurationGuid = new Guid(guid);
            return res;
        }

        public static byte GetActiveConfiguration(string FileName) => UsbDllWrapper.DPConfigFileData((FileName + '\0').ToCharArray());

        public static string GetActiveConfiguration()
        {
            var guidActive = new byte[16];
            ushort LogNumber;

            UsbDllWrapper.DPUSBGetConfigInfo(guidActive, out uint ConfigSize);

            short LogCount = SDLogCount(0);
            if (LogCount > 0)
            {
                for (var i = LogCount - 1; i >= 0; i--)
                {
                    LogNumber = (ushort)i;
                    SDLogInfo(
                        0,
                        LogNumber,
                        out uint StartLogDateTime, out uint EndLogDateTime,
                        out uint LoggingTimeStart, out uint LoggingTimeEnd,
                        out uint LogStartDataSector, out uint LogEndDataSector, out uint LogDataSize,
                        out Guid guid, out string structname
                    );

                    if (Guid.Equals(guid, new Guid(guidActive)))
                        return structname;
                }
            }

            return "";
        }

        private static bool ArrayEquality(byte[] a1, byte[] b1)
        {
            int i;
            if (a1.Length == b1.Length)
            {
                i = 0;
                while (i < a1.Length && (a1[i] == b1[i])) //Earlier it was a1[i]!=b1[i]
                {
                    i++;
                }
                if (i == a1.Length)
                {
                    return true;
                }
            }

            return false;
        }

        public static string GetFirmware()
        {
            string[] FirmwareReleaseType = new string[4] { "", "A", "B", "RC" };

            byte MinVersion, Res, BranchVersion, fwType;
            ushort MajorVersion;
            Res = UsbDllWrapper.DPGetFirmwareVersion(out MajorVersion, out MinVersion, out BranchVersion, out fwType);
            return $"{MajorVersion}.{MinVersion}.{BranchVersion} {FirmwareReleaseType[fwType]}";
        }

        public static DateTime GetDateTime()
        {
            byte Res;
            uint uDateTime;
            Res = UsbDllWrapper.DPGetDateTime(out uDateTime);
            return DateTimeOffset.FromUnixTimeSeconds(uDateTime).DateTime;
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
            {
                byte res = UsbDllWrapper.DPSetHWSettings(PageNum, offset, size, po);
                return res;
            }
        }

        public static byte StoreEEPROMHWSettings(byte PageNum, byte CRC) => UsbDllWrapper.StoreEEPROMHWSettings(PageNum, CRC);

        public static byte SendSerialNumber(byte size, byte[] Data)
        {
            using (PinObj po = new PinObj(Data))
            {
                byte res = UsbDllWrapper.DPSetSerialNumber(size, po);
                return res;
            }
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

        public static byte GetRexgenInfo(out byte NumOfPartitions, out UInt32 SN)
        {
            byte res = 0;
            NumOfPartitions = 0;
            SN = 0;
            if (Version == 0)
            {
                res = 0;
            }
            else if (Version == 1)
                res = UsbDllWrapper.DPGetRexgenInfo(out NumOfPartitions, out SN);

            return res;
        }

    }
}
