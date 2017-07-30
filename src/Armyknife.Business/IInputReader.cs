using System.Collections.Generic;

namespace Armyknife.Business
{
    public interface IInputReader
    {
        string GetInput(string[] args, IDictionary<string, string> argsDictionary);
    }
}
