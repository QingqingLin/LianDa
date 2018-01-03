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
    public partial class M9 : UserControl
    {
        public M9()
        {
            InitializeComponent();
        }

        public enum OperatingStatus
        {
            normal,
            ATPfailure,
            ATOfailure,
            RADfailure
        } ;

        //Point 
        //public override Point Location
        //{ 
        
        //}

        private OperatingStatus operatingStatus = OperatingStatus.ATOfailure;

        public OperatingStatus OperateStatus
        {
            get { return operatingStatus; }
            set
            { 
                operatingStatus = value;
                this.Refresh();
              
            }
        }


        private void M9_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            Font m9Font = new Font("Arial", 15, FontStyle.Bold);
            Pen m9Pen = new Pen(Color.Yellow, 4);
            string[] mode = { "ATP", "ATO", "RAD" };
            string presentMode = "ATP";
            switch ((int)operatingStatus)
            {
                case 0:
                    {
                        g.Clear(Color.Black);

                        break;
                    }
                case 1:
                    {
                        
                        presentMode =string.Copy(mode[0]);
                        m9Pen.Color = Color.Yellow;
                        g.DrawString(presentMode, m9Font, Brushes.White, new Point(25, 20));

                        Rectangle rect = new Rectangle(20, 10, 60, 40);
                        g.DrawRectangle(m9Pen, rect);
                        g.DrawLine(m9Pen, 20, 10, 80, 50);
                        g.DrawLine(m9Pen, 20, 50, 80, 10);
                        break;
                    }
                case 2:
                    {
                        
                        presentMode = string.Copy(mode[1]);
                        m9Pen.Color = Color.Red;
                        g.DrawString(presentMode, m9Font, Brushes.White, new Point(25, 20));

                        Rectangle rect = new Rectangle(20, 10, 60, 40);
                        g.DrawRectangle(m9Pen, rect);
                        g.DrawLine(m9Pen, 20, 10, 80, 50);
                        g.DrawLine(m9Pen, 20, 50, 80, 10);
                        break;
                    }
                case 3:
                    {
                        
                        presentMode = string.Copy(mode[2]);
                        m9Pen.Color = Color.Red;
                        g.DrawString(presentMode, m9Font, Brushes.White, new Point(25, 20));

                        Rectangle rect = new Rectangle(20, 10, 60, 40);
                        g.DrawRectangle(m9Pen, rect);
                        g.DrawLine(m9Pen, 20, 10, 80, 50);
                        g.DrawLine(m9Pen, 20, 50, 80, 10);
                        break;
                    }
                default:
                    {
                        g.Clear(Color.Black);
                        break;
                    
                    }
                   

            }





 
        }
    }
}
