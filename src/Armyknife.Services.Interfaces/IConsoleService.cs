namespace Armyknife.Services.Interfaces
{
    public interface IConsoleService
    {
        void WriteLine(string text);

        string GetConsolePath();

        string ReadPipedData();
    }
}
