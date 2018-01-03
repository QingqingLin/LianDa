using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMI.UIControl
{
    public partial class C5 : UserControl
    {
        public C5()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);  
        }

        private bool isOppositeCommunicationRight=true;

        public bool IsOppositeCommunicationRight
        {
            get { return isOppositeCommunicationRight; }
            set 
            { 
                isOppositeCommunicationRight = value;
                this.Refresh();
            }
        }



        private void C5_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            SolidBrush c5Brush = new SolidBrush(Color.Green);
            Point[] pointArr1 = new Point[] { new Point(30, 10), new Point(17, 20), new Point(43, 20) };
            Point[] pointArr2 = new Point[] { new Point(30, 50), new Point(17, 40), new Point(43, 40) };
            g.FillPolygon(c5Brush, pointArr1);
            c5Brush.Color = Color.LightGray;
            g.FillRectangle(c5Brush, 17, 20, 26, 20);
            if (IsOppositeCommunicationRight)
            {
                c5Brush.Color = Color.White;
            }
            else
            {
                c5Brush.Color = Color.Red;
            }
            
            g.FillPolygon(c5Brush, pointArr2);
        }
    }
}
