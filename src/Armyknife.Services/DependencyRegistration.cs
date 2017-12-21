using Armyknife.Services.Implementations;
using Armyknife.Services.Interfaces;

namespace Armyknife.Services
{
   public static class DependencyRegistration
   {
      public static void RegisterDependencies(IServiceContainerWrapper wrapper)
      {
         wrapper.RegisterType<IAssemblyService, AssemblyService>();
         wrapper.RegisterType<IBarcodeService, BarcodeService>();
         wrapper.RegisterType<IConsoleService, ConsoleService>();
         wrapper.RegisterType<IDateTimeService, DateTimeService>();
         wrapper.RegisterType<IFileExtensionService, FileExtensionService>();
         wrapper.RegisterType<IFileService, FileService>();
         wrapper.RegisterType<ILogger, Logger>();
         wrapper.RegisterType<IMimeService, MimeService>();
         wrapper.RegisterType<IProcessService, ProcessService>();
         wrapper.RegisterType<IWebService, WebService>();
      }
   }
}
