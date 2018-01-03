using System;
using System.Linq;
using System.Windows;
using System.Collections;

namespace CBTC
{
    class SearchDistances
    {
        int limSpeedNum = 0;
        int limSpeedDistance_1 = 0;
        int limSpeedLength_1 = 0;
        int limSpeedDistance_2 = 0;
        int limSpeedLength_2 = 0;
        int limSpeedDistance_3 = 0;
        int limSpeedLength_3 = 0;
        int limSpeedDistance_4 = 0;
        int limSpeedLength_4 = 0;
        int MAEndDistance = 0;
        int[] limSpeedDistance = new int[10];
        int[] limSpeedLength = new int[10];
        int[] retuenValue = new int[9];
        int[] nodeID = new int[2];//存储每一次索引的区段ID
        int i = 0;
        int j = 0;//用来放道岔DG
        int reverseNum = 0;//经过的处于反位状态的障碍物的数量
        int count = 0;//用来统计经过多少障碍物        
        int offset = 0;//距离区段的当前的偏移量   
        int curNodeID = 0;//道岔时从应答器解的区段ID    
        int distance_1 = 0;//距当前区段端点的距离
        int distance_2 = 0;//当前区段距离MA的距离  
        int tempLink = 0;
        int tempLinkNum = 0;
        int nextLimitSpeed = 0;
        int firstReverseNum = 0;
        int Namelength = 0;//判断当前所在区段是道岔还是区段,在列车是道岔时索引距离时使用   
        string curNodeName = "";//从应答器解的区段     
        string[] nodeName = new string[2];//存储每一次索引的区段
        string[] switchName = new string[2];//存储道岔的区段号
        string tempSwitchName_1 = null;//临时存储道岔的区段号
        string tempSwitchName_2 = null;//临时存储道岔的区段号
        string switchConvert = "";
        string firstSwitchName = "";//在列车经过道岔时索引距离时使用
        string firstRverseObstacle = "";
        bool isSwitchMAEnd = false;//MA终点是否是道岔                  
        bool isFirstBalise_1 = false;//用来列车在道岔时寻路时使用
        bool isBreak = false;//用来跳出索引循环    
        bool isFirst = true;//列车在道岔时计算偏移量
        bool isNextLimitSpeed = true;
        HashTable hashTable;

