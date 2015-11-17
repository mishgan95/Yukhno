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
    public partial class MainWindow : Window
    {
        RectBox rect;   
        Point pDrawStart; 
        bool isDraw = true;

        string typeRect="Зеленый";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void cnvsMain_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (isDraw)
            {
                pDrawStart = e.GetPosition(this.cnvsMain); 

                switch(typeRect)
                {
                    case "Зеленый":
                        rect = new RectBoxGreen();
                        break;
                    case "Красный":
                        rect = new RectBoxRed();
                        break;
                }

                this.cnvsMain.Children.Add(rect.XRectangle); 
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

                rect.XRectangle.Width = width; 
                rect.XRectangle.Height = height;

                Canvas.SetLeft(rect.XRectangle, x);
                Canvas.SetTop(rect.XRectangle, y);
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
        }

        private void btnDraw_Click_1(object sender, RoutedEventArgs e)
        {
            isDraw = true;  
            RectBox.isMove = false;  
        }

        private void rbRed_Checked_1(object sender, RoutedEventArgs e)
        {
            typeRect = ((RadioButton)sender).Content.ToString();
        }
        

    }
}
