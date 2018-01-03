using System;
using System.Windows.Forms;
using System.Threading;
using System.Timers;
using System.Diagnostics;
using System.Drawing;

namespace CBTC
{
    public partial class ATP : Form
    {
        public static string ATPIP = "";
        public static string ATPPort = "";
        public static string desZCIP = "";
        public static string desZCPort = "";
        public static string desDMIIP = "";
        public static string desDMIPort = "";
        public static string desDCIP = "";
        public static string desDCPort = "";
        public static string sendID = "";
        public static string trainID = "";
        ConfigData configdata = new ConfigData();
        Socket socket = new Socket();
        DCPackage DCPackage_;
        DMIPackage DMIPackage_;
        ZCPackage ZCPackage_;
        public double[] x = new double[39];
        public double[] y = new double[39];
        public static StationElements stationElements_;
        public static StationTopoloty stationTopoloty_;
        public static StationElements stationElements_1_;
        public static StationTopoloty stationTopoloty_1_;
        public static StationElements stationElements_2_;
        public static StationTopoloty stationTopoloty_2_;
        bool isCancel = false;
        int limSpeedNum = 0;
        int limSpeedDistance_1 = 0;
        int limSpeedLength_1 = 0;
        int limSpeedDistance_2 = 0;
        int limSpeedLength_2 = 0;
        int limSpeedDistance_3 = 0;
        int limSpeedLength_3 = 0;
        int MAEndDistance = 0;
        int frontLimit = 0;
        int deDistance = (int)((80 / 3.6 * 80 / 3.6 - 40 / 3.6 * 40 / 3.6) / (2 * 1.2));//从80km/h降到40km/h距离，106
        int brDistance = (int)(80 / 3.6 * 80 / 3.6 / (2 * 1.2));//从80km/h降到0的距离，158
        double fourtyDistance = 40 / 3.6 * 40 / 3.6 / 2 / 1.2;//从40km/h降为0的距离，52
        int node_1 = (80 - 40) / 4;//用来产生ATP曲线的弧形
        int node_2 = 80 / 7;//用来产生ATP曲线的弧形
        int node_3 = 80 / 4;//用来产生ATP曲线的弧形        
        public static int curProtectionSpeed = 100;//防护曲线的实时速度
        double frontProtectionSpeed = 25;//下一个区段的防护速度
        double speed_1 = 0;//有一个限速区段时未到80km/h顶尖的速度
        double speed_2 = 0;//两个限速区段中间的速度，，过了第一个限速区段后不能够加速到80km/h，只能升一小段然后要下降到第二个限速区间
        double speed_3 = 0;//第二个限速区段和MA之间的速度，过了第二个限速区段后不能加速到80km/h，只能升一小段然后要下降
        double speed_4 = 0;//两个限速区段中间的速度，过了第一个限速区段后不能够加速到80km/h，只能升一小段然后要下降
        double speed_5 = 0;//两个限速区段中间的速度，过了第二个限速区段后不能够加速到80km/h，只能升一小段然后要下降到第三个限速区间
        double speed_6 = 0;//第三个限速区段和MA之间的中间速度，过了第三个限速区段后不能加速到80km/h，只能升一小段然后要下降
        int lockFlag = 1;//0锁定，1开锁   
        int RMPoint = 0;
        double inialProtectSpeed = 100;
        bool isBtnStart = true;
        bool isBtnCancle = true;
        bool isFirst = false;

