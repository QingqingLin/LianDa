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
    public partial class A1_CurState : UserControl
    {
        private float _curSpeed;
        private float _limitSpeed;

        public float LimitSpeed
        {
            get { return _limitSpeed; }
            set { _limitSpeed = value; }
        }

        public float CurSpeed
        {
            get { return _curSpeed; }
            set { _curSpeed = value; }
        }
        public A1_CurState()
        {
            InitializeComponent();
        }

        private void A1_CurState_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush curStateBrush = new SolidBrush(Color.Green);
            if (CurSpeed < LimitSpeed)
            {
                curStateBrush.Color=Color.Green;
            }
            else
            {
                curStateBrush.Color = Color.Red;
            }
            g.FillRectangle(curStateBrush, 0, 0, this.Width, this.Height);
            curStateBrush.Dispose();
            g.Dispose();
        }
    }
}
