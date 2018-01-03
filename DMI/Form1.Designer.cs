namespace DMI
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer_DateAndTime = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.CurState = new System.Windows.Forms.Label();
            this.btnRelease = new System.Windows.Forms.Button();
            this.M2 = new System.Windows.Forms.Label();
            this.lbl_T3_driver = new System.Windows.Forms.Label();
            this.lbl_T2_nextStation = new System.Windows.Forms.Label();
            this.lbl_HighModel = new System.Windows.Forms.Label();
            this.btn_D_INFO = new System.Windows.Forms.Button();
            this.lbl_M1_curMode = new System.Windows.Forms.Label();
            this.lbl_trainNum = new System.Windows.Forms.Label();
            this.lbl_curTime = new System.Windows.Forms.Label();
            this.lbl_curDate = new System.Windows.Forms.Label();
            this.targetDistance_21 = new DMI.TargetDistance_2();
            this.departSign1 = new DMI.UIControl.DepartSign();
            this.doorState1 = new DMI.UIControl.DoorState();
            this.c11 = new DMI.UIControl.RunState();
            this.c51 = new DMI.UIControl.C5();
            this.c31 = new DMI.UIControl.C3();
            this.m101 = new DMI.UIControl.M10();
            this.m91 = new DMI.UIControl.M9();
            this.m61 = new DMI.UIControl.M6();
            this.m51 = new DMI.UIControl.M5();
            this.m41 = new DMI.UIControl.M4();
            this.m31 = new DMI.UIControl.M3();
            this.N = new DMI.UIControl.N();
            this.b_AGauge1 = new DMI.UIControl.Gauge();
            this.a2_TargetDistance1 = new DMI.TargetDistance_1();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer_DateAndTime
            // 
            this.timer_DateAndTime.Enabled = true;
            this.timer_DateAndTime.Interval = 1000;
            this.timer_DateAndTime.Tick += new System.EventHandler(this.timer_DateAndTime_Tick);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.targetDistance_21);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.CurState);
            this.panel1.Controls.Add(this.btnRelease);
            this.panel1.Controls.Add(this.M2);
            this.panel1.Controls.Add(this.departSign1);
            this.panel1.Controls.Add(this.doorState1);
            this.panel1.Controls.Add(this.c11);
            this.panel1.Controls.Add(this.lbl_T3_driver);
            this.panel1.Controls.Add(this.lbl_T2_nextStation);
            this.panel1.Controls.Add(this.c51);
            this.panel1.Controls.Add(this.c31);
            this.panel1.Controls.Add(this.m101);
            this.panel1.Controls.Add(this.m91);
            this.panel1.Controls.Add(this.lbl_HighModel);
            this.panel1.Controls.Add(this.m61);
            this.panel1.Controls.Add(this.m51);
            this.panel1.Controls.Add(this.m41);
            this.panel1.Controls.Add(this.m31);
            this.panel1.Controls.Add(this.btn_D_INFO);
            this.panel1.Controls.Add(this.N);
            this.panel1.Controls.Add(this.lbl_M1_curMode);
            this.panel1.Controls.Add(this.lbl_trainNum);
            this.panel1.Controls.Add(this.lbl_curTime);
            this.panel1.Controls.Add(this.lbl_curDate);
            this.panel1.Controls.Add(this.b_AGauge1);
            this.panel1.Controls.Add(this.a2_TargetDistance1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(793, 599);
            this.panel1.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Font = new System.Drawing.Font("宋体", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(75, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 27);
            this.label1.TabIndex = 54;
            this.label1.Text = "0";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CurState
            // 
            this.CurState.BackColor = System.Drawing.Color.Green;
            this.CurState.Location = new System.Drawing.Point(54, 34);
            this.CurState.Name = "CurState";
            this.CurState.Size = new System.Drawing.Size(106, 60);
            this.CurState.TabIndex = 55;
            // 
            // btnRelease
            // 
            this.btnRelease.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnRelease.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRelease.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRelease.Location = new System.Drawing.Point(543, 522);
            this.btnRelease.Name = "btnRelease";
            this.btnRelease.Size = new System.Drawing.Size(98, 39);
            this.btnRelease.TabIndex = 53;
            this.btnRelease.Text = "缓解确认";
            this.btnRelease.UseVisualStyleBackColor = false;
            this.btnRelease.Click += new System.EventHandler(this.btnRelease_Click);
            // 
            // M2
            // 
            this.M2.AutoSize = true;
            this.M2.Font = new System.Drawing.Font("微软雅黑", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.M2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.M2.Location = new System.Drawing.Point(641, 153);
            this.M2.Name = "M2";
            this.M2.Size = new System.Drawing.Size(108, 46);
            this.M2.TabIndex = 40;
            this.M2.Text = "CBTC";
            // 
            // lbl_T3_driver
            // 
            this.lbl_T3_driver.AutoSize = true;
            this.lbl_T3_driver.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_T3_driver.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbl_T3_driver.Location = new System.Drawing.Point(661, 47);
            this.lbl_T3_driver.Name = "lbl_T3_driver";
            this.lbl_T3_driver.Size = new System.Drawing.Size(88, 24);
            this.lbl_T3_driver.TabIndex = 52;
            this.lbl_T3_driver.Text = "C00987";
            // 
            // lbl_T2_nextStation
            // 
            this.lbl_T2_nextStation.AutoSize = true;
            this.lbl_T2_nextStation.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_T2_nextStation.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbl_T2_nextStation.Location = new System.Drawing.Point(592, 47);
            this.lbl_T2_nextStation.Name = "lbl_T2_nextStation";
            this.lbl_T2_nextStation.Size = new System.Drawing.Size(49, 24);
            this.lbl_T2_nextStation.TabIndex = 51;
            this.lbl_T2_nextStation.Text = "D45";
            // 
            // lbl_HighModel
            // 
            this.lbl_HighModel.AutoSize = true;
            this.lbl_HighModel.Font = new System.Drawing.Font("微软雅黑", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_HighModel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbl_HighModel.Location = new System.Drawing.Point(524, 343);
            this.lbl_HighModel.Name = "lbl_HighModel";
            this.lbl_HighModel.Size = new System.Drawing.Size(79, 46);
            this.lbl_HighModel.TabIndex = 45;
            this.lbl_HighModel.Text = "AM";
            // 
            // btn_D_INFO
            // 
            this.btn_D_INFO.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_D_INFO.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_D_INFO.Location = new System.Drawing.Point(641, 84);
            this.btn_D_INFO.Name = "btn_D_INFO";
            this.btn_D_INFO.Size = new System.Drawing.Size(105, 64);
            this.btn_D_INFO.TabIndex = 39;
            this.btn_D_INFO.Text = "INFO";
            this.btn_D_INFO.UseVisualStyleBackColor = false;
            this.btn_D_INFO.Click += new System.EventHandler(this.btn_D_INFO_Click);
            // 
            // lbl_M1_curMode
            // 
            this.lbl_M1_curMode.AutoSize = true;
            this.lbl_M1_curMode.Font = new System.Drawing.Font("微软雅黑", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_M1_curMode.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbl_M1_curMode.Location = new System.Drawing.Point(526, 153);
            this.lbl_M1_curMode.Name = "lbl_M1_curMode";
            this.lbl_M1_curMode.Size = new System.Drawing.Size(77, 46);
            this.lbl_M1_curMode.TabIndex = 37;
            this.lbl_M1_curMode.Text = "RM";
            // 
            // lbl_trainNum
            // 
            this.lbl_trainNum.AutoSize = true;
            this.lbl_trainNum.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_trainNum.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbl_trainNum.Location = new System.Drawing.Point(511, 47);
            this.lbl_trainNum.Name = "lbl_trainNum";
            this.lbl_trainNum.Size = new System.Drawing.Size(75, 24);
            this.lbl_trainNum.TabIndex = 36;
            this.lbl_trainNum.Text = "T0291";
            // 
            // lbl_curTime
            // 
            this.lbl_curTime.AutoSize = true;
            this.lbl_curTime.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_curTime.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbl_curTime.Location = new System.Drawing.Point(215, 537);
            this.lbl_curTime.Name = "lbl_curTime";
            this.lbl_curTime.Size = new System.Drawing.Size(114, 24);
            this.lbl_curTime.TabIndex = 35;
            this.lbl_curTime.Text = "00:00:00";
            // 
            // lbl_curDate
            // 
            this.lbl_curDate.AutoSize = true;
            this.lbl_curDate.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_curDate.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbl_curDate.Location = new System.Drawing.Point(50, 537);
            this.lbl_curDate.Name = "lbl_curDate";
            this.lbl_curDate.Size = new System.Drawing.Size(140, 24);
            this.lbl_curDate.TabIndex = 34;
            this.lbl_curDate.Text = "2017-03-17";
            // 
            // targetDistance_21
            // 
            this.targetDistance_21.Distance = 750F;
            this.targetDistance_21.Location = new System.Drawing.Point(113, 146);
            this.targetDistance_21.Name = "targetDistance_21";
            this.targetDistance_21.Size = new System.Drawing.Size(31, 263);
            this.targetDistance_21.TabIndex = 58;
            // 
            // departSign1
            // 
            this.departSign1.BackColor = System.Drawing.Color.Black;
            this.departSign1.Location = new System.Drawing.Point(320, 402);
            this.departSign1.Name = "departSign1";
            this.departSign1.Size = new System.Drawing.Size(100, 60);
            this.departSign1.TabIndex = 32;
            // 
            // doorState1
            // 
            this.doorState1.BackColor = System.Drawing.Color.Black;
            this.doorState1.DoorStatus = DMI.UIControl.DoorState.DoorStateInformation.close;
            this.doorState1.Location = new System.Drawing.Point(641, 343);
            this.doorState1.Name = "doorState1";
            this.doorState1.Size = new System.Drawing.Size(100, 60);
            this.doorState1.TabIndex = 31;
            // 
            // c11
            // 
            this.c11.Location = new System.Drawing.Point(40, 410);
            this.c11.Name = "c11";
            this.c11.Size = new System.Drawing.Size(120, 60);
            this.c11.TabIndex = 48;
            this.c11.TractionAndBrakingState = DMI.UIControl.RunState.TractionBrakingState.traction;
            // 
            // c51
            // 
            this.c51.IsOppositeCommunicationRight = true;
            this.c51.Location = new System.Drawing.Point(435, 400);
            this.c51.Name = "c51";
            this.c51.Size = new System.Drawing.Size(60, 60);
            this.c51.TabIndex = 50;
            // 
            // c31
            // 
            this.c31.IsTrainIntergrality = true;
            this.c31.Location = new System.Drawing.Point(188, 410);
            this.c31.Name = "c31";
            this.c31.Size = new System.Drawing.Size(100, 52);
            this.c31.TabIndex = 49;
            // 
            // m101
            // 
            this.m101.Location = new System.Drawing.Point(639, 400);
            this.m101.Name = "m101";
            this.m101.Size = new System.Drawing.Size(99, 60);
            this.m101.TabIndex = 47;
            // 
            // m91
            // 
            this.m91.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.m91.Location = new System.Drawing.Point(510, 400);
            this.m91.Name = "m91";
            this.m91.OperateStatus = DMI.UIControl.M9.OperatingStatus.ATOfailure;
            this.m91.Size = new System.Drawing.Size(100, 60);
            this.m91.TabIndex = 46;
            // 
            // m61
            // 
            this.m61.Location = new System.Drawing.Point(636, 268);
            this.m61.Name = "m61";
            this.m61.Size = new System.Drawing.Size(100, 60);
            this.m61.TabIndex = 44;
            // 
            // m51
            // 
            this.m51.Location = new System.Drawing.Point(508, 266);
            this.m51.Name = "m51";
            this.m51.Opentype = DMI.UIControl.M5.OpenType.bothOpen;
            this.m51.Size = new System.Drawing.Size(100, 60);
            this.m51.Stopwindow = DMI.UIControl.M5.StopWindow.ATPwindow;
            this.m51.TabIndex = 43;
            // 
            // m41
            // 
            this.m41.BackColor = System.Drawing.Color.Black;
            this.m41.IsReachStopPlace = false;
            this.m41.Location = new System.Drawing.Point(637, 202);
            this.m41.Name = "m41";
            this.m41.Size = new System.Drawing.Size(97, 60);
            this.m41.TabIndex = 42;
            // 
            // m31
            // 
            this.m31.BackColor = System.Drawing.Color.Black;
            this.m31.IsAR = false;
            this.m31.Location = new System.Drawing.Point(508, 202);
            this.m31.Name = "m31";
            this.m31.Size = new System.Drawing.Size(100, 63);
            this.m31.TabIndex = 41;
            // 
            // N
            // 
            this.N.BackColor = System.Drawing.Color.Black;
            this.N.IsJumpstop = true;
            this.N.Location = new System.Drawing.Point(509, 92);
            this.N.Name = "N";
            this.N.Size = new System.Drawing.Size(104, 56);
            this.N.TabIndex = 38;
            // 
            // b_AGauge1
            // 
            this.b_AGauge1.BackColor = System.Drawing.Color.Transparent;
            this.b_AGauge1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.b_AGauge1.CurSpeed = 50F;
            this.b_AGauge1.LimitSpeed = 0F;
            this.b_AGauge1.Location = new System.Drawing.Point(168, 103);
            this.b_AGauge1.Name = "b_AGauge1";
            this.b_AGauge1.RecommedSpeed = 0F;
            this.b_AGauge1.Size = new System.Drawing.Size(314, 300);
            this.b_AGauge1.TabIndex = 33;
            // 
            // a2_TargetDistance1
            // 
            this.a2_TargetDistance1.Location = new System.Drawing.Point(42, 144);
            this.a2_TargetDistance1.Name = "a2_TargetDistance1";
            this.a2_TargetDistance1.Size = new System.Drawing.Size(65, 259);
            this.a2_TargetDistance1.TabIndex = 57;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(793, 599);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DMI";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer_DateAndTime;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label CurState;
        private System.Windows.Forms.Button btnRelease;
        private System.Windows.Forms.Label M2;
        private UIControl.DepartSign departSign1;
        private UIControl.DoorState doorState1;
        private UIControl.RunState c11;
        private System.Windows.Forms.Label lbl_T3_driver;
        private System.Windows.Forms.Label lbl_T2_nextStation;
        private UIControl.C5 c51;
        private UIControl.C3 c31;
        private UIControl.M10 m101;
        private UIControl.M9 m91;
        public System.Windows.Forms.Label lbl_HighModel;
        private UIControl.M6 m61;
        private UIControl.M5 m51;
        private UIControl.M4 m41;
        private UIControl.M3 m31;
        private System.Windows.Forms.Button btn_D_INFO;
        private UIControl.N N;
        public System.Windows.Forms.Label lbl_M1_curMode;
        public System.Windows.Forms.Label lbl_trainNum;
        private System.Windows.Forms.Label lbl_curTime;
        private System.Windows.Forms.Label lbl_curDate;
        private UIControl.Gauge b_AGauge1;
        private TargetDistance_1 a2_TargetDistance1;
        private TargetDistance_2 targetDistance_21;
    }
}

