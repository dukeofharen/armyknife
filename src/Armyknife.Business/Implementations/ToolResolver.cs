using System;
using System.Collections.Generic;
using System.Linq;
using Armyknife.Models;
using Microsoft.Extensions.DependencyInjection;
using Armyknife.Business.Interfaces;

namespace Armyknife.Business.Implementations
{
    internal class ToolResolver : IToolResolver
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
                .Single(t => t.Name == name);
        }

        public IEnumerable<ToolMetaDataModel> GetToolMetData()
        {
            return _serviceProvider
                .GetServices<ITool>()
                .Select(t => new ToolMetaDataModel
                {
                    Key = t.Name,
                    Category = t.Category,
                    HelpText = t.HelpText,
                    ShortDescription = t.Description,
                    ShowToolInHelp = t.ShowToolInHelp
                });
        }
    }
}
