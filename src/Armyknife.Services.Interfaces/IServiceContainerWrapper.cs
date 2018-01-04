using System;
using System.Collections.Generic;

namespace Armyknife.Services.Interfaces
{
   public interface IServiceContainerWrapper
   {
      void RegisterType<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface;

      void RegisterType(Type interfaceType, Type implementationType);

      void RegisterType(Type interfaceType, Type implementationType, string name);

      void RegisterSingleton<TInterface>(TInterface implementationInstance) where TInterface : class;

      TInterface Resolve<TInterface>();

      object Resolve(Type type);

      IEnumerable<TInterface> ResolveMultiple<TInterface>();

      IEnumerable<Type> GetInterfaceTypes();
   }
}
