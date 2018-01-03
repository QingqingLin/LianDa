using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZC
{
    class ATPPackage
    {
        public Pack ATPPack = new Pack();

        UInt16 Cycle_;
        public UInt16 Cycle { set { Cycle_ = value; } }

        UInt16 DataType_ = 9;
        public UInt16 DataType { set { DataType_ = value; } }

        Byte SenderID_ = 3;
        public Byte SenderID { set { SenderID_ = value; } }

        Byte ReceiverID_;
        public Byte ReceiverID { set { ReceiverID_ = value; } }

        UInt16 DataLength_;
        public UInt16 DataLength { set { DataLength_ = value; } }

        UInt16 NID_ZC_ = 1;
        public UInt16 NID_ZC { set { NID_ZC_ = value; } }

        UInt16 NID_Train_;
        public UInt16 NID_Train { set { NID_Train_ = value; } }

        Byte NC_ZC_;
        public Byte NC_ZC { set { NC_ZC_ = value; } }

        Byte NC_StopEnsure_;
        public Byte NC_StopEnsure { set { NC_StopEnsure_ = value; } }

        UInt64 NID_DataBase_;
        public UInt64 NID_DataBase { set { NID_DataBase_ = value; } }

        UInt16 NID_ARButton_;
        public UInt16 NID_ARButton { set { NID_ARButton_ = value; } }

        Byte Q_ARButtonStatus_;
        public Byte Q_ARButtonStatus { set { Q_ARButtonStatus_ = value; } }

        UInt16 NID_LoginZCNext_;
        public Byte NID_LoginZCNext { set { NID_LoginZCNext_ = value; } }

        Byte N_Length_;
        public Byte N_Length { set { N_Length_ = value; } }

        Byte NC_MAEndType_;
        public Byte NC_MAEndType { set { NC_MAEndType_ = value; } }

        UInt16 D_MAHeadLink_;
        public UInt16 D_MAHeadLink { set { D_MAHeadLink_ = value; } }

        UInt32 D_MAHeadOff_;
        public UInt32 D_MAHeadOff { set { D_MAHeadOff_ = value; } }

        Byte Q_MAHeadDir_;
        public Byte Q_MAHeadDir { set { Q_MAHeadDir_ = value; } }

        UInt16 D_MATailLink_;
        public UInt16 D_MATailLink { set { D_MATailLink_ = value; } }

        UInt32 D_MATailOff_;
        public UInt32 D_MATailOff { set { D_MATailOff_ = value; } }

        Byte Q_MATailDir_;
        public Byte Q_MATailDir { set { Q_MATailDir_ = value; } }

        Byte N_Obstacle_;
        public Byte N_Obstacle
        {
            get { return N_Obstacle_; }
            set { N_Obstacle_ = value; }
        }

        List<ObstacleInfo> Obstacle_ = new List<ObstacleInfo>();
        public List<ObstacleInfo> Obstacle
        {
            get { return Obstacle_; }
            set { Obstacle_ = value; }
        }

        Byte N_TSR_;
        public Byte N_TSR { set { N_TSR_ = value; } }

        UInt32 Q_ZC_;
        public UInt32 Q_ZC { set { Q_ZC_ = value; } }

        Byte EB_Type_;
        public Byte EB_Type { set { EB_Type_ = value; } }

        Byte EB_DEV_Type_;
        public Byte EB_DEV_Type { set { EB_DEV_Type_ = value; } }

        UInt16 EB_DEV_Name_;
        public UInt16 EB_DEV_Name { set { EB_DEV_Name_ = value; } }

        public void PackATP()
        {
            ATPPack.PackUint16(Cycle_++);
            ATPPack.PackUint16(DataType_);
            ATPPack.PackByte(SenderID_);
            ATPPack.PackByte(ReceiverID_);
            ATPPack.PackUint16(DataLength_);
            ATPPack.PackUint16(NID_ZC_);
            ATPPack.PackUint16(NID_Train_);
            ATPPack.PackByte(NC_ZC_);
            ATPPack.PackByte(NC_StopEnsure_);
            ATPPack.PackUint64(NID_DataBase_);
            ATPPack.PackUint16(NID_ARButton_);
            ATPPack.PackByte(Q_ARButtonStatus_);
            ATPPack.PackUint16(NID_LoginZCNext_);
            ATPPack.PackByte(N_Length_);
            ATPPack.PackByte(NC_MAEndType_);
            ATPPack.PackUint16(D_MAHeadLink_);
            ATPPack.PackUint32(D_MAHeadOff_);
            ATPPack.PackByte(Q_MAHeadDir_);
            ATPPack.PackUint16(D_MATailLink_);
            ATPPack.PackUint32(D_MATailOff_);
            ATPPack.PackByte(Q_MATailDir_);
            ATPPack.PackByte(N_Obstacle_);
            foreach (var ObstacleInfo in Obstacle_)
            {
                ATPPack.PackByte(ObstacleInfo.NC_Obstacle);
                ATPPack.PackUint16(ObstacleInfo.NID_Obstacle);
                ATPPack.PackByte(ObstacleInfo.Q_Obstacle_Now);
                ATPPack.PackByte(ObstacleInfo.Q_Obstacle_CI);
            }
            ATPPack.PackByte(N_TSR_);
            ATPPack.PackUint32(Q_ZC_);
            ATPPack.PackByte(EB_Type_);
            ATPPack.PackByte(EB_DEV_Type_);
            ATPPack.PackUint16(EB_DEV_Name_);
        }
    }
}
