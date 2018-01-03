using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 线路绘图工具;

namespace ZC
{
    class SetMA
    {
        public string MASection = "";
        public int MAOffset = 0;
        public int MADir = 0;
        public List<CItable> CurTrainIn = new List<CItable>();
        public List<string> Route = new List<string>();
        public bool IsFront = true;
        public byte MAEndType = 0x01;
        HandleVOBCData Handle;

        public byte[] DetermineMA(HandleVOBCData Handle)
        {
            this.Handle = Handle;
            int TrainSectionInt = Convert.ToInt16(HandleVOBCData.TrainPosition[Handle.NID_Train][1]) * 256 + Convert.ToInt16(HandleVOBCData.TrainPosition[Handle.NID_Train][0]);//纯数字
            int RailSwitchInt = Convert.ToInt16(HandleVOBCData.TrainPosition[Handle.NID_Train][2]);
            string RailSwitchString;
            string TrainSectionString;
            if (TrainSectionInt == 501)
            {
                TrainSectionString = "XJ1G";
            }
            else if (TrainSectionInt == 502)
            {
                TrainSectionString = "XJ2G";
            }
            else
            {
                TrainSectionString = TrainSectionInt.ToString();
            }

            if (TrainSectionString == "118")
            {

            }
            RailSwitchString = RailSwitchInt.ToString();
            int TrainDir = Handle.Q_TrainRealDirection;
            foreach (var item in MainWindow.stationElements_.Elements)
            {
                if (item is Section)
                {
                    if (item.Name.Length > 2)
                    {
                        if (item.Name.Substring(0, 3) == TrainSectionString)
                        {
                            FindCurTrainIn(TrainSectionString + "-0", TrainDir);
                        }
                    }
                }
                if (item is RailSwitch)
                {
                    if ((item as RailSwitch).SectionName.Length > 2)
                    {
                        if ((item as RailSwitch).SectionName.Substring(0, 3) == TrainSectionString)
                        {
                            FindCurTrainIn(TrainSectionString + "-" + RailSwitchString, TrainDir);
                        }
                    }
                }
            }
            foreach (var item in MainWindow.stationElements_1_.Elements)
            {
                if (item is Section)
                {
                    if (item.Name.Length > 2)
                    {
                        if (item.Name.Substring(0, 3) == TrainSectionString)
                        {
                            FindCurTrainIn(TrainSectionString + "-0", TrainDir);
                        }
                    }
                }
                if (item is RailSwitch)
                {
                    if ((item as RailSwitch).SectionName.Length > 2)
                    {
                        if ((item as RailSwitch).SectionName.Substring(0, 3) == TrainSectionString)
                        {
                            FindCurTrainIn(TrainSectionString + "-" + RailSwitchString, TrainDir);
                        }
                    }
                }
            }
            CItable NextAccess = null;
            if (CurTrainIn.Count != 0)
            {
                foreach (var item in CurTrainIn)
                {
                    if ((TrainSectionString + "-" + RailSwitchString) == item.StartSection)
                    {
                        Signal startSignal = TraverseSignal(item.StartSignal) as Signal;
                        if (!startSignal.IsSignalOpen)
                        {
                            return ConvertToByte(TrainSectionString, 130, TrainDir);
                        }
                    }


                    int num = item.Section.IndexOf(TrainSectionString + "-" + RailSwitchString);
                    if (item.IsReturn)
                    {
                        MAEndType = 0x02;
                        for (int i = num; i < item.Section.Count; i++)
                        {
                            if (!Route.Contains(item.Section[i]))
                            {
                                Route.Add(item.Section[i]);
                            }
                        }
                        if (Route.Count > 1)
                        {
                            for (int i = 1; i < Route.Count; i++)
                            {
                                if (Route[i].Substring(0, 3) != item.Section[num].Substring(0, 3))
                                {
                                    byte[] a = SectionAxleOccpy(Route[i], TrainDir);
                                    if (a != null)
                                    {
                                        for (int x = i; x < Route.Count; x++)
                                        {
                                            Route.Remove(Route[i]);
                                        }
                                        return a;
                                    }
                                }
                                byte[] b = SectionOccpy(Route[i], TrainDir);
                                if (b != null)
                                {
                                    for (int x = i; x < Route.Count; x++)
                                    {
                                        Route.Remove(Route[i]);
                                    }
                                    return b;
                                }
                            }
                            return SetAccessEndMA(Route[Route.Count - 1], TrainDir);
                        }
                        else if (Route.Count == 1)
                        {
                            return SetAccessEndMA(Route[Route.Count - 1], TrainDir);
                        }
                    }
                    else
                    {
                        for (int i = num; i < item.Section.Count; i++)
                        {
                            if (!Route.Contains(item.Section[i]))
                            {
                                Route.Add(item.Section[i]);
                            }
                            if (item.Section[i].Substring(0,3) != item.Section[num].Substring(0,3))
                            {
                                byte[] a = SectionAxleOccpy(item.Section[i], TrainDir);
                                if (a != null)
                                {
                                    return a;
                                }
                            }
                            byte[] b = SectionOccpy(item.Section[i], TrainDir);
                            if (b != null)
                            {
                                return b;
                            }
                            CItable Access = IsApproachSection(item.Section[i], TrainDir);
                            if (Access != null)
                            {
                                NextAccess = Access;
                            }
                        }
                    }
                }
            }
            if (NextAccess != null)
            {
                bool HasNextAccessOpen = true;
                while (HasNextAccessOpen)
                {
                    if (NextAccess.IsReturn)
                    {
                        for (int i = 0; i < NextAccess.Section.Count; i++)
                        {
                            if (!Route.Contains(NextAccess.Section[i]))
                            {
                                Route.Add(NextAccess.Section[i]);
                            }
                        }
                        MAEndType = 0x02;
                        return SetReturnAccess(Route, TrainDir);
                    }
                    else
                    {
                        CItable Next = null;
                        for (int i = 0; i < NextAccess.Section.Count; i++)
                        {
                            if (NextAccess.Section[i] == TrainSectionString + "-" + RailSwitchString)
                            {

                            }
                            else
                            {
                                if (!Route.Contains(NextAccess.Section[i]))
                                {
                                    Route.Add(NextAccess.Section[i]);
                                }
                                byte[] a = SectionOccpy(NextAccess.Section[i], TrainDir);
                                if (a != null)
                                {
                                    return a;
                                }
                                Next = IsApproachSection(NextAccess.Section[i], TrainDir);
                            }
                        }
                        if (Next != null)
                        {
                            NextAccess = Next;
                        }
                        else
                        {
                            HasNextAccessOpen = false;
                        }
                    }
                }
            }
            else
            {
                if (CurTrainIn.Count > 0)
                {
                    NextAccess = CurTrainIn[CurTrainIn.Count - 1];
                }
                else
                {
                    return null;
                }
            }
            MASection = NextAccess.EndSection;
            return SetAccessEndMA(MASection, TrainDir); ;
        }