        public ATP()
        {
            InitializeComponent();
            StationElements ele = StationElements.Open("StationElements.xml", null);
            LoadGraphicElements("StationElements.xml");
            LoadStationTopo("StationTopoloty.xml");
            LoadSecondStation();
            CheckForIllegalCrossThreadCalls = false;
            configdata.ReadConfigData();
            this.KeyPreview = true;
        }
        private void ATP_Load(object sender, EventArgs e)
        {
            DCPackage_ = new DCPackage() { PackageType = 5 };
            DMIPackage_ = new DMIPackage() { PackageType = 3, ActulSpeed = 25, TrainNum = "" };
            ZCPackage_ = new ZCPackage() { PackageType = 8, ReceiveID = 3, ZCID = 3 };
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (isBtnStart)
            {
                socket.Start(ATPIP, Convert.ToInt32(ATPPort));
                SetupTimer();
                timer1.Enabled = true;
                timer1.Start();
                btnStart.ForeColor = Color.Red;
                isBtnStart = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            isBtnCancle = false;
            btnCancel.ForeColor = Color.Red;
        }

        private void SetupTimer()
        {
            System.Timers.Timer timer = new System.Timers.Timer(200);
            timer.Elapsed += TimerElapsed;
            timer.Start();
        }
        int inTimer = 0;
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (Interlocked.Exchange(ref inTimer, 1) == 0)
            {
                DCPackage_.HighSpeed = (UInt16)curProtectionSpeed;
                DCPackage_.PermitSpeed = (UInt16)(curProtectionSpeed - 5);
                DCPackage_.InterSpeed = (UInt16)(curProtectionSpeed - 2);
                DCPackage_.Direction = socket.ATPPermitDirection;

                DMIPackage_.TrainID = 65536;
                DMIPackage_.TrainNum = "T0" + trainID;
                DMIPackage_.HighModel = 1;
                DMIPackage_.CurModel = socket.curModel;

                if (isCancel == true)
                {
                    DMIPackage_.ActulSpeed = 0;
                }
                else
                {
                    DMIPackage_.ActulSpeed = (UInt16)System.Math.Abs(socket.DCTrainSpeed);
                }

                ZCPackage_.SendID = Convert.ToByte(sendID);
                ZCPackage_.TrainID = Convert.ToByte(trainID);

                if (isBtnCancle == false || socket.isUnRegister == true)
                {
                    socket.isFirstEnter = true;
                    socket.isSendToZC = false;
                    socket.isDCFirst = false;
                    socket.isBaliseFirst = false;
                    ZCPackage_.RunInformation = 0x03;
                }
                else
                {
                    ZCPackage_.RunInformation = socket.runInfoType;
                }

                ZCPackage_.HeadLink = socket.headLink;
                ZCPackage_.HeadLinkSecond = socket.headSecLink;
                ZCPackage_.HeadOff = socket.headOff;
                ZCPackage_.TailLink = socket.tailLink;
                ZCPackage_.TailLinkSecond = socket.tailSecLink;
                ZCPackage_.TailOff = socket.tailOff;
                ZCPackage_.HeadActDirection = socket.actualDirection;

                if (socket.DMIRelieveOrder == 2)
                {
                    DCPackage_.IsEB = 7;
                    DMIPackage_.BreakOut = 7;
                    RMPoint = 0;
                    isFirst = true;
                    socket.isEB = false;
                }

                if (frontLimit == 40)
                {
                    DCPackage_.NextSpeed = (40 - 5);
                }

                if (curProtectionSpeed < (UInt16)System.Math.Abs(socket.DCTrainSpeed) && socket.curModel != 4)
                {
                    socket.isEB = true;
                    DCPackage_.IsEB = 6;
                    DMIPackage_.BreakOut = 6;
                    DMIPackage_.Alarm = 1;
                }

                if (socket.isEB == true || isCancel == true || socket.isReleaseEB == true)
                {
                    if (socket.curModel != 4 && isCancel == false && socket.isReleaseEB == false)
                    {
                        DCPackage_.IsEB = 6;
                        DMIPackage_.BreakOut = 6;
                        DMIPackage_.Alarm = 1;
                    }
                    DMIPackage_.HighSpeed = 0;
                    DMIPackage_.PermitSpeed = 0;
                    DMIPackage_.FrontPermSpeed = 0;
                    DMIPackage_.TargetLoca = 0;
                }
                else
                {
                    if (curProtectionSpeed - 5 < 0)
                    {
                        DMIPackage_.PermitSpeed = 0;
                        DMIPackage_.FrontPermSpeed = 0;
                    }
                    else
                    {
                        DMIPackage_.PermitSpeed = (UInt16)(curProtectionSpeed - 5);
                        DMIPackage_.FrontPermSpeed = (UInt16)(frontProtectionSpeed - 5);
                    }
                    DMIPackage_.HighSpeed = (UInt16)curProtectionSpeed;
                    DMIPackage_.TargetLoca = (UInt16)Socket.MAEndDistance;
                }


                if (isBtnCancle == false && socket.ZCInfoType == 0x03)
                {
                    isCancel = true;
                    socket.isSendToZC = false;
                }
                if (socket.curModel == 4)
                {
                    DMIPackage_.HighSpeed = 0;
                    DMIPackage_.PermitSpeed = 0;
                    DMIPackage_.FrontPermSpeed = 0;
                }
                int DCPackageSize = DCPackage_.Pack(socket.SendBuf);
                socket.Send(DCPackageSize, desDCIP, Convert.ToInt32(desDCPort));
                int DMIPackageSize = DMIPackage_.Pack(socket.SendBuf);
                socket.Send(DMIPackageSize, desDMIIP, Convert.ToInt32(desDMIPort));
                int ZCPackageSize = ZCPackage_.Pack(socket.SendBuf);

                if (socket.isSendToZC == true)
                {
                    socket.Send(ZCPackageSize, desZCIP, Convert.ToInt32(desZCPort));
                }
            }
            Interlocked.Exchange(ref inTimer, 0);

        }
        int i = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (socket.isEB == true || (isBtnCancle == false && socket.ZCInfoType == 0x03))
            {
                chart1.Series["Series1"].IsVisibleInLegend = false;
                chart1.Series["Series2"].IsVisibleInLegend = false;
                chart1.Series["Series3"].IsVisibleInLegend = false;
                chart1.Series["Series4"].IsVisibleInLegend = false;
                chart1.Series["Series1"].Points.Clear();
                chart1.Series["Series2"].Points.Clear();
                chart1.Series["Series3"].Points.Clear();
                chart1.Series["Series4"].Points.Clear();
                RMPoint = 0;
            }
            else if (socket.curModel == 3)//RM
            {
                chart1.Series["Series2"].Points.Clear();
                chart1.Series["Series3"].Points.Clear();
                chart1.Series["Series4"].Points.Clear();
                chart1.Series["Series1"].IsVisibleInLegend = true;
                chart1.Series["Series1"].Points.AddXY(RMPoint, inialProtectSpeed);
                RMPoint++;
                if (isFirst)
                {
                    chart1.Series["Series1"].Points.Clear();
                    chart1.Series["Series1"].Points.AddXY(0, inialProtectSpeed);
                    isFirst = false;
                }
            }
            else if (socket.curModel == 4)//EUM
            {
                chart1.Series["Series1"].IsVisibleInLegend = false;
                chart1.Series["Series2"].IsVisibleInLegend = false;
                chart1.Series["Series3"].IsVisibleInLegend = false;
                chart1.Series["Series4"].IsVisibleInLegend = false;
                chart1.Series["Series2"].Points.Clear();
                chart1.Series["Series3"].Points.Clear();
                chart1.Series["Series4"].Points.Clear();
                chart1.Series["Series1"].Points.Clear();
                isFirst = false;
            }
            else //CM
            {
                if (socket.isPrintATP)
                {
                    chart1.Series["Series1"].IsVisibleInLegend = false;
                    chart1.Series["Series2"].IsVisibleInLegend = true;
                    chart1.Series["Series3"].IsVisibleInLegend = true;
                    chart1.Series["Series4"].IsVisibleInLegend = true;
                    chart1.Series["Series2"].Points.Clear();
                    chart1.Series["Series3"].Points.Clear();
                    chart1.Series["Series4"].Points.Clear();
                    chart1.Series["Series1"].Points.Clear();
                    chart1.Series["Series1"].Enabled = false;
                    PrintATPCurve();
                    isFirst = false;
                    socket.isPrintATP = false;
                }
            }
        }

