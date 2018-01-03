using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

namespace DMI
{
    public partial class Form1 : Form
    {
        public static string sourceIP = "";
        public static string sourcePort = "";
        public static string desIP = "";
        public static string desPort = "";
        public bool isRelease = false;
        string[] highestDrivingMode = { "AM", "CM", "RM" };
        string[] currentDrivingMode = { "AM", "CM", "RM", "EUM" };
        Socket socket;
        DMIPackage DMIPackage_;


        public Form1()
        {
            InitializeComponent();
            targetDistance_21.Distance = 750;
            label1.Text = 0.ToString();           
            b_AGauge1.CurSpeed = 0;
            b_AGauge1.RecommedSpeed = 0;
            b_AGauge1.LimitSpeed = 0;
            m31.IsAR = false;
            c11.TractionAndBrakingState = UIControl.RunState.TractionBrakingState.traction;

            socket = new Socket();
            DMIPackage_ = new DMIPackage();
            ConfigurationData.ReadConfigData();
            socket.Start(sourceIP, Convert.ToInt32(sourcePort), desIP, Convert.ToInt32(desPort));
            Control.CheckForIllegalCrossThreadCalls = false;
            this.KeyPreview = true;
            timer_DateAndTime.Enabled = true;
            timer_DateAndTime.Interval = 1000;
            label1.Font = new Font("Arial", 20);
            socket.refEvent += new Socket.RefDelegate(refreshChart);
            SetupTimer();
        }

        
        private void SetupTimer()
        {
            System.Timers.Timer timer = new System.Timers.Timer(200);
            timer.Elapsed += TimerElapsed;
            timer.Start();
        }
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            DMIPackage_.PackageType = 4;
            DMIPackage_.Length = 20;
            DMIPackage_.TrainOrder = "111111";
            DMIPackage_.TrainNumber = 222;
            DMIPackage_.DriverNumber = 22;

            if (socket.ActulSpeed == 0)
            {
                if (isRelease)
                {
                    DMIPackage_.RelieveOrder = 2;
                }
                else
                {
                    DMIPackage_.RelieveOrder = 0;
                }
            }
            else
            {
                isRelease = false;
                DMIPackage_.RelieveOrder = 0;
            }
            int DMIPackageSize = DMIPackage_.Pack(socket.SendBuf);
            socket.Send(DMIPackageSize, desIP, Convert.ToInt32(desPort));
        }

        public void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult dr = MessageBox.Show("确定要退出程序吗？", "退出", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    socket.closeThread();
                    this.Close();

                }
            }
            if (e.Control && e.KeyCode == Keys.D)
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        public void refreshChart()
        {
            lbl_M1_curMode.Text = currentDrivingMode[Convert.ToInt32(socket.CurModel) - 1];
            lbl_HighModel.Text = highestDrivingMode[Convert.ToInt32(socket.HighModel) - 1];
            lbl_trainNum.Text = socket.TrainNum;
            lbl_T3_driver.Text = socket.TrainID.ToString();
            b_AGauge1.CurSpeed = socket.ActulSpeed;
            b_AGauge1.LimitSpeed = socket.HighSpeed;
            label1.Text= (socket.PermitSpeed).ToString();
            targetDistance_21.Distance = socket.TargetLocation;
            if (lbl_M1_curMode.Text == "CM")
            {
                b_AGauge1.RecommedSpeed = socket.PermitSpeed;
            }
            else
            {
                b_AGauge1.RecommedSpeed = socket.HighSpeed;
            }

            if (socket.BreakOut==6)
            {
                CurState.BackColor = Color.Red;
                c11.TractionAndBrakingState = UIControl.RunState.TractionBrakingState.brake;
            }
            else
            {
                CurState.BackColor = Color.Green;
                c11.TractionAndBrakingState = UIControl.RunState.TractionBrakingState.traction;
            }

            if(socket.ActulSpeed > socket.PermitSpeed)
            {
                CurState.BackColor = Color.Yellow;
            }

            b_AGauge1.Refresh();
        }

        public void timer_DateAndTime_Tick(object sender, EventArgs e)
        {
            lbl_curDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            lbl_curTime.Text = DateTime.Now.ToLongTimeString().ToString();
        }


        private void btnRelease_Click(object sender, EventArgs e)
        {
            isRelease = true;
        }

        private void btn_D_INFO_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}
