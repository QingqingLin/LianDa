using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZC
{
    class VOBCQuit
    {
        public VOBCQuit(Dictionary<byte, List<string>> PreAccess,byte TrainID,Dictionary<byte,byte[]> TrainPosition)
        {
            CancelOccupy(TrainID, TrainPosition);
            CancelPreAccess(TrainID, PreAccess);
        }

        private void CancelPreAccess(byte trainID, Dictionary<byte, List<string>> preAccess)
        {
            if (VOBCData.PreAccess.Keys.Contains(trainID))
            {
                foreach (var Route in VOBCData.PreAccess[trainID])
                {
                    Section TraverseSection = UpdateInfo.TraverseSection(Route.Substring(0, 3));
                    if (TraverseSection != null)
                    {
                        if (TraverseSection.IsAccess.Contains(trainID))
                        {
                            TraverseSection.IsAccess.Remove(trainID);
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
                        if (TraverseRailSwitch.IsAccess.Contains(trainID))
                        {
                            TraverseRailSwitch.IsAccess.Remove(trainID);
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

        private void CancelOccupy(byte trainID, Dictionary<byte,byte[]> TrainPosition)
        {
            string TrainHeadSection = (Convert.ToInt16(TrainPosition[trainID][1]) * 256 + Convert.ToInt16(TrainPosition[trainID][0])).ToString();
            string RailHeadSwitch = (Convert.ToInt16(TrainPosition[trainID][2])).ToString();
            Section section = NonCommunicationTrain.TraverseSection(TrainHeadSection);
            if (section != null)
            {
                section.TrainOccupy.Remove(trainID);
                if (section.IsFrontLogicOccupy.Contains(trainID))
                {
                    section.IsFrontLogicOccupy.Remove(trainID);
                }
                if (section.IsLastLogicOccupy.Contains(trainID))
                {
                    section.IsLastLogicOccupy.Remove(trainID);
                }
                System.Windows.Application.Current.Dispatcher.Invoke(
                 new Action(
                 delegate
                 {
                     section.InvalidateVisual();
                 }));
            }
            else
            {
                RailSwitch railSwitch = NonCommunicationTrain.TraverseRailSwitch(TrainHeadSection, RailHeadSwitch);
                if(railSwitch != null)
                {
                    railSwitch.TrainOccupy.Remove(trainID);
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate
                    {
                        railSwitch.InvalidateVisual();
                    }));
                }
            }


            string TrainTailSection = (Convert.ToInt16(TrainPosition[trainID][7]) * 256 + Convert.ToInt16(TrainPosition[trainID][6])).ToString();
            string RailTailSwitch = (Convert.ToInt16(TrainPosition[trainID][8])).ToString();
            Section sectionTail = NonCommunicationTrain.TraverseSection(TrainTailSection);
            if (sectionTail != null)
            {
                sectionTail.TrainOccupy.Remove(trainID);
                if (sectionTail.IsFrontLogicOccupy.Contains(trainID))
                {
                    sectionTail.IsFrontLogicOccupy.Remove(trainID);
                }
                if (sectionTail.IsLastLogicOccupy.Contains(trainID))
                {
                    sectionTail.IsLastLogicOccupy.Remove(trainID);
                }
                System.Windows.Application.Current.Dispatcher.Invoke(
                 new Action(
                 delegate
                 {
                     sectionTail.InvalidateVisual();
                 }));
            }
            else
            {
                RailSwitch railSwitchTail = NonCommunicationTrain.TraverseRailSwitch(TrainTailSection, RailTailSwitch);
                if (railSwitchTail != null)
                {
                    railSwitchTail.TrainOccupy.Remove(trainID);
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate
                    {
                        railSwitchTail.InvalidateVisual();
                    }));
                }
            }
        }
    }
}
