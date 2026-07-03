using System;
using System.Runtime.InteropServices;

namespace InfluxShared.Generic
{
    public static class Bytes
    {
        public static byte[] ObjectToBytes(object obj)
        {
            byte[] buffer = new byte[Marshal.SizeOf(obj)];
            GCHandle h = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            IntPtr p = h.AddrOfPinnedObject();
            Marshal.StructureToPtr(obj, p, false);
            h.Free();
            return buffer;
        }

       

    }
}
