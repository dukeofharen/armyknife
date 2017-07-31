using System.Collections.Generic;

namespace Armyknife.Business
{
    public interface IOutputWriter
    {
        void WriteOutput(string result, IDictionary<string, string> argsDictionary);
    }
}
