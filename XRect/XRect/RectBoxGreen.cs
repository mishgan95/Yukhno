﻿using System;
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
    class RectBoxGreen : RectBox 
    {
        public RectBoxGreen()
            : base() 
        {
            _XRectangle.StrokeThickness = 2;
            _XRectangle.Fill = Brushes.Green; 
            _XRectangle.Stroke = Brushes.Black; 
        }
    }
}
