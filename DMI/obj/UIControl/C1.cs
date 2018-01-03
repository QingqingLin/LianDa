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
    public partial class C1 : UserControl
    {
        public C1()
        {
            InitializeComponent();
        }
        public enum TractionBrakingState
        {
            traction, brake
        };
        TractionBrakingState tractionBrakingState=TractionBrakingState.traction;

        public TractionBrakingState TractionAndBrakingState
        {
            get { return tractionBrakingState; }
            set 
            {    
                tractionBrakingState = value;
                this.Refresh();
            }
        }


        private void C1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            Pen c1Pen = new Pen(Color.LightGray, 2);
            SolidBrush c1Brush = new SolidBrush(Color.Black);
            Font c1Font = new Font("Arial", 20, FontStyle.Bold);
            g.DrawLine(c1Pen,new Point(20,30),new Point(100,30));
            c1Pen.Width=25;
            g.DrawLine(c1Pen,new Point(30,30),new Point(90,30));
            g.FillEllipse(c1Brush, 40, 10, 40, 40);
            c1Pen.Width=3;
            if (TractionAndBrakingState == TractionBrakingState.traction)
            {
                g.DrawEllipse(c1Pen, 41, 11, 38, 38);
                c1Brush.Color = Color.LightGray;
                g.DrawString("M", c1Font, c1Brush, new Point(45, 16));
            }
            else
            {
                g.DrawEllipse(c1Pen, 43, 13, 34, 34);
                c1Brush.Color = Color.LightGray;
                g.DrawString("B", c1Font, c1Brush, new Point(45, 16));
            }
            ////g.DrawEllipse(c1Pen,43,13,34,34);
            //g.DrawEllipse(c1Pen, 41, 11,38, 38);
            //c1Brush.Color=Color.LightGray;
            //g.DrawString("M",c1Font,c1Brush,new Point(45,16));
        }
    }
}
