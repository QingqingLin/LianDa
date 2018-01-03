using System;
using System.Text;

namespace CBTC
{
    class MyStruct
    {
        public int PackedSize { get; set; }

        public void PackByte(byte[] buf, byte value)
        {
            buf[PackedSize] = value;
            PackedSize++;
        }

        public byte UnpackByte(byte[] buf)
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
            PackedSize += 2;
            headValue = value;
            return value;
        }

        //单独解尾包
        UInt16 headValue = 0;
        public string UnPackTailString(byte[] buf)
        {
            PackedSize += headValue;
            UInt16 count = UnpackTailUint16(buf);
            return Encoding.ASCII.GetString(buf, PackedSize, count);
        }

        public UInt16 UnpackTailUint16(byte[] buf)
        {
            UInt16 value = (UInt16)(buf[PackedSize + 1] << 8);
            value |= buf[PackedSize];           
            PackedSize += 2;           
            return value;
        }
        //

        public void PackString(byte[] buf, string value)
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
            DMIvalue = count;
            return Encoding.ASCII.GetString(buf, PackedSize, count);
        }
        int DMIvalue = 0;
        public UInt32 UnpackDMIUint32(byte[] buf)
        {
            PackedSize += DMIvalue;
            UInt32 value_1 = (UInt32)(buf[PackedSize + 3] << 24);
            value_1 |= (UInt32)(buf[PackedSize + 2] << 16);
            UInt32 value_2 = (UInt32)(buf[PackedSize + 1] << 8);
            value_2 |= buf[PackedSize];
            PackedSize += 4;
            UInt32 value = value_1 |= value_2;
            return value;
        }

        public void PackUint32(byte[] buf, UInt32 value)
        {
            PackUint16(buf, (UInt16)(value & 0xffff));
            PackUint16(buf, (UInt16)(value >> 16));
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
        
        public UInt64 UnpackUint64(byte[] buf)
        {
            UInt64 value_1 = (UInt64)(buf[PackedSize + 7] << 56);
            UInt64 value_2 = (UInt64)(buf[PackedSize + 6] << 48);
            value_1 |= value_2;
            UInt64 value_3 = (UInt64)(buf[PackedSize + 5] << 40);
            UInt64 value_4 = (UInt64)(buf[PackedSize + 4] << 32);
            value_3 |= value_4;
            UInt64 value_5 = (UInt64)(buf[PackedSize + 3] << 24);
            UInt64 value_6 = (UInt64)(buf[PackedSize + 2] << 16);
            value_5 |= value_6;
            UInt64 value_7 = (UInt64)(buf[PackedSize + 1] << 8);
            UInt64 value_8 = (UInt64)buf[PackedSize];
            value_7 |= value_8;
            PackedSize += 8;
            UInt64 value = value_1 |= value_3 |= value_5 |= value_7;
            return value;
        }
    }
}
