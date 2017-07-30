using System;
using System.Linq;
using Armyknife.Business.Tools;
using Microsoft.Extensions.DependencyInjection;

namespace Armyknife.Business.Implementations
{
    public class ToolResolver : IToolResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public ToolResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ITool ResolveTool(string name)
        {
            return _serviceProvider
                .GetServices<ITool>()
                .FirstOrDefault(t => t.Name == name);
        }
    }
}
