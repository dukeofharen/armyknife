using Armyknife.Models;

namespace Armyknife.Services.Interfaces
{
    public interface IFileExtensionService
    {
        FileExtensionInfoModel GetFileExtensionInfo(string extension);
    }
}
