namespace Armyknife.Services.Interfaces
{
    public interface IFileService
    {
        string ReadAllText(string path);

        byte[] ReadAllBytes(string path);

        bool FileExists(string path);

        void WriteAllBytes(string path, byte[] contents);

        void WriteAllText(string path, string contents);

        void Delete(string path);

        string GetTempPath();

        bool DirectoryExists(string path);

        void CreateDirectory(string path);

        string[] GetFiles(string path);

        string[] GetFiles(string path, string searchPattern);
    }
}
