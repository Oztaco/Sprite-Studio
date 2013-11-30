using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpriteStudio
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ColorSpaces.SuperColor.Test();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            EffectHost aa = new EffectHost();
            aa.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (((ComboBoxItem)effectslist.SelectedItem).Content.ToString().ToLower() == "black and white") BlackAndWhite();
            //Services.SuperColor.Test();
        }



        WriteableBitmap picture = new WriteableBitmap(960,600,96,96,PixelFormats.Bgra32,null);



        // Effect Prototypes
        // These should only be per-pixel, so no drawing shapes!

        public void BlackAndWhite() {
            IPerPixelFilter b = new Filters.BlackAndWhite();
            unsafe
            {
                picture = new WriteableBitmap((BitmapSource)(new FormatConvertedBitmap(picture, PixelFormats.Bgra32, null, 0)));
                picture.Lock();
                ColorSpaces.SuperColor temp = new ColorSpaces.SuperColor(0, 0, 0);

                IntPtr pBackBuffer = picture.BackBuffer;
                byte* pBuff = (byte*)pBackBuffer.ToPointer();

                short pieces = 4; // Red, Green, Blue, Alpha are each counted as a "piece"

                int PreMath = 0,    //
                    PreMath1 = 0,   //
                    PreMath2 = 0,   // Just to stop repititive math to speed up a bit
                    PreMath3 = 0;   //

                for (int x = 0; x < picture.PixelWidth; x++)
                {
                    for (int y = 0; y < picture.PixelHeight; y++)
                    {
                        PreMath = (pieces * x) + (y * picture.BackBufferStride);
                        PreMath1 = PreMath + 1;
                        PreMath2 = PreMath + 2;
                        PreMath3 = PreMath + 3;
                        temp.SetRGBA(pBuff[PreMath], pBuff[PreMath1], pBuff[PreMath2], pBuff[PreMath3]);

                        b.OperatePixel(ref temp);

                        pBuff[PreMath] = (byte)temp.Blue;
                        pBuff[PreMath1] = (byte)temp.Green;
                        pBuff[PreMath2] = (byte)temp.Red;
                        pBuff[PreMath3] = (byte)temp.Alpha;
                    }
                }
                picture.AddDirtyRect(new Int32Rect(0, 0, picture.PixelWidth, picture.PixelHeight));
                picture.Unlock();

                img.Source = picture;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //picture = new WriteableBitmap((BitmapSource)(new FormatConvertedBitmap(picture, PixelFormats.Bgra32, null, 0)));
            
            ////using (picture.GetBitmapContext())
            ////{
            //unsafe {
            //    picture.Lock();
            //    IntPtr pBackBuffer = picture.BackBuffer;

            //    byte* pBuff = (byte*)pBackBuffer.ToPointer();
            //    for (int x = 0; x < picture.PixelWidth; x++)
            //    {
            //        for (int y = 0; y < picture.PixelHeight; y++)
            //        {
            //            pBuff[4 * x + (y * picture.BackBufferStride)] = (byte)((x / 960.0) * 255);
            //            pBuff[4 * x + (y * picture.BackBufferStride) + 1] = (byte)((y / 800.0) * 255);
            //            pBuff[4 * x + (y * picture.BackBufferStride) + 2] = (byte)(((x * y) / 960.0) * 255);
            //            pBuff[4 * x + (y * picture.BackBufferStride) + 3] = 200;
            //            //(byte)((x / 960.0) * 255), (byte)((y / 800.0) * 255), (byte)(((x * y) / 960.0) * 255)
            //        }
            //    }
            //    picture.AddDirtyRect(new Int32Rect(0,0,960,800));
            //picture.Unlock();
            //}
            ////}
            //picture = new WriteableBitmap((BitmapSource)(new FormatConvertedBitmap(picture, PixelFormats.Pbgra32, null, 0)));
            //WriteableBitmap temp = new WriteableBitmap(960,800,96,96,PixelFormats.Pbgra32, null);
            //temp.FillRectangle(20, 20, 940, 780, Colors.Beige);
            //temp.FillRectangle(60, 60, 900, 740, Colors.Green);
            //temp.FillRectangle(100, 100, 860, 700, Colors.Blue);
            //temp.FillRectangle(200, 200, 760, 600, Colors.Red);
            //picture.Blit(new Rect(0, 0, 960, 800), temp, new Rect(0, 0, 960, 800), WriteableBitmapExtensions.BlendMode.Multiply);
            BitmapImage temp = new BitmapImage();
            temp.BeginInit();
            temp.CacheOption = BitmapCacheOption.OnLoad;
            temp.UriSource = new Uri(@"C:\Users\Efe\Documents\Visual Studio 2012\Projects\SpriteStudio\SpriteStudio\res\test file.png", UriKind.Absolute);;
            temp.EndInit();
            picture = new WriteableBitmap(temp);
            img.Source = picture;
        }

        // End of Effects
    }
}