        public void PrintATPCurve()
        {
            if (lockFlag == 1)
            {
                MAEndDistance = Socket.MAEndDistance;
                limSpeedNum = Socket.limSpeedNum;
                limSpeedDistance_1 = Socket.limSpeedDistance_1;
                limSpeedLength_1 = Socket.limSpeedLength_1;
                limSpeedDistance_2 = Socket.limSpeedDistance_2;
                limSpeedLength_2 = Socket.limSpeedLength_2;
                limSpeedDistance_3 = Socket.limSpeedDistance_3;
                limSpeedLength_3 = Socket.limSpeedLength_3;
                frontLimit = Socket.limitSpeed;
                speed_1 = Math.Sqrt(3.6 * 3.6 * 1.2 * (MAEndDistance - limSpeedDistance_1 - limSpeedLength_1) + 40 * 40 / 2);
                speed_2 = Math.Sqrt(40 * 40 + 3.6 * 3.6 * 1.2 * (limSpeedDistance_2 - limSpeedDistance_1 - limSpeedLength_1));
                speed_3 = Math.Sqrt(3.6 * 3.6 * 1.2 * (MAEndDistance - limSpeedDistance_2 - limSpeedLength_2) + 40 * 40 / 2);
                speed_4 = Math.Sqrt(40 * 40 + 3.6 * 3.6 * 1.2 * (limSpeedDistance_2 - limSpeedDistance_1 - limSpeedLength_1));
                speed_5 = Math.Sqrt(40 * 40 + 3.6 * 3.6 * 1.2 * (limSpeedDistance_3 - limSpeedDistance_2 - limSpeedLength_2));
                speed_6 = Math.Sqrt(3.6 * 3.6 * 1.2 * (MAEndDistance - limSpeedDistance_3 - limSpeedLength_3) + 40 * 40 / 2);
            }
            switch (limSpeedNum)
            {
                case 0:

                    x[0] = 0;
                    y[0] = 80;
                    SeventyToMA(1);
                    Interlocked.Exchange(ref lockFlag, 0);
                    PrintMAStart(0, 7);
                    PrintMAEnd(8);
                    Interlocked.Exchange(ref lockFlag, 1);
                    if (MAEndDistance - brDistance > 0)
                    {
                        curProtectionSpeed = 80;
                    }
                    else
                    {
                        curProtectionSpeed = (int)(Math.Sqrt(80 / 3.6 * 80 / 3.6 + 2 * (-1.2) * (brDistance - MAEndDistance)) * 3.6);
                    }

                    if (frontLimit == 40)
                    {
                        frontProtectionSpeed = 40;
                    }
                    else
                    {
                        frontProtectionSpeed = curProtectionSpeed;
                    }
                    break;

                case 1:

                    x[0] = 0;
                    y[0] = 80;
                    SeventyToFourty(1, limSpeedDistance_1);
                    Interlocked.Exchange(ref lockFlag, 0);
                    PrintMAStart(0, 5);
                    if (MAEndDistance - brDistance >= limSpeedDistance_1 + limSpeedLength_1 + deDistance)//限速区段过后还能加速到80km/h在过一段后在降速为0
                    {
                        FourtyToSeventy(6, limSpeedDistance_1, limSpeedLength_1);
                        SeventyToMA(11);
                        PrintMAStart(6, 17);
                        PrintMAEnd(18);
                    }
                    else if (MAEndDistance - fourtyDistance < limSpeedDistance_1 + limSpeedLength_1)//MA离第二个限速区段太近，不能够保证40km/h走出限速区段，在第二个限速区段要开始降速
                    {
                        MidToMA(6);
                        PrintMAStart(6, 9);
                        PrintMAEnd(10);
                    }
                    else//限速区段过后能加速到小于80km/h的速度然后降速为0
                    {
                        int j = Convert.ToInt32((speed_1 - 40) / 4);
                        int k = Convert.ToInt32(speed_1 / 7);
                        FourtyToHalf(6, j, limSpeedDistance_1, limSpeedLength_1, speed_1);
                        HalfToMA(11, k, speed_1);
                        PrintMAStart(6, 16);
                        PrintMAEnd(17);
                    }
                    Interlocked.Exchange(ref lockFlag, 1);
                    CalculateProtectSpeed();
                    break;

                case 2:

                    int intervalSpeed_1 = Convert.ToInt32((speed_2 - 40) / 4);
                    x[0] = 0;
                    y[0] = 80;
                    SeventyToFourty(1, limSpeedDistance_1);
                    if (limSpeedDistance_1 + limSpeedLength_1 + deDistance > limSpeedDistance_2 - deDistance)//在第一个限速区段和第二个限速区段之间ATP曲线没办法升到80km/h    
                    {
                        FourtyToHalf(6, intervalSpeed_1, limSpeedDistance_1, limSpeedLength_1, speed_2);
                        HalfToFourty(11, intervalSpeed_1, limSpeedDistance_2);
                        x[15] = limSpeedDistance_2;
                        y[15] = 40;
                    }
                    else//在第一个限速区段和第二个限速区段之间ATP曲线可以升到80km/h 
                    {
                        FourtyToSeventy(6, limSpeedDistance_1, limSpeedLength_1);
                        SeventyToFourty(11, limSpeedDistance_2);
                    }
                    Interlocked.Exchange(ref lockFlag, 0);
                    PrintMAStart(0, 15);
                    if (MAEndDistance - brDistance >= limSpeedDistance_2 + limSpeedLength_2 + deDistance)//第二个限速区段过后还能加速到80km/h在过一段后在降速为0   
                    {
                        FourtyToSeventy(16, limSpeedDistance_2, limSpeedLength_2);
                        SeventyToMA(21);
                        PrintMAStart(16, 27);
                        PrintMAEnd(28);
                    }
                    else if (MAEndDistance - fourtyDistance < limSpeedDistance_2 + limSpeedLength_2)//MA离第二个限速区段太近，不能够保证40km/h，在第二个限速区段要开始降速
                    {
                        MidToMA(16);
                        PrintMAStart(16, 19);
                        PrintMAEnd(20);
                    }
                    else//第二个限速区段过后能加速到小于80km/h的速度然后降速为0
                    {
                        int j = Convert.ToInt32((speed_3 - 40) / 4);
                        int k = Convert.ToInt32(speed_3 / 7);
                        FourtyToHalf(16, j, limSpeedDistance_2, limSpeedLength_2, speed_3);
                        HalfToMA(21, k, speed_3);
                        PrintMAStart(16, 26);
                        PrintMAEnd(27);
                    }
                    Interlocked.Exchange(ref lockFlag, 1);
                    CalculateProtectSpeed();
                    break;

                case 3:

                    int intervalSpeed_2 = Convert.ToInt32((speed_4 - 40) / 4);
                    int intervalSpeed_3 = Convert.ToInt32((speed_5 - 40) / 4);
                    x[0] = 0;
                    y[0] = 80;
                    SeventyToFourty(1, limSpeedDistance_1);
                    if (limSpeedDistance_1 + limSpeedLength_1 + deDistance > limSpeedDistance_2 - deDistance)//在第一个限速区段和第二个限速区段之间ATP曲线没办法升到80km/h
                    {
                        FourtyToHalf(6, intervalSpeed_2, limSpeedDistance_1, limSpeedLength_1, speed_4);
                        HalfToFourty(11, intervalSpeed_2, limSpeedDistance_2);
                        x[15] = limSpeedDistance_2;
                        y[15] = 40;
                    }
                    else//在第一个限速区段和第二个限速区段之间ATP曲线可以升到80km/h
                    {
                        FourtyToSeventy(6, limSpeedDistance_1, limSpeedLength_1);
                        SeventyToFourty(11, limSpeedDistance_2);
                    }
                    if (limSpeedDistance_2 + limSpeedLength_2 + deDistance > limSpeedDistance_3 - deDistance)//在第二个限速区段和第三个限速区段之间ATP曲线没办法升到80km/h
                    {
                        FourtyToHalf(16, intervalSpeed_3, limSpeedDistance_2, limSpeedLength_2, speed_5);
                        HalfToFourty(21, intervalSpeed_3, limSpeedDistance_3);
                        x[25] = limSpeedDistance_3;
                        y[25] = 40;
                    }
                    else//在第二个限速区段和第三个限速区段之间ATP曲线可以升到80km/h
                    {
                        FourtyToSeventy(16, limSpeedDistance_2, limSpeedLength_2);
                        SeventyToFourty(21, limSpeedDistance_3);
                    }
                    Interlocked.Exchange(ref lockFlag, 0);
                    PrintMAStart(0, 25);
                    if (MAEndDistance - brDistance >= limSpeedDistance_3 + limSpeedLength_3 + deDistance)//第三个限速区段过后还能加速到80km/h在过一段后在降速为0
                    {
                        FourtyToSeventy(26, limSpeedDistance_3, limSpeedLength_3);
                        SeventyToMA(31);
                        PrintMAStart(26, 37);
                        PrintMAEnd(38);
                    }
                    else if (MAEndDistance - fourtyDistance < limSpeedDistance_3 + limSpeedLength_3)//MA离第三个限速区段太近，不能够保证40km/h，在第三个限速区段要开始降速
                    {
                        MidToMA(26);
                        PrintMAStart(26, 29);
                        PrintMAEnd(30);
                    }
                    else//在第三个限速区段过后能加速到小于80km/h的速度然后降速为0
                    {
                        int j = Convert.ToInt32((speed_6 - 40) / 4);
                        int k = Convert.ToInt32(speed_6 / 7);
                        FourtyToHalf(26, j, limSpeedDistance_3, limSpeedLength_3, speed_6);
                        HalfToMA(31, k, speed_6);
                        PrintMAStart(26, 36);
                        PrintMAEnd(37);
                    }
                    Interlocked.Exchange(ref lockFlag, 1);
                    CalculateProtectSpeed();
                    break;
            }
        }

