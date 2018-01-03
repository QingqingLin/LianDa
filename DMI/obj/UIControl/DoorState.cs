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
    public partial class DoorState : UserControl
    {
        public DoorState()
        {
            InitializeComponent();
            this.BackColor = Color.Black;
        }

        public enum DoorStateInformation
        {

            open,
            close
        };
        private DoorStateInformation _doorStatus = DoorStateInformation.close;

        public DoorStateInformation DoorStatus
        {
            get { return _doorStatus; }
            set 
            {
                _doorStatus = value;
                this.Refresh();
            }
        }

 

        private void DoorState_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            SolidBrush doorBrush = new SolidBrush(Color.Yellow);

            if (DoorStatus == DoorStateInformation.open)
            {
                Pen doorPen = new Pen(Color.Yellow,2);
                g.DrawRectangle(doorPen, 13, 5, 20, 50);
                g.DrawRectangle(doorPen, 67, 5, 20, 50);
                g.FillRectangle(doorBrush, 17, 11, 12, 38);
                g.FillRectangle(doorBrush, 71, 11, 12, 38);
            }
            else
            {
                g.FillRectangle(doorBrush, 28, 5, 20, 50);
                g.FillRectangle(doorBrush, 52, 5, 20, 50);
            }

        }
    }
}