        private byte[] SetReturnAccess(List<string> Route, int TrainDir)
        {
            for (int i = 0; i < Route.Count; i++)
            {
                byte[] a = SectionAxleOccpy(Route[i], TrainDir);
                if (a != null)
                {
                    return a;
                }
                byte[] b = SectionOccpy(Route[i], TrainDir);
                if (b != null)
                {
                    return b;
                }
            }
            return SetAccessEndMA(Route[Route.Count - 1], TrainDir);
        }

        private byte[] SetAccessEndMA(string MASection,int TrainDir)
        {
            foreach (var item in MainWindow.stationElements_.Elements)
            {
                if (item is Section)
                {
                    if ((item as Section).Name.Length > 2)
                    {
                        if ((item as Section).Name.Substring(0, 3) == MASection.Substring(0, 3))
                        {
                            MAOffset = 130;
                            MADir = TrainDir;
                        }
                    }
                }
                else if (item is RailSwitch)
                {
                    if ((item as RailSwitch).SectionName.Length > 2 && MASection.Length > 2 && MASection.Length >4)
                    {
                        if ((item as RailSwitch).SectionName.Substring(0, 3) == MASection.Substring(0, 3)
                        && (item as RailSwitch).Name == MASection.Substring(4))
                        {
                            MAOffset = SetMAOffset(item as RailSwitch);
                            MADir = TrainDir;
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
                        if ((item as Section).Name.Substring(0, 3) == MASection.Substring(0, 3))
                        {
                            MAOffset = 130;
                            MADir = TrainDir;
                        }
                    }
                }
                else if (item is RailSwitch)
                {
                    if ((item as RailSwitch).SectionName.Length > 2 && (item as RailSwitch).Name.Length > 2)
                    {
                        if ((item as RailSwitch).SectionName.Substring(0, 3) == MASection.Substring(0, 3)
                         && (item as RailSwitch).Name == MASection.Substring(4))
                        {
                            MAOffset = SetMAOffset(item as RailSwitch);
                            MADir = TrainDir;
                        }
                    }
                }
            }
            return ConvertToByte(MASection, MAOffset, MADir);
        }

        private int SetMAOffset(RailSwitch RailSwitch)
        {
            if (RailSwitch.SectionName.Substring(0,3) == "110" || RailSwitch.SectionName.Substring(0,3) == "111"
                || RailSwitch.SectionName.Substring(0, 3) == "118" || RailSwitch.SectionName.Substring(0, 3) == "119")
            {
                if (RailSwitch.IsPositionNormal == true && RailSwitch.IsPositionReverse == false)
                {
                    return 60;
                }
                else
                {
                    return 35;
                }
            }
            else
            {
                return 35;
            }
        }

        private void FindCurTrainIn(string TrainSectionString, int TrainDir)
        {
            foreach (var item in AddCIAccess.CITableListDown)
            {
                if (item.Section.Contains(TrainSectionString) && item.Direction == TrainDir && item.AccessState == 1)
                {
                    CurTrainIn.Add(item);
                }
                else if (item.Section.Contains(TrainSectionString) && item.Direction == TrainDir && item.IsReturn)
                {
                    CurTrainIn.Add(item);
                }
            }
            foreach (var item in AddCIAccess.CITableListTop)
            {
                if (item.Section.Contains(TrainSectionString) && item.Direction == TrainDir && item.AccessState == 1)
                {
                    CurTrainIn.Add(item);
                }
                else if (item.Section.Contains(TrainSectionString) && item.Direction == TrainDir && item.IsReturn)
                {
                    CurTrainIn.Add(item);
                }
            }
        }

        public byte[] SectionAxleOccpy(string CurSection,int TrainDir)
        {
            byte[] OccupyMA = SectionAxleOccpyJudge(CurSection, MainWindow.stationElements_.Elements,TrainDir);
            if (OccupyMA == null)
            {
               OccupyMA = SectionAxleOccpyJudge(CurSection, MainWindow.stationElements_1_.Elements,TrainDir);
            }
            return OccupyMA;
        }

        private byte[] SectionAxleOccpyJudge(string CurSection, List<线路绘图工具.GraphicElement> Elements,int TrainDir)
        {
            foreach (var item in Elements)
            {
                if (item is Section)
	            {
                    if ((item as Section).Name.Substring(0,3) == CurSection.Substring(0,3))
	                {
		                if ((item as Section).AxleOccupy == 0)
                        {
                            MASection = item.Name.Substring(0,3);
                            MAOffset = 0;
                            MADir = TrainDir;
                            return ConvertToByte(MASection, MAOffset, MADir);
                        }
	                }
	            }
                else if (item is RailSwitch)
	            {
                    if ((item as RailSwitch).SectionName.Substring(0, 3) == CurSection.Substring(0, 3) 
                        && (item as RailSwitch).Name == CurSection.Substring(4))
	                {
		                if ((item as RailSwitch).AxleOccupy == 0)
                        {
                            MASection = (item as RailSwitch).SectionName.Substring(0,3);
                            MADir = TrainDir;
                            MAOffset = 0;
                            return ConvertToByte(MASection, MAOffset, MADir);
                        }
	                }
	            }
            }
            return null;
        }


        public byte[] SectionOccpy(string CurSection, int TrainDir)
        {
            byte[] OccupyMA = SectionOccpyJudge(CurSection, MainWindow.stationElements_.Elements, TrainDir);
            if (OccupyMA == null)
            {
                SectionOccpyJudge(CurSection, MainWindow.stationElements_1_.Elements, TrainDir);
            }
            return OccupyMA;
        }

        private byte[] SectionOccpyJudge(string CurSection, List<线路绘图工具.GraphicElement> Elements, int TrainDir)
        {
            foreach (var item in Elements)
            {
                if (item is Section)
                {
                    if ((item as Section).Name.Substring(0, 3) == CurSection.Substring(0, 3))
                    {
                        if (HasOtherTrain((item as Section).TrainOccupy))
                        {
                            MASection = item.Name.Substring(0, 3);
                            MAOffset = (item as Section).Offset;
                            MADir = TrainDir;
                            return ConvertToByte(MASection, MAOffset, MADir);
                        }
                    }
                }
                else if (item is RailSwitch)
                {
                    if ((item as RailSwitch).SectionName.Substring(0, 3) == CurSection.Substring(0, 3)
                        && (item as RailSwitch).Name == CurSection.Substring(4))
                    {
                        if (HasOtherTrain((item as RailSwitch).TrainOccupy))
                        {
                            MASection = (item as RailSwitch).SectionName.Substring(0, 3);
                            MADir = TrainDir;
                            MAOffset = (item as RailSwitch).Offset;
                            return ConvertToByte(MASection, MAOffset, MADir);
                        }
                    }
                }
            }
            return null;
        }

        private bool HasOtherTrain(List<byte> TrainList)
        {
            if (TrainList.Contains(Handle.NID_Train))
            {
                for (int i = 0; i < TrainList.Count; i++)
                {
                    if (TrainList[i] == Handle.NID_Train)
                    {
                        TrainList.Remove(Handle.NID_Train);
                    }
                }
                if (TrainList.Count != 0)
                {
                    TrainList.Add(Handle.NID_Train);
                    return true;
                }
                else
                {
                    TrainList.Add(Handle.NID_Train);
                    return false;
                }
            }
            else
            {
                if (TrainList.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            
        }

        public byte[] ConvertToByte(string MASection, int MAOffset, int MADir)
        {
            if (MASection == "XJ1G-0")
            {
                MASection = "501-0";
            }
            else if (MASection == "XJ2G-0")
            {
                MASection = "502-0";
            }
            byte[] MAEnd = new byte[7];
            int MAInt = Convert.ToInt16(MASection.Substring(0,3));
            if (MAInt > 256)
            {
                MAEnd[0] = Convert.ToByte(MAInt - 256);
                MAEnd[1] = 1;
            }
            else
            {
                MAEnd[0] = Convert.ToByte(MAInt);
                MAEnd[1] = 0;
            }
            MAEnd[2] = Convert.ToByte(MAOffset);
            MAEnd[3] = 0;
            MAEnd[4] = 0;
            MAEnd[5] = 0;
            if (MADir == 0)
            {
                MAEnd[6] = 0xaa;
            }
            else if (MADir == 1)
            {
                MAEnd[6] = 0x55;
            }
            return MAEnd;
        }

        private Device TraverseSignal(string Signal)
        {
            foreach (var item in MainWindow.stationElements_.Elements)
            {
                if (item is Signal)
                {
                    if (item.Name == Signal)
                    {
                        return (item as Device);
                    }
                }
            }
            foreach (var item in MainWindow.stationElements_1_.Elements)
            {
                if (item is Signal)
                {
                    if (item.Name == Signal)
                    {
                        return (item as Device);
                    }
                }
            }
            return null;
        }

        public CItable IsApproachSection(string SectionName, int Direction)
        {
            foreach (var item in AddCIAccess.CITableListDown)
            {
                if (item.StartSection == SectionName && item.AccessState == 1 && item.Direction == Direction && (TraverseSignal(item.StartSignal) as Signal).IsSignalOpen)
                {
                    return item;
                }
            }
            foreach (var item in AddCIAccess.CITableListTop)
            {
                if (item.StartSection == SectionName && item.AccessState == 1 && item.Direction == Direction && (TraverseSignal(item.StartSignal) as Signal).IsSignalOpen)
                {
                    return item;
                }
            }
            return null;
        }

        public int GetNumOfBarrier(List<string> Route)
        {
            int NumOfBarrier = 0;
            for (int i = 0; i < Route.Count; i++)
            {
                if (i==0)
                {
                    if (IsRailSwitch(Route[i]))
                    {
                        NumOfBarrier++;
                    }
                }
                else
                {
                    if (Route[i].Substring(0, 3) == Route[i - 1].Substring(0, 3)) { }
                    else
                    {
                        if (IsRailSwitch(Route[i]))
                        {
                            NumOfBarrier++;
                        }
                    }
                }
            }
            return NumOfBarrier;
        }
      
        private bool IsRailSwitch(string RouteMember)
        {
            foreach (var element in MainWindow.stationElements_.Elements)
            {
                if (element is RailSwitch)
                {
                    if ((element as RailSwitch).SectionName.Substring(0, 3) == RouteMember.Substring(0, 3))
                    {
                        return true;
                    }
                }
            }
            foreach (var element in MainWindow.stationElements_1_.Elements)
            {
                if (element is RailSwitch)
                {
                    if ((element as RailSwitch).SectionName.Substring(0, 3) == RouteMember.Substring(0, 3))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void SetMAObstacle(List<ObstacleInfo> Obstacle, List<string> Route)
        {
            for (int i = 0; i < Route.Count; i++)
            {
                if (i == 0)
                {
                    AddObstacleCollection(MainWindow.stationElements_, Obstacle, Route[i]);
                    AddObstacleCollection(MainWindow.stationElements_1_, Obstacle, Route[i]);
                }
                else
                {
                    if (Route[i].Substring(0, 3) == Route[i - 1].Substring(0, 3)) { }
                    else
                    {
                        AddObstacleCollection(MainWindow.stationElements_, Obstacle, Route[i]);
                        AddObstacleCollection(MainWindow.stationElements_1_, Obstacle, Route[i]);
                    }
                }
            }
        }

        public void AddObstacleCollection(StationElements StationElements, List<ObstacleInfo> Obstacle, string Section)
        {
            foreach (var element in StationElements.Elements)
            {
                if (element is RailSwitch && (element as RailSwitch).SectionName.Substring(0, 3) == Section.Substring(0, 3) && (element as RailSwitch).Name == Section.Substring(4))
                {
                    ObstacleInfo railSwitchObstacle = new ObstacleInfo();
                    railSwitchObstacle.NC_Obstacle = 4;
                    railSwitchObstacle.NID_Obstacle = (ushort)Convert.ToInt16((element as RailSwitch).SectionName.Substring(0, 3));
                    railSwitchObstacle.Q_Obstacle_Now  = ((element as RailSwitch).IsPositionNormal == true ? Convert.ToByte(1) : Convert.ToByte(2));
                    railSwitchObstacle.Q_Obstacle_CI = railSwitchObstacle.Q_Obstacle_Now;
                    Obstacle.Add(railSwitchObstacle);
                }
            }
        }
    }
}