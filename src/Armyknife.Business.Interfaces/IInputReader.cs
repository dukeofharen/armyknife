using System.Collections.Generic;

namespace Armyknife.Business.Interfaces
{
    public interface IInputReader
    {
        string GetInput(string[] args, IDictionary<string, string> argsDictionary);
    }
}
