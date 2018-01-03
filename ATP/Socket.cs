using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Timers;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing;

namespace CBTC
{
    class Socket
    {
        public IPEndPoint ipLocalPoint;
        public EndPoint RemotePoint;
        public System.Net.Sockets.Socket socket;
        public bool runningFlag = false;
        public int localPort = 0;
        public int sendPort = 0;
        public byte[] recv = new byte[1024];
        public string curBalise = "";
        public string number_1 = "";
        public string number_2 = "";
        public int obstacleNum = 0;
        public byte[] obstacleType = new byte[10];
        public string[] obstacleID = new string[10];
        public byte[] obstacleState = new byte[10];
        public byte[] obstacleLogicState = new byte[10];
        public byte[] sendBuf_ = new byte[1024];
        public byte runInfoType = 0x01;
        public byte MAEndType = 0;
        public UInt16 MAEndLink = 0;
        public UInt32 MAEndOff = 0;
        public UInt16 ZCD_MAHeadLink = 0;
        public int MAEndDir = 0;
        public int curNodeName = 0;
        public Int16 DCTrainSpeed = 0;
        public static UInt16 DCCtrlMode = 0;
        public static UInt16 DCHandlePos = 0;
        public byte DMIRelieveOrder = 0;
        public static string baliseHead = "";
        public static string baliseTail = "";
        public static int MAEndDistance = 0;
        public static int limSpeedNum = 0;
        public static int limSpeedDistance_1 = 0;
        public static int limSpeedLength_1 = 0;
        public static int limSpeedDistance_2 = 0;
        public static int limSpeedLength_2 = 0;
        public static int limSpeedDistance_3 = 0;
        public static int limSpeedLength_3 = 0;
        public static int limitSpeed = 0;
        public string trainHead = "";
        public string trainTail = "";
        public bool isDCFirst = false;
        public bool isBaliseFirst = false;
        bool isLeftSearch = false;
        public bool isRecvZC = false;
        bool isFirst = true;
        public bool isReleaseEB = false;
        public bool isEB = false;
        public bool isSendToZC = false;
        UInt16 Link = 0;
        UInt16 SecLink = 0;
        UInt16 Off = 0;
        public UInt16 headLink = 0;
        public UInt16 headSecLink = 0;
        public UInt16 headOff = 0;
        public UInt16 tailLink = 0;
        public UInt16 tailSecLink = 0;
        public UInt16 tailOff = 0;
        public byte ZCInfoType = 0;
        public byte actualDirection = 0;
        public UInt16 ATPPermitDirection = 0;
        ZCPackage ZCPackage_;
        SearchDistances Search;
        HashTable hashTable;
        UInt16 tempLink = 0;
        int tempHeadLinkNum = 0;
        Thread thread;
        MyStruct ZCStruct = new MyStruct();
        MyStruct DMIStruct = new MyStruct();
        MyStruct DCStruct = new MyStruct();
        MyStruct BaliseStruct_ = new MyStruct();
        bool isReentry = true;
        bool isBreak = true;
        public bool isFirstEnter = true;
        public byte curModel = 3;
        DateTime recTime = DateTime.Now;
        DateTime firstTime = DateTime.Now;
        public bool isAuthority = true;
        public byte[] SendBuf { get { return sendBuf_; } }
        public bool isPrintATP = false;
        public bool isUnRegister = false;
        public string State = "";
        public string ID = "";
        public string time = "";
        public bool _IsEB = false;
        public Int16 _DCSpeed = 0;
        public UInt16 _DCModel = 0;
        public UInt16 _DCHandle = 0;
        public string _Balise = "";
        public UInt16 _MAEndLink = 0;
        public int _ProtectSpeed = 0;
        public int _Num = 0;
        public string _State = "";
        public string _ID = "";
        public UInt16 _ATPDirection = 0;
        public bool isSaveLog = false;
        public bool isPrintConsole = false;
        string logPath = Application.StartupPath + @"\log\" + DateTime.Today.ToString("yyyyMMdd") + ".txt";
        string[] DrivingMode = { "AM", "CM", "RM", "EUM" };

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool AllocConsole();

