using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace XRect
{
    class LineBoxBlue : LineBox
    {
        public LineBoxBlue()
            :base()
        {
            _mainLine.Stroke = Brushes.Blue;
        }
    }
}
