using System;
using System.Drawing;
using System.Windows.Forms;

namespace DMI
{
    public partial class TargetDistance_1 : UserControl
    {
        private int[] rulingArr = new int[] { 1, 2, 5, 10, 20, 50, 100, 200, 500, 750 };   
        public TargetDistance_1()
        {            
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
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
    }
}
