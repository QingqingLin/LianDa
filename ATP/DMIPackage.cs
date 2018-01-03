using System;

namespace CBTC
{
    
    class DMIPackage
    {
        MyStruct DMIStruct = new MyStruct();

        UInt16 cycle_;
        public UInt16 Cycle { set { cycle_ = value; } }

        UInt16 type_;
        public UInt16 PackageType { set { type_ = value; } }

        UInt16 length_;
        public UInt16 Length { set { length_ = value; } }

        String trainNum_;
        public string TrainNum { get { return trainNum_; } set { trainNum_ = value; } }

        UInt32 trainID_;
        public UInt32 TrainID { set { trainID_ = value; } }

        Byte highModel_;
        public Byte HighModel { set { highModel_ = value; } }

        Byte curModel_;
        public Byte CurModel { set { curModel_ = value; } }

        Byte curRate_;
        public Byte CurRate { set { curRate_ = value; } }

        Byte breakOut_;
        public Byte BreakOut { set { breakOut_ = value; } }

        UInt32 trainStation_;
        public UInt32 TrainStation { set { trainStation_ = value; } }

        UInt32 trainHeadLoca_;
        public UInt32 TrainHeadLoca { set { trainHeadLoca_ = value; } }

        UInt16 targetLoca_;
        public UInt16 TargetLoca { set { targetLoca_ = value; } }

        UInt16 startLoca_;
        public UInt16 StartLoca { set { startLoca_ = value; } }

        UInt16 actulSpeed_;
        public UInt16 ActulSpeed { set { actulSpeed_ = value; } }

        UInt16 highSpeed_;
        public UInt16 HighSpeed { set { highSpeed_ = value; } }

        UInt16 openSpeed_;
        public UInt16 OpenSpeed { set { openSpeed_ = value; } }

        UInt16 permitSpeed_;
        public UInt16 PermitSpeed { set { permitSpeed_ = value; } }

        UInt16 interSpeed_;
        public UInt16 InterSpeed { set { interSpeed_ = value; } }

        UInt16 targetSpeed_;
        public UInt16 TargetSpeed { set { targetSpeed_ = value; } }

        Byte alarm_;
        public Byte Alarm { set { alarm_ = value; } }

        UInt16 tempLimitSpeedStart1_;
        public UInt16 TempLimitSpeedStart1 { set { tempLimitSpeedStart1_ = value; } }

        UInt16 tempLimitSpeedEnd1_;
        public UInt16 TempLimitSpeedEnd1 { set { tempLimitSpeedEnd1_ = value; } }

        UInt16 tempLimitSpeed1_;
        public UInt16 TempLimitSpeed1 { set { tempLimitSpeed1_ = value; } }

        UInt16 tempLimitSpeedStart2_;
        public UInt16 TempLimitSpeedStart2 { set { tempLimitSpeedStart2_ = value; } }

        UInt16 tempLimitSpeedEnd2_;
        public UInt16 TempLimitSpeedEnd2 { set { tempLimitSpeedEnd2_ = value; } }

        UInt16 tempLimitSpeed2_;
        public UInt16 TempLimitSpeed2 { set { tempLimitSpeed2_ = value; } }

        UInt16 tempLimitSpeedStart3_;
        public UInt16 TempLimitSpeedStart3 { set { tempLimitSpeedStart3_ = value; } }

        UInt16 tempLimitSpeedEnd3_;
        public UInt16 TempLimitSpeedEnd3 { set { tempLimitSpeedEnd3_ = value; } }

        UInt16 tempLimitSpeed3_;
        public UInt16 TempLimitSpeed3 { set { tempLimitSpeed3_ = value; } }

        Byte runLocation_;
        public Byte RunLocation { set { runLocation_ = value; } }

        Byte runDirection_;
        public Byte RunDirection { set { runDirection_ = value; } }

        Byte hint_;
        public Byte Hint { set { hint_ = value; } }

        UInt16 frontPermSpeed_;
        public UInt16 FrontPermSpeed { set { frontPermSpeed_ = value; } }


        public int Pack(byte[] buf)
        {
            DMIStruct.PackedSize = 0;
            DMIStruct.PackUint16(buf, cycle_++);
            DMIStruct.PackUint16(buf, type_);
            DMIStruct.PackUint16(buf, length_);
            DMIStruct.PackString(buf, trainNum_);
            DMIStruct.PackUint32(buf, trainID_);
            DMIStruct.PackByte(buf, highModel_);
            DMIStruct.PackByte(buf, curModel_);
            DMIStruct.PackByte(buf, curRate_);
            DMIStruct.PackByte(buf, breakOut_);
            DMIStruct.PackUint32(buf, trainStation_);
            DMIStruct.PackUint32(buf, trainHeadLoca_);
            DMIStruct.PackUint16(buf, targetLoca_);
            DMIStruct.PackUint16(buf, startLoca_);
            DMIStruct.PackUint16(buf, actulSpeed_);
            DMIStruct.PackUint16(buf, highSpeed_);
            DMIStruct.PackUint16(buf, openSpeed_);
            DMIStruct.PackUint16(buf, permitSpeed_);
            DMIStruct.PackUint16(buf, interSpeed_);
            DMIStruct.PackUint16(buf, targetSpeed_);
            DMIStruct.PackByte(buf, alarm_);
            DMIStruct.PackUint16(buf, tempLimitSpeedStart1_);
            DMIStruct.PackUint16(buf, tempLimitSpeedEnd1_);
            DMIStruct.PackUint16(buf, tempLimitSpeed1_);
            DMIStruct.PackUint16(buf, tempLimitSpeedStart2_);
            DMIStruct.PackUint16(buf, tempLimitSpeedEnd2_);
            DMIStruct.PackUint16(buf, tempLimitSpeed2_);
            DMIStruct.PackUint16(buf, tempLimitSpeedStart3_);
            DMIStruct.PackUint16(buf, tempLimitSpeedEnd3_);
            DMIStruct.PackUint16(buf, tempLimitSpeed3_);
            DMIStruct.PackByte(buf, runLocation_);
            DMIStruct.PackByte(buf, runDirection_);
            DMIStruct.PackByte(buf, hint_);
            DMIStruct.PackUint16(buf, frontPermSpeed_);
            return DMIStruct.PackedSize;
        }
    }
}
