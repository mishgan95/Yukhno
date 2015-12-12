using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace XRect
{
    class LineBox
    {
        protected Rectangle _Rect1;
        protected Rectangle _Rect2;
        protected Line _mainLine;

        public static bool isDraw { get; set; }

        public Rectangle Rect1
        {
            get { return _Rect1; }
            set { _Rect1 = value; }
        }

        public Rectangle Rect2
        {
            get { return _Rect2; }
            set { _Rect2 = value; }
        }

        public Line mainLine
        {
            get { return _mainLine; }
            set { _mainLine = value; }
        }

        public LineBox()
        {
            _mainLine = new Line();
            _mainLine.StrokeThickness = 2;
        }

        public void Rect2_Move(Object sender, MouseEventArgs e)
        {
            _mainLine.X2 = Canvas.GetLeft(Rect2) + Rect2.Width/2;
            _mainLine.Y2 = Canvas.GetTop(Rect2) + Rect2.Height/2;
        }
        public void Rect1_Move(Object sender, MouseEventArgs e)
        {
            _mainLine.X1 = Canvas.GetLeft(Rect1) + Rect1.Width / 2;
            _mainLine.Y1 = Canvas.GetTop(Rect1) + Rect1.Height / 2;
        }
    }
}