        public int[] SearchDistance(bool isLeftSearch, string MAEndLink, int MAEndOff, int obstacleNum, string curBalise, string[] obstacleID, byte[] obstacleState)
        {
            #region 初始化变量
            i = 0;
            j = 0;
            reverseNum = 0;
            count = 0;
            offset = 0;
            distance_1 = 0;
            distance_2 = 0;
            MAEndDistance = 0;
            limSpeedNum = 0;
            limSpeedDistance_1 = 0;
            limSpeedLength_1 = 0;
            limSpeedDistance_2 = 0;
            limSpeedLength_2 = 0;
            limSpeedDistance_3 = 0;
            limSpeedLength_3 = 0;
            nodeID = new int[2];
            nodeName = new string[2];
            switchName = new string[2];
            limSpeedDistance = new int[10];
            limSpeedLength = new int[10];
            retuenValue = new int[9];
            hashTable = new HashTable();
            isBreak = false;
            isSwitchMAEnd = false;
            isFirstBalise_1 = false;
            firstRverseObstacle = "";
            nextLimitSpeed = 0;
            firstReverseNum = 0;
            isNextLimitSpeed = true;
            hashTable = new HashTable();
            hashTable.MAIsSwitch();
            hashTable.sectionHashTable();
            hashTable.switchHashTable();
            #endregion

            #region 判断MA终点是否道岔
            if (hashTable.ht_2.Contains(MAEndLink) == false)
            {
                MAEndLink = MAEndLink + "G";
            }
            else//是道岔
            {
                MAEndLink = MAEndLink + "DG";
                isSwitchMAEnd = true;
            }
            #endregion

            #region 沿途障碍物加DG
            if (obstacleNum != 0)
            {
                for (int k = 0; k < obstacleNum; k++)
                {
                    if (obstacleID[k].Length == 3)
                    {
                        obstacleID[k] = obstacleID[k] + "DG";
                    }
                }
                for (int m = 0; m < obstacleNum; m++)
                {
                    if (obstacleState[m] == 2)
                    {
                        firstReverseNum = m + 1;
                        break;
                    }
                }
                if (firstReverseNum > 0)
                {
                    firstRverseObstacle = obstacleID[firstReverseNum - 1];
                }
            }
            #endregion

            #region 判断所在区段是否道岔
            if (hashTable.ht.Contains(curBalise) == false)//不是道岔
            {
                switchConvert = null;
                isFirst = true;
            }
            else
            {
                foreach (string key in hashTable.ht.Keys)
                {
                    if (curBalise == key)
                    {
                        switchConvert = (string)hashTable.ht[key];
                    }
                }
                curNodeName = switchConvert.Substring(0, switchConvert.IndexOf("-"));
                curNodeID = Convert.ToInt32(switchConvert.Substring(switchConvert.IndexOf("-") + 1));
            }
            #endregion

            #region 判断所在区段的偏移量
            if (switchConvert != null)//所在区段是道岔
            {
                int curdLink = Convert.ToInt32(curBalise.Substring(0, 3));
                int curLinkNum = Convert.ToInt32(curBalise.Substring(curBalise.IndexOf("-") + 1));
                if (isFirst == true)
                {
                    tempLink = curdLink;
                    tempLinkNum = curLinkNum;
                    isFirst = false;
                }
                if (curdLink == 110 || curdLink == 111 || curdLink == 118 || curdLink == 119)
                {
                    if (curdLink == tempLink && curLinkNum == tempLinkNum)
                    {
                        if (obstacleState[0] == 2)//反位
                        {
                            distance_1 = 20;
                            offset = 5;
                        }
                        else
                        {
                            distance_1 = 45;
                            offset = 5;
                        }
                        tempLink = curdLink;
                        tempLinkNum = curLinkNum;
                    }
                    else if (curdLink == tempLink && (Math.Abs(curLinkNum - tempLinkNum)) == 1)
                    {
                        distance_1 = 5;
                        offset = 45;
                    }
                    else if (curdLink == tempLink && (Math.Abs(curLinkNum - tempLinkNum)) == 2)
                    {
                        distance_1 = 5;
                        offset = 20;
                    }
                    else if (curdLink != tempLink)
                    {
                        distance_1 = 20;
                        offset = 5;
                        tempLink = curdLink;
                        tempLinkNum = curLinkNum;
                    }
                }
                else
                {
                    if (curdLink == tempLink && curLinkNum == tempLinkNum)
                    {
                        distance_1 = 20;
                        offset = 5;
                        tempLink = curdLink;
                        tempLinkNum = curLinkNum;
                    }
                    else if (curdLink == tempLink && curLinkNum != tempLinkNum)
                    {
                        distance_1 = 5;
                        offset = 20;
                    }
                    else if (curdLink != tempLink)
                    {
                        distance_1 = 20;
                        offset = 5;
                        tempLink = curdLink;
                        tempLinkNum = curLinkNum;
                    }
                }
            }
            else//所在是正常区段
            {
                curNodeName = curBalise.Substring(0, curBalise.IndexOf("-")) + "G";
                foreach (string key in hashTable.ht_1.Keys)
                {
                    if (key == curBalise.Substring(curBalise.IndexOf("-") + 1))
                    {
                        if (Convert.ToInt32(curNodeName.Substring(0, 3)) % 2 == 0)
                        {
                            if (isLeftSearch == true)
                            {
                                if (curNodeName.Substring(0, 3) == "116")
                                {
                                    distance_1 = (int)hashTable.ht_1[key];
                                    offset = (120 - (int)hashTable.ht_1[key]);
                                }
                                else
                                {
                                    distance_1 = (120 - (int)hashTable.ht_1[key]);
                                    offset = 120 - distance_1;
                                }
                            }
                            else
                            {
                                if (curNodeName.Substring(0, 3) == "116")
                                {
                                    distance_1 = 120 - (int)hashTable.ht_1[key];
                                    offset = (int)hashTable.ht_1[key];
                                }
                                else
                                {
                                    distance_1 = (int)hashTable.ht_1[key];
                                    offset = 120 - distance_1;
                                }
                            }
                        }
                        else
                        {
                            if (isLeftSearch == true)
                            {
                                distance_1 = (int)hashTable.ht_1[key];
                                offset = 120 - distance_1;
                            }
                            else
                            {
                                distance_1 = (120 - (int)hashTable.ht_1[key]);
                                offset = 120 - distance_1;
                            }
                        }
                    }
                }
            }
            #endregion

            #region 判断当前区段是否是MA终点。若不是进行左右巡
            if (String.Compare(MAEndLink.Substring(0, 3), curBalise.Substring(0, 3)) == 0)
            {
                MAEndDistance = MAEndOff - offset;
                nextLimitSpeed = 0;
                retuenValue[0] = MAEndDistance;
                retuenValue[8] = nextLimitSpeed;
            }
            else
            {
                if (isLeftSearch == true)
                {
                    retuenValue = LeftSearch(curBalise, MAEndLink, MAEndOff, obstacleID, obstacleState, obstacleNum);
                }
                else
                {
                    retuenValue = RightSearch(curBalise, MAEndLink, MAEndOff, obstacleID, obstacleState, obstacleNum);
                }
            }
            return retuenValue;
        }
        #endregion

