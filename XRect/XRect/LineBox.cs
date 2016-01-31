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
        public static bool isDraw;
        public Rectangle rect1 { get; set; }
        public Rectangle rect2 { get; set; }
        public Line lineMain { get; set; }

        public LineBox()
        {
            lineMain = new Line();
            lineMain.StrokeThickness = 2;
        }

        public void Rect2_Move(Object sender, MouseEventArgs e)
        {
            lineMain.X2 = Canvas.GetLeft(rect2) + rect2.Width/2;
            lineMain.Y2 = Canvas.GetTop(rect2) + rect2.Height/2;
        }
        public void Rect1_Move(Object sender, MouseEventArgs e)
        {
            lineMain.X1 = Canvas.GetLeft(rect1) + rect1.Width / 2;
            lineMain.Y1 = Canvas.GetTop(rect1) + rect1.Height / 2;
        }


        public void Rect2_Move()
        {
            lineMain.X2 = Canvas.GetLeft(rect2) + rect2.Width / 2;
            lineMain.Y2 = Canvas.GetTop(rect2) + rect2.Height / 2;
        }
        public void Rect1_Move()
        {
            lineMain.X1 = Canvas.GetLeft(rect1) + rect1.Width / 2;
            lineMain.Y1 = Canvas.GetTop(rect1) + rect1.Height / 2;
        }
    }
}