        // 释放控制台  
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool FreeConsole();

        private static ConsoleColor GetConsoleColor(string time, bool isEB, Int16 DCSpeed, UInt16 DCModel, UInt16 DCHandle, string Balise, int MAEndLink, int ProtectSpeed, int Num, string ID, string State, UInt16 ATPDirection)
        {
            if (isEB == true)
                return ConsoleColor.Red;
            else
                return ConsoleColor.Green;
        }

        public void Start(string ip, int port)
        {
            ZCPackage_ = new ZCPackage();
            Search = new SearchDistances();
            hashTable = new HashTable();
            localPort = port;
            ipLocalPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            RemotePoint = ipLocalPoint;
            socket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            try
            {
                socket.Bind(ipLocalPoint);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            runningFlag = true;
            thread = new Thread(new ThreadStart(this.ReceiveHandle));
            thread.IsBackground = true;
            thread.Start();
            hashTable.sectionHashTable();
            hashTable.switchHashTable();
        }
        public void Send(int packageSize, string dIP, int dPort)
        {
            sendPort = dPort;
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(dIP), dPort);
            RemotePoint = (EndPoint)(ipep);
            socket.SendTo(sendBuf_, packageSize, SocketFlags.None, RemotePoint);
        }
        public void ReceiveHandle()
        {
            while (runningFlag)
            {
                try
                {
                    int length = socket.ReceiveFrom(recv, ref RemotePoint);
                    if (length > 0)
                    {
                        ReceiveData(recv);
                        Array.Clear(recv, 0, 1024);
                    }
                }
                catch (Exception e)
                {
                }
            }
        }

