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
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    enum ColorLine{ Black, Blue, Yellow };
    enum ColorRect{ Green, Red };

    public partial class MainWindow : Window
    {
        RectBox rect;
        LineBox line;
        Point pDrawStart; 
        bool isDraw = false;

        ColorLine colorL = ColorLine.Black;
        ColorRect colorR = ColorRect.Green;

        public MainWindow()
        {
            InitializeComponent();
            LineBox.isDraw = false;
        }

        private void cnvsMain_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (isDraw)
            {
                pDrawStart = e.GetPosition(this.cnvsMain); 

                switch(colorR)
                {
                    case ColorRect.Green:
                        rect = new RectBoxGreen();
                        break;
                    case ColorRect.Red:
                        rect = new RectBoxRed();
                        break;
                }
                this.cnvsMain.Children.Add(rect.rect);
                rect.rect.MouseDown += Rectangle_MouseDown_MainWindow;
                rect.rect.MouseUp += Rectangle_MouseUp_MainWindow;
            }
        }

        private void cnvsMain_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (!(e.LeftButton == MouseButtonState.Released || rect == null)) 
            {
                Point pos = e.GetPosition(this.cnvsMain);

                double x = Math.Min(pos.X, pDrawStart.X);
                double y = Math.Min(pos.Y, pDrawStart.Y);

                double width = Math.Max(pos.X, pDrawStart.X) - x;
                double height = Math.Max(pos.Y, pDrawStart.Y) - y;

                rect.rect.Width = width; 
                rect.rect.Height = height;

                Canvas.SetLeft(rect.rect, x);
                Canvas.SetTop(rect.rect, y);
            }
        }

        private void cnvsMain_MouseUp_1(object sender, MouseButtonEventArgs e) 
        {
            rect = null;  
        }

        private void btnMove_Click_1(object sender, RoutedEventArgs e)
        {
            isDraw = false;  
            RectBox.isMove = true;
            LineBox.isDraw = false;
        }

        private void btnDraw_Click_1(object sender, RoutedEventArgs e)
        {
            isDraw = true;  
            RectBox.isMove = false;
            LineBox.isDraw = false;
        }

        private void Rectangle_MouseDown_MainWindow(object sender, MouseEventArgs e)
        {
            if (LineBox.isDraw == true)
            {
                switch (colorL)
                {
                    case ColorLine.Black:
                        line = new LineBoxBlack();
                        break;
                    case ColorLine.Blue:
                        line = new LineBoxBlue();
                        break;
                    case ColorLine.Yellow:
                        line = new LineBoxYellow();
                        break;
                }
                line.rect1 = (Rectangle)sender;
                line.Rect1_Move(sender, e);
                cnvsMain.MouseMove += line.Rect1_Move;
            }
        }

        private void Rectangle_MouseUp_MainWindow(object sender, MouseEventArgs e)
        {
            if (LineBox.isDraw == true)
            {
                line.rect2 = (Rectangle)sender;
                if (line.rect1 != null)
                {
                    line.Rect2_Move(sender, e);
                    cnvsMain.MouseMove += line.Rect2_Move;
                    cnvsMain.Children.Add(line.lineMain);
                }
            }
        }

        private void btnDrawLine_Click_1(object sender, RoutedEventArgs e)
        {
            isDraw = false;
            RectBox.isMove = false;
            LineBox.isDraw = true;
        }

        private void rbGreen_Checked_1(object sender, RoutedEventArgs e)
        {
            colorR = ColorRect.Green;
        }

        private void rbLineBlack_Checked_1(object sender, RoutedEventArgs e)
        {
            colorL = ColorLine.Black;
        }

        private void rbRed_Checked_1(object sender, RoutedEventArgs e)
        {
            colorR = ColorRect.Red;
        }

        private void rbLineBlue_Checked_1(object sender, RoutedEventArgs e)
        {
            colorL = ColorLine.Blue;
        }

        private void rbLineYellow_Checked_1(object sender, RoutedEventArgs e)
        {
            colorL = ColorLine.Yellow;
        }
    }
}
