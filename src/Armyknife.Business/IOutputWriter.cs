using System.Collections.Generic;

namespace Armyknife.Business
{
    public interface IOutputWriter
    {
        void WriteOutput(byte[] result, IDictionary<string, string> argsDictionary);
    }
}
