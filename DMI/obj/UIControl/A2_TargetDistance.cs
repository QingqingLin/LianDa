using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace MMI
{
    public partial class A2_TargetDistance : UserControl
    {
        private int[] rulingArr = new int[] { 1, 2, 5, 10, 20, 50, 100, 200, 500, 750 };
        private float _distance;
        private float _speed;

        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("TargetDistance"),
        System.ComponentModel.Description("目标速度")]
        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        
        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("TargetDistance"),
        System.ComponentModel.Description("目标距离")]
        public float Distance
        {
            get 
            {
                if (_distance < 1)
                {
                    return 1;
                }
                else if (_distance > 750)
                {
                    return 750;
                }
                else
                {
                    return _distance;
                }
            }
            set 
            {
                if (value < 1)
                {
                    _distance = 1;
                }
                else if (value > 750)
                {
                    _distance = 750;
                }
                else
                {
                    _distance = value;
                }
            }
        }
        public A2_TargetDistance()
        {
            
            InitializeComponent();
            Distance = 750;
            Speed = 100;
            
        }

        private void pnl_ruling_Paint(object sender, PaintEventArgs e)
        {
            PointF p1 = new PointF();
            PointF p2 = new PointF();
            p1.X = 5;
            p2.X = 20;
            Graphics g = e.Graphics;
            Pen rulingPen = new Pen(Color.White, 2);
            for (int i = 0; i < rulingArr.Length; i++)
            {
                p1.Y = p2.Y = (float)(260 - (20 + Math.Log10(rulingArr[i]) * (260 - 30) / Math.Log10(750)));
                g.DrawLine(rulingPen, p1, p2);
            }
            
        }

        private void pnl_distance_Paint(object sender, PaintEventArgs e)
        {
            PointF p1 = new PointF();
            PointF p2 = new PointF();
            p1.X = p2.X = 15;
            p1.Y = 260 - 20;
            p2.Y = (float)(260 - (20 + Math.Log10(Distance) * (260 - 30) / Math.Log10(750)));
            Graphics g = e.Graphics;
            Pen distancePen = new Pen(Color.Yellow, 20);
            g.DrawLine(distancePen, p1, p2);
        }

        private void pnl_str_Paint(object sender, PaintEventArgs e)
        {
            PointF p1 = new PointF();
            Font strFont = new Font("Arail", 11);
            StringFormat strformat=new StringFormat(StringFormatFlags.DirectionRightToLeft);
            Graphics g = e.Graphics;
            
            for (int i = 0; i < rulingArr.Length; i++)
            {
                p1.Y = (float)(260 - (20 + Math.Log10(rulingArr[i]) * (260 - 30) / Math.Log10(750))) - 10;
                RectangleF rect = new RectangleF(p1.X, p1.Y, pnl_str.Width, strFont.Height);
                g.DrawString(rulingArr[i].ToString(), strFont, Brushes.White, rect, strformat);
            }
            
        }

        private void pnl_targetSpeed_Paint(object sender, PaintEventArgs e)
        {
            Font strFont = new Font("Arail", 20);
            StringFormat strformat = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            Graphics g = e.Graphics;
            RectangleF rect = new RectangleF(0, 0, pnl_targetSpeed.Width, pnl_targetSpeed.Height);
            g.DrawString(Math.Round(Speed).ToString(), strFont, Brushes.White, rect, strformat);
        }
    }
}