        public void ReceiveData(byte[] data)
        {
            if (recv[2] == 9)//ZC发来的消息
            {
                ZCStruct.PackedSize = 0;
                UInt16 ZCCycle = ZCStruct.UnpackUint16(recv);
                UInt16 ZCPackageType = ZCStruct.UnpackUint16(recv);
                byte ZCSendID = ZCStruct.UnpackByte(recv);
                byte ZCReceiveID = ZCStruct.UnpackByte(recv);
                UInt16 ZCDataLength = ZCStruct.UnpackUint16(recv);
                UInt16 ZCNID_ZC = ZCStruct.UnpackUint16(recv);
                UInt16 ZCNID_Train = ZCStruct.UnpackUint16(recv);
                ZCInfoType = ZCStruct.UnpackByte(recv);
                byte ZCStopEnsure = ZCStruct.UnpackByte(recv);
                UInt64 ZCNID_DataBase = ZCStruct.UnpackUint64(recv);
                UInt16 ZCNID_ARButton = ZCStruct.UnpackUint16(recv);
                byte ZCQ_ARButtonStatus = ZCStruct.UnpackByte(recv);
                UInt16 ZCNID_LoginZCNext = ZCStruct.UnpackUint16(recv);
                byte ZCN_Length = ZCStruct.UnpackByte(recv);
                MAEndType = ZCStruct.UnpackByte(recv);
                ZCD_MAHeadLink = ZCStruct.UnpackUint16(recv);
                UInt32 ZCD_D_MAHeadOff = ZCStruct.UnpackUint32(recv);
                byte ZCQ_MAHeadDir = ZCStruct.UnpackByte(recv);
                MAEndLink = ZCStruct.UnpackUint16(recv);
                MAEndOff = ZCStruct.UnpackUint32(recv);
                MAEndDir = ZCStruct.UnpackByte(recv);
                obstacleNum = ZCStruct.UnpackByte(recv);
                if (obstacleNum != 0)
                {
                    State = "";
                    ID = "";
                    obstacleType = new byte[obstacleNum];
                    obstacleID = new string[obstacleNum];
                    obstacleState = new byte[obstacleNum];
                    obstacleLogicState = new byte[obstacleNum];
                    for (int i = 0; i < obstacleNum; i++)
                    {
                        obstacleType[i] = ZCStruct.UnpackByte(recv);
                        obstacleID[i] = (ZCStruct.UnpackUint16(recv)).ToString();
                        obstacleState[i] = ZCStruct.UnpackByte(recv);
                        obstacleLogicState[i] = ZCStruct.UnpackByte(recv);
                        State = State + obstacleState[i] + " ";
                        ID = ID + obstacleID[i] + " ";
                    }
                }
                byte ZCN_TSR = ZCStruct.UnpackByte(recv);
                UInt32 ZCQ_ZC = ZCStruct.UnpackUint32(recv);
                byte ZCEB_Type = ZCStruct.UnpackByte(recv);
                byte ZCEB_DEV_Typ = ZCStruct.UnpackByte(recv);
                UInt16 ZCEB_DEV_Name = ZCStruct.UnpackUint16(recv);
                if (curModel != 4)
                {
                    isRecvZC = true;
                    recTime = DateTime.Now;
                }
            }
            else if (recv[2] == 4)//DMI传来的数据
            {
                DMIStruct.PackedSize = 0;
                UInt16 DMICycle = DMIStruct.UnpackUint16(recv);
                UInt16 DMIPackageType = DMIStruct.UnpackUint16(recv);
                UInt16 DMILength = DMIStruct.UnpackUint16(recv);
                string DMITrainOrder = DMIStruct.UnPackString(recv);
                UInt32 DMITrainNumber = DMIStruct.UnpackDMIUint32(recv);
                UInt16 DMIDriverNumber = DMIStruct.UnpackUint16(recv);
                byte DMITestOrder = DMIStruct.UnpackByte(recv);
                DMIRelieveOrder = DMIStruct.UnpackByte(recv);
            }
            else if (recv[2] == 6)//司控器传来的数据
            {
                DCStruct.PackedSize = 0;
                UInt16 DCCycle = DCStruct.UnpackUint16(recv);
                UInt16 DCPackageType = DCStruct.UnpackUint16(recv);
                UInt16 DCLength = DCStruct.UnpackUint16(recv);
                DCTrainSpeed = (Int16)DCStruct.UnpackUint16(recv);
                DCCtrlMode = DCStruct.UnpackUint16(recv);
                DCHandlePos = DCStruct.UnpackUint16(recv);
                UInt16 DCisKeyIn = DCStruct.UnpackByte(recv);
                if (DCTrainSpeed != 0)
                {
                    isDCFirst = true;
                }
            }
            else if (recv[2] == 7)//应答器传来的数据
            {
                BaliseStruct_.PackedSize = 0;
                UInt16 baliseCycle = BaliseStruct_.UnpackUint16(recv);
                UInt16 balisePackageType = BaliseStruct_.UnpackUint16(recv);
                string Head = BaliseStruct_.UnPackString(recv);
                string Tail = BaliseStruct_.UnPackTailString(recv);
                baliseHead = Regex.Replace(Head, "[D,G]", "", RegexOptions.IgnoreCase);
                baliseTail = Regex.Replace(Tail, "[D,G]", "", RegexOptions.IgnoreCase);

                if (Regex.Matches(baliseHead, "[a-zA-Z]").Count > 0 || baliseHead.Contains("/") || baliseHead.Length < 5)
                {
                    isBaliseFirst = false;//过滤掉正线之前的应答器
                }
                else
                {
                    isBaliseFirst = true;
                }


                if (MAEndLink == 501 || MAEndLink == 502)
                {
                    if(Regex.Matches(baliseHead, "[X]").Count > 0)
                    {
                        isUnRegister = true;                                           
                    }
                }

            }
            

            //判断ZC是否在规定时间内发来移动授权
            if (isRecvZC == true)
            {
                if (recTime.AddSeconds(1) < DateTime.Now)
                {
                    isAuthority = false;
                }
                else
                {
                    isAuthority = true;
                }
                if (MAEndLink == 0)
                {
                    isEB = true;
                }
            }

            //选择控车模式
            if (Regex.Matches(baliseHead, "[a-zA-Z]").Count > 0 || baliseHead.Contains("/") || baliseHead.Length < 5)
            {
                curModel = 3;//正线之前RM
            }
            else
            {
                if(DCCtrlMode == 0)
                {
                    curModel = 1; //AM                 
                }
                else if(DCCtrlMode == 1)
                {
                    curModel = 2; //CM               
                }
                else if(DCCtrlMode == 2)
                {
                    curModel = 3; //RM                 
                }
                else
                {
                    curModel = 4; //EUM
                }
            }            

            //刷到应答器且始计算MA
            if (isBaliseFirst && isDCFirst)
            {
                curBalise = baliseHead;
                trainHead = baliseHead;
                trainTail = baliseTail;
                ReceiceDataHandling();
            }

            if (isPrintConsole == true)
            {
                //控制台输出
                if (_IsEB != isEB || _DCSpeed != DCTrainSpeed || _DCModel != DCCtrlMode || _DCHandle != DCHandlePos || _Balise != curBalise || _MAEndLink != MAEndLink || _ProtectSpeed != ATP.curProtectionSpeed || _Num != obstacleNum || _ID != ID || _State != State || _ATPDirection != ATPPermitDirection)
                {
                    AllocConsole();
                    Console.ForegroundColor = GetConsoleColor(time, isEB, DCTrainSpeed, DCCtrlMode, DCHandlePos, curBalise, MAEndLink, _ProtectSpeed, obstacleNum, ID, State, ATPPermitDirection);
                    Console.WriteLine("[{0}] 是否EB:{1}  司控器速度:{2}  司控器行车模式:{3}  司控器手柄工况:{4}  应答器:{5}  MA终点区段:{6}  防护速度:{7}  道岔数量:{8}  道岔区段:{9}  道岔状态:{10}  ATP允许方向:{11}", DateTime.Now, isEB, DCTrainSpeed, DrivingMode[DCCtrlMode], DCHandlePos, curBalise, MAEndLink, ATP.curProtectionSpeed, obstacleNum, ID, State, ATPPermitDirection);
                    _IsEB = isEB;
                    _DCSpeed = DCTrainSpeed;
                    _DCModel = DCCtrlMode;
                    _DCHandle = DCHandlePos;
                    _Balise = curBalise;
                    _MAEndLink = MAEndLink;
                    _ProtectSpeed = ATP.curProtectionSpeed;
                    _Num = obstacleNum;
                    _ID = ID;
                    _State = State;
                    _ATPDirection = ATPPermitDirection;
                    if (isSaveLog)
                    {
                        using (StreamWriter sw = File.AppendText(logPath))
                        {
                            sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " 是否EB:" + isEB + " 司控器速度:" + DCTrainSpeed + " 司控器行车模式:" + DrivingMode[DCCtrlMode] + " 司控器手柄工况:" + DCHandlePos + " 应答器:" + curBalise + " MA终点区段:" + MAEndLink + " 防护速度:" + ATP.curProtectionSpeed + " 道岔数量:" + obstacleNum + " 道岔区段:" + ID + " 道岔状态:" + State + " ATP允许方向:" + ATPPermitDirection);
                        };
                    }
                }
            }

        }

