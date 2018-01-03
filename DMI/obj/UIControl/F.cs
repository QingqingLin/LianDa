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
    public partial class F : UserControl
    {
        public F()
        {
            InitializeComponent();
        }

        private void F_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen fPen = new Pen(Color.Yellow, 2);
            g.DrawRectangle(fPen, 10, 10, 230, 130);
        }
    }
}
