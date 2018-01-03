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
    public partial class M10 : UserControl
    {
        public M10()
        {
            InitializeComponent();
        }


       
        private void M10_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen m10Pen = new Pen(Color.LightGray, 2);
            SolidBrush m10Brush = new SolidBrush(Color.LightGray);
            g.DrawRectangle(m10Pen, 15, 10, 70, 40);
            Point[] pointArr = new Point[] { new Point(25, 20), new Point(75, 20), new Point(80, 40), new Point(20, 40) };
            g.FillPolygon(m10Brush, pointArr);
            g.FillEllipse(m10Brush, 30, 35, 10, 10);
            g.FillEllipse(m10Brush, 60, 35, 10, 10);
        }


    }
}