        public void ReceiceDataHandling()
        {
            //列车初始化确定方向和寻路
            if (isFirstEnter)
            {
                if (DCTrainSpeed > 0)//1端有数据
                {
                    isLeftSearch = false;
                    ATPPermitDirection = 1;
                }
                else if(DCTrainSpeed < 0)//2端有数据
                {
                    isLeftSearch = true;
                    ATPPermitDirection = 2;
                }

                isFirstEnter = false;
                isSendToZC = true;
            }

            //在行驶过程中判断方向，若方向不对则EB
            if(curModel!=4)
            {
                if (ATPPermitDirection != 0 && DCTrainSpeed!=0)
                {
                    if (DCTrainSpeed < 0 && DCHandlePos == 2)
                    {
                        ATPPermitDirection = 2;
                        actualDirection = 0xAA;
                        isLeftSearch = true;
                    }
                    else if (DCTrainSpeed < 0 && DCHandlePos ==1 )
                    {
                        isEB = true;
                    }
                    else if (DCTrainSpeed > 0 && DCHandlePos == 1)
                    {
                        ATPPermitDirection = 1;
                        actualDirection = 0x55;
                        isLeftSearch = false;
                    }
                    else
                    {
                        isEB = true;
                    }
                }
                if (ATPPermitDirection == 0)
                {
                    if (DCTrainSpeed > 0 && DCHandlePos == 1)
                    {
                        ATPPermitDirection = 1;
                        actualDirection = 0x55;
                        isLeftSearch = false;
                    }
                    else if (DCTrainSpeed > 0 && DCHandlePos == 2)
                    {
                        isEB = true;
                    }
                    else if (DCTrainSpeed < 0 && DCHandlePos == 1)
                    {
                        isEB = true;
                    }
                    else
                    {
                        ATPPermitDirection = 2;
                        actualDirection = 0xAA;
                        isLeftSearch = true;
                    }
                }
            }
            else
            {
                ATPPermitDirection = 0;
                actualDirection = 0;
            }
            
            CaculateHeadorTailOffandSection();

            //ZC通信后且VOBC在申请MA时，开始处理ATP曲线需要的数据
            if (isRecvZC && runInfoType == 0x01 && isEB == false && curModel != 4)
            {
                isReleaseEB = false;
                if(ZCD_MAHeadLink.ToString()==curBalise.Substring(0,3))
                {
                    int[] value = Search.SearchDistance(isLeftSearch, MAEndLink.ToString(), Convert.ToInt32(MAEndOff), obstacleNum, curBalise, obstacleID, obstacleState);
                    MAEndDistance = value[0];
                    limSpeedNum = value[1];
                    limSpeedDistance_1 = value[2];
                    limSpeedLength_1 = value[3];
                    limSpeedDistance_2 = value[4];
                    limSpeedLength_2 = value[5];
                    limSpeedDistance_3 = value[6];
                    limSpeedLength_3 = value[7];
                    limitSpeed = value[8];
                }
                for (int i = 0; i < obstacleNum; i++)
                {
                    obstacleID[i] = obstacleID[i].Substring(0, 3);
                }
                if (MAEndDistance > 0)
                {
                    isPrintATP = true;
                }                

            }

            //列车正常停车后的处理
            if (MAEndLink.ToString() == trainTail.Substring(0,3) && isSendToZC == true && isEB == false)
            {
                if ((MAEndType == 0x01 || ZCInfoType == 0x05) && isBreak)
                {
                    isBreak = false;
                    SetupTimerBreak();
                }
                else if (MAEndType == 0x02 && ZCInfoType != 0x04)
                {
                    runInfoType = 0x04;
                }
                else if (MAEndType == 0x02 && ZCInfoType == 0x04 && isReentry)
                {
                    isReentry = false;
                    SetupTimerChange();
                }
            }

            //紧急制动在DMI缓解后标志位初始化
            if (DMIRelieveOrder == 2)
            {
                isFirstEnter = true;
                isEB = false;
                isReleaseEB = true;
                isBreak = true;
                isReentry = true;
            }

            if (isReentry == false || isBreak == false)
            {
                if (MAEndLink.ToString() != trainTail.Substring(0, 3))
                {
                    isBreak = true;
                    isReentry = true;
                }
            }

        }

