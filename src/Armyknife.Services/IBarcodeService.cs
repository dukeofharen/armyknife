namespace Armyknife.Services
{
    public interface IBarcodeService
    {
        byte[] GenerateQrCode(string input, int height, int width);
    }
}
