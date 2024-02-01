using InfluxShared.Generic;
using InfluxShared.Helpers;
using System;
using System.Text;
using USBDll;

namespace ReXconfig
{
    public class Rexgen
    {
        enum GNSSEnum { NotFitted, NEOM8L, NEOM8U, NEOM8Q }
        enum AccelerometerEnum { NotFitted, STLSM6DS33 }
        enum CANEnum { NotFitted, Integrated }
        public bool Selected { get; set; }
        public UInt64 ID { get; set; }   //DBID
        public string ProductCode { get; set; }
        public byte Version { get; set; }
        public byte Location { get; set; }
        public short Batch { get; set; }
        public int Number { get; set; }
        public string Serial
        {
            get
            {
                return ProductCode + Version.ToString("00") + "_A" + Location.ToString("0") + "_B" + Batch.ToString("000") + "_SN" + Number.ToString("0000000");
            }
        }
        public string SNShort { get; set; } = "";
        public string SNLong { get; set; } = "";

        public UInt16 HWVersion { get; set; }
        public uint AssemblyDate { get; set; }
        public byte BoardType { get; set; }
        public byte EMMC { get; set; }
        public byte GNSS { get; set; }
        public string GNSSStr { get => ((GNSSEnum)GNSS).ToString(); }
        public byte Accelerometer { get; set; }
        public string AccelerometerStr { get => ((AccelerometerEnum)Accelerometer).ToString(); }
        public byte NumberOfCANFD { get; set; }
        public byte NumberOfDigIn { get; set; }
        public byte NumberOfDigOut { get; set; }
        public byte NumberOfAnalogIn { get; set; }
        public byte CAN0 { get; set; }
        public byte CAN1 { get; set; }
        public string CAN0Str { get => ((CANEnum)CAN0).ToString(); }
        public string CAN1Str { get => ((CANEnum)CAN1).ToString(); }
        public string SalesCode { get; set; }
        public string CustomerCode { get; set; }
        public string TransactionCode { get; set; }
        public double DispatchDate { get; set; }
        public double ExpireDate { get; set; }
        public string DispatchDateStr { get; }
        public string ExpireDateStr { get; }
        public byte LIN { get; set; }

        public void ToFirmware()
        {
            /* SERIAL_NUMBER ser = new SERIAL_NUMBER();
             ser.PCode = ProductCode + Version.ToString("00");
             ser.AssemblyCode = "A" + Location.ToString("0");
             ser.BatchCode = "B" + Batch.ToString("000");
             ser.SerialNumber = "SER" + Number.ToString("00000");
             USBDllComm.DPSetSerialNumber(ser);*/
        }

        public void FromFirmware()
        {
            /*SERIAL_NUMBER ser = new SERIAL_NUMBER();            
            USBDllComm.DPGetSerialNumber(out ser);
            Console.WriteLine(ser.PCode.ToString());*/
        }

        public bool ReadHWInfo(ref byte[] arrHWInfo)
        {
            byte[] hw;
            byte pos = 0;
            byte[] pageFormat;

            dynamic GetValue(Type targetType, byte pageFormatIdx)
            {
                dynamic result = hw.ConvertTo(targetType, pos);
                pos += pageFormat[pageFormatIdx];
                return result;
            }


            if (USBDllComm.GetEEPROMPageFormat(3, out pageFormat) == 0)
                if (USBDllComm.GetHWSettings(out hw) == 0)
                {
                    HWVersion = GetValue(HWVersion.GetType(), 0);
                    AssemblyDate = GetValue(AssemblyDate.GetType(), 1);
                    BoardType = GetValue(BoardType.GetType(), 2);
                    NumberOfCANFD = GetValue(NumberOfCANFD.GetType(), 3);
                    NumberOfDigIn = GetValue(NumberOfDigIn.GetType(), 4);
                    NumberOfDigOut = GetValue(NumberOfDigOut.GetType(), 5);
                    NumberOfAnalogIn = GetValue(NumberOfAnalogIn.GetType(), 6);
                    CAN0 = GetValue(CAN0.GetType(), 7);
                    CAN1 = GetValue(CAN1.GetType(), 8);
                    pos += 4; //skip can2, can3 
                    LIN = GetValue(LIN.GetType(), 11);
                    GNSS = GetValue(GNSS.GetType(), 12);
                    Accelerometer = GetValue(Accelerometer.GetType(), 13);
                    if (NumberOfCANFD > 2)
                        NumberOfCANFD = 2;
                    if (NumberOfDigIn > 2)
                        NumberOfDigIn = 2;
                    if (NumberOfDigOut > 2)
                        NumberOfDigOut = 2;
                    if (NumberOfAnalogIn > 2)
                        NumberOfAnalogIn = 2;
                    arrHWInfo = hw;
                    return true;
                }

            return false;
        }