        //停车重新启动5s
        System.Timers.Timer timerBreak = new System.Timers.Timer(5000);
        private void SetupTimerBreak()
        {
            timerBreak.Elapsed += TimerBreak_Elapsed;
            timerBreak.AutoReset = false;
            timerBreak.Start();
        }
        private void TimerBreak_Elapsed(object sender, ElapsedEventArgs e)
        {
            runInfoType = 0x01;
            timerBreak.Stop();
        }

        //折返换端5s
        System.Timers.Timer timerChange = new System.Timers.Timer(5000);
        private void SetupTimerChange()
        {
            timerChange.Elapsed += TimerChange_Elapsed;
            timerChange.AutoReset = false;
            timerChange.Start();
        }
        private void TimerChange_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (MAEndType == 0x02)
            {
                if (isLeftSearch == true)
                {
                    isLeftSearch = false;
                    actualDirection = 0x55;
                    ATPPermitDirection = 1;
                }
                else
                {
                    isLeftSearch = true;
                    actualDirection = 0xAA;
                    ATPPermitDirection = 2;
                }
                runInfoType = 0x05;
            }
            timerChange.Stop();
        }

        //计算车头车尾区段号，偏移量
        public void CaculateHeadorTailOffandSection()
        {
            if (trainHead.Length >= 5)
            {
                UInt16[] value = SectionAndOff(trainHead);
                headLink = value[0];
                headSecLink = value[1];
                headOff = value[2];
            }
            else
            {
                headLink = 0;
                headSecLink = 0;
                headOff = 0;
            }

            if (trainTail.Length >= 5 && Regex.Matches(trainTail, "[a-zA-Z]").Count == 0 && trainTail.Contains("/")==false)
            {
                UInt16[] value = SectionAndOff(trainTail);
                tailLink = value[0];
                tailSecLink = value[1];
                tailOff = value[2];
            }
            else
            {
                tailLink = 0;
                tailSecLink = 0;
                tailOff = 0;
            }
        }

        public UInt16[] SectionAndOff(string balise)
        {
            if (hashTable.ht.Contains(balise) == false)//不是道岔
            {
                isFirst = true;
                Link = Convert.ToUInt16(balise.Substring(0, 3));
                SecLink = 0;
                foreach (string key in hashTable.ht_1.Keys)
                {
                    if (key == balise.Substring(balise.IndexOf("-") + 1))
                    {
                        if (Link % 2 == 0)
                        {
                            if (isLeftSearch == true)
                            {
                                Off = Convert.ToUInt16(hashTable.ht_1[key]);
                            }
                            else
                            {
                                Off = Convert.ToUInt16(120 - (int)hashTable.ht_1[key]);
                            }
                        }
                        else
                        {
                            if (isLeftSearch == true)
                            {
                                Off = Convert.ToUInt16(120 - (int)hashTable.ht_1[key]);
                            }
                            else
                            {
                                Off = Convert.ToUInt16(hashTable.ht_1[key]);
                            }
                        }
                    }
                }
            }
            else//是道岔
            {
                Link = Convert.ToUInt16(balise.Substring(0, 3));
                int LinkNum = Convert.ToInt32(balise.Substring(balise.IndexOf("-") + 1));
                foreach (string key in hashTable.ht.Keys)
                {
                    if (key == balise)
                    {
                        string secLink = (string)hashTable.ht[key];
                        int num = secLink.IndexOf("-");
                        SecLink = Convert.ToUInt16(secLink.Substring(0, num));
                    }
                }
                if (isFirst == true)
                {
                    tempLink = Link;
                    tempHeadLinkNum = LinkNum;
                    isFirst = false;
                }
                if (Link == 110 || Link == 111 || Link == 118 || Link == 119)
                {
                    if (Link == tempLink && LinkNum == tempHeadLinkNum)
                    {
                        Off = 5;
                        tempLink = Link;
                        tempHeadLinkNum = LinkNum;
                    }
                    else if (Link == tempLink && (Math.Abs(LinkNum - tempHeadLinkNum)) == 1)
                    {
                        Off = 45;
                    }
                    else if (Link == tempLink && (Math.Abs(LinkNum - tempHeadLinkNum)) == 2)
                    {
                        Off = 20;
                    }
                    else if (Link != tempLink)
                    {
                        Off = 5;
                        tempLink = Link;
                        tempHeadLinkNum = LinkNum;
                    }
                }
                else
                {
                    if (Link == tempLink && LinkNum == tempHeadLinkNum)
                    {
                        Off = 5;
                        tempLink = Link;
                        tempHeadLinkNum = LinkNum;
                    }
                    else if (Link == tempLink && LinkNum != tempHeadLinkNum)
                    {
                        Off = 20;
                    }
                    else if (Link != tempLink)
                    {
                        Off = 5;
                        tempLink = Link;
                        tempHeadLinkNum = LinkNum;
                    }
                }
            }
            UInt16[] returnValue = new UInt16[3];
            returnValue[0] = Link;
            returnValue[1] = SecLink;
            returnValue[2] = Off;
            return returnValue;
        }

        public void CloseThread()
        {
            thread.Abort();
        }
    }
}