        public void CalculateProtectSpeed()
        {
            if (limSpeedDistance_1 - deDistance > 0)
            {
                curProtectionSpeed = 80;
            }
            else if (limSpeedDistance_1 - deDistance <= 0 && limSpeedDistance_1 > 0)
            {
                curProtectionSpeed = (int)(Math.Sqrt(80 / 3.6 * 80 / 3.6 + 2 * (-1.2) * (deDistance - limSpeedDistance_1)) * 3.6);
            }
            else if (limSpeedDistance_1 == 0 && limSpeedNum > 0)
            {
                curProtectionSpeed = 40;
            }

            if (frontLimit == 40)
            {
                frontProtectionSpeed = 40;
            }
            else
            {
                frontProtectionSpeed = curProtectionSpeed;
            }
        }
        public void PrintMAStart(int j, int k)
        {
            for (int i = j; i <= k; i++)
            {
                chart1.Series["Series2"].Points.AddXY(x[i], y[i]);
                chart1.Series["Series3"].Points.AddXY(x[i], y[i] - 2);
                chart1.Series["Series4"].Points.AddXY(x[i], y[i] - 5);
            }
        }

        public void PrintMAEnd(int i)
        {
            chart1.Series["Series2"].Points.AddXY(x[i], y[i]);
            chart1.Series["Series3"].Points.AddXY(x[i], y[i]-2);
            chart1.Series["Series4"].Points.AddXY(x[i], y[i]-5);
        }

