using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMI
{
    class MyStruct
    {
        public  int PackedSize { get; set; }

        public  void PackByte(byte[] buf, byte value)
        {
            buf[PackedSize] = value;
            PackedSize++;
        }

        public  byte UnpackByte(byte[] buf)
        {            
            return buf[PackedSize++];
            
        }

        public void PackUint16(byte[] buf, UInt16 value)
        {
            buf[PackedSize] = (byte)(value & 0xff);
            buf[PackedSize + 1] = (byte)(value >> 8);
            PackedSize += 2;
        }

        public UInt16 UnpackUint16(byte[] buf)
        {
            UInt16 value = (UInt16)(buf[PackedSize + 1] << 8);
            value |= buf[PackedSize];
            TempValue = value;
            PackedSize += 2;
            return value;
        }

        int TempValue = 0;
        public UInt32 UnpackIDUint32(byte[] buf)
        {
            PackedSize += TempValue;
            UInt32 value_1 = (UInt32)(buf[PackedSize + 3] << 24);
            value_1 |= (UInt32)(buf[PackedSize+2] <<16);
            UInt32 value_2 = (UInt32)(buf[PackedSize + 1] << 8);
            value_2 |= buf[PackedSize];
            PackedSize += 4;
            UInt32 value = value_1 |= value_2;
            return value;
        }

        public UInt32 UnpackUint32(byte[] buf)
        {
            UInt32 value_1 = (UInt32)(buf[PackedSize + 3] << 24);
            value_1 |= (UInt32)(buf[PackedSize + 2] << 16);
            UInt32 value_2 = (UInt32)(buf[PackedSize + 1] << 8);
            value_2 |= buf[PackedSize];
            PackedSize += 4;
            UInt32 value = value_1 |= value_2;
            return value;
        }
        public  void PackString(byte[] buf, string value)
        {
            UInt16 bytesCount = (UInt16)Encoding.ASCII.GetByteCount(value);
            PackUint16(buf, bytesCount);
            byte[] bytes = Encoding.ASCII.GetBytes(value);
            foreach (var item in bytes)
            {
                PackByte(buf, item);
            }
        }

        public string UnPackString(byte[] buf)
        {
            UInt16 count = UnpackUint16(buf);
            return Encoding.ASCII.GetString(buf, PackedSize, count);
        }

        public void PackUint32(byte[] buf, UInt32 value)
        {
            PackUint16(buf, (UInt16)(value & 0xffff));
            PackUint16(buf, (UInt16)(value >> 16));
        }


        //解析不出正确的4个字节的包，欠完善
        //public UInt32 UnPackUint32(byte[] buf)
        //{           
        //    return (UInt32)((UnpackUint16(buf) << 16)) | UnpackUint16(buf);
        //}
    }
}
