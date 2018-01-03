using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMI
{
    class DMIPackage
    {
        UInt16 cycle_;
        public UInt16 Cycle { set { cycle_ = value; } }

        UInt16 type_;
        public UInt16 PackageType { set { type_ = value; } }

        UInt16 length_;
        public UInt16 Length { set { length_ = value; } }

        String trainOrder_;
        public String TrainOrder { set { trainOrder_ = value; } }

        UInt32 trainNumber_;
        public UInt16 TrainNumber { set { trainNumber_ = value; } }

        UInt16 driverNumber_;
        public UInt16 DriverNumber { set { driverNumber_ = value; } }

        Byte testOrder_;
        public Byte TestOrder { set { testOrder_ = value; } }

        Byte relieveOrder_;
        public Byte RelieveOrder { set { relieveOrder_ = value; } }

        MyStruct Struct = new MyStruct();
        public int Pack(byte[] buf)
        {
            Struct.PackedSize = 0;
            Struct.PackUint16(buf, cycle_++);
            Struct.PackUint16(buf, type_);
            Struct.PackUint16(buf, length_);
            Struct.PackString(buf, trainOrder_);
            Struct.PackUint32(buf, trainNumber_);
            Struct.PackUint16(buf, driverNumber_);
            Struct.PackByte(buf, testOrder_);
            Struct.PackByte(buf, relieveOrder_);

            return Struct.PackedSize;
        }
    }
}