        public void SeventyToMA(int i)
        {
            x[i] = MAEndDistance - brDistance;
            y[i] = 80;
            x[i + 1] = x[i] + ((80 - node_2) / 3.6 * (80 - node_2) / 3.6 - 80 / 3.6 * 80 / 3.6) / 2 / (-1.2);
            y[i + 1] = 80 - node_2;
            x[i + 2] = x[i] + ((80 - 2 * node_2) / 3.6 * (80 - 2 * node_2) / 3.6 - 80 / 3.6 * 80 / 3.6) / 2 / (-1.2);
            y[i + 2] = 80 - 2 * node_2;
            x[i + 3] = x[i] + ((80 - 3 * node_2) / 3.6 * (80 - 3 * node_2) / 3.6 - 80 / 3.6 * 80 / 3.6) / 2 / (-1.2);
            y[i + 3] = 80 - 3 * node_2;
            x[i + 4] = x[i] + ((80 - 4 * node_2) / 3.6 * (80 - 4 * node_2) / 3.6 - 80 / 3.6 * 80 / 3.6) / 2 / (-1.2);
            y[i + 4] = 80 - 4 * node_2;
            x[i + 5] = x[i] + ((80 - 5 * node_2) / 3.6 * (80 - 5 * node_2) / 3.6 - 80 / 3.6 * 80 / 3.6) / 2 / (-1.2);
            y[i + 5] = 80 - 5 * node_2;
            x[i + 6] = x[i] + ((80 - 6 * node_2) / 3.6 * (80 - 6 * node_2) / 3.6 - 80 / 3.6 * 80 / 3.6) / 2 / (-1.2);
            y[i + 6] = 80 - 6 * node_2;
            x[i + 7] = MAEndDistance;
            y[i + 7] = 0;
        }

