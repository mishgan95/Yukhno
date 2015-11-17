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
        protected Rectangle _XRectangle; 
        protected static bool _isMove;
        protected bool _isMoveThis;
        protected double mouseDownX;
        protected double mouseDownY;

        public Rectangle XRectangle 
        {
            get { return _XRectangle; } 
        }

        public static bool isMove 
        {
            set
            { _isMove = value; }
        }

        public RectBox()  
        {
            _isMoveThis = false;
            _XRectangle = new Rectangle();
            _XRectangle.MouseDown += Rectangle_MouseDown; 
            _XRectangle.MouseUp += Rectangle_MouseUp;
            _XRectangle.MouseMove += Rectangle_MouseMove;

        }

        private void Rectangle_MouseDown(object sender, MouseEventArgs e)
        {
            if (_isMove) 
            {
                _isMoveThis = true; 
                _XRectangle.CaptureMouse();
                mouseDownX = e.GetPosition(_XRectangle).X;
                mouseDownY = e.GetPosition(_XRectangle).Y;
            }
        }

        private void Rectangle_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isMove)
            {
                _isMoveThis = false;  
                _XRectangle.ReleaseMouseCapture();
            }
        }

        private void Rectangle_MouseMove(object sender, MouseEventArgs e) 
        {
            if (_isMove && _isMoveThis)
            {
                _XRectangle.SetValue(Canvas.LeftProperty, e.GetPosition((Canvas)_XRectangle.Parent).X - mouseDownX);
                _XRectangle.SetValue(Canvas.TopProperty, e.GetPosition((Canvas)_XRectangle.Parent).Y - mouseDownY);
            }

        }

    }
}
