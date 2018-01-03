using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZC;

namespace 线路数据应用示例
{
    public partial class NetConfig : Form
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        MainWindow main;
        string iniPath = "IP-PortList.ini";
        public NetConfig(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
        }

        private void NetConfig_Load(object sender, EventArgs e)
        {
            LoadIni();
        }

        private void LoadIni()
        {
            foreach (var item in IPConfigure.IPList)
            {
                switch (item.DeviceName)
                {
                    case "ZC":
                        this.LocalIPTxt.Text = item.IP.ToString();
                        this.LocalPortTxt.Text = item.Port.ToString();
                        break;
                    case "CI1":
                        this.TopLinkIPTxt.Text = item.IP.ToString();
                        this.TopLinkPortTxt.Text = item.Port.ToString();
                        break;
                    case "CI2":
                        this.DownLinkIPTxt.Text = item.IP.ToString();
                        this.DownLinkPortTxt.Text = item.Port.ToString();
                        break;
                    case "ATP1":
                        this.ATP1IPTxt.Text = item.IP.ToString();
                        this.ATP1PortTxt.Text = item.Port.ToString();
                        break;
                    case "ATP2":
                        this.ATP2IPTxt.Text = item.IP.ToString();
                        this.ATP2PortTxt.Text = item.Port.ToString();
                        break;
                    case "ATP3":
                        this.ATP3IPTxt.Text = item.IP.ToString();
                        this.ATP3PortTxt.Text = item.Port.ToString();
                        break;
                    case "ATP4":
                        this.ATP4IPTxt.Text = item.IP.ToString();
                        this.ATP4PortTxt.Text = item.Port.ToString();
                        break;                            
                    default:
                        break;
                }
            }  
        }

        private void SureBtn_Click(object sender, EventArgs e)
        {
            UpdateIPConfig();
        }

        private void UpdateIPConfig()
        {
            bool isLocalChanged = false;
            for (int i = 0; i < 7; i++)
            {
                switch (IPConfigure.IPList[i].DeviceName)
                {
                    case "ZC":
                        if (!(IPConfigure.IPList[i].IP == IPAddress.Parse(this.LocalIPTxt.Text) &&
                            IPConfigure.IPList[i].Port == Convert.ToInt16(this.LocalPortTxt.Text)))
                        {
                            IniWriteValue("ZC", "IP", this.LocalIPTxt.Text);
                            IniWriteValue("ZC", "Port", this.LocalIPTxt.Text);
                            IPConfigure.IPList[i].IP = IPAddress.Parse(this.LocalIPTxt.Text);
                            IPConfigure.IPList[i].Port = Convert.ToInt16(this.LocalPortTxt.Text);
                            isLocalChanged = true;
                        }
                        break;
                    case "CI1":
                        if (!(IPConfigure.IPList[i].IP == IPAddress.Parse(this.TopLinkIPTxt.Text) &&
                            IPConfigure.IPList[i].Port == Convert.ToInt16(this.TopLinkPortTxt.Text)))
                        {
                            IniWriteValue("CI1", "IP", this.LocalIPTxt.Text);
                            IniWriteValue("CI1", "Port", this.LocalIPTxt.Text);
                            IPConfigure.IPList[i].IP = IPAddress.Parse(this.TopLinkIPTxt.Text);
                            IPConfigure.IPList[i].Port = Convert.ToInt16(this.TopLinkPortTxt.Text);
                        }
                        break;
                    case "CI2":
                        if (!(IPConfigure.IPList[i].IP == IPAddress.Parse(this.DownLinkIPTxt.Text) &&
                            IPConfigure.IPList[i].Port == Convert.ToInt16(this.DownLinkPortTxt.Text)))
                        {
                            IniWriteValue("CI2", "IP", this.LocalIPTxt.Text);
                            IniWriteValue("CI2", "Port", this.LocalIPTxt.Text);
                            IPConfigure.IPList[i].IP = IPAddress.Parse(this.DownLinkIPTxt.Text);
                            IPConfigure.IPList[i].Port = Convert.ToInt16(this.DownLinkPortTxt.Text);
                        }
                        break;
                    case "ATP1":
                        if (!(IPConfigure.IPList[i].IP == IPAddress.Parse(this.ATP1IPTxt.Text) &&
                            IPConfigure.IPList[i].Port == Convert.ToInt16(this.ATP1PortTxt.Text)))
                        {
                            IniWriteValue("ATP1", "IP", this.LocalIPTxt.Text);
                            IniWriteValue("ATP1", "Port", this.LocalIPTxt.Text);
                            IPConfigure.IPList[i].IP = IPAddress.Parse(this.ATP1IPTxt.Text);
                            IPConfigure.IPList[i].Port = Convert.ToInt16(this.ATP1PortTxt.Text);
                        }
                        break;
                    case "ATP2":
                        if (!(IPConfigure.IPList[i].IP == IPAddress.Parse(this.ATP2IPTxt.Text) &&
                            IPConfigure.IPList[i].Port == Convert.ToInt16(this.ATP2PortTxt.Text)))
                        {
                            IniWriteValue("ATP2", "IP", this.LocalIPTxt.Text);
                            IniWriteValue("ATP2", "Port", this.LocalIPTxt.Text);
                            IPConfigure.IPList[i].IP = IPAddress.Parse(this.ATP2IPTxt.Text);
                            IPConfigure.IPList[i].Port = Convert.ToInt16(this.ATP2PortTxt.Text);
                        }
                        break;
                    case "ATP3":
                        if (!(IPConfigure.IPList[i].IP == IPAddress.Parse(this.ATP3IPTxt.Text) &&
                             IPConfigure.IPList[i].Port == Convert.ToInt16(this.ATP3PortTxt.Text)))
                        {
                            IniWriteValue("ATP3", "IP", this.LocalIPTxt.Text);
                            IniWriteValue("ATP3", "Port", this.LocalIPTxt.Text);
                            IPConfigure.IPList[i].IP = IPAddress.Parse(this.ATP3IPTxt.Text);
                            IPConfigure.IPList[i].Port = Convert.ToInt16(this.ATP3PortTxt.Text);
                        }
                        break;
                    case "ATP4":
                        if (!(IPConfigure.IPList[i].IP == IPAddress.Parse(this.ATP4IPTxt.Text) &&
                             IPConfigure.IPList[i].Port == Convert.ToInt16(this.ATP4PortTxt.Text)))
                        {
                            IniWriteValue("ATP4", "IP", this.LocalIPTxt.Text);
                            IniWriteValue("ATP4", "Port", this.LocalIPTxt.Text);
                            IPConfigure.IPList[i].IP = IPAddress.Parse(this.ATP4IPTxt.Text);
                            IPConfigure.IPList[i].Port = Convert.ToInt16(this.ATP4PortTxt.Text);
                        }
                        break;
                    default:
                        break;
                }
            }
            if (isLocalChanged)
            {
                UpdateSocket();
            }
        }

        private void UpdateSocket()
        {
            main.Receive.thread.Abort();
            ZCSocket.socketMain.Close();
            ZCSocket.socketMain.Dispose();
            main.Receive = new ZCSocket();
            main.Receive.Start();
            this.Close();
            this.Dispose();
        }

        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.iniPath);
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
