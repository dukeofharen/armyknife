namespace Armyknife.Services.Interfaces
{
    public interface IBarcodeService
    {
        byte[] GenerateQrCode(string input, int height, int width);
    }
}
