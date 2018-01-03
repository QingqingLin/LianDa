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
    public partial class M3 : UserControl
    {
        public M3()
        {
            InitializeComponent();
            this.BackColor = Color.Black;
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);  
        }
        private bool isAR = false;

        public bool IsAR
        {
            get { return isAR; }
            set 
            { 
                isAR = value;
                this.Refresh();
            }
        }

        private void M3_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen m3Pen;
            if (IsAR)
            {
                m3Pen = new Pen(Color.Yellow, 6);
            }
            else
            {
                m3Pen = new Pen(Color.Green, 6);
            }
            
            Point[] pointArr1 = new Point[] { new Point(20, 30), new Point(45, 30), new Point(60, 20), new Point(80, 20)};
            Point[] pointArr2 = new Point[] { new Point(20, 30), new Point(45, 30), new Point(60, 40), new Point(80, 40) };
            g.DrawLines(m3Pen, pointArr1);
            g.DrawLines(m3Pen, pointArr2);

        }
    }
}