        #region 左寻
        public int[] LeftSearch(string curBalise, string MAEndLink, int MAEndOff, string[] obstacleID, byte[] obstacleState, int obstacleNum)
        {
            if (curNodeName.Length != 4)
            {
                Namelength = curNodeName.Length;
                firstSwitchName = curBalise.Substring(0, 3) + "DG";
                isFirstBalise_1 = true;
            }
            else
            {
                Namelength = 4;
            }

            while (true)
            {
                if (curNodeName.Length == 4)
                {
                    foreach (var p in find_2(curNodeName).LeftNodes)
                    {
                        nodeName[i] = p.NodeDevice.Name;
                        nodeID[i] = p.NodeDevice.ID;
                        if (p.NodeDevice.Name.Length != 4)
                        {
                            switchName[j] = (p.NodeDevice as RailSwitch).SectionName;
                        }
                        i++;
                        j++;
                    }
                }
                else
                {
                    foreach (var p in find_1(curNodeName, curNodeID).LeftNodes)
                    {
                        nodeName[i] = p.NodeDevice.Name;
                        nodeID[i] = p.NodeDevice.ID;
                        if (p.NodeDevice.Name.Length != 4)
                        {
                            switchName[j] = (p.NodeDevice as RailSwitch).SectionName;
                        }
                        i++;
                        j++;
                    }
                }

                BreakSearch(MAEndLink, MAEndOff, obstacleState, obstacleNum);
                if (isBreak)
                {
                    MAEndDistance = distance_1 + distance_2 + MAEndOff;
                    break;
                }

                if (Namelength != 4 && isFirstBalise_1)
                {
                    count++;
                    tempSwitchName_1 = firstSwitchName;
                    isFirstBalise_1 = false;
                }

                if (curNodeName.Length == 4)
                {
                    NormalSection(obstacleID, obstacleState);
                }
                else//当前区段是道岔
                {
                    if (obstacleID[count - 1] == tempSwitchName_1)
                    {
                        if (obstacleState[count - 1] == 1)//定位
                        {
                            SwitchInNormal(obstacleID, obstacleState);
                        }
                        else//反位
                        {
                            if (i == 1)
                            {
                                SwitchInReverse_1(obstacleID, obstacleState);
                            }
                            else
                            {
                                SwitchInReverse_2(obstacleID, obstacleState);
                            }
                        }
                    }
                }

                i = 0;
                j = 0;
                tempSwitchName_2 = null;
                nodeID = new int[2];
                nodeName = new string[2];
                switchName = new string[2];
                NextSectionSpeed(obstacleState);
            }

            int[] returnValue = new int[9];
            returnValue[0] = MAEndDistance;
            returnValue[1] = limSpeedNum;
            returnValue[2] = limSpeedDistance_1;
            returnValue[3] = limSpeedLength_1;
            returnValue[4] = limSpeedDistance_2;
            returnValue[5] = limSpeedLength_2;
            returnValue[6] = limSpeedDistance_3;
            returnValue[7] = limSpeedLength_3;
            returnValue[8] = nextLimitSpeed;
            return returnValue;

        }
        #endregion

