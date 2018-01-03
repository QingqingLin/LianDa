using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MMI.UIControl
{
    public partial class M5 : UserControl
    {
        public M5()
        {
            InitializeComponent();
        }


        public enum StopWindow
        {
            ATPwindow,
            ATOwindow,
        }
        private StopWindow stopwindow = StopWindow.ATPwindow;

        public StopWindow Stopwindow
        {
            get { return stopwindow; }
            set 
            { 
                stopwindow = value;
                this.Refresh();
            }
        }

        public enum OpenType
        { 

            bothOpen,
            firstLeftOpen,
            firstRightOpen,
        }
        private OpenType opentype = OpenType.bothOpen;

        public OpenType Opentype
        {
            get { return opentype; }
            set 
            { 
                opentype = value;
                this.Refresh();
            }
        }

        private void M5_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            SolidBrush m5Brush = new SolidBrush(Color.LightGray);

            //两个方形
            g.FillRectangle(m5Brush, 36, 10, 12, 40);
            g.FillRectangle(m5Brush, 52, 10, 12, 40);


            SolidBrush m5RightBrush, m5LeftBrush;
         
            switch ((int)Opentype)
            {
                case 0:
                    {
                        if (Stopwindow == StopWindow.ATPwindow)
                        {
                            m5LeftBrush = new SolidBrush(Color.Yellow);
                            m5RightBrush = new SolidBrush(Color.Yellow);
                        }
                        else
                        {
                            m5LeftBrush = new SolidBrush(Color.Green);
                            m5RightBrush = new SolidBrush(Color.Green);
                        }
                        
                        break;
                    }
                case 1:
                    {
                        if (Stopwindow == StopWindow.ATPwindow)
                        {
                            m5LeftBrush = new SolidBrush(Color.Yellow);
                            m5RightBrush = new SolidBrush(Color.LightGray);
                        }
                        else
                        {
                            m5LeftBrush = new SolidBrush(Color.Green);
                            m5RightBrush = new SolidBrush(Color.LightGray);
                        }


                        break;
                    }
                case 2:
                    {
                        if (Stopwindow == StopWindow.ATPwindow)
                        {
                            m5LeftBrush = new SolidBrush(Color.LightGray);
                            m5RightBrush = new SolidBrush(Color.Yellow);
                        }
                        else
                        {
                            m5LeftBrush = new SolidBrush(Color.LightGray);
                            m5RightBrush = new SolidBrush(Color.Green);
                        }
                        break;

                    }
                default:
                        {

                            m5LeftBrush = new SolidBrush(Color.LightGray);
                            m5RightBrush = new SolidBrush(Color.LightGray);
                            break;
                        }
            }
            //两个三角形
            Point[] leftPointArr = new Point[] { new Point(20, 30), new Point(33, 10), new Point(33, 50) };
            Point[] rightPointArr = new Point[] { new Point(80, 30), new Point(67, 10), new Point(67, 50) };

            g.FillPolygon(m5LeftBrush, leftPointArr);

            g.FillPolygon(m5RightBrush, rightPointArr);
        }
    }
}
