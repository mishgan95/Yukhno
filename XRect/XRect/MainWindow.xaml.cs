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
    /// 
    enum ColorLine { Black, Blue, Yellow };
    enum ColorRect { Green, Red };

    public partial class MainWindow : Window
    {
        RectBox rect;
        LineBox line;
        Point pDrawStart;
        bool isDraw = false;

        ColorLine colorL = ColorLine.Black;
        ColorRect colorR = ColorRect.Green;


        System.Drawing.Bitmap loadingImg;
        List<ImgRec> LIR;
        List<RectBox> LRB;
        List<ImgLine> LIL;

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

                switch (colorR)
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

        private void btnSave_Click_1(object sender, RoutedEventArgs e)
        {
            SaveLoadImg.SaveImg(cnvsMain);
        }

        private void btnLoad_Click_1(object sender, RoutedEventArgs e)
        {
            cnvsMain.Children.Clear();

            loadingImg = new System.Drawing.Bitmap("logo.png");
            LRB = new List<RectBox>();
            for (int j = 0; j < 2; j++)
            {
                LIR = SaveLoadImg.drawRect(j, loadingImg);
                for (int i = 0; i < LIR.Count; i++)
                {
                    drawRectCanvas(LIR[i], j);
                }
            }

            for (int j = 0; j < 2; j++)
            {
                LIL = SaveLoadImg.drawLine(j, loadingImg);
                for (int i = 0; i < LIL.Count; i++)
                {
                    drawLineCanvas(LIL[i], j);
                }
            }

        }

        Rectangle SearchRectBox(int x, int y, List<RectBox> LIR)
        {
            Rectangle res = null;
            for (int i = 0; i < LIR.Count; i++)
            {
                if (x >= Canvas.GetLeft(LIR[i].rect) && x <= Canvas.GetLeft(LIR[i].rect) + LIR[i].rect.Width && y >= Canvas.GetTop(LIR[i].rect) && y <= Canvas.GetTop(LIR[i].rect) + LIR[i].rect.Height)
                {
                    res = LIR[i].rect;
                }
            }
            return res;
        }

        void drawRectCanvas(ImgRec ir, int type)
        {
            if (type == 0)
            {
                rect = new RectBoxRed();
            }
            else
            {
                rect = new RectBoxGreen();
            }

            LRB.Add(rect);

            this.cnvsMain.Children.Add(rect.rect);
            rect.rect.MouseDown += Rectangle_MouseDown_MainWindow;
            rect.rect.MouseUp += Rectangle_MouseUp_MainWindow;

            rect.rect.Width = ir.w + 9;
            rect.rect.Height = ir.h + 9;

            Canvas.SetLeft(rect.rect, ir.x);
            Canvas.SetTop(rect.rect, ir.y);
        }

        void drawLineCanvas(ImgLine il, int type)
        {
            if (type == 0)
            {
                line = new LineBoxBlue();
            }
            else
            {
                line = new LineBoxYellow();
            }

            line.rect1 = SearchRectBox(il.x1, il.y1, LRB);
            line.rect2 = SearchRectBox(il.x2, il.y2, LRB);
            if (line.rect1 != null && line.rect2 != null)
            {
                line.Rect1_Move();
                cnvsMain.MouseMove += line.Rect1_Move;

                line.Rect2_Move();
                cnvsMain.MouseMove += line.Rect2_Move;
                cnvsMain.Children.Add(line.lineMain);
            }
        }
    }
}
