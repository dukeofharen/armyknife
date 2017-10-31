using System.Collections.Generic;

namespace Armyknife.Services.Interfaces
{
    public interface IMimeService
    {
        string GetMimeType(string input);

        IEnumerable<string> GetExtensions(string mimeType);
    }
}
