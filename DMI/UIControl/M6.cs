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
    public partial class M6 : UserControl
    {
        public M6()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);  
        }

        private void M6_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            SolidBrush m6Brush = new SolidBrush(Color.LightGray);
            Point[] pointArr1=new Point[]{new Point(33,10),new Point(48,30),new Point(33,50)};
            Point[] pointArr2=new Point[]{new Point(67,10),new Point(52,30),new Point(67,50)};
            g.FillRectangle(m6Brush, 13, 10, 15, 40);
            g.FillRectangle(m6Brush, 72, 10, 15, 40);
            m6Brush.Color = Color.Yellow;
            g.FillPolygon(m6Brush, pointArr1);
            g.FillPolygon(m6Brush, pointArr2);
        }
    }
}
