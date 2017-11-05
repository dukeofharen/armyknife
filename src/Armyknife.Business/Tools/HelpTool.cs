using Armyknife.Business.Interfaces;
using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Resources;
using Armyknife.Services.Interfaces;
using Armyknife.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Armyknife.Business.Tools
{
    internal class HelpTool : ISynchronousTool
    {
        private readonly IAssemblyService _assemblyService;
        private readonly IToolResolver _toolResolver;

        public HelpTool(
            IAssemblyService assemblyService,
            IToolResolver toolResolver)
        {
            _assemblyService = assemblyService;
            _toolResolver = toolResolver;
        }

        public string Name => Constants.HelpKey;

        public string Description => string.Empty;

        public string Category => string.Empty;

        public string HelpText => string.Empty;

        public bool ShowToolInHelp => false;

        public string Execute(IDictionary<string, string> args)
        {
            var builder = new StringBuilder();
            string result;
            string version = _assemblyService.GetVersionNumber();

            // If the HelpTool has any input, it means it should show the help page of a specific tool.
            string input = args.GetValue(Constants.InputKey);
            if (!string.IsNullOrWhiteSpace(input))
            {
                var tool = _toolResolver.ResolveTool(input);
                if (tool == null)
                {
                    throw new ArmyknifeException($"No tool found with name '{input}'");
                }
                else
                {
                    builder.Append(tool.Description);
                    builder.Append(Environment.NewLine);
                    builder.Append(tool.HelpText);
                    result = builder.ToString();
                }
            }
            else
            {
                var tools = _toolResolver.GetToolMetData();
                var toolCategoryGroups = tools
                    .Where(t => t.ShowToolInHelp)
                    .GroupBy(t => t.Category, t => t);

                foreach (var group in toolCategoryGroups)
                {
                    builder.AppendLine(group.Key);
                    foreach (var tool in group)
                    {
                        builder.AppendLine($"- {tool.Key}: {tool.ShortDescription}");
                    }

                    builder.AppendLine();
                }

                result = string.Format(GenericResources.GenericHelp, builder, version);
            }

            return result;
        }
    }
}
