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
    public partial class B_AGauge : UserControl
    {
        #region//属性和字段
        private float _curSpeed;
        private float _recommedSpeed;
        private float _limitSpeed;
        private Bitmap backBmp=new Bitmap(300,300);

        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("限制速度")]
        public float LimitSpeed
        {
            get 
            {
                if (_limitSpeed > 110)
                {
                    _limitSpeed = 110;
                }
                else if (_limitSpeed < 0)
                {
                    _limitSpeed = 0;
                }
                return _limitSpeed; 
            }
            set 
            {
                if (value > 110)
                {
                    _limitSpeed = 110;
                }
                else if (value < 0)
                {
                    _limitSpeed = 0;
                }
                else
                {
                    _limitSpeed = value;
                }
            }
        }

        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("推荐速度")]
        public float RecommedSpeed
        {
            get
            {
                if (_recommedSpeed > 110)
                {
                    _recommedSpeed = 110;
                }
                else if (_recommedSpeed < 0)
                {
                    _recommedSpeed = 0;
                }
                return _recommedSpeed;
            }
            set
            {
                if (value > 110)
                {
                    _recommedSpeed = 110;
                }
                else if (value < 0)
                {
                    _recommedSpeed = 0;
                }
                else
                {
                    _recommedSpeed = value;
                }
            }
        }

        [System.ComponentModel.Browsable(true),
        System.ComponentModel.Category("AGauge"),
        System.ComponentModel.Description("当前速度")]
        public float CurSpeed
        {
            get
            {
                if (_curSpeed > 110)
                {
                    _curSpeed = 110;
                }
                else if (_curSpeed < 0)
                {
                    _curSpeed = 0;
                }
                return _curSpeed;
            }
            set
            {
                if (value > 110)
                {
                    _curSpeed = 110;
                }
                else if (value < 0)
                {
                    _curSpeed = 0;
                }
                else
                {
                    _curSpeed = value;
                }
            }
        }
        #endregion
        public B_AGauge()
        {
            InitializeComponent();
        }

        private void B_AGauge_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = Graphics.FromImage(backBmp);
            Pen rulingPen = new Pen(Color.White, 2);
            SolidBrush strBrush = new SolidBrush(Color.White);
            Font strFont = new Font("Arial", 14, FontStyle.Bold);

            float _stepAngle = 15;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            #region//绘制刻度及刻度值
            for (int i = 0; i < 23; i++)
            {
                float offsetX, offsetY;
                PointF p1 = new PointF();
                PointF p2 = new PointF();
                p1.X = 130;
                p1.Y = p2.Y = _stepAngle * (i + 1);
                if (i % 2 == 0)
                {
                    p2.X = 110;
                }
                else
                {
                    p2.X = 120;
                }
                p1 = ORtoXY(p1);
                p2 = ORtoXY(p2);
                g.DrawLine(rulingPen, p1, p2);
                switch (i)
                {
                    case 0: offsetX = -8; offsetY = -24;
                        break;
                    case 2: offsetX = -10; offsetY = -24;
                        break;
                    case 4: offsetX = -3; offsetY = -15;
                        break;
                    case 6: offsetX = -2; offsetY = -10;
                        break;
                    case 8: offsetX = -5; offsetY = -4;
                        break;
                    case 10: offsetX = -14; offsetY = 0;
                        break;
                    case 12: offsetX = -18; offsetY = 0;
                        break;
                    case 14: offsetX = -22; offsetY = 0;
                        break;
                    case 16: offsetX = -30; offsetY = -10;
                        break;
                    case 18: offsetX = -28; offsetY = -15;
                        break;
                    case 20: offsetX = -25; offsetY = -25;
                        break;
                    case 22: offsetX = -25; offsetY = -25;
                        break;
                    default: offsetX = 0; offsetY = 0;
                        break;

                }
                p2.X = p2.X + offsetX;
                p2.Y = p2.Y + offsetY;
                if (i % 2 == 0)
                {
                    g.DrawString((i * 5).ToString(), strFont, strBrush, p2);
                }
            }
            #endregion
            rulingPen.Dispose();
            strBrush.Dispose();
            strFont.Dispose();

        }

        #region//极坐标转换为直角坐标
        private PointF ORtoXY(PointF or)
        {
            float x;
            float y;
            PointF p = new PointF();
            float r = or.X;
            float angle = or.Y;

            x = (float)(150 - r * Math.Sin(angle / 360 * (2 * Math.PI)));
            y = (float)(150 + r * Math.Cos(angle / 360 * (2 * Math.PI)));

            p.X = x;
            p.Y = y;

            return p;
        }
        #endregion

        private void B_picBox_Paint(object sender, PaintEventArgs e)
        {
            Pen needlePen = new Pen(Color.White);
            SolidBrush recommendBrush = new SolidBrush(Color.Yellow);
            SolidBrush limitBrush = new SolidBrush(Color.Red);
            SolidBrush strBrush = new SolidBrush(Color.White);
            Font speedFont = new Font("Arial", 17, FontStyle.Bold);
            float _stepAngle = 15;

            #region//绘制指针
            Graphics g = e.Graphics;
            g.Clear(this.BackColor);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.DrawImage(backBmp, 0, 0);
            
            needlePen.Width = 2;
            g.DrawEllipse(needlePen, 150 - 20, 150 - 20, 40, 40);
            PointF speedPos = new PointF();
            if ((int)(_curSpeed + 0.5) / 100 != 0)
            {
                speedPos.X = 150 - 23;
                speedPos.Y = 150 - 12;
                g.DrawString(((int)(_curSpeed + 0.5)).ToString(), speedFont, strBrush, speedPos);
            }
            else if ((int)(_curSpeed + 0.5) / 10 != 0)
            {
                speedPos.X = 150 - 16;
                speedPos.Y = 150 - 12;
                g.DrawString(((int)(_curSpeed + 0.5)).ToString(), speedFont, strBrush, speedPos);
            }
            else
            {
                speedPos.X = 150 - 9;
                speedPos.Y = 150 - 12;
                g.DrawString(((int)(_curSpeed + 0.5)).ToString(), speedFont, strBrush, speedPos);
            }

            PointF[] needlePoint = new PointF[5];
            needlePoint[0].Y = needlePoint[1].Y = needlePoint[2].Y = needlePoint[3].Y = needlePoint[4].Y = CurSpeed / (float)120 * 360 + _stepAngle;
            needlePoint[0].X = 20;
            needlePoint[1].X = 77;
            needlePoint[2].X = 81;
            needlePoint[3].X = 85;
            needlePoint[4].X = 100;
            for (int i = 0; i < 5; i++)
            {
                needlePoint[i] = ORtoXY(needlePoint[i]);
            }


            needlePen.Width = 10;
            g.DrawLine(needlePen, needlePoint[0], needlePoint[1]);
            needlePen.Width = 7;
            g.DrawLine(needlePen, needlePoint[1], needlePoint[2]);
            needlePen.Width = 5;
            g.DrawLine(needlePen, needlePoint[2], needlePoint[3]);
            needlePen.Width = 3;
            g.DrawLine(needlePen, needlePoint[3], needlePoint[4]);
            #endregion

            #region//绘制推荐速度箭头
            PointF[] recommendArrowArr = new PointF[3];
            recommendArrowArr[0].Y = _recommedSpeed / 120 * 360 + _stepAngle;
            recommendArrowArr[0].X = 130;
            recommendArrowArr[1].X = recommendArrowArr[2].X = 145;
            recommendArrowArr[1].Y = recommendArrowArr[0].Y - 3;
            recommendArrowArr[2].Y = recommendArrowArr[0].Y + 3;
            for (int i = 0; i < 3; i++)
            {
                recommendArrowArr[i] = ORtoXY(recommendArrowArr[i]);
            }
            g.FillPolygon(recommendBrush, recommendArrowArr);
            #endregion

            #region//绘制限制速度箭头
            PointF[] limitArrowArr = new PointF[3];
            limitArrowArr[0].Y = _limitSpeed / 120 * 360 + _stepAngle;
            limitArrowArr[0].X = 130;
            limitArrowArr[1].X = limitArrowArr[2].X = 145;
            limitArrowArr[1].Y = limitArrowArr[0].Y - 3;
            limitArrowArr[2].Y = limitArrowArr[0].Y + 3;
            for (int i = 0; i < 3; i++)
            {
                limitArrowArr[i] = ORtoXY(limitArrowArr[i]);
            }
            g.FillPolygon(limitBrush, limitArrowArr);
            #endregion
            needlePen.Dispose();
            recommendBrush.Dispose();
            limitBrush.Dispose();
            speedFont.Dispose();
            strBrush.Dispose();
        }
    }
}
