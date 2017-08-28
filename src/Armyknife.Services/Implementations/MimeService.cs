using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Armyknife.Services.Implementations
{
    // https://github.com/khellang/MimeTypes/blob/master/src/MimeTypes/MimeTypes.cs.pp
    internal class MimeService : IMimeService
    {
        private const string FallbackMimeType = "application/octet-stream";
        private static Dictionary<string, string> _typeMap;
        private readonly IConsoleService _consoleService;
        private readonly IFileService _fileService;

        public MimeService(
            IConsoleService consoleService,
            IFileService fileService)
        {
            _consoleService = consoleService;
            _fileService = fileService;
            InitializeTypeMap();
        }

        public string GetMimeType(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return FallbackMimeType;
            }

            var parts = input.Split('.');
            string result;
            if (_typeMap.TryGetValue(parts.Last(), out result))
            {
                return result;
            }

            return FallbackMimeType;
        }

        public IEnumerable<string> GetExtensions(string mimeType)
        {
            if (string.IsNullOrEmpty(mimeType))
            {
                return new string[0];
            }

            if (!_typeMap.Any(e => e.Value == mimeType))
            {
                return new string[0];
            }

            var result = _typeMap
                .Where(e => e.Value == mimeType)
                .Select(e => e.Key);
            return result;
        }

        private void InitializeTypeMap()
        {
            if (_typeMap == null)
            {
                string mimePath = Path.Combine(_consoleService.GetConsolePath(), "Resources/mime.json"); 
                string contents = _fileService.ReadAllText(mimePath);
                _typeMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(contents);
            }
        }
    }
}