        #region 右寻
        public int[] RightSearch(string curBalise, string MAEndLink, int MAEndOff, string[] obstacleID, byte[] obstacleState, int obstacleNum)
        {
            if (curNodeName.Length != 4)
            {
                Namelength = curNodeName.Length;
                firstSwitchName = curBalise.Substring(0, 3) + "DG";
                isFirstBalise_1 = true;
            }
            else
            {
                Namelength = 4;
            }

            while (true)
            {
                if (curNodeName.Length == 4)
                {
                    foreach (var p in find_2(curNodeName).RightNodes)
                    {
                        nodeName[i] = p.NodeDevice.Name;
                        nodeID[i] = p.NodeDevice.ID;
                        if (p.NodeDevice.Name.Length != 4)
                        {
                            switchName[j] = (p.NodeDevice as RailSwitch).SectionName;
                        }
                        i++;
                        j++;
                    }
                }
                else
                {
                    foreach (var p in find_1(curNodeName, curNodeID).RightNodes)
                    {
                        nodeName[i] = p.NodeDevice.Name;
                        nodeID[i] = p.NodeDevice.ID;
                        if (p.NodeDevice.Name.Length != 4)
                        {
                            switchName[j] = (p.NodeDevice as RailSwitch).SectionName;
                        }
                        i++;
                        j++;
                    }
                }

                BreakSearch(MAEndLink, MAEndOff, obstacleState, obstacleNum);
                if (isBreak)
                {
                    MAEndDistance = distance_1 + distance_2 + MAEndOff;
                    break;
                }


                if (Namelength != 4 && isFirstBalise_1)
                {
                    count++;
                    tempSwitchName_1 = firstSwitchName;
                    isFirstBalise_1 = false;
                }
                if (curNodeName.Length == 4)
                {
                    NormalSection(obstacleID, obstacleState);
                }
                else//当前区段是道岔
                {
                    if (obstacleID[count - 1] == tempSwitchName_1)
                    {
                        if (obstacleState[count - 1] == 1)//定位
                        {
                            SwitchInNormal(obstacleID, obstacleState);
                        }
                        else//反位
                        {
                            if (i == 1)
                            {
                                SwitchInReverse_1(obstacleID, obstacleState);
                            }
                            else
                            {
                                SwitchInReverse_2(obstacleID, obstacleState);
                            }
                        }
                    }
                }

                i = 0;
                j = 0;
                tempSwitchName_2 = null;
                nodeID = new int[2];
                nodeName = new string[2];
                switchName = new string[2];
                NextSectionSpeed(obstacleState);
            }

            int[] returnValue = new int[9];
            returnValue[0] = MAEndDistance;
            returnValue[1] = limSpeedNum;
            returnValue[2] = limSpeedDistance_1;
            returnValue[3] = limSpeedLength_1;
            returnValue[4] = limSpeedDistance_2;
            returnValue[5] = limSpeedLength_2;
            returnValue[6] = limSpeedDistance_3;
            returnValue[7] = limSpeedLength_3;
            returnValue[8] = nextLimitSpeed;
            return returnValue;
        }
        #endregion
        public void BreakSearch(string MAEndLink, int MAEndOff, byte[] obstacleState, int obstacleNum)
        {
            if (isSwitchMAEnd == true)
            {
                for (int n = 0; n < j; n++)
                {
                    if (String.Compare(switchName[n], MAEndLink) == 0)
                    {
                        if (firstReverseNum > 0)
                        {
                            ObstacleHandle();
                            MAEndIsNotReverse();
                        }
                        isBreak = true;
                    }
                }
            }
            else
            {
                if (String.Compare(nodeName[0], MAEndLink) == 0)
                {
                    if (curNodeName.Length != 4 && obstacleState[obstacleNum - 1] == 2)
                    {
                        limSpeedDistance[reverseNum] = distance_2 - 25;
                        limSpeedLength[reverseNum] = 25;
                        reverseNum++;
                    }
                    if (firstReverseNum > 0)
                    {
                        ObstacleHandle();
                        MAEndIsNotReverse();
                    }
                    isBreak = true;
                }
            }
        }

