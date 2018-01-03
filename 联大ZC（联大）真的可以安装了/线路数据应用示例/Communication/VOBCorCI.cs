using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ZC
{
    class VOBCorCI
    {
        public static List<byte> VOBCNonCom = new List<byte>();
        public static int NumToVOBC = 1;

        public VOBCorCI(byte[] DATA)
        {
            byte SenderID = DATA[4];
            if (SenderID == 4 || SenderID == 5 || SenderID == 6 || SenderID == 7)
            {
                lock (NonCommunicationTrain.LoseTrain)
                {
                    if (NonCommunicationTrain.LoseTrain.Keys.Contains(DATA[8]))
                    {
                        CancelNonComTrain(DATA[8]);
                    }
                }
                HandleVOBC(SenderID, DATA);
            }
            if (SenderID == 2)
            {
                //Debug.Print("CI2 Seq : {0}", DATA[0] | (DATA[1] << 8));
                HandleCI2Data HandleCI2Data = new HandleCI2Data(DATA);
                InfoSendToCI2 SendToCI = new InfoSendToCI2(SenderID);
                Send(SendToCI.MyStruct.buf_, GetIPByDataType(SenderID), GetPortByDataType(SenderID), SendToCI.MyStruct.byteFlag_);
            }
            if (SenderID == 1)
            {
                //Debug.Print("CI1 Seq : {0}", DATA[0] | (DATA[1] << 8));
                HandleCI1Data HandleCI1Data = new HandleCI1Data(DATA);
                InfoSendToCI1 SendToCI = new InfoSendToCI1(SenderID);
                Send(SendToCI.MyStruct.buf_, GetIPByDataType(SenderID), GetPortByDataType(SenderID), SendToCI.MyStruct.byteFlag_);
            }
        }

        public void HandleVOBC(int SenderID, byte[] DATA)
        {
            int num = VOBCNonCom.IndexOf(DATA[8]);
            if (num == -1)
            {
                VOBCNonCom.Add(DATA[8]);
            }
            HandleVOBCData HandleVOBCData = new HandleVOBCData(DATA);
            if (HandleVOBCData.NC_Train == 0x03)
            {
                ATPPackage InfoSendToATP = new ATPPackage();
                InfoSendToATP.NC_ZC = 0x03;
                VOBCQuit Quit = new VOBCQuit(VOBCData.PreAccess,HandleVOBCData.NID_Train,HandleVOBCData.TrainPosition);
                HandleVOBCData.TrainPosition.Remove(HandleVOBCData.NID_Train);
                HandleVOBCData.TrainDirection.Remove(HandleVOBCData.NID_Train);
                VOBCData.PreAccess.Remove(HandleVOBCData.NID_Train);
                Send(InfoSendToATP.ATPPack.buf_, GetIPByDataType(SenderID), GetPortByDataType(SenderID), InfoSendToATP.ATPPack.byteFlag_);
            }
            else
            {
                UpdateInfo UpdateInfo = new UpdateInfo(HandleVOBCData, DATA);
                VOBCData VOBCData = new VOBCData(DATA, HandleVOBCData);
                Send(VOBCData.InfoSendToATP.ATPPack.buf_, GetIPByDataType(SenderID), GetPortByDataType(SenderID), VOBCData.InfoSendToATP.ATPPack.byteFlag_);
            }            
        }

        public void Send(byte[] Data, IPAddress IP, int port, int DataSize)
        {
            ZCSocket.DIP = IP;
            ZCSocket.Dport = port;
            ZCSocket.SendControlData(Data, DataSize);
        }

        public IPAddress GetIPByDataType(int SenderID)
        {
            foreach (var item in IPConfigure.IPList)
            {
                if (item.DeviceID == SenderID)
                {
                    return item.IP;
                }
            }
            return null;
        }

        public int GetPortByDataType(int SenderID)
        {
            foreach (var item in IPConfigure.IPList)
            {
                if (item.DeviceID == SenderID)
                {
                    return item.Port;
                }
            }
            return 0;
        }

        public static void CancelNonComTrain(byte TrainID)
        {
            string PreNonComTrainHeadPosition;
            string PreNonComTrainHeadPositionSectionName;
            string PreNonComTrainTailPosition;
            string PreNonComTrainTailPositionSectionName;
            lock (NonCommunicationTrain.LoseTrain)
            {
                PreNonComTrainHeadPosition = (Convert.ToInt16(NonCommunicationTrain.LoseTrain[TrainID][1]) * 256 + Convert.ToInt16(NonCommunicationTrain.LoseTrain[TrainID][0])).ToString();
                PreNonComTrainHeadPositionSectionName = (Convert.ToInt16(NonCommunicationTrain.LoseTrain[TrainID][2])).ToString();
                PreNonComTrainTailPosition = (Convert.ToInt16(NonCommunicationTrain.LoseTrain[TrainID][7]) * 256 + Convert.ToInt16(NonCommunicationTrain.LoseTrain[TrainID][6])).ToString();
                PreNonComTrainTailPositionSectionName = (Convert.ToInt16(NonCommunicationTrain.LoseTrain[TrainID][8])).ToString();
                NonCommunicationTrain.LoseTrain.Remove(TrainID);
            }
            if (UpdateInfo.TraverseSection(PreNonComTrainHeadPosition) != null)
            {
                Section section = UpdateInfo.TraverseSection(PreNonComTrainHeadPosition);
                section.HasNonComTrain.Remove(TrainID);
                System.Windows.Application.Current.Dispatcher.Invoke(
                 new Action(
                 delegate
                 {
                     section.InvalidateVisual();
                 }));
            }
            else if (UpdateInfo.TraverseRailSwitch(PreNonComTrainHeadPosition, PreNonComTrainHeadPositionSectionName) != null)
            {
                RailSwitch railswitch = UpdateInfo.TraverseRailSwitch(PreNonComTrainHeadPosition, PreNonComTrainHeadPositionSectionName);
                railswitch.HasNonComTrain.Remove(TrainID);
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate
                {
                    railswitch.InvalidateVisual();
                }));
            }


            if (UpdateInfo.TraverseSection(PreNonComTrainTailPosition) != null)
            {
                Section section = UpdateInfo.TraverseSection(PreNonComTrainTailPosition);
                section.HasNonComTrain.Remove(TrainID);
                System.Windows.Application.Current.Dispatcher.Invoke(
                 new Action(
                 delegate
                 {
                     section.InvalidateVisual();
                 }));
            }
            else if (UpdateInfo.TraverseRailSwitch(PreNonComTrainTailPosition, PreNonComTrainTailPositionSectionName) != null)
            {
                RailSwitch railswitch = UpdateInfo.TraverseRailSwitch(PreNonComTrainTailPosition, PreNonComTrainTailPositionSectionName);
                railswitch.HasNonComTrain.Remove(TrainID);
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate
                {
                    railswitch.InvalidateVisual();
                }));
            }
        }
    }
}
