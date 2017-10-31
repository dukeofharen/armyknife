using Armyknife.Services.Interfaces;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using ZXing;
using ZXing.QrCode;

namespace Armyknife.Services.Implementations
{
    internal class BarcodeService : IBarcodeService
    {
        public byte[] GenerateQrCode(string input, int height, int width)
        {
            var writer = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = height,
                    Width = width,
                    Margin = 0
                }
            };
            var pixelData = writer.Write(input);
            using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            using (var ms = new MemoryStream())
            {
                var bitmapData = bitmap.LockBits(
                    new Rectangle(
                        0,
                        0,
                        pixelData.Width,
                        pixelData.Height),
                    System.Drawing.Imaging.ImageLockMode.WriteOnly,
                    System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                try
                {
                    // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image    
                    Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }

                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}
