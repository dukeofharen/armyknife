using System;
using Armyknife.Business;
using Armyknife.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Armyknife.Business.Interfaces;

namespace Armyknife
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            DependencyRegistration.RegisterDependencies(serviceCollection);
            var provider = serviceCollection.BuildServiceProvider();

            var executor = provider.GetService<IExecutor>();
            try
            {
                executor.ExecuteAsync(args).Wait();
            }
            catch (ArmyknifeException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (Exception)
            {
                // TODO log this exception
                throw;
            }
        }
    }
}