        public void NormalSection(string[] obstacleID, byte[] obstacleState)
        {
            curNodeName = nodeName[0];
            curNodeID = nodeID[0];
            if (curNodeName.Length != 4)
            {
                count++;
                tempSwitchName_1 = switchName[0];
                if (obstacleID[count - 1] == tempSwitchName_1)
                {
                    if (obstacleState[count - 1] == 1)
                    {
                        if (tempSwitchName_1 == "110DG" || tempSwitchName_1 == "111DG" || tempSwitchName_1 == "118DG" || tempSwitchName_1 == "119DG")
                        {
                            distance_2 = distance_2 + 50;
                        }
                        else
                        {
                            distance_2 = distance_2 + 25;
                        }
                    }
                    else
                    {
                        distance_2 = distance_2 + 25;
                    }
                }
            }
            else
            {
                distance_2 = distance_2 + 120;
            }
        }

        public void SwitchInNormal(string[] obstacleID, byte[] obstacleState)
        {
            curNodeName = nodeName[0];
            curNodeID = nodeID[0];
            if (curNodeName.Length != 4)
            {
                tempSwitchName_2 = switchName[0];
                if (String.Compare(tempSwitchName_1, tempSwitchName_2) != 0)
                {
                    count++;
                    tempSwitchName_1 = tempSwitchName_2;
                    if (obstacleID[count - 1] == tempSwitchName_1)
                    {
                        if (obstacleState[count - 1] == 1)
                        {
                            if (tempSwitchName_1 == "110DG" || tempSwitchName_1 == "111DG" || tempSwitchName_1 == "118DG" || tempSwitchName_1 == "119DG")
                            {
                                distance_2 = distance_2 + 50;
                            }
                            else
                            {
                                distance_2 = distance_2 + 25;
                            }
                        }
                        else
                        {
                            distance_2 = distance_2 + 25;
                        }
                    }
                }
            }
            else
            {
                distance_2 = distance_2 + 120;
            }
        }

        public void SwitchInReverse_1(string[] obstacleID, byte[] obstacleState)
        {
            limSpeedDistance[reverseNum] = distance_2 - 25;
            limSpeedLength[reverseNum] = 25;
            reverseNum++;
            curNodeName = nodeName[0];
            curNodeID = nodeID[0];

            if (curNodeName.Length != 4)
            {
                count++;
                tempSwitchName_1 = switchName[0];
                if (obstacleID[count - 1] == tempSwitchName_1)
                {
                    if (obstacleState[count - 1] == 1)
                    {
                        if (tempSwitchName_1 == "110DG" || tempSwitchName_1 == "111DG" || tempSwitchName_1 == "118DG" || tempSwitchName_1 == "119DG")
                        {
                            distance_2 = distance_2 + 50;
                        }
                        else
                        {
                            distance_2 = distance_2 + 25;
                        }
                    }
                    else
                    {
                        distance_2 = distance_2 + 25;
                    }
                }
            }
            else
            {
                distance_2 = distance_2 + 120;

            }
        }

        public void SwitchInReverse_2(string[] obstacleID, byte[] obstacleState)
        {
            limSpeedDistance[reverseNum] = distance_2 - 25;
            limSpeedLength[reverseNum] = 25;
            reverseNum++;
            curNodeName = nodeName[1];
            curNodeID = nodeID[1];

            switchName[0] = switchName[1];

            if (curNodeName.Length != 4)
            {
                count++;
                tempSwitchName_1 = switchName[0];
                if (obstacleID[count - 1] == tempSwitchName_1)
                {
                    if (obstacleState[count - 1] == 1)
                    {
                        if (tempSwitchName_1 == "110DG" || tempSwitchName_1 == "111DG" || tempSwitchName_1 == "118DG" || tempSwitchName_1 == "119DG")
                        {
                            distance_2 = distance_2 + 50;
                        }
                        else
                        {
                            distance_2 = distance_2 + 25;
                        }
                    }
                    else
                    {
                        distance_2 = distance_2 + 25;
                    }
                }
            }
            else
            {
                distance_2 = distance_2 + 120;
            }
        }

