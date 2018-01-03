using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace DMI
{
    //配置端口和IP地址
    public class ConfigurationData
    {
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);
        private static string filePath = Application.StartupPath + "\\IP-Port-List.ini";//获取INI文件路径
        private static string sectionVOBC = "DMI"; //INI文件名
        private static string sectionZC = "ATP"; //INI文件名  

        // 自定义读取INI文件中的内容方法
        private static string ContentValue(string Section, string key)
        {
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(Section, key, "", temp, 1024, filePath);
            return temp.ToString();
        }
        public static void ReadConfigData()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("未找到配置文件，DMI将不能运行！");
                    return;
                }
                else
                {
                    Form1.sourceIP = ContentValue(sectionVOBC, "IP");
                    Form1.sourcePort = ContentValue(sectionVOBC, "port");
                    Form1.desIP = ContentValue(sectionZC, "IP");
                    Form1.desPort = ContentValue(sectionZC, "port");
                }
            }
            catch
            {
                MessageBox.Show("配置文件中有错误，请修改，并重新启动！配置文件路径为：" + filePath);
                System.Environment.Exit(0);
            }
        }
    }
}