        public bool WriteHWInfo(ref byte[] arrHWInfo)
        {
            byte[] hw;
            byte pos = 0;
            byte[] pageFormat;

            void SetValue(object obj, byte idx)
            {
                if (pos + pageFormat[idx] < hw.Length)
                    if (pageFormat[idx] > 0)
                    {
                        Bytes.ObjectToBytes(obj).CopyTo(hw, pos);
                        pos += pageFormat[idx];
                    }
            }


            if (USBDllComm.GetEEPROMPageFormat(3, out pageFormat) == 0)
            {
                arrHWInfo = new byte[50];
                arrHWInfo.Initialize();
                hw = new byte[50];  //Page size is 128 bytes, but only 55 bytes can be transfered per one time. 
                                    // If there is need for more than 55 bytes, a function has to be implemented to transfer the second part.
                if (pageFormat.Length > 10)
                {
                    SetValue(HWVersion, 0);
                    SetValue(AssemblyDate, 1);
                    SetValue(BoardType, 2);
                    SetValue(NumberOfCANFD, 3);
                    SetValue(NumberOfDigIn, 4);
                    SetValue(NumberOfDigOut, 5);
                    SetValue(NumberOfAnalogIn, 6);
                    SetValue(CAN0, 7);
                    SetValue(CAN1, 8);
                    pos += 4;  //skip can2, can3
                    SetValue(LIN, 11);
                    SetValue(GNSS, 12);
                    SetValue(Accelerometer, 13);

                    byte crc = 0;
                    foreach (byte x in hw)
                    {
                        crc += x;
                    }
                    arrHWInfo = hw;
                    if (USBDllComm.SendHWSettings(3, 0, 50, hw) == 0)
                    {
                        byte res = USBDllComm.StoreEEPROMHWSettings(3, crc);
                        if (res == 0)
                            return true;
                    }

                }

            }

            return false;
        }

        public Rexgen()
        {
            ProductCode = "REX";
        }

        public string GetSerial()
        {
            if (ReadSerial())
                return Serial;
            else
                return "";
        }

        public bool ReadSerial()
        {
            byte[] ser;
            byte pos = 0;
            byte[] pageFormat;

            dynamic GetValue(Type targetType, byte pageFormatIdx)
            {
                if (targetType.Name == "String")
                {
                    byte[] arr = new byte[pageFormat[pageFormatIdx]];
                    if ((pos + arr.Length) > ser.Length)
                        return "";
                    Array.Copy(ser, pos, arr, 0, arr.Length);
                    string resStr = Encoding.UTF8.GetString(arr).Trim('\0');
                    pos += pageFormat[pageFormatIdx];
                    return resStr;
                }
                else
                {
                    dynamic result = ser.ConvertTo(targetType, pos);
                    pos += pageFormat[pageFormatIdx];
                    return result;
                }

            }

            if (USBDllComm.GetEEPROMPageFormat(2, out pageFormat) == 0)
                if (USBDllComm.DPGetSerialNumber(out ser) == 0)
                {
                    Version = GetValue(Version.GetType(), 0);
                    Location = GetValue(Location.GetType(), 1);
                    Batch = GetValue(Batch.GetType(), 2);
                    Number = GetValue(Number.GetType(), 3);
                    SNShort = GetValue(SNShort.GetType(), 4);
                    SNLong = GetValue(SNLong.GetType(), 5);
                    if (Version > 99)
                        Version = 0;
                    if (Location > 9)
                        Location = 0;
                    if (Batch > 999)
                        Batch = 0;
                    if (Number > 9999999)
                        Number = 0;
                    return true;
                }

            return false;
        }

        internal bool WriteSerial()
        {
            byte[] ser;
            byte pos = 0;
            byte[] pageFormat;

            void SetValue(object obj, byte idx)
            {
                if (pos + pageFormat[idx] < ser.Length)
                    if (pageFormat[idx] > 0)
                    {
                        if (obj is string)
                        {
                            byte[] str = Encoding.ASCII.GetBytes(obj as string + '\0');
                            str.CopyTo(ser, pos);
                            pos += pageFormat[idx];
                        }
                        else
                        {
                            Bytes.ObjectToBytes(obj).CopyTo(ser, pos);
                            pos += pageFormat[idx];
                        }
                    }
            }


            if (USBDllComm.GetEEPROMPageFormat(2, out pageFormat) == 0)
            {
                ser = new byte[51];  //Page size is 128 bytes, but only 55 bytes can be transfered per one time. 
                                     // If there is need for more than 55 bytes, a function has to be implemented to transfer the second part.
                if (pageFormat.Length > 10)
                {
                    SetValue(Version, 0);
                    SetValue(Location, 1);
                    SetValue(Batch, 2);
                    SetValue(Number, 3);
                    string serString = "SN" + Number.ToString("0000000");
                    SetValue(serString, 4);
                    SetValue(Serial, 5);

                    byte crc = 0;
                    foreach (byte x in ser)
                    {
                        crc += x;
                    }
                    if (USBDllComm.SendSerialNumber(51, ser) == 0)
                        return true;
                }

            }

            return false;
        }
    }
}
