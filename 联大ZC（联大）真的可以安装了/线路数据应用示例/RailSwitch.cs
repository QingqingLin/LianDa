using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using 线路绘图工具;

namespace ZC
{
    public class RailSwitch : 线路绘图工具.RailSwitch, IRailway
    {
        static Pen YellowPen_ = new Pen(Brushes.Yellow,3);
        static Pen RedPen_ = new Pen(Brushes.Red, 3);
        static Pen DefaultPen_ = new Pen(Brushes.Cyan, 3);
        static Pen PurplePen_ = new Pen(Brushes.Purple, 3);
        static Pen GreenPen_ = new Pen(Brushes.Green, 3);

        public bool IsPositionNormal { get; set; }
        public bool IsPositionReverse { get; set; }
        public bool Islock { get; set; }
        public bool IsAccessLock { get; set; }

        private int _Offset = 45;
        public int Offset
        {
            get { return _Offset;}
            set
            {
                if (_Offset != value)
                {
                    _Offset = value;
                }
            }
        }
        public int Direction { get; set; }

        private int _AxleOccpy = 0;
        public int AxleOccupy
        {
            get { return _AxleOccpy; }
            set
            {
                if (_AxleOccpy != value)
                {
                    _AxleOccpy = value;
                }
            }
        }

        private List<byte> TrainOccupy_ = new List<byte>();
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

        }

        private List<byte> isAccess = new List<byte>();
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

        public Graphic InsuLine { get; set; }

        RailSwitch()
        {
            IsPositionNormal = false;
            IsPositionReverse = false;
        }

