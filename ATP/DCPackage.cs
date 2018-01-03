using System;

namespace CBTC
{
    class DCPackage
    {
        MyStruct DCStruct = new MyStruct();

        UInt16 cycle_;
        public UInt16 Cycle { set { cycle_ = value; } }

        UInt16 type_;
        public UInt16 PackageType { set { type_ = value; } }

        UInt16 length_;
        public UInt16 Length { set { length_ = value; } }

        UInt16 highSpeed_;
        public UInt16 HighSpeed { set { highSpeed_ = value; } }

        UInt16 openSpeed_;
        public UInt16 OpenSpeed { set { openSpeed_ = value; } }

        UInt16 permitSpeed_;
        public UInt16 PermitSpeed { set { permitSpeed_ = value; } }

        UInt16 interSpeed_;
        public UInt16 InterSpeed { set { interSpeed_ = value; } }

        UInt16 direction_;
        public UInt16 Direction { set { direction_ = value; } }

        Byte isEB_;
        public Byte IsEB { set { isEB_ = value; } }

        UInt16 nextSpeed_;
        public UInt16 NextSpeed { set { nextSpeed_ = value; } }

        

        public int Pack(byte[] buf)
        {
            DCStruct.PackedSize = 0;
            DCStruct.PackUint16(buf, cycle_++);
            DCStruct.PackUint16(buf, type_);
            DCStruct.PackUint16(buf, length_);
            DCStruct.PackUint16(buf, highSpeed_);
            DCStruct.PackUint16(buf, openSpeed_);
            DCStruct.PackUint16(buf, permitSpeed_);
            DCStruct.PackUint16(buf, interSpeed_);
            DCStruct.PackUint16(buf, direction_);
            DCStruct.PackByte(buf, isEB_);
            DCStruct.PackUint16(buf, nextSpeed_);
            return DCStruct.PackedSize;
        }
    }
}
