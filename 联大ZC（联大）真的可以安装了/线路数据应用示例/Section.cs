using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using 线路绘图工具;

namespace ZC
{
    public class Section : 线路绘图工具.Section, ICheckDistance, IRailway
    {
        static Pen DefaultPen_ = new Pen(Brushes.Cyan, 2.9);
        static Pen AxleOccupyPen_ = new Pen(Brushes.Red, 3);
        static Pen TrainOccpyPen_ = new Pen(Brushes.Yellow, 3);
        static Pen NonComTrainOccupy_ = new Pen(Brushes.Purple, 3);
        static Pen AccessPen_ = new Pen(Brushes.Green, 3);

        public bool IsAccessLock { get; set; }

        public double LeftDistance { get; set; }
        public double RightDistance { get; set; }
        private int _Offset = 130;
        public int Offset 
        { 
            get {return _Offset; }
            set
            {
                if (_Offset != value)
                {
                    _Offset = value;
                }
            }
        }

        public int Direction { get; set; } //1表示上行，0表示下行

        private int _AxleOccpy = 0;
        public int AxleOccupy 
        {
            get {return _AxleOccpy; } 
            set 
            {                 
                if (_AxleOccpy != value)
                {
                    _AxleOccpy = value;
                }
            } 
        } //1表示计轴检测为空闲，0表示计轴检测为占用

        List<byte> TrainOccupy_ = new List<byte>();
        public List<byte> TrainOccupy
        {
            get { return TrainOccupy_; }
            set
            {
                
                    if (TrainOccupy_ != value)
                    {
                        TrainOccupy_ = value;
                    }
                }
            
        }//1表示空闲，0表示占用

        public bool IsDistanceIn(double distance)
        {
            return distance <= LeftDistance && distance >= RightDistance;
        }

        List<byte> isAccess = new List<byte>();
        public List<byte> IsAccess
        {
            get { return isAccess; }
            set
            {
                if (isAccess != value)
                {
                    isAccess = value;
                }
            }
        }

        List<byte> hasNonComTrain_ = new List<byte>();
        public List<byte> HasNonComTrain
        {
            get { return hasNonComTrain_; }
            set
            {
                if (hasNonComTrain_ != value)
                {
                    hasNonComTrain_ = value;
                }
            }
        }

        List<byte> isFrontLogicOccupy_ = new List<byte>();
        public List<byte> IsFrontLogicOccupy
        {
            get { return isFrontLogicOccupy_; }
            set
            {
                if (isFrontLogicOccupy_ != value)
                {
                    isFrontLogicOccupy_ = value;
                }
            }
        }

        List<byte> isLastLogicOccupy_ = new List<byte>();
        public List<byte> IsLastLogicOccupy
        {
            get { return isLastLogicOccupy_; }
            set
            {
                if (isLastLogicOccupy_ != value)
                {
                    isLastLogicOccupy_ = value;
                }
            }
        }

        public Graphic InsuLine { get; set; }

        protected override void OnRender(System.Windows.Media.DrawingContext dc)
        {
            foreach (Line line in graphics_)
            {
                dc.DrawLine(DefaultPen_, line.Points[0], line.Points[1]);
                if (HasNonComTrain.Count != 0)
                {
                    dc.DrawLine(NonComTrainOccupy_, line.Points[0], line.Points[1]);
                }
                else
                {
                    System.Windows.Point Middle = new System.Windows.Point((line.Points[0].X + line.Points[1].X) / 2, (line.Points[0].Y + line.Points[1].Y) / 2);
                    if (isAccess.Count != 0)
                    {
                        dc.DrawLine(AccessPen_, line.Points[0], line.Points[1]);
                    }
                    else if (_AxleOccpy == 0)
                    {
                        dc.DrawLine(AxleOccupyPen_, line.Points[0], line.Points[1]);
                    }
                    if (IsFrontLogicOccupy.Count != 0 && IsLastLogicOccupy.Count != 0)
                    {
                        dc.DrawLine(TrainOccpyPen_, line.Points[0], line.Points[1]);
                    }
                    else if (IsFrontLogicOccupy.Count != 0 && IsLastLogicOccupy.Count == 0)
                    {
                        dc.DrawLine(TrainOccpyPen_, line.Points[0], Middle);
                    }
                    else if (IsFrontLogicOccupy.Count == 0 && IsLastLogicOccupy.Count != 0)
                    {
                        dc.DrawLine(TrainOccpyPen_, Middle, line.Points[1]);
                    }
                    else if (_AxleOccpy == 0)
                    {
                        dc.DrawLine(AxleOccupyPen_, line.Points[0], line.Points[1]);
                    }
                }
            }
            dc.DrawText(formattedName_, namePoint_);

            if (InsuLine is Line)
            {
                (InsuLine as Line).OnRender(dc, DefaultPen_);
            }
        }

        public void AddInsulation()
        {
            List<Point> leftPts = new List<Point>();
            GetLeftPoints(leftPts);

            InsuLine = new Line()
            {
                //X0 = leftPts[0].X,
                //Y0 = leftPts[0].Y - Line.LineThickness,
                //X1 = leftPts[0].X,
                //Y1 = leftPts[0].Y + Line.LineThickness,
                Pt0 = new Point(leftPts[0].X, leftPts[0].Y - Line.LineThickness),
                Pt1 = new Point(leftPts[0].X, leftPts[0].Y + Line.LineThickness),

            };

        }
    }
}
