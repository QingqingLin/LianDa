using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBTC
{
    class ZCPackage
    {
        MyStruct ZCStruct = new MyStruct();

        UInt16 cycle_;
        public UInt16 Cycle { set { cycle_ = value; } }

        UInt16 type_;
        public UInt16 PackageType { set { type_ = value; } }

        Byte sendID_;
        public Byte SendID { set { sendID_ = value; } }

        Byte receiveID_;
        public Byte ReceiveID { set { receiveID_ = value; } }

        UInt16 length_;
        public UInt16 Length { set { length_ = value; } }

        UInt16 trainID_;
        public UInt16 TrainID { set { trainID_ = value; } }

        UInt16 zcID_;
        public UInt16 ZCID { set { zcID_ = value; } }

        Byte runInformation_;
        public Byte RunInformation { set { runInformation_ = value; } }

        Byte stopFlag_;
        public Byte StopFlag { set { stopFlag_ = value; } }

        UInt16 stopMALink_;
        public UInt16 StopMAlink { set { stopMALink_ = value; } }

        UInt32 stopMAOff_;
        public UInt32 StopMAOfff { set { stopMAOff_ = value; } }

        UInt16 headLink_;
        public UInt16 HeadLink { set { headLink_ = value; } }

        UInt16 headLinkSecond_;
        public UInt16 HeadLinkSecond { set { headLinkSecond_ = value; } }

        UInt16 headOff_;
        public UInt16 HeadOff { set { headOff_ = value; } }

        UInt16 tailLink_;
        public UInt16 TailLink { set { tailLink_ = value; } }

        UInt16 tailLinkSecond_;
        public UInt16 TailLinkSecond { set { tailLinkSecond_ = value; } }

        UInt16 tailOff_;
        public UInt16 TailOff { set { tailOff_ = value; } }

        Byte headExpDirection_;
        public Byte HeadExpDirection { set { headExpDirection_ = value; } }

        Byte headActDirection_;
        public Byte HeadActDirection { set { headActDirection_ = value; } }

        Byte runModel_;
        public Byte RunModel { set { runModel_ = value; } }

        Byte runLevel_;
        public Byte RunLevel { set { runLevel_ = value; } }

        UInt16 actSpeed_;
        public UInt16 ACtSpeed { set { actSpeed_ = value; } }

        Byte stopInfo_;
        public Byte StopInfo { set { stopInfo_ = value; } }

        UInt16 error_;
        public UInt16 Error { set { error_ = value; } }

        UInt16 back_;
        public UInt16 Back { set { back_ = value; } }

        UInt16 limitSpeed_;
        public UInt16 LimitSpeed { set { limitSpeed_ = value; } }

        Byte integrity_;
        public Byte Integrity { set { integrity_ = value; } }

        Byte emergenvy_;
        public Byte Emergenvy { set { emergenvy_ = value; } }

        UInt16 arlamp_;
        public UInt16 Arlamp { set { arlamp_ = value; } }

        Byte arlampCmd_;
        public Byte ArlampCmd { set { arlampCmd_ = value; } }

        UInt32 vobc_;
        public UInt32 VOBC { set { vobc_ = value; } }

        UInt16 controlZC_;
        public UInt16 ControlZC { set { controlZC_ = value; } }

        UInt32 sendTime_;
        public UInt32 SendTime { set { sendTime_ = value; } }

        UInt32 reserved_;
        public UInt32 Reserved { set { reserved_ = value; } }


        public int Pack(byte[] buf)
        {
            ZCStruct.PackedSize = 0;
            ZCStruct.PackUint16(buf, cycle_++);
            ZCStruct.PackUint16(buf, type_);
            ZCStruct.PackByte(buf, sendID_);
            ZCStruct.PackByte(buf, receiveID_);
            ZCStruct.PackUint16(buf, length_);
            ZCStruct.PackUint16(buf, trainID_);
            ZCStruct.PackUint16(buf, zcID_);
            ZCStruct.PackByte(buf, runInformation_);
            ZCStruct.PackByte(buf, stopFlag_);
            ZCStruct.PackUint16(buf, stopMALink_);
            ZCStruct.PackUint32(buf, stopMAOff_);
            ZCStruct.PackUint16(buf, headLink_);
            ZCStruct.PackUint16(buf, headLinkSecond_);
            ZCStruct.PackUint16(buf, headOff_);
            ZCStruct.PackUint16(buf, tailLink_);
            ZCStruct.PackUint16(buf, tailLinkSecond_);
            ZCStruct.PackUint16(buf, tailOff_);
            ZCStruct.PackByte(buf, headExpDirection_);
            ZCStruct.PackByte(buf, headActDirection_);
            ZCStruct.PackByte(buf, runModel_);
            ZCStruct.PackByte(buf, runLevel_);
            ZCStruct.PackUint16(buf, actSpeed_);
            ZCStruct.PackByte(buf, stopInfo_);
            ZCStruct.PackUint16(buf, error_);
            ZCStruct.PackUint16(buf, back_);
            ZCStruct.PackUint16(buf, limitSpeed_);
            ZCStruct.PackByte(buf, integrity_);
            ZCStruct.PackByte(buf, emergenvy_);
            ZCStruct.PackUint16(buf, arlamp_);
            ZCStruct.PackByte(buf, arlampCmd_);
            ZCStruct.PackUint32(buf, vobc_);
            ZCStruct.PackUint16(buf, controlZC_);
            ZCStruct.PackUint32(buf, sendTime_);
            ZCStruct.PackUint32(buf, reserved_);

            return ZCStruct.PackedSize;
        }
    }
}
