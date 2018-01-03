using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZC
{
    class InfoSendToCI1
    {
        public Pack MyStruct = new Pack();
        public static ushort NumToCI1 = 1;
        string[] Station = new string[] { "108", "109", "202", "205", "305", "314", "407", "410" };


        public InfoSendToCI1(byte SenderID)
        {
            WriteCIHead(SenderID);
            SetLogicState(MainWindow.stationElements_.Elements);
            MyStruct.Skip();
            SetTrainAccessInfo(AddCIAccess.CITableListTop);
            MyStruct.Skip();
            SetDataLength();
        }

        private void SetDataLength()
        {
            ushort Length = (ushort)MyStruct.byteFlag_;
            MyStruct.byteFlag_ = 6;
            MyStruct.PackUint16(Length);
            MyStruct.byteFlag_ = Length;
        }

        public void WriteCIHead(byte SenderID)
        {
            MyStruct.PackUint16(NumToCI1++);
            MyStruct.PackUint16(2);
            MyStruct.PackByte(3);
            MyStruct.PackByte(SenderID);
            MyStruct.PackUint16(0);
        }

        private void SetTrainAccessInfo(List<CItable> CITable)
        {
            foreach (var item in CITable)
            {
                bool isFind = false;
                foreach (var Section in MainWindow.stationElements_.Elements)
                {
                    if (Section.Name.Length > 2)
                    {
                        if (Section.Name.Substring(0, 3) == item.StartSection.Substring(0, 3))
                        {
                            isFind = true;
                            if ((Section as Section).TrainOccupy.Count != 0)
                            {
                                MyStruct.SetBit(true);
                            }
                            else
                            {
                                MyStruct.SetBit(false);
                            }
                            break;
                        }
                    }
                }
                if (!isFind)
                {
                    foreach (var Section in MainWindow.stationElements_1_.Elements)
                    {
                        if (Section.Name.Length > 2)
                        {
                            if (Section.Name.Substring(0, 3) == item.StartSection.Substring(0, 3))
                            {
                                if ((Section as Section).TrainOccupy.Count != 0)
                                {
                                    MyStruct.SetBit(true);
                                }
                                else
                                {
                                    MyStruct.SetBit(false);
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void SetLogicState(List<线路绘图工具.GraphicElement> Element)
        {
            foreach (var item in Element)
            {
                if (item is Section)
                {
                    if (Station.Contains((item as Section).Name.Substring(0,3)))
                    {
                        bool bitFront = (item as Section).IsFrontLogicOccupy.Count == 0 ? false : true;
                        MyStruct.SetBit(!bitFront);
                    }
                    else
                    {
                        bool bitFront = (item as Section).IsFrontLogicOccupy.Count == 0 ? false : true;
                        MyStruct.SetBit(!bitFront);
                        bool bitLast = (item as Section).IsLastLogicOccupy.Count == 0 ? false : true;
                        MyStruct.SetBit(!bitLast);
                    }
                }
            }
            foreach (var item in Element)
            {
                if (item is RailSwitch)
                {
                    bool bit = (item as RailSwitch).TrainOccupy.Count != 0 ? false : true;
                    MyStruct.SetBit(bit);
                }
            }
        }
    }
}
