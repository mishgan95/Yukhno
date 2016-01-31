using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XRect
{
    class ImgRec
    {
        public int x { get; set; } 
        public int y { get; set; }
        public int h { get; set; }
        public int w { get; set; }

        public ImgRec()
        {

        }

        public ImgRec(int tx, int ty, int th, int tw)
        {
            x = tx;
            y = ty;
            h = th;
            w = tw;
        }
    }
}
