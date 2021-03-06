﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace MMI.UIControl
{
    public partial class DepartSign : UserControl
    {
        public DepartSign()
        {
            InitializeComponent();
            this.BackColor = Color.Black;
        }

        private void DepartSign_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(this.BackColor);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            PointF point1 = new PointF(50f, 15f);
            PointF point2 = new PointF(35f, 45f);
            PointF point3 = new PointF(45f, 45f);
            PointF point4 = new PointF(45f, 55f);
            PointF point5 = new PointF(55f, 55f);
            PointF point6 = new PointF(55f, 45f);
            PointF point7 = new PointF(65f, 45f);
            PointF[] pol = { point1, point2, point3, point4, point5, point6, point7 };
            g.FillPolygon(new SolidBrush(Color.LightGray), pol, FillMode.Winding);
        }
    }
}
