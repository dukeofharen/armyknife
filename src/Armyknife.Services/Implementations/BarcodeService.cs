using Armyknife.Services.Interfaces;
using SixLabors.ImageSharp;
using System.IO;
using ZXing;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace Armyknife.Services.Implementations
{
   internal class BarcodeService : IBarcodeService
   {
      public byte[] GenerateQrCodePng(string input, int height, int width)
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
         var image = writer.WriteAsImageSharp<Rgba32>(input);

         using (var stream = new MemoryStream())
         {
            image.SaveAsPng(stream);
            return stream.ToArray();
         }
      }

      public string GenerateQrCodeSvg(string input, int height, int width)
      {
         var writerSvg = new BarcodeWriterSvg
         {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
               ErrorCorrection = ErrorCorrectionLevel.H,
               Width = width,
               Height = height
            }
         };
         var svgImageData = writerSvg.Write(input);
         return svgImageData.Content;
      }
   }
}
