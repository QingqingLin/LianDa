using System;
using System.Windows;
using System.Threading;
using System.Collections.Generic;
using 线路绘图工具;
using System.Net.Sockets;
using System.Net;
using System.Windows.Input;
using System.Diagnostics;
using 线路数据应用示例;
using System.Windows.Controls;

namespace ZC
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public static StationElements stationElements_;
        public static StationElements stationElements_1_;
        public static StationTopoloty stationTopoloty_;
        public static StationTopoloty stationTopoloty_1_;
        public ZCSocket Receive;
        private Point lastPosition_;

        public StationTopoloty Topo { get { return stationTopoloty_; } }
        public MainWindow()
        {
            InitializeComponent();

            Graphic.InitializeColorDict();

            LoadGraphicElements("StationElements.xml");
            LoadStationTopo("StationTopoloty.xml");

            LoadSecondStation();

            AddCIAccess LoadInterlockTable = new AddCIAccess();
            IPConfigure LoadIPConfig = new IPConfigure();

            Receive = new ZCSocket();
            Receive.Start();
            NonComTrain();

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            stationElements_.CheckSectionSwitches();
        }

        internal void ScrollStationCanvas(Vector vMouseMove)
        {
            Debug.WriteLine(string.Format("{0} {1} {2} {3}", MainScroll.HorizontalOffset, MainScroll.VerticalOffset, vMouseMove.X, vMouseMove.Y));
            MainScroll.ScrollToHorizontalOffset(MainScroll.HorizontalOffset - vMouseMove.X);
            MainScroll.ScrollToVerticalOffset(MainScroll.VerticalOffset - vMouseMove.Y);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point currentposition = e.GetPosition(this);
                Vector vMouseMove = currentposition - lastPosition_;
                this.ScrollStationCanvas(vMouseMove);
                lastPosition_ = currentposition;
            }
        }

        private void LoadSecondStation()
        {
            stationElements_1_ = StationElements.Open("StationElements1.xml");
            stationTopoloty_1_ = new StationTopoloty();
            stationTopoloty_1_.Open("StationTopoloty1.xml", stationElements_1_.Elements);

            foreach (var item in stationElements_1_.Elements)
            {
                item.Top += 55;
                item.Left += 1450;
            }

            stationElements_1_.AddElementsToCanvas(MainCanvas);
        }
        
        private void LoadStationTopo(string path)
        {
            stationTopoloty_ = new StationTopoloty();
            stationTopoloty_.Open(path, stationElements_.Elements);
        }

        private void LoadStationTopo1(string path)
        {
            stationTopoloty_1_ = new StationTopoloty();
            stationTopoloty_1_.Open(path, stationElements_.Elements);
        }

        private void LoadGraphicElements(string path)
        {
            stationElements_ = StationElements.Open(path);
            stationElements_.AddElementsToCanvas(MainCanvas);
        }

        private void NonComTrain()
        {
            var obj = new NonCommunicationTrain();
            var NonComTrainThread = new Thread(obj.JudgeLostTrain);
            NonComTrainThread.IsBackground = true;
            NonComTrainThread.Start();
        }

        private void MainCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            lastPosition_ = e.GetPosition(this);
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void NetConfig_Click(object sender, RoutedEventArgs e)
        {
            NetConfig dlg = new NetConfig(this);
            dlg.ShowDialog();
        }

        private void OpenLogs_Click(object sender, RoutedEventArgs e)
        {
            if (OpenLogs.IsChecked)
            {
                
            }
        }

        private void SaveLogs_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
