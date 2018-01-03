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
    public partial class C3 : UserControl
    {
        public C3()
        {
            InitializeComponent();
        }
        private bool isTrainIntergrality=true;

        public bool IsTrainIntergrality
        {
            get { return isTrainIntergrality; }
            set 
            {
                isTrainIntergrality = value;
                this.Refresh();
            }
        }

        private void C3_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            SolidBrush m4Brush;
            if (IsTrainIntergrality)
            {
                m4Brush = new SolidBrush(Color.Green);
            }
            else
            {
                m4Brush = new SolidBrush(Color.Red);
            }
            
            Pen m4Pen = new Pen(Color.Black, 6);
            Point[] pointArr = new Point[] { new Point(15, 35), new Point(25, 10), new Point(75, 10), new Point(85, 35) };
            g.FillPolygon(m4Brush, pointArr);
            g.FillEllipse(m4Brush, 25, 30, 12, 12);
            g.FillEllipse(m4Brush, 63, 30, 12, 12);
            g.DrawLine(m4Pen, new Point(50, 0), new Point(50, 60));
        }
    }
}
