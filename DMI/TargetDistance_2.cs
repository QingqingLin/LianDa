using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMI
{
    public partial class TargetDistance_2 : UserControl
    {
        private float _distance;


        [System.ComponentModel.Browsable(true),
         System.ComponentModel.Category("TargetDistance_1"),
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
                    this.pnl_distance.Refresh();
                }

            }
        }
        public TargetDistance_2()
        {
            InitializeComponent();
            Distance = 750;
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
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
    }
}