        protected override void OnRender(System.Windows.Media.DrawingContext dc)
        {
            List<int> sectionIndexs = this.SectionIndexList[0];
            List<int> normalIndexs = SectionIndexList[1];
            List<int> reverseIndexs = SectionIndexList[2];
            if (HasNonComTrain.Count != 0)
            {
                foreach (int index in sectionIndexs)
                {
                    Line line = graphics_[index] as Line;
                    dc.DrawLine(PurplePen_, line.Points[0], line.Points[1]);
                }
                foreach (int index in normalIndexs)
                {
                    Line line = graphics_[index] as Line;
                    dc.DrawLine(PurplePen_, line.Points[0], line.Points[1]);
                }
                foreach (int index in reverseIndexs)
                {
                    Line line = graphics_[index] as Line;
                    dc.DrawLine(PurplePen_, line.Points[0], line.Points[1]);
                }
            }
            else
            {
                if (TrainOccupy_.Count > 0)
                {
                    foreach (int index in sectionIndexs)
                    {
                        Line line = graphics_[index] as Line;
                        dc.DrawLine(YellowPen_, line.Points[0], line.Points[1]);
                    }
                    if (IsPositionNormal == true && IsPositionReverse == false)
                    {
                        foreach (int index in normalIndexs)
                        {
                            Line lineNormal = graphics_[index] as Line;
                            Line linesection = graphics_[sectionIndexs[0]] as Line;
                            dc.DrawLine(YellowPen_, linesection.Points[1], lineNormal.Points[1]);
                        }

                        foreach (int index in reverseIndexs)
                        {
                            Line line = graphics_[index] as Line;
                            dc.DrawLine(DefaultPen_, line.Points[0], line.Points[1]);
                        }
                    }
                    else if (IsPositionNormal == false && IsPositionReverse == true)
                    {
                        foreach (int index in reverseIndexs)
                        {
                            Line lineReverse = graphics_[index] as Line;
                            Line lineSection = graphics_[sectionIndexs[0]] as Line;
                            dc.DrawLine(YellowPen_, lineSection.Points[1], lineReverse.Points[1]);
                        }

                        foreach (int index in normalIndexs)
                        {
                            Line line = graphics_[index] as Line;
                            dc.DrawLine(DefaultPen_, line.Points[0], line.Points[1]);
                        }
                    }
                    else
                    {
                        foreach (int index in sectionIndexs)
                        {
                            Line line = graphics_[index] as Line;
                            dc.DrawLine(YellowPen_, line.Points[0], line.Points[1]);
                        }

                        foreach (int index in normalIndexs)
                        {
                            Line line = graphics_[index] as Line;
                            dc.DrawLine(YellowPen_, line.Points[0], line.Points[1]);
                        }

                        foreach (int index in reverseIndexs)
                        {
                            Line line = graphics_[index] as Line;
                            dc.DrawLine(YellowPen_, line.Points[0], line.Points[1]);
                        }
                    }
                }
                else if (_AxleOccpy == 0)
                {
                    foreach (int index in sectionIndexs)
                    {
                        Line line = graphics_[index] as Line;
                        dc.DrawLine(RedPen_, line.Points[0], line.Points[1]);
                    }
                    if (IsPositionNormal == true && IsPositionReverse == false)
                    {
                        foreach (int index in normalIndexs)
                        {
                            Line lineNormal = graphics_[index] as Line;
                            Line linesection = graphics_[sectionIndexs[0]] as Line;
                            dc.DrawLine(RedPen_, linesection.Points[1], lineNormal.Points[1]);
                        }

                        foreach (int index in reverseIndexs)
                        {
                            Line line = graphics_[index] as Line;
                            dc.DrawLine(DefaultPen_, line.Points[0], line.Points[1]);
                        }
                    }
                    else if (IsPositionNormal == false && IsPositionReverse == true)
                    {
                        foreach (int index in reverseIndexs)
                        {
                            Line lineReverse = graphics_[index] as Line;
                            Line lineSection = graphics_[sectionIndexs[0]] as Line;
                            dc.DrawLine(RedPen_, lineSection.Points[1], lineReverse.Points[1]);
                        }
                        foreach (int index in normalIndexs)
                        {
                            Line line = graphics_[index] as Line;
                            dc.DrawLine(DefaultPen_, line.Points[0], line.Points[1]);
                        }
                    }
                    else
                    {
                        foreach (int index in sectionIndexs)
                        {
                            Line line = graphics_[index] as Line;
                            dc.DrawLine(RedPen_, line.Points[0], line.Points[1]);
                        }

                        foreach (int index in normalIndexs)
                        {
                            Line line = graphics_[index] as Line;
                            dc.DrawLine(RedPen_, line.Points[0], line.Points[1]);
                        }

                        foreach (int index in reverseIndexs)
                        {
                            Line line = graphics_[index] as Line;
                            dc.DrawLine(RedPen_, line.Points[0], line.Points[1]);
                        }
                    }
                }
                else if (isAccess.Count != 0)
                {
                    foreach (int index in sectionIndexs)
                    {
                        Line line = graphics_[index] as Line;
                        dc.DrawLine(GreenPen_, line.Points[0], line.Points[1]);
                    }
                    if (IsPositionNormal == true && IsPositionReverse == false)
                    {
                        foreach (int index in normalIndexs)
                        {
                            Line lineNormal = graphics_[index] as Line;
                            Line linesection = graphics_[sectionIndexs[0]] as Line;
                            dc.DrawLine(GreenPen_, linesection.Points[1], lineNormal.Points[1]);
                        }

                        foreach (int index in reverseIndexs)
                        {
                            Line line = graphics_[index] as Line;
                            dc.DrawLine(DefaultPen_, line.Points[0], line.Points[1]);
                        }
                    }
                    else if (IsPositionNormal == false && IsPositionReverse == true)
                    {
                        foreach (int index in reverseIndexs)
                        {
                            Line lineReverse = graphics_[index] as Line;
                            Line lineSection = graphics_[sectionIndexs[0]] as Line;
                            dc.DrawLine(GreenPen_, lineSection.Points[1], lineReverse.Points[1]);
                        }

                        foreach (int index in normalIndexs)
                        {
                            Line line = graphics_[index] as Line;
                            dc.DrawLine(DefaultPen_, line.Points[0], line.Points[1]);
                        }
                    }
                    else
                    {
                        foreach (int index in sectionIndexs)
                        {
                            Line line = graphics_[index] as Line;
                            dc.DrawLine(RedPen_, line.Points[0], line.Points[1]);
                        }

                        foreach (int index in normalIndexs)
                        {
                            Line line = graphics_[index] as Line;
                            dc.DrawLine(RedPen_, line.Points[0], line.Points[1]);
                        }

                        foreach (int index in reverseIndexs)
                        {
                            Line line = graphics_[index] as Line;
                            dc.DrawLine(RedPen_, line.Points[0], line.Points[1]);
                        }
                    }
                }
                else
                {
                    foreach (int index in sectionIndexs)
                    {
                        Line line = graphics_[index] as Line;
                        dc.DrawLine(DefaultPen_, line.Points[0], line.Points[1]);
                    }
                    if (IsPositionNormal == true && IsPositionReverse == false)
                    {
                        foreach (int index in normalIndexs)
                        {
                            Line lineNormal = graphics_[index] as Line;
                            Line linesection = graphics_[sectionIndexs[0]] as Line;
                            dc.DrawLine(DefaultPen_, linesection.Points[1], lineNormal.Points[1]);
                        }

                        foreach (int index in reverseIndexs)
                        {
                            Line line = graphics_[index] as Line;
                            dc.DrawLine(DefaultPen_, line.Points[0], line.Points[1]);
                        }
                    }
                    else if (IsPositionNormal == false && IsPositionReverse == true)
                    {
                        foreach (int index in reverseIndexs)
                        {
                            Line lineReverse = graphics_[index] as Line;
                            Line lineSection = graphics_[sectionIndexs[0]] as Line;
                            dc.DrawLine(DefaultPen_, lineSection.Points[1], lineReverse.Points[1]);
                        }

                        foreach (int index in normalIndexs)
                        {
                            Line line = graphics_[index] as Line;
                            dc.DrawLine(DefaultPen_, line.Points[0], line.Points[1]);
                        }
                    }
                    else
                    {
                        foreach (int index in sectionIndexs)
                        {
                            Line line = graphics_[index] as Line;
                            dc.DrawLine(DefaultPen_, line.Points[0], line.Points[1]);
                        }

                        foreach (int index in normalIndexs)
                        {
                            Line line = graphics_[index] as Line;
                            dc.DrawLine(DefaultPen_, line.Points[0], line.Points[1]);
                        }

                        foreach (int index in reverseIndexs)
                        {
                            Line line = graphics_[index] as Line;
                            dc.DrawLine(DefaultPen_, line.Points[0], line.Points[1]);
                        }
                    }
                }
            }

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
