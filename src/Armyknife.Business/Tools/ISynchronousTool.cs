using System.Collections.Generic;

namespace Armyknife.Business.Tools
{
    public interface ISynchronousTool : ITool
    {
        string Execute(IDictionary<string, string> args);
    }
}
