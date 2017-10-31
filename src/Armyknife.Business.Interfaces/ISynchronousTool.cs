using System.Collections.Generic;

namespace Armyknife.Business.Interfaces
{
    public interface ISynchronousTool : ITool
    {
        string Execute(IDictionary<string, string> args);
    }
}
