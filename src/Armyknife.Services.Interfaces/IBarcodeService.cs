namespace Armyknife.Services.Interfaces
{
   public interface IBarcodeService
   {
      byte[] GenerateQrCodePng(string input, int height, int width);

      string GenerateQrCodeSvg(string input, int height, int width);
   }
}