        public void NextSectionSpeed(byte[] obstacleState)
        {
            if (isNextLimitSpeed)
            {
                if (Namelength == 4)
                {
                    if (curNodeName.Length != 4)
                    {
                        if (obstacleState[0] == 2)
                        {
                            nextLimitSpeed = 40;
                        }
                        else
                        {
                            nextLimitSpeed = 70;
                        }
                    }
                    else
                    {
                        nextLimitSpeed = 70;
                    }
                }
                else
                {
                    if (curNodeName.Length != 4)
                    {
                        if (String.Compare(firstSwitchName, tempSwitchName_1) != 0)
                        {
                            if (obstacleState[1] == 2)
                            {
                                nextLimitSpeed = 40;
                            }
                            else
                            {
                                nextLimitSpeed = 70;
                            }
                        }
                        else
                        {
                            nextLimitSpeed = 70;
                        }
                    }
                    else
                    {
                        nextLimitSpeed = 70;
                    }
                }
                isNextLimitSpeed = false;
            }
        }

        public void ObstacleHandle()
        {
            if (reverseNum == 1)
            {
                limSpeedDistance_1 = limSpeedDistance[0];
                limSpeedLength_1 = limSpeedLength[0];
                limSpeedNum = 1;
            }
            else if (reverseNum == 2)
            {
                if (limSpeedDistance[0] + 25 == limSpeedDistance[1])
                {
                    limSpeedDistance_1 = limSpeedDistance[0];
                    limSpeedLength_1 = limSpeedLength[0] + limSpeedLength[1];
                    limSpeedNum = 1;
                }
                else
                {
                    limSpeedDistance_1 = limSpeedDistance[0];
                    limSpeedLength_1 = limSpeedLength[0];
                    limSpeedDistance_2 = limSpeedDistance[1];
                    limSpeedLength_2 = limSpeedLength[1];
                    limSpeedNum = 2;
                }
            }
            else if (reverseNum == 3)
            {
                if (limSpeedDistance[0] + 25 == limSpeedDistance[1])
                {
                    if (limSpeedDistance[1] + 25 == limSpeedDistance[2])
                    {
                        limSpeedDistance_1 = limSpeedDistance[0];
                        limSpeedLength_1 = limSpeedLength[0] + limSpeedLength[1] + limSpeedLength[2];
                        limSpeedNum = 1;
                    }
                    else
                    {
                        limSpeedDistance_1 = limSpeedDistance[0];
                        limSpeedLength_1 = limSpeedLength[0] + limSpeedLength[1];
                        limSpeedDistance_2 = limSpeedDistance[2];
                        limSpeedLength_2 = limSpeedLength[2];
                        limSpeedNum = 2;
                    }
                }
                else
                {
                    limSpeedDistance_1 = limSpeedDistance[0];
                    limSpeedLength_1 = limSpeedLength[0];
                    limSpeedDistance_2 = limSpeedDistance[1];
                    limSpeedLength_2 = limSpeedLength[1] + limSpeedLength[2];
                    limSpeedNum = 2;
                }
            }
            else if (reverseNum == 4)
            {
                limSpeedDistance_1 = limSpeedDistance[0];
                limSpeedLength_1 = limSpeedLength[0] + limSpeedLength[1];
                limSpeedDistance_2 = limSpeedDistance[2];
                limSpeedLength_2 = limSpeedLength[2] + limSpeedLength[3];
                limSpeedNum = 2;
            }
            else if (reverseNum == 5)
            {
                if (limSpeedDistance[0] + 25 != limSpeedDistance[1])
                {
                    limSpeedDistance_1 = limSpeedDistance[0];
                    limSpeedLength_1 = limSpeedLength[0];
                    limSpeedDistance_2 = limSpeedDistance[1];
                    limSpeedLength_2 = limSpeedLength[1] + limSpeedLength[2];
                    limSpeedDistance_3 = limSpeedDistance[3];
                    limSpeedLength_3 = limSpeedLength[3] + limSpeedLength[4];
                    limSpeedNum = 3;
                }
                else if (limSpeedDistance[0] + 25 == limSpeedDistance[1])
                {
                    if (limSpeedDistance[1] + 25 == limSpeedDistance[2])
                    {
                        limSpeedDistance_1 = limSpeedDistance[0];
                        limSpeedLength_1 = limSpeedLength[0] + limSpeedLength[1] + limSpeedLength[2];
                        limSpeedDistance_2 = limSpeedDistance[3];
                        limSpeedLength_2 = limSpeedLength[3] + limSpeedLength[4];
                    }
                    else
                    {
                        limSpeedDistance_1 = limSpeedDistance[0];
                        limSpeedLength_1 = limSpeedLength[0] + limSpeedLength[1];
                        limSpeedDistance_2 = limSpeedDistance[2];
                        limSpeedLength_2 = limSpeedLength[2] + limSpeedLength[3] + limSpeedLength[4];
                    }
                    limSpeedNum = 2;
                }
            }
            else if (reverseNum == 6)
            {
                limSpeedDistance_1 = limSpeedDistance[0];
                limSpeedLength_1 = limSpeedLength[0] + limSpeedLength[1];
                limSpeedDistance_2 = limSpeedDistance[2];
                limSpeedLength_2 = limSpeedLength[2] + limSpeedLength[3];
                limSpeedDistance_3 = limSpeedDistance[4];
                limSpeedLength_3 = limSpeedLength[4] + limSpeedLength[5];
                limSpeedNum = 3;
            }
            else if (reverseNum == 7)
            {
                if (limSpeedDistance[0] + 25 == limSpeedDistance[1] && limSpeedDistance[1] + 25 == limSpeedDistance[2])
                {
                    limSpeedDistance_1 = limSpeedDistance[0];
                    limSpeedLength_1 = limSpeedLength[0] + limSpeedLength[1] + limSpeedLength[2];
                    limSpeedDistance_2 = limSpeedDistance[3];
                    limSpeedLength_2 = limSpeedLength[3] + limSpeedLength[4];
                    limSpeedDistance_3 = limSpeedDistance[5];
                    limSpeedLength_3 = limSpeedLength[5] + limSpeedLength[6];

                }
                else if (limSpeedDistance[4] + 25 == limSpeedDistance[5] && limSpeedDistance[5] + 25 == limSpeedDistance[6])
                {
                    limSpeedDistance_1 = limSpeedDistance[0] + limSpeedDistance[1];
                    limSpeedLength_1 = limSpeedLength[0] + limSpeedLength[1];
                    limSpeedDistance_2 = limSpeedDistance[2] + limSpeedDistance[3];
                    limSpeedLength_2 = limSpeedLength[2] + limSpeedLength[3];
                    limSpeedDistance_3 = limSpeedDistance[4] + limSpeedDistance[5] + limSpeedDistance[6];
                    limSpeedLength_3 = limSpeedLength[4] + limSpeedLength[5] + limSpeedLength[6];
                }
                limSpeedNum = 3;
            }
        }