        public void SeventyToFourty(int i, double location)
        {
            x[i] = location - deDistance;
            y[i] = 80;
            x[i + 1] = x[i] + ((80 - node_1) / 3.6 * (80 - node_1) / 3.6 - 80 / 3.6 * 80 / 3.6) / 2 / (-1.2);
            y[i + 1] = 80 - node_1;
            x[i + 2] = x[i] + ((80 - 2 * node_1) / 3.6 * (80 - 2 * node_1) / 3.6 - 80 / 3.6 * 80 / 3.6) / 2 / (-1.2);
            y[i + 2] = 80 - 2 * node_1;
            x[i + 3] = x[i] + ((80 - 3 * node_1) / 3.6 * (80 - 3 * node_1) / 3.6 - 80 / 3.6 * 80 / 3.6) / 2 / (-1.2);
            y[i + 3] = 80 - 3 * node_1;
            x[i + 4] = location;
            y[i + 4] = 40;
        }

        public void FourtyToSeventy(int i, double location, double length)
        {
            x[i] = location + length;
            y[i] = 40;
            x[i + 1] = x[i] + ((40 + node_1) / 3.6 * (40 + node_1) / 3.6 - 40 / 3.6 * 40 / 3.6) / (2 * 1.2);
            y[i + 1] = 40 + node_1;
            x[i + 2] = x[i] + ((40 + 2 * node_1) / 3.6 * (40 + 2 * node_1) / 3.6 - 40 / 3.6 * 40 / 3.6) / (2 * 1.2);
            y[i + 2] = 40 + 2 * node_1;
            x[i + 3] = x[i] + ((40 + 3 * node_1) / 3.6 * (40 + 3 * node_1) / 3.6 - 40 / 3.6 * 40 / 3.6) / (2 * 1.2);
            y[i + 3] = 40 + 3 * node_1;
            x[i + 4] = location + length + deDistance;
            y[i + 4] = 80;
        }

