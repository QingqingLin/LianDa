using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;


namespace ZC
{
    public class ZCSocket
    {
        public static Socket socketMain = null;
        private IPAddress HostIP;
        private int HostPort;
        public static IPAddress DIP;
        public static int Dport;
        public bool runningFlag;
        byte[] receiveDataArray = new byte[200];
        public Thread thread;
        private byte[] DATA;

        public ZCSocket()
        {
            SetHostIPAndPort();
            IPEndPoint IPEP = new IPEndPoint(HostIP, HostPort);
            EndPoint EP = (EndPoint)IPEP;
            socketMain = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socketMain.Bind(EP);
            CancelReceiveException();
        }

        public void CancelReceiveException()
        {
            uint IOC_IN = 0x80000000; uint IOC_VENDOR = 0x18000000;
            uint SIO_UDP_CONNRESET = IOC_IN | IOC_VENDOR | 12;
            socketMain.IOControl((int)SIO_UDP_CONNRESET, new byte[] { Convert.ToByte(false) }, null);
        }


        public void SetHostIPAndPort()
        {
            foreach (var item in IPConfigure.IPList)
            {
                if (item.DeviceName == "ZC")
                {
                    HostIP = item.IP;
                    HostPort = item.Port;
                }
            }
        }

        public void Start()
        {
            runningFlag = true;
            thread = new Thread(ListenControlData);
            thread.IsBackground = true;
            thread.Start();
        }

        public void ListenControlData()
        {
            IPEndPoint ipEP = new IPEndPoint(HostIP, HostPort);
            EndPoint EP = (EndPoint)ipEP;

            while (runningFlag)
            {
                int nRecv = socketMain.ReceiveFrom(receiveDataArray, ref EP);
                SaveData(receiveDataArray);
            }
        }
        
        private void SaveData(byte[] receiveDataArray)
        {
            DATA = receiveDataArray;
            if (DATA != null)
            {
                VOBCorCI VOBCorCI = new VOBCorCI(DATA);
            }
            Array.Clear(receiveDataArray, 0, receiveDataArray.Length);
        }

        public static void SendControlData(byte[] sendControlPacket, int packetLength)
        {
            IPEndPoint ipep = new IPEndPoint(DIP, Dport);
            socketMain.SendTo(sendControlPacket, 0, packetLength, SocketFlags.None, ipep);
        }
    }
}
