using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace XRect
{
    class LineBoxYellow : LineBox
    {
        public LineBoxYellow()
            :base()
        {
            _mainLine.Stroke = Brushes.Yellow;
        }
    }
}