        public void FourtyToHalf(int i, int j, double location, double length, double speed)
        {
            x[i] = location + length;
            y[i] = 40;
            x[i + 1] = x[i] + ((40 + j) / 3.6 * (40 + j) / 3.6 - 40 / 3.6 * 40 / 3.6) / (2 * 1.2);
            y[i + 1] = 40 + j;
            x[i + 2] = x[i] + ((40 + 2 * j) / 3.6 * (40 + 2 * j) / 3.6 - 40 / 3.6 * 40 / 3.6) / (2 * 1.2);
            y[i + 2] = 40 + 2 * j;
            x[i + 3] = x[i] + ((40 + 3 * j) / 3.6 * (40 + 3 * j) / 3.6 - 40 / 3.6 * 40 / 3.6) / (2 * 1.2);
            y[i + 3] = 40 + 3 * j;
            x[i + 4] = x[i] + (speed / 3.6 * speed / 3.6 - 40 / 3.6 * 40 / 3.6) / (2 * 1.2);
            y[i + 4] = speed;
        }

        public void MidToMA(int i)
        {
            x[i] = MAEndDistance - fourtyDistance;
            y[i] = 40;
            x[i + 1] = x[6] + ((40 - node_3) / 3.6 * (40 - node_3) / 3.6 - 40 / 3.6 * 40 / 3.6) / 2 / (-1.2);
            y[i + 1] = 40 - node_3;
            x[i + 2] = x[6] + ((40 - 2 * node_3) / 3.6 * (40 - 2 * node_3) / 3.6 - 40 / 3.6 * 40 / 3.6) / 2 / (-1.2);
            y[i + 2] = 40 - 2 * node_3;
            x[i + 3] = x[6] + ((40 - 3 * node_3) / 3.6 * (40 - 3 * node_3) / 3.6 - 40 / 3.6 * 40 / 3.6) / 2 / (-1.2);
            y[i + 3] = 40 - 3 * node_3;
            x[i + 4] = MAEndDistance;
            y[i + 4] = 0;
        }

        public void HalfToFourty(int i, int j, double location)
        {
            x[i] = location - ((40 + 3 * j) / 3.6 * (40 + 3 * j) / 3.6 - 40 / 3.6 * 40 / 3.6) / (2 * 1.2);
            y[i] = 40 + 3 * j;
            x[i + 1] = location - ((40 + 2 * j) / 3.6 * (40 + 2 * j) / 3.6 - 40 / 3.6 * 40 / 3.6) / (2 * 1.2);
            y[i + 1] = 40 + 2 * j;
            x[i + 2] = location - ((40 + j) / 3.6 * (40 + j) / 3.6 - 40 / 3.6 * 40 / 3.6) / (2 * 1.2);
            y[i + 2] = 40 + j;
            x[i + 3] = location;
            y[i + 3] = 40;
        }

