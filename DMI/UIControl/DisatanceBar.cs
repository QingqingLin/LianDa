using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace DMI
{
    public partial class DisatanceBar : UserControl
    {
        Bitmap background;
        public delegate void vaule_change_invoke(EventArgs args);
        public event vaule_change_invoke value_change;//值改变时触发的事件


        private float _distance;
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
                //重绘控件
                this.Invalidate();
                if (value_change != null)
                {
                    //触发事件
                    this.value_change(new EventArgs());
                }
            }
        }



        public DisatanceBar()
        {
            InitializeComponent();
            Distance = 750;
            setinfo();
            //这里触发一次OnInvalidated，保证在onpaint前建立背景位图
            this.Invalidate();
        }

        private void setinfo()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        protected override void OnResize(EventArgs e)
        {
            build_bitmap_buffer();
            this.Invalidate();
            base.OnResize(e);
        }

        /// <summary>
        /// 控件及子控件重绘
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        /// <summary>
        /// 控件重绘
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInvalidated(InvalidateEventArgs e)
        {

            if (background != null)
                update_the_progress_bar();
            else
            {
                build_bitmap_buffer();
            }
            base.OnInvalidated(e);
        }

        /// <summary>
        /// 创建背景缓冲位图
        /// </summary>
        private void build_bitmap_buffer()
        {
            if (background != null)
            {
                background.Dispose();
            }
            background = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(background);
            g.Clear(Color.Transparent);
            g.Dispose();
        }

        /// <summary>
        /// 根据当前值更新进度条
        /// </summary>
        private void update_the_progress_bar()
        {
            Graphics g = Graphics.FromImage(background);
            Draw_with_HighQuality(g);
            g.Clear(Color.Transparent);

            PointF p1 = new PointF();
            PointF p2 = new PointF();
            p1.X = p2.X = 15;
            p1.Y = 260 - 20;
            p2.Y = (float)(260 - (20 + Math.Log10(Distance) * (260 - 30) / Math.Log10(750)));
            Pen distancePen = new Pen(Color.Yellow, 20);
            g.DrawLine(distancePen, p1, p2);

            g.Dispose();
            this.BackgroundImage = background;
        }

        /// <summary>
        /// 高质量绘图
        /// </summary>
        /// <param name="g">绘图工具graphics</param>
        private void Draw_with_HighQuality(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
        }
    }
}
