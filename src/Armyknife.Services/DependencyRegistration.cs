using Armyknife.Services.Implementations;
using Armyknife.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Armyknife.Services
{
   public static class DependencyRegistration
   {
      public static void RegisterDependencies(IServiceCollection serviceCollection)
      {
         serviceCollection.AddTransient<IAssemblyService, AssemblyService>();
         serviceCollection.AddTransient<IBarcodeService, BarcodeService>();
         serviceCollection.AddTransient<IConsoleService, ConsoleService>();
         serviceCollection.AddTransient<IDateTimeService, DateTimeService>();
         serviceCollection.AddTransient<IFileExtensionService, FileExtensionService>();
         serviceCollection.AddTransient<IFileService, FileService>();
         serviceCollection.AddTransient<ILogger, Logger>();
         serviceCollection.AddTransient<IMimeService, MimeService>();
         serviceCollection.AddTransient<IProcessService, ProcessService>();
         serviceCollection.AddTransient<IWebService, WebService>();
      }
   }
}
