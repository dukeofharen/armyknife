using Armyknife.Services.Models;

namespace Armyknife.Services
{
    public interface IFileExtensionService
    {
        FileExtensionInfoModel GetFileExtensionInfo(string extension);
    }
}
