using Armyknife.Services.Interfaces;
using System;
using System.Text;

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

        public string ReadPipedData()
        {
            if(!Console.IsInputRedirected)
            {
                return null;
            }

            var builder = new StringBuilder();
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                builder.AppendLine(line);
            }

            return builder.Length > 0 ? builder.ToString() : null;
        }
    }
}