        public void HalfToMA(int i, int k, double speed)
        {
            x[i] = MAEndDistance - (speed - k) / 3.6 * (speed - k) / 3.6 / 2 / 1.2;
            y[i] = speed - k;
            x[i + 1] = MAEndDistance - (speed - 2 * k) / 3.6 * (speed - 2 * k) / 3.6 / 2 / 1.2;
            y[i + 1] = speed - 2 * k;
            x[i + 2] = MAEndDistance - (speed - 3 * k) / 3.6 * (speed - 3 * k) / 3.6 / 2 / 1.2;
            y[i + 2] = speed - 3 * k;
            x[i + 3] = MAEndDistance - (speed - 4 * k) / 3.6 * (speed - 4 * k) / 3.6 / 2 / 1.2;
            y[i + 3] = speed - 4 * k;
            x[i + 4] = MAEndDistance - (speed - 5 * k) / 3.6 * (speed - 5 * k) / 3.6 / 2 / 1.2;
            y[i + 4] = speed - 5 * k;
            x[i + 5] = MAEndDistance - (speed - 6 * k) / 3.6 * (speed - 6 * k) / 3.6 / 2 / 1.2;
            y[i + 5] = speed - 6 * k;
            x[i + 6] = MAEndDistance;
            y[i + 6] = 0;
        }

        private void ATP_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult dr = MessageBox.Show("确定要退出程序吗？", "退出", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        socket.CloseThread();
                    }
                    catch (Exception a)
                    {
                    }
                    this.Close();
                }
            }
        }

        private void LoadSecondStation()
        {
            StationElements elements_1 = StationElements.Open("StationElements1.xml", null);
            stationTopoloty_1_ = new StationTopoloty();
            stationTopoloty_1_.Open("StationTopoloty1.xml", elements_1.Elements);

            foreach (var item in elements_1.Elements)
            {
                item.Left += 2000;
            }
            ConnectNodes(stationTopoloty_, stationTopoloty_1_, "201G", "301G");
            ConnectNodes(stationTopoloty_, stationTopoloty_1_, "210G", "306G");
            ConnectNodes(stationTopoloty_1_, stationTopoloty_, "413G", "115G");//避免轨道连成环索引不方便
            ConnectNodes(stationTopoloty_1_, stationTopoloty_, "416G", "114G");//避免轨道连成环索引不方便
        }

        private void ConnectNodes(StationTopoloty topo1, StationTopoloty topo2, string deviceName1, string deviceName2)
        {
            TopolotyNode leftNode = null;
            foreach (var item in topo1.Nodes)
            {
                if (item.NodeDevice.Name == deviceName1)
                {
                    leftNode = item;
                }
            }

            TopolotyNode rightNode = null;
            foreach (var item in topo2.Nodes)
            {
                if (item.NodeDevice.Name == deviceName2)
                {
                    rightNode = item;
                }
            }

            if (leftNode != null && rightNode != null)
            {
                leftNode.LeftNodes.Add(rightNode);
                rightNode.RightNodes.Add(leftNode);
            }
        }

        private void LoadStationTopo(string path)
        {
            stationTopoloty_ = new StationTopoloty();
            stationTopoloty_.Open(path, stationElements_.Elements);
        }

        private void LoadGraphicElements(string path)
        {
            stationElements_ = StationElements.Open(path, null);
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (btnChange.Text == "实际模式")
            {
                btnChange.Text = "教学模式";
                curProtectionSpeed = 100;
                inialProtectSpeed = 100;
            }
            else
            {
                btnChange.Text = "实际模式";
                curProtectionSpeed = 25;
                inialProtectSpeed = 25;
            }

        }
        private void saveLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            socket.isSaveLog = true;
            socket.isPrintConsole = true;
        }

        private void notSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            socket.isSaveLog = false;
            socket.isPrintConsole = true;
        }
    }
}
