﻿using System;
using Armyknife.Business;
using Armyknife.Exceptions;
using Microsoft.Extensions.DependencyInjection;

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
                executor.Execute(args);
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

#if DEBUG
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
#endif
        }
    }
}