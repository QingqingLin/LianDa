using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZC
{
    class VOBCData
    {
        HandleVOBCData Handle;
        SetMA Determine;
        byte[] Data;
        public ATPPackage InfoSendToATP = new ATPPackage();
        public static Dictionary<byte,List<string>> PreAccess = new Dictionary<byte, List<string>>();

        public VOBCData(byte[] data, HandleVOBCData handle)
        {
            this.Data = data;
            this.Handle = handle;
            SetInfoToVOBC();
            InfoSendToATP.PackATP();
            CancelPreAccess();
            UpdateAccess();
            UpdatePreAccess();
        }
        
        private void UpdatePreAccess()
        {
            if (PreAccess.Keys.Contains(Handle.NID_Train))
            {
                PreAccess[Handle.NID_Train].Clear();
                foreach (var item in Determine.Route)
                {
                    PreAccess[Handle.NID_Train].Add(item);                        
                }
            }
            else
            {
                List<string> Access = new List<string>();
                foreach (var item in Determine.Route)
                {
                    Access.Add(item);
                }
                PreAccess.Add(Handle.NID_Train,Access);
            }
        }

        private void CancelPreAccess()
        {
            if (PreAccess.Keys.Contains(Handle.NID_Train))
            {
                foreach (var item in PreAccess[Handle.NID_Train])
                {
                    if (!Determine.Route.Contains(item))
                    {
                        Section TraverseSection = UpdateInfo.TraverseSection(item.Substring(0, 3));
                        if (TraverseSection != null)
                        {
                            if (TraverseSection.IsAccess.Contains(Handle.NID_Train))
                            {
                                TraverseSection.IsAccess.Remove(Handle.NID_Train);
                            }
                            System.Windows.Application.Current.Dispatcher.Invoke(
                            new Action(delegate
                            {
                                TraverseSection.InvalidateVisual();
                            }));
                        }
                        else
                        {
                            RailSwitch TraverseRailSwitch = UpdateInfo.TraverseRailSwitch(item.Substring(0, 3), item.Substring(4));
                            if (TraverseRailSwitch.IsAccess.Contains(Handle.NID_Train))
                            {
                                TraverseRailSwitch.IsAccess.Remove(Handle.NID_Train);
                            }
                            System.Windows.Application.Current.Dispatcher.Invoke(
                            new Action(
                            delegate
                            {
                                TraverseRailSwitch.InvalidateVisual();
                            }));
                        }
                    }
                }
            }
        }

        private void UpdateAccess()
        {
            if (Determine.Route.Count >0)
            {
                if (Determine.Route[0].Substring(4) == "0")
                {
                    int CurTrainHeadOffset = Convert.ToInt16(HandleVOBCData.TrainPosition[Handle.NID_Train][4]);
                    Section TraverseSection = UpdateInfo.TraverseSection(Determine.Route[0].Substring(0, 3));
                    if (TraverseSection != null)
                    {
                        if (CurTrainHeadOffset < 60)
                        {
                            if (!TraverseSection.IsAccess.Contains(Handle.NID_Train))
                            {
                                TraverseSection.IsAccess.Add(Handle.NID_Train);
                            }
                            System.Windows.Application.Current.Dispatcher.Invoke(
                            new Action(
                            delegate
                            {
                                TraverseSection.InvalidateVisual();
                            }));
                        }
                        else
                        {
                            if (TraverseSection.IsAccess.Contains(Handle.NID_Train))
                            {
                                TraverseSection.IsAccess.Remove(Handle.NID_Train);
                            }
                            System.Windows.Application.Current.Dispatcher.Invoke(
                            new Action(
                            delegate
                            {
                                TraverseSection.InvalidateVisual();
                            }));
                        }
                    }
                }
                else
                {
                    RailSwitch TraverseRailSwitch = UpdateInfo.TraverseRailSwitch(Determine.Route[0].Substring(0, 3), Determine.Route[0].Substring(4));
                    if (!TraverseRailSwitch.IsAccess.Contains(Handle.NID_Train))
                    {
                        TraverseRailSwitch.IsAccess.Add(Handle.NID_Train);
                    }
                    System.Windows.Application.Current.Dispatcher.Invoke(
                    new Action(
                    delegate
                    {
                        TraverseRailSwitch.InvalidateVisual();
                    }));
                }
                for (int i = 1; i < Determine.Route.Count; i++)
                {
                    Section TraverseSection = UpdateInfo.TraverseSection(Determine.Route[i].Substring(0, 3));
                    if (TraverseSection != null)
                    {
                        if (!TraverseSection.IsAccess.Contains(Handle.NID_Train))
                        {
                            TraverseSection.IsAccess.Add(Handle.NID_Train);
                        }
                        System.Windows.Application.Current.Dispatcher.Invoke(
                        new Action(
                        delegate
                        {
                            TraverseSection.InvalidateVisual();
                        }));
                    }
                    else
                    {
                        RailSwitch TraverseRailSwitch = UpdateInfo.TraverseRailSwitch(Determine.Route[i].Substring(0, 3), Determine.Route[i].Substring(4));
                        if (!TraverseRailSwitch.IsAccess.Contains(Handle.NID_Train))
                        {
                            TraverseRailSwitch.IsAccess.Add(Handle.NID_Train);
                        }
                        System.Windows.Application.Current.Dispatcher.Invoke(
                        new Action(
                        delegate
                        {
                            TraverseRailSwitch.InvalidateVisual();
                        }));
                    }
                }
            }
            
        }

        private void SetInfoToVOBC()
        {
            SetVOBCHead();
            SetID();
            SetNCofZC();
            SetMAHead();
            SetMATail();
            SetNumberOfBarrier();
            SetMALength();
            SetMAEndType();
            SetMAObstacle();
        }

        private void SetVOBCHead()
        {
            InfoSendToATP.DataType = 9;
            InfoSendToATP.SenderID = 3;
            InfoSendToATP.ReceiverID = Data[4];
        }

        private void SetID()
        {
            InfoSendToATP.NID_ZC = 1;
            InfoSendToATP.NID_Train = Handle.NID_Train;
        }

        private void SetNCofZC()
        {
            switch (Handle.NC_Train)
            {
                case 0x01:
                    InfoSendToATP.NC_ZC = 0x01;
                    break;
                case 0x02:
                    InfoSendToATP.NC_ZC = 0x02;
                    break;
                case 0x03:
                    InfoSendToATP.NC_ZC = 0x03;
                    break;
                case 0x04:
                    InfoSendToATP.NC_ZC = 0x04;
                    break;
                case 0x05:
                    InfoSendToATP.NC_ZC = 0x05;
                    break;
                default:
                    break;
            }
        }

        private void SetMAHead()
        {
            InfoSendToATP.D_MAHeadLink = (ushort)(Data[21] * 256 + Data[20]);
            InfoSendToATP.D_MAHeadOff = Data[24];
            InfoSendToATP.Q_MAHeadDir = 0;
        }

        private void SetMATail()
        {
            Determine = new SetMA();
            byte[] MATail = Determine.DetermineMA(Handle);
            if (MATail != null)
            {
                InfoSendToATP.D_MATailLink = (ushort)(MATail[1] * 256 + MATail[0]);
                InfoSendToATP.D_MATailOff = MATail[2];
                InfoSendToATP.Q_MATailDir = MATail[6];
            }

        }

        private void SetNumberOfBarrier()
        {
            int NumOfBarrier = Determine.GetNumOfBarrier(Determine.Route);
            InfoSendToATP.N_Obstacle = (Byte)NumOfBarrier;
        }

        private void SetMALength()
        {
            int NumOfBarrier = InfoSendToATP.N_Obstacle;
            InfoSendToATP.N_Length = Convert.ToByte(17 + 5 * NumOfBarrier);
        }

        private void SetMAEndType()
        {
            InfoSendToATP.NC_MAEndType = Determine.MAEndType;
        }

        private void SetMAObstacle()
        {
            InfoSendToATP.Obstacle = new List<ObstacleInfo>();
            Determine.SetMAObstacle(InfoSendToATP.Obstacle, Determine.Route);
        }
    }
}
