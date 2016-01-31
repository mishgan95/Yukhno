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
    class RectBox
    {

        public static bool isMove;

        public Rectangle rect
        {
            get { return _rect; }
        }       

        protected Rectangle _rect;         
        protected bool _isMoveThis;
        protected double _mouseDownX;
        protected double _mouseDownY;

        public RectBox()
        {
            _isMoveThis = false;
            _rect = new Rectangle();
            _rect.MouseDown += Rectangle_MouseDown;
            _rect.MouseUp += Rectangle_MouseUp;
            _rect.MouseMove += Rectangle_MouseMove;

        }

        private void Rectangle_MouseDown(object sender, MouseEventArgs e)
        {
            if (isMove) 
            {
                _isMoveThis = true; 
                _rect.CaptureMouse();
                _mouseDownX = e.GetPosition(_rect).X;
                _mouseDownY = e.GetPosition(_rect).Y;
            }
        }

        private void Rectangle_MouseUp(object sender, MouseEventArgs e)
        {
            if (isMove)
            {
                _isMoveThis = false;  
                _rect.ReleaseMouseCapture();
            }
        }

        private void Rectangle_MouseMove(object sender, MouseEventArgs e) 
        {
            if (isMove && _isMoveThis)
            {
                _rect.SetValue(Canvas.LeftProperty, e.GetPosition((Canvas)_rect.Parent).X - _mouseDownX);
                _rect.SetValue(Canvas.TopProperty, e.GetPosition((Canvas)_rect.Parent).Y - _mouseDownY);
            }

        }

    }
}
