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
    static class SaveLoadImg
    {
        public static void SaveImg(Canvas cnvsMain)
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

        static bool compareColor(System.Drawing.Color c1, SolidColorBrush c2)
        {
            bool Result = false;
            if (c1.R == c2.Color.R && c1.B == c2.Color.B && c1.G == c2.Color.G)
            {
                Result = true;
            }
            return Result;
        }

        public static List<ImgRec> drawRect(int type, System.Drawing.Bitmap loadingImg)
        {
            List<ImgRec> LIR = new List<ImgRec>();
            SolidColorBrush ColorBrush;

            if (type == 0)
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
                        }
                    }
                }

            }

            return LIR;
        }


        public static List<ImgLine> drawLine(int type, System.Drawing.Bitmap loadingImg)
        {
            List<ImgLine> LIL = new List<ImgLine>();
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
                    }

                }
            }
            return LIL;
        }

        static bool serchP(int x, int y, List<Point> lp)
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

        static bool SearchImgRec(int x, int y, List<ImgRec> LIR)
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

        static bool SearchImgLine(int x, int y, List<ImgLine> LIL) 
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
            }
            return res;
        }

    }
}
