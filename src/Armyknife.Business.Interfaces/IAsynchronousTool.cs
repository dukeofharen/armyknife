using System.Collections.Generic;
using System.Threading.Tasks;

namespace Armyknife.Business.Interfaces
{
    public interface IAsynchronousTool : ITool
    {
        Task<string> ExecuteAsync(IDictionary<string, string> args);
    }
}
