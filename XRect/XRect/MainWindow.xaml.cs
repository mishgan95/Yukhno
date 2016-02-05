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
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)cnvsMain.RenderSize.Width,
    (int)cnvsMain.RenderSize.Height, 96d, 96d, System.Windows.Media.PixelFormats.Default);
            rtb.Render(cnvsMain);

            var crop = new CroppedBitmap(rtb, new Int32Rect(0, 0, 450, 450));

            BitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(crop));

            using (var fs = System.IO.File.OpenWrite("logo.png")) 
            {
                pngEncoder.Save(fs);
            }
        }

        private void btnLoad_Click_1(object sender, RoutedEventArgs e)
        {
            cnvsMain.Children.Clear();

            loadingImg = new System.Drawing.Bitmap("logo.png");
            LIR = new List<ImgRec>(); 
            LRB = new List<RectBox>();

            LIL = new List<ImgLine>();

            drawRect(0);
            drawRect(1); 

            drawLine(0);
            drawLine(1);
        }

        bool SearchImgRec(int x, int y, List<ImgRec> LIR)
        {
            bool res = false;
            for (int i = 0; i < LIR.Count; i++)
            {
                if (x >= LIR[i].x && x <= LIR[i].x + LIR[i].w && y >= LIR[i].y && y <= LIR[i].y + LIR[i].h)
                {
                    res = true;
                }
            }
            return res;
        }

        bool SearchImgLine(int x, int y, List<ImgLine> LIL)
        {
            bool res = false;
            for (int i = 0; i < LIL.Count; i++)
            {
                if (LIL[i].x1 != LIL[i].x2 && LIL[i].y1 != LIL[i].y2)
                {
                    if (x == ((LIL[i].x1 - LIL[i].x2) * (y - LIL[i].y1) / (LIL[i].y2 - LIL[i].y1)) + LIL[i].x2) 
                    {
                        res = true;
                    }
                }
                else
                {
                    if (LIL[i].x1 == LIL[i].x2)
                    {
                        if (x == LIL[i].x1 && ((y >= LIL[i].y1 && y <= LIL[i].y2) || (y <= LIL[i].y1 && y >= LIL[i].y2)))
                        {
                            res = true;
                        }
                    }
                    else
                    {
                        if (y == LIL[i].y1 && ((x >= LIL[i].x1 && x <= LIL[i].x2) || (x <= LIL[i].x1 && x >= LIL[i].x2)))
                        {
                            res = true;
                        }
                    }
                }


            }
            return res;
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

        bool serchP(int x, int y, List<Point> lp)
        {
            bool b = true;

            for (int i = 0; i < lp.Count; i++)
            {
                if (x == lp[i].X && y == lp[i].Y)
                {
                    b = false;
                }
            }

            return b;
        }

        void drawRect(int type)
        {
            SolidColorBrush ColorBrush;

            if(type == 0)
            {
                ColorBrush = Brushes.DarkRed;
            }
            else
            {
                ColorBrush = Brushes.Green;
            }
            

            for (int i = 0; i < loadingImg.Height; i++)
            {
                for (int j = 0; j < loadingImg.Width; j++) 
                {
                    if (compareColor(loadingImg.GetPixel(j, i), ColorBrush) && !SearchImgRec(j, i, LIR)) 
                    {
                        int w = 0;
                        int h = 0;
                        while (compareColor(loadingImg.GetPixel(j, i + h), ColorBrush) || compareColor(loadingImg.GetPixel(j, i + h), Brushes.Black) || compareColor(loadingImg.GetPixel(j, i + h), Brushes.Blue) || compareColor(loadingImg.GetPixel(j, i + h), Brushes.Yellow))
                        {
                            h++;
                        }

                        while (compareColor(loadingImg.GetPixel(j + w, i), ColorBrush) || compareColor(loadingImg.GetPixel(j + w, i), Brushes.Black) || compareColor(loadingImg.GetPixel(j + w, i), Brushes.Blue) || compareColor(loadingImg.GetPixel(j + w, i), Brushes.Yellow))
                        {
                            w++;
                        }

                        if (w > 5 && h > 5)
                        {
                            ImgRec tIR = new ImgRec(j, i, h - 1, w - 1);
                            LIR.Add(tIR);

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

                            rect.rect.Width = w - 1 +9;
                            rect.rect.Height = h - 1 +9;

                            Canvas.SetLeft(rect.rect, j);
                            Canvas.SetTop(rect.rect, i);
                        }
                    }
                }

            }
        }

        void drawLine(int type)
        {
            SolidColorBrush ColorBrush;

            if (type == 0)
            {
                ColorBrush = Brushes.Blue;
            }
            else
            {
                ColorBrush = Brushes.Yellow;
            }

            for (int i = 0; i < loadingImg.Height; i++)
            {
                for (int j = 0; j < loadingImg.Width; j++)
                {
                    if (compareColor(loadingImg.GetPixel(j, i), ColorBrush) && !SearchImgLine(j, i, LIL)) 
                    {
                        int w = 0; 
                        int h = 0; 

                        List<Point> LP = new List<Point>();

                        bool end = true;

                        w = j; 
                        h = i;
                        LP.Add(new Point(w, h));

                        while (end)
                        {
                            end = false;

                            for (int i8 = -1; i8 < 2; i8++)
                            {
                                for (int j8 = -1; j8 < 2; j8++)
                                {
                                    if (compareColor(loadingImg.GetPixel(w + j8, h + i8), ColorBrush))
                                    {
                                        if (serchP(w + j8, h + i8, LP)) 
                                        {
                                            w = w + j8; 
                                            h = h + i8;
                                            LP.Add(new Point(w, h));
                                            end = true;
                                        }
                                    }
                                }
                            }
                        }

                        ImgLine il = new ImgLine();
                        il.x1 = j;
                        il.y1 = i;

                        il.x2 = w;
                        il.y2 = h;

                        LIL.Add(il);
                        if (type == 0)
                        {
                            line = new LineBoxBlue();
                        }
                        else
                        {
                            line = new LineBoxYellow();
                        }

                        line.rect1 = SearchRectBox(LIL[LIL.Count - 1].x1, LIL[LIL.Count - 1].y1, LRB);
                        line.rect2 = SearchRectBox(LIL[LIL.Count - 1].x2, LIL[LIL.Count - 1].y2, LRB); 
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
        }

        bool compareColor(System.Drawing.Color c1, SolidColorBrush c2) 
        {
            bool Result = false;
            if (c1.R == c2.Color.R && c1.B == c2.Color.B && c1.G == c2.Color.G)
            {
                Result = true;
            }
            return Result;
        }

    }
}
