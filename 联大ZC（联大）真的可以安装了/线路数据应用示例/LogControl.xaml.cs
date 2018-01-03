using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 线路数据应用示例
{
    /// <summary>
    /// LogControl.xaml 的交互逻辑
    /// </summary>
    public partial class LogControl : UserControl, INotifyPropertyChanged
    {
        public LogControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        private List<string> logText;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<string> LogText
        {
            get { return logText; }
            set { logText = value;
                OnPropertyChanged("LogText");}
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
