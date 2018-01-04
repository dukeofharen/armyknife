using Armyknife.Business.Implementations;
using Armyknife.Business.Interfaces;
using Armyknife.Services.Interfaces;
using Armyknife.Utilities;

namespace Armyknife.Business
{
   public static class DependencyRegistration
   {
      public static void RegisterDependencies(IServiceContainerWrapper wrapper)
      {
         wrapper.RegisterSingleton(wrapper);
         wrapper.RegisterType<IExecutor, Executor>();
         wrapper.RegisterType<IInputReader, InputReader>();
         wrapper.RegisterType<IOutputWriter, OutputWriter>();
         wrapper.RegisterType<IToolResolver, ToolResolver>();

         // This is done so the types in the Armyknife.Tools project are preloaded and available through reflection.
         // ReSharper disable once UnusedVariable
         string toolName = typeof(Armyknife.Tools.Implementations.Base64DecodeTool).ToString();

         var toolTypes = AssemblyHelper.GetImplementations<ITool>();
         foreach (var type in toolTypes)
         {
            wrapper.RegisterType(typeof(ITool), type, type.ToString());
         }

         Services.DependencyRegistration.RegisterDependencies(wrapper);
      }
   }
}
