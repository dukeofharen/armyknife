﻿using System;
using System.Collections.Generic;
using System.Linq;
using Armyknife.Business.Tools;
using Armyknife.Models;
using Microsoft.Extensions.DependencyInjection;

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
                .FirstOrDefault(t => t.Name == name);
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
                    ShortDescription = t.Description
                });
        }
    }
}