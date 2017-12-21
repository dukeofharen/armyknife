using System;
using System.Collections.Generic;
using System.Text;
using Armyknife.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Armyknife.Services.Implementations
{
   public class DnCoreServiceContainerWrapper : IServiceContainerWrapper
   {
      private readonly IServiceCollection _serviceCollection;

      public IServiceProvider Provider { get; set; }

      public DnCoreServiceContainerWrapper(IServiceCollection serviceCollection)
      {
         _serviceCollection = serviceCollection;
      }

      public void RegisterType<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface
      {
         _serviceCollection.AddTransient<TInterface, TImplementation>();
      }

      public void RegisterType(Type interfaceType, Type implementationType)
      {
         _serviceCollection.AddTransient(interfaceType, implementationType);
      }

      public void RegisterSingleton<TInterface>(TInterface implementationInstance) where TInterface : class
      {
         _serviceCollection.AddSingleton(implementationInstance);
      }

      public TInterface Resolve<TInterface>()
      {
         return Provider.GetService<TInterface>();
      }

      public object Resolve(Type type)
      {
         return Provider.GetService(type);
      }

      public IEnumerable<TInterface> ResolveMultiple<TInterface>()
      {
         return Provider.GetServices<TInterface>();
      }
   }
}
