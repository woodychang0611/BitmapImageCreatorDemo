using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


using System.Windows.Media;
using System.Windows.Media.Imaging;

//PresentationCore and WindowsBase are needed, add them to "References"

namespace BitmapImageCreatorDemo
{
    class Program
    {


        static void SetPixel(WriteableBitmap bitmap,Point point,Color color)
        {
            byte[] ColorData = { color.B, color.G, color.R, 0 }; // B G R
            Int32Rect rect = new Int32Rect((int)point.X,(int)point.Y,1,1);
            bitmap.WritePixels(rect, ColorData, 4, 0);
        }

        static void Main(string[] args)
        {
            WriteableBitmap bitmap = new WriteableBitmap(500,500,96,96, PixelFormats.Bgr32,null);
            
            for(int i=0;i<bitmap.Width;i++)
            {
                for(int j=0;j<bitmap.Height;j++)
                {
                    Color color = new Color();
                    color.R = 0xFF;
                    color.G = 0xFF;
                    color.B = (Byte)((double)0xFF*(i + j) / (bitmap.Height + bitmap.Width));
                    SetPixel(bitmap, new Point(i, j), color);

                    //Draw circle
                    Double r = bitmap.Width / 4;
                    Point center = new Point(bitmap.Width / 2, bitmap.Height / 2);
                    Double distance = Math.Sqrt(Math.Pow(i - center.X, 2) + Math.Pow(j - center.Y, 2));
                    if (distance<r)
                    {
                        SetPixel(bitmap, new Point(i, j), Colors.Cyan);
                    }
                }
            }

            //Save the image to png 
            String pngFileName = "test.png";
            using (FileStream stream = new FileStream(pngFileName, FileMode.Create))
            {
                PngBitmapEncoder pngEncoder = new PngBitmapEncoder();
                pngEncoder.Frames.Add(BitmapFrame.Create(bitmap));
                pngEncoder.Save(stream);
            }
            //Save image to bmp
            String bmpFileName = "test.bmp";
            using (FileStream stream = new FileStream(bmpFileName, FileMode.Create))
            {
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                encoder.Save(stream);
            }

        }
    }
}
