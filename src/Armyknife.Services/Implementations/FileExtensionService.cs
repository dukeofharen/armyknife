using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Armyknife.Services.Interfaces;
using Armyknife.Models;

namespace Armyknife.Services.Implementations
{
   internal class FileExtensionService : IFileExtensionService
   {
      private List<FileExtensionInfoModel> _extensions;
      private readonly IConsoleService _consoleService;
      private readonly IFileService _fileService;

      public FileExtensionService(
          IConsoleService consoleService,
          IFileService fileService)
      {
         _consoleService = consoleService;
         _fileService = fileService;
         InitializeExtensions();
      }

      public FileExtensionInfoModel GetFileExtensionInfo(string extension)
      {
         var result = _extensions
             .FirstOrDefault(e => e.Extension.Equals(extension, StringComparison.OrdinalIgnoreCase));
         return result;
      }

      private void InitializeExtensions()
      {
         string mimePath = Path.Combine(_consoleService.GetConsolePath(), "Resources/filetypes.json");
         string contents = _fileService.ReadAllText(mimePath);
         _extensions = JsonConvert.DeserializeObject<List<FileExtensionInfoModel>>(contents);
      }
   }
}
