using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZC
{
    class NonCommunicationTrain
    {
        public static Dictionary<byte, byte[]> LoseTrain = new Dictionary<byte, byte[]>();

        public void JudgeLostTrain()
        {
            while (true)
            {
                HiPerfTimer hip = new HiPerfTimer();
                hip.Start();
                hip.Interval(3000);
                lock (VOBCorCI.VOBCNonCom)
                {
                    Judge(VOBCorCI.VOBCNonCom);
                    VOBCorCI.VOBCNonCom.Clear();
                }
                UpdateLostTrain();
            }
        }

        private void Judge(List<byte> VOBCList)
        {
            lock (HandleVOBCData.TrainPosition)
            {
                foreach (var item in HandleVOBCData.TrainPosition.Keys)
                {
                    if (!VOBCList.Contains(item))
                    {
                        if (!LoseTrain.Keys.Contains(item))
                        {
                            LoseTrain.Add(item, HandleVOBCData.TrainPosition[item]);
                        }
                    }
                }
            }
        }

        private void UpdateLostTrain()
        {
            foreach (var item in LoseTrain.Keys)
            {
                string TrainHeadSection = (Convert.ToInt16(LoseTrain[item][1]) * 256 + Convert.ToInt16(LoseTrain[item][0])).ToString();
                string RailHeadSwitch = (Convert.ToInt16(LoseTrain[item][2])).ToString();
                if (TraverseSection(TrainHeadSection) != null)
                {
                    Section section = TraverseSection(TrainHeadSection);
                    if (!section.HasNonComTrain.Contains(item))
                    {
                        section.HasNonComTrain.Add(item);
                    }
                    System.Windows.Application.Current.Dispatcher.Invoke(
                     new Action(
                     delegate
                     {
                         section.InvalidateVisual();
                     }));
                }
                else if (TraverseRailSwitch(TrainHeadSection, RailHeadSwitch) != null)
                {
                    RailSwitch railswitch = TraverseRailSwitch(TrainHeadSection, RailHeadSwitch);
                    if (!railswitch.HasNonComTrain.Contains(item))
                    {
                        railswitch.HasNonComTrain.Add(item);
                    }
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate
                     {
                         railswitch.InvalidateVisual();
                     }));
                }


                string TrainTailSection = (Convert.ToInt16(LoseTrain[item][7]) * 256 + Convert.ToInt16(LoseTrain[item][6])).ToString();
                string RailTailSwitch = (Convert.ToInt16(LoseTrain[item][8])).ToString();
                if (TraverseSection(TrainTailSection) != null)
                {
                    Section section = TraverseSection(TrainTailSection);
                    if (!section.HasNonComTrain.Contains(item))
                    {
                        section.HasNonComTrain.Add(item);
                    }
                    System.Windows.Application.Current.Dispatcher.Invoke(
                     new Action(
                     delegate
                     {
                         section.InvalidateVisual();
                     }));
                }
                else if (TraverseRailSwitch(TrainTailSection, RailTailSwitch) != null)
                {
                    RailSwitch railswitch = TraverseRailSwitch(TrainTailSection, RailTailSwitch);
                    if (!railswitch.HasNonComTrain.Contains(item))
                    {
                        railswitch.HasNonComTrain.Add(item);
                    }
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate
                    {
                        railswitch.InvalidateVisual();
                    }));
                }
            }
            UpdateAccessOfTrain();
        }

        private void UpdateAccessOfTrain()
        {
            foreach (var item in LoseTrain.Keys)
            {
                if (VOBCData.PreAccess.Keys.Contains(item))
                {
                    foreach (var Route in VOBCData.PreAccess[item])
                    {
                        Section TraverseSection = UpdateInfo.TraverseSection(Route.Substring(0, 3));
                        if (TraverseSection != null)
                        {
                            if (TraverseSection.IsAccess.Contains(item))
                            {
                                TraverseSection.IsAccess.Remove(item);
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
                            RailSwitch TraverseRailSwitch = UpdateInfo.TraverseRailSwitch(Route.Substring(0, 3), Route.Substring(4));
                            if (TraverseRailSwitch.IsAccess.Contains(item))
                            {
                                TraverseRailSwitch.IsAccess.Remove(item);
                            }
                            System.Windows.Application.Current.Dispatcher.Invoke(
                            new Action(
                            delegate
                            {
                                TraverseRailSwitch.InvalidateVisual();
                            }));
                        }
                    }
                    lock(VOBCData.PreAccess)
                    {
                        VOBCData.PreAccess.Remove(item);
                    }
                }
            }
        }

        public static Section TraverseSection(string TrainPosition)
        {
            foreach (var item in MainWindow.stationElements_.Elements)
            {

                    if (item is Section)
                    {
                        if ((item as Section).Name.Length >2)
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

        public static RailSwitch TraverseRailSwitch(string TrainPosition, string TrainRailSwitchName)
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
    }
}
