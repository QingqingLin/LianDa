using System;
using System.Collections.Generic;
using System.Linq;
using 线路绘图工具;

namespace ZC
{
    class UpdateInfo
    {
        List<Device> NeedChange = new List<Device>();
        HandleVOBCData VOBCInfo;
        public static Dictionary<byte, byte[]> PreTrainPosition = new Dictionary<byte, byte[]>();
        byte[] Data;
        string[] Station = new string[] {"108" , "109" , "202" , "205" , "305" , "314" , "407" , "410" };

        public UpdateInfo(HandleVOBCData Handle, byte[] Data)
        {
            this.Data = Data;
            this.VOBCInfo = Handle;

            CancelPreTrainPosition();
            UpDataTrainOccupy();
            UpdataLine();
            UpdatePrePosition();
        }

        private void UpdataLine()
        {
            foreach (var item in NeedChange)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(
                new Action(
                delegate
                {
                    item.InvalidateVisual();
                }));
            }
        }

        public void UpdatePrePosition()
        {
            if (PreTrainPosition.Keys.Contains(VOBCInfo.NID_Train))
            {
                Array.Copy(Data, 20, PreTrainPosition[VOBCInfo.NID_Train], 0, 12);
            }
            else
            {
                byte[] pre = new byte[12];
                Array.Copy(Data, 20, pre, 0, 12);
                PreTrainPosition.Add(VOBCInfo.NID_Train, pre);
            }
        }

        private void CancelPreTrainPosition()
        {
            if (PreTrainPosition.Keys.Contains(VOBCInfo.NID_Train))
            {
                string PreTrainHeadPosition = (Convert.ToInt16(PreTrainPosition[VOBCInfo.NID_Train][1]) * 256 + Convert.ToInt16(PreTrainPosition[VOBCInfo.NID_Train][0])).ToString();
                string PreTrainHeadRailSwitchName = (Convert.ToInt16(PreTrainPosition[VOBCInfo.NID_Train][2])).ToString();
                Cancel(PreTrainHeadPosition, PreTrainHeadRailSwitchName);
                string PreTrainTailPosition = (Convert.ToInt16(PreTrainPosition[VOBCInfo.NID_Train][7]) * 256 + Convert.ToInt16(PreTrainPosition[VOBCInfo.NID_Train][6])).ToString();
                string PreTrainTailRailSwitchName = (Convert.ToInt16(PreTrainPosition[VOBCInfo.NID_Train][8])).ToString();
                Cancel(PreTrainTailPosition, PreTrainTailRailSwitchName);
            }
        }

        private void Cancel(string PreTrainPosition,string PreTrainSwitchName)
        {
            Section section = TraverseSection(PreTrainPosition);
            if (section != null)
            {
                for (int i = 0; i < section.TrainOccupy.Count; i++)
                {
                    if (section.TrainOccupy[i] == VOBCInfo.NID_Train)
                    {
                        section.TrainOccupy.Remove(VOBCInfo.NID_Train);
                        break;
                    }
                }
                if (section.IsFrontLogicOccupy.Contains(VOBCInfo.NID_Train))
                {
                    section.IsFrontLogicOccupy.Remove(VOBCInfo.NID_Train);
                }
                if (section.IsLastLogicOccupy.Contains(VOBCInfo.NID_Train))
                {
                    section.IsLastLogicOccupy.Remove(VOBCInfo.NID_Train);
                }
                if (!NeedChange.Contains(TraverseSection(PreTrainPosition) as Device))
                {
                    NeedChange.Add(TraverseSection(PreTrainPosition));
                }
            }
            else if(TraverseRailSwitch(PreTrainPosition, PreTrainSwitchName) != null)
            {
                for (int i = 0; i < TraverseRailSwitch(PreTrainPosition, PreTrainSwitchName).TrainOccupy.Count; i++)
                {
                    if (TraverseRailSwitch(PreTrainPosition, PreTrainSwitchName).TrainOccupy[i] == VOBCInfo.NID_Train)
                    {
                        TraverseRailSwitch(PreTrainPosition, PreTrainSwitchName).TrainOccupy.Remove(VOBCInfo.NID_Train);
                        break;
                    }
                }
                if (!NeedChange.Contains(TraverseRailSwitch(PreTrainPosition, PreTrainSwitchName) as Device))
                {
                    NeedChange.Add(TraverseRailSwitch(PreTrainPosition, PreTrainSwitchName));
                }
            }

        }

        public static Section TraverseSection(string TrainPosition)
        {
            foreach (var item in MainWindow.stationElements_.Elements)
            {
                if (item is Section)
                {
                    if ((item as Section).Name.Length > 2)
                    {
                        if ((item as Section).Name.Substring(0, 3) == TrainPosition)
                        {
                            return (item as Section);
                        }
                    }
                }
            }
            foreach (var item in MainWindow.stationElements_1_.Elements)
            {
                if (item is Section)
                {
                    if ((item as Section).Name.Length > 2)
                    {
                        if ((item as Section).Name.Substring(0, 3) == TrainPosition)
                        {
                            return (item as Section);
                        }
                    }
                }
            }
            return null;
        }

        public static RailSwitch TraverseRailSwitch(string TrainPosition,string TrainRailSwitchName)
        {
            foreach (var item in MainWindow.stationElements_.Elements)
            {
                if (item is RailSwitch)
                {
                    if ((item as RailSwitch).SectionName.Length > 2)
                    {
                        if ((item as RailSwitch).SectionName.Substring(0, 3) == TrainPosition && (item as RailSwitch).Name == TrainRailSwitchName)
                        {
                            return (item as RailSwitch);
                        }
                    }
                }
            }
            foreach (var item in MainWindow.stationElements_1_.Elements)
            {
                if (item is RailSwitch)
                {
                    if ((item as RailSwitch).SectionName.Length > 2)
                    {
                        if ((item as RailSwitch).SectionName.Substring(0, 3) == TrainPosition && (item as RailSwitch).Name == TrainRailSwitchName)
                        {
                            return (item as RailSwitch);
                        }
                    }
                }
            }
            return null;
        }

        private void UpDataTrainOccupy()
        {
            string CurTrainHeadSectionName = (Convert.ToInt16(HandleVOBCData.TrainPosition[VOBCInfo.NID_Train][1]) * 256 + Convert.ToInt16(HandleVOBCData.TrainPosition[VOBCInfo.NID_Train][0])).ToString();
            string CurTrainHeadName = (Convert.ToInt16(HandleVOBCData.TrainPosition[VOBCInfo.NID_Train][2])).ToString();
            int CurTrainHeadOffset = Convert.ToInt16(HandleVOBCData.TrainPosition[VOBCInfo.NID_Train][4]);

            string CurTrainTailSectionName = (Convert.ToInt16(HandleVOBCData.TrainPosition[VOBCInfo.NID_Train][7]) * 256 + Convert.ToInt16(HandleVOBCData.TrainPosition[VOBCInfo.NID_Train][6])).ToString();
            string CurTrainTailName = (Convert.ToInt16(HandleVOBCData.TrainPosition[VOBCInfo.NID_Train][8])).ToString();
            int CurTrainTailOffset = Convert.ToInt16(HandleVOBCData.TrainPosition[VOBCInfo.NID_Train][10]);

            if (CurTrainHeadSectionName == "501")
            {
                CurTrainHeadSectionName = "XJ1G";
            }
            else if (CurTrainTailSectionName == "502")
            {
                CurTrainTailSectionName = "XJ2G";
            }

            Updata(CurTrainHeadSectionName, CurTrainHeadName, CurTrainHeadOffset);
            Updata(CurTrainTailSectionName, CurTrainTailName, CurTrainTailOffset);
        }

        public void Updata(string CurTrainSectionName,string CurTrainName,int CurTrainOffset)
        {
            if (TraverseSection(CurTrainSectionName) != null)
            {
                if (Station.Contains(CurTrainSectionName))
                {
                    if (!NeedChange.Contains(TraverseSection(CurTrainSectionName) as Device))
                    {
                        NeedChange.Add(TraverseSection(CurTrainSectionName));
                    }
                    if (!TraverseSection(CurTrainSectionName).TrainOccupy.Contains(VOBCInfo.NID_Train))
                    {
                        TraverseSection(CurTrainSectionName).TrainOccupy.Add(VOBCInfo.NID_Train);
                    }
                    TraverseSection(CurTrainSectionName).IsFrontLogicOccupy.Add(VOBCInfo.NID_Train);
                    TraverseSection(CurTrainSectionName).IsLastLogicOccupy.Add(VOBCInfo.NID_Train);
                }
                else
                {
                    if (!NeedChange.Contains(TraverseSection(CurTrainSectionName) as Device))
                    {
                        NeedChange.Add(TraverseSection(CurTrainSectionName));
                    }
                    if (!TraverseSection(CurTrainSectionName).TrainOccupy.Contains(VOBCInfo.NID_Train))
                    {
                        TraverseSection(CurTrainSectionName).TrainOccupy.Add(VOBCInfo.NID_Train);
                    }
                    if (VOBCInfo.Q_TrainRealDirection == 1)
                    {
                        if (CurTrainOffset <= 60)
                        {
                            TraverseSection(CurTrainSectionName).IsFrontLogicOccupy.Add(VOBCInfo.NID_Train);
                        }
                        else
                        {
                            TraverseSection(CurTrainSectionName).IsLastLogicOccupy.Add(VOBCInfo.NID_Train);
                        }
                    }
                    if (VOBCInfo.Q_TrainRealDirection == 0)
                    {
                        if (CurTrainOffset <= 60)
                        {
                            TraverseSection(CurTrainSectionName).IsLastLogicOccupy.Add(VOBCInfo.NID_Train);
                        }
                        else
                        {
                            TraverseSection(CurTrainSectionName).IsFrontLogicOccupy.Add(VOBCInfo.NID_Train);
                        }
                    }
                }
            }
            else if (TraverseRailSwitch(CurTrainSectionName, CurTrainName) != null)
            {
                if (!NeedChange.Contains(TraverseRailSwitch(CurTrainSectionName, CurTrainName) as Device))
                {
                    NeedChange.Add(TraverseRailSwitch(CurTrainSectionName, CurTrainName));
                }
                if (!TraverseRailSwitch(CurTrainSectionName, CurTrainName).TrainOccupy.Contains(VOBCInfo.NID_Train))
                {
                    TraverseRailSwitch(CurTrainSectionName, CurTrainName).TrainOccupy.Add(VOBCInfo.NID_Train);
                }
            }
        }
    }
}
