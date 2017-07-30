using System;

namespace Armyknife.Services.Implementations
{
    internal class ConsoleService : IConsoleService
    {
        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }

        public string GetConsolePath()
        {
            return AppContext.BaseDirectory;
        }
    }
}
