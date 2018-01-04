using System;
using System.Collections.Generic;
using System.Linq;
using Armyknife.Services.Interfaces;
using Unity;

namespace Armyknife.DI.Unity
{
   public class UnityServiceContainerWrapper : IServiceContainerWrapper
   {
      private readonly IUnityContainer _unityContainer;
      private static UnityServiceContainerWrapper _wrapper;

      public IUnityContainer Container => _unityContainer;

      public static UnityServiceContainerWrapper GetInstance() => _wrapper ?? (_wrapper = new UnityServiceContainerWrapper());

      internal UnityServiceContainerWrapper()
      {
         _unityContainer = new UnityContainer();
      }

      public void RegisterType<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface
      {
         _unityContainer.RegisterType<TInterface, TImplementation>();
      }

      public void RegisterType(Type interfaceType, Type implementationType)
      {
         _unityContainer.RegisterType(interfaceType, implementationType);
      }

      public void RegisterType(Type interfaceType, Type implementationType, string name)
      {
         _unityContainer.RegisterType(interfaceType, implementationType, name);
      }

      public void RegisterSingleton<TInterface>(TInterface implementationInstance) where TInterface : class
      {
         _unityContainer.RegisterInstance(implementationInstance);
      }

      public TInterface Resolve<TInterface>()
      {
         return _unityContainer.Resolve<TInterface>();
      }

      public object Resolve(Type type)
      {
         return _unityContainer.Resolve(type);
      }

      public IEnumerable<TInterface> ResolveMultiple<TInterface>()
      {
         var result = _unityContainer.ResolveAll<TInterface>();
         return result;
      }

      public IEnumerable<Type> GetInterfaceTypes()
      {
         return _unityContainer.Registrations
            .Select(r => r.RegisteredType);
      }
   }
}
