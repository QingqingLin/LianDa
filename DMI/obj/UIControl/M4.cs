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
    public partial class M4 : UserControl
    {
        public M4()
        {
            InitializeComponent();
            this.BackColor = Color.Black;
        }

        private bool isReachStopPlace;

        public bool IsReachStopPlace
        {
            get { return isReachStopPlace; }
            set 
            { 
                isReachStopPlace = value;
                this.Refresh();
            }
        }
        private void M4_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            SolidBrush m4Brush = new SolidBrush(Color.Red);
            Pen m4Pen;
            if (IsReachStopPlace)
            {
                m4Pen = new Pen(Color.Green, 4);
                m4Brush.Color = Color.Green;
            }
            else
            {
                m4Pen = new Pen(Color.Red, 4);
                m4Brush.Color = Color.Red;
            }
            
            Point[] pointArr = new Point[] { new Point(15, 35), new Point(25, 10), new Point(75, 10), new Point(85, 35) };
            g.FillPolygon(m4Brush, pointArr);
            g.FillEllipse(m4Brush, 25, 30, 12, 12);
            g.FillEllipse(m4Brush, 63, 30, 12, 12);
            g.DrawLine(m4Pen, 63, 45, 75, 45);
        }
    }
}