        public void MAEndIsNotReverse()
        {
            if (Namelength != 4 && String.Compare(firstSwitchName, firstRverseObstacle) == 0)
            {
                if (limSpeedNum == 2)
                {
                    limSpeedDistance_2 += distance_1;
                }
                else if (limSpeedNum == 3)
                {
                    limSpeedDistance_2 += distance_1;
                    limSpeedDistance_3 += distance_1;
                }
                limSpeedDistance_1 = 0;
                limSpeedLength_1 = limSpeedLength_1 - offset;
            }
            else
            {
                if (limSpeedNum == 2)
                {
                    limSpeedDistance_2 += distance_1;
                }
                else if (limSpeedNum == 3)
                {
                    limSpeedDistance_2 += distance_1;
                    limSpeedDistance_3 += distance_1;
                }
                limSpeedDistance_1 += distance_1;
            }
        }

        public TopolotyNode find_1(string nodeDeviceName, int nodeDeviceID)
        {
            foreach (var item in ATP.stationTopoloty_.Nodes)
            {
                if (item.NodeDevice.Name == nodeDeviceName && item.NodeDevice.ID == nodeDeviceID)
                {
                    return item;
                }
            }
            foreach (var item in ATP.stationTopoloty_1_.Nodes)
            {
                if (item.NodeDevice.Name == nodeDeviceName && item.NodeDevice.ID == nodeDeviceID)
                {
                    return item;
                }
            }
            return null;
        }

        public TopolotyNode find_2(string nodeDeviceName)
        {
            foreach (var item in ATP.stationTopoloty_.Nodes)
            {
                if (item.NodeDevice.Name == nodeDeviceName)
                {
                    return item;
                }
            }
            foreach (var item in ATP.stationTopoloty_1_.Nodes)
            {
                if (item.NodeDevice.Name == nodeDeviceName)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
