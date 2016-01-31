using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XRect
{
    class RectBoxRed : RectBox 
    {
        public RectBoxRed()
            : base()
        {
            rect.StrokeThickness = 1;
            rect.Fill = Brushes.DarkRed;
            rect.Stroke = Brushes.Black;
            rect.StrokeDashArray = new DoubleCollection() { 3 }; 
        }
    }
}
