using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace DMI.UIControl
{
    public partial class N : UserControl
    {
        public N()
        {
            InitializeComponent();
            this.BackColor = Color.Black;
            IsJumpstop = true;
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);  
        }

        private bool isJumpStop;

        public bool IsJumpstop
        {
            get { return isJumpStop; }
            set 
            {
                
                isJumpStop = value;
                this.Refresh();
 
            }
        }
        private void N_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(this.BackColor);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;


            if (IsJumpstop)
            {
                Pen nPen = new Pen(Color.Yellow, 5);
                nPen.DashStyle = DashStyle.Solid;
                nPen.EndCap = LineCap.ArrowAnchor;
                g.DrawBezier(nPen, 25, 30, 40, 5, 60, 5, 75, 30);
                nPen.Color = Color.LightGray;
                nPen.EndCap = LineCap.Custom;
                g.DrawLine(nPen, 35, 35, 65, 35);
            }
            else
            {

                Pen nPen = new Pen(Color.LightGray, 10);
                nPen.DashStyle = DashStyle.Solid;

                PointF point1 = new PointF(50f, 15f);
                PointF point2 = new PointF(35f, 45f);
                PointF point3 = new PointF(45f, 45f);
                PointF point4 = new PointF(45f, 55f);
                PointF point5 = new PointF(55f, 55f);
                PointF point6 = new PointF(55f, 45f);
                PointF point7 = new PointF(65f, 45f);
                PointF[] pol = { point1, point2, point3, point4, point5, point6, point7 };
                g.FillPolygon(new SolidBrush(Color.LightGray), pol, FillMode.Winding);
                nPen = new Pen(Color.Red, 3);
                g.DrawLine(nPen, 35, 25, 65, 50);
                g.DrawLine(nPen, 35, 50, 65, 25);
            }




        }



    }
}
