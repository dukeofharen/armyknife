using Armyknife.Business.Interfaces;
using Armyknife.Services.Interfaces;

namespace Armyknife.Business.Implementations
{
    internal class OutputWriter : IOutputWriter
    {
        private readonly IConsoleService _consoleService;

        public OutputWriter(IConsoleService consoleService)
        {
            _consoleService = consoleService;
        }

        public void WriteOutput(string result)
        {
            _consoleService.WriteLine(result);
        }
    }
}
