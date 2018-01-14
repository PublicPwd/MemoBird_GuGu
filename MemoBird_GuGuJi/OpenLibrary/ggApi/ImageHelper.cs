using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace MemoBird_GuGuJi.OpenLibrary.ggApi
{
    class ImageHelper
    {
        public static string GetPoitImgBase64(Image img)
        {
            Image imgTemp = CovertImg(img);
            byte[] imglastbyte = ImageToBytes(imgTemp);
            int imglastHeight = BytesToInt(imglastbyte, 22);
            byte[] byteTemp = IntToBytes(-imglastHeight);
            imglastbyte[22] = byteTemp[0];
            imglastbyte[23] = byteTemp[1];
            imglastbyte[24] = byteTemp[2];
            imglastbyte[25] = byteTemp[3];
            return Convert.ToBase64String(imglastbyte);
        }

        private static Bitmap CovertImg(Image img)
        {
            Image tempImage = ZoomPic(img, 384.0);
            Bitmap map = new Bitmap(tempImage);
            return ToBitmap(DoDither(map), 8);
        }

        private static byte[] ImageToBytes(Image image)
        {
            ImageFormat format = image.RawFormat;
            byte[] result;
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Bmp);
                List<ImageFormat> list = new List<ImageFormat>
                {
                    ImageFormat.Jpeg,
                    ImageFormat.Png,
                    ImageFormat.Gif,
                    ImageFormat.Icon
                };
                foreach(ImageFormat imageFormat in list)
                {
                    if(format.Equals(imageFormat))
                    {
                        image.Save(ms, imageFormat);
                        break;
                    }
                }

                byte[] buffer = new byte[ms.Length];
                ms.Seek(0L, SeekOrigin.Begin);
                ms.Read(buffer, 0, buffer.Length);
                result = buffer;
            }
            return result;
        }

        private static int BytesToInt(byte[] src, int offset)
        {
            return src[offset] & 255 | (src[offset + 1] & 255) << 8 | (src[offset + 2] & 255) << 16 | (src[offset + 3] & 255) << 24;
        }

        private static byte[] IntToBytes(int value)
        {
            byte[] src = new byte[] { 0, 0, 0, (byte)(value >> 24 & 255) };
            src[2] = (byte)(value >> 16 & 255);
            src[1] = (byte)(value >> 8 & 255);
            src[0] = (byte)(value & 255);
            return src;
        }

        private static Image ZoomPic(Image initImage, double targetWidth)
        {
            Image result;
            if (initImage.Width <= targetWidth)
            {
                result = initImage;
            }
            else
            {
                double newHeight = initImage.Height * (targetWidth / initImage.Width);
                Image toBitmap = new Bitmap(Convert.ToInt32(targetWidth), Convert.ToInt32(newHeight));
                using (Graphics g = Graphics.FromImage(toBitmap))
                {
                    g.InterpolationMode = InterpolationMode.High;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.Clear(Color.Transparent);
                    g.DrawImage(initImage, new Rectangle(0, 0, Convert.ToInt32(targetWidth), Convert.ToInt32(newHeight)), new Rectangle(0, 0, initImage.Width, initImage.Height), GraphicsUnit.Pixel);
                    initImage.Dispose();
                    result = toBitmap;
                }
            }
            return result;
        }

        private static Bitmap ToBitmap(Bitmap matrix, int v)
        {
            int width = matrix.Width;
            int height = matrix.Height;
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format1bppIndexed);
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int sWidth = Math.Abs(bmpData.Stride);
            int sDepth = 8;
            int bytes = sWidth * bmp.Height;
            byte[] rgbValues = new byte[bytes];
            Marshal.Copy(ptr, rgbValues, 0, bytes);
            for (int h = 0; h < bmp.Height; h++)
            {
                for (int w = 0; w < sWidth; w++)
                {
                    int sColor = 0;
                    for (int i = 0; i < sDepth; i++)
                    {
                        if (i + w * sDepth >= bmp.Width)
                        {
                            break;
                        }
                        int r = matrix.GetPixel(i + w * sDepth, h).R;
                        int g = matrix.GetPixel(i + w * sDepth, h).G;
                        int b = matrix.GetPixel(i + w * sDepth, h).B;
                        sColor += (byte)((byte)(0.2125 * r + 0.7154 * g + 0.0721 * b) >= v ? (128 >> i) : 0);
                    }
                    rgbValues[sWidth * h + w] = (byte)sColor;
                }
            }
            Marshal.Copy(rgbValues, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
            return bmp;
        }

        private static Bitmap DoDither(Bitmap fileIn)
        {
            int width = fileIn.Width;
            int height = fileIn.Height;
            int[,] red = new int[width, height];
            int[,] grn = new int[width, height];
            int[,] blu = new int[width, height];
            int[,] average = new int[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    red[x, y] = (ColorTranslator.ToWin32(fileIn.GetPixel(x, y)) >> 16 & 255);
                    grn[x, y] = (ColorTranslator.ToWin32(fileIn.GetPixel(x, y)) >> 8 & 255);
                    blu[x, y] = (ColorTranslator.ToWin32(fileIn.GetPixel(x, y)) & 255);
                    average[x, y] = (int)(0.3 * red[x, y] + 0.59 * grn[x, y] + 0.11 * blu[x, y]);
                }
            }
            int[,] pixel_floyd = Dither(average, height, width);
            Bitmap bitmapAfterDither = new Bitmap(fileIn, width, height);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int pixel = pixel_floyd[x, y] << 16 | pixel_floyd[x, y] << 8 | pixel_floyd[x, y];
                    bitmapAfterDither.SetPixel(x, height - 1 - y, Color.FromArgb(pixel));
                }
            }
            return bitmapAfterDither;
        }

        private static int[,] Dither(int[,] pixel, int height, int width)
        {
            for (int y = 0; y < height; y++)
            {
                bool nbottom = y < height - 1;
                for (int x = 0; x < width; x++)
                {
                    bool nleft = x > 0;
                    bool nright = x < width - 1;
                    int oldpixel = pixel[x, y];
                    int newpixel = (oldpixel < 128) ? 0 : 255;
                    pixel[x, y] = newpixel;
                    int error = oldpixel - newpixel;
                    if (nright)
                    {
                        pixel[x + 1, y] += 7 * error / 16;
                    }
                    if (nleft & nbottom)
                    {
                        pixel[x - 1, y + 1] += 3 * error / 16;
                    }
                    if (nbottom)
                    {
                        pixel[x, y + 1] += 5 * error / 16;
                    }
                    if (nright && nbottom)
                    {
                        pixel[x + 1, y + 1] += error / 16;
                    }
                }
            }
            return pixel;
        }
    }
}
