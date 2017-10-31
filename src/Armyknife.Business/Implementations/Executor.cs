using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Resources;
using Armyknife.Services;
using Armyknife.Utilities;
using Armyknife.Business.Interfaces;
using Armyknife.Services.Interfaces;

namespace Armyknife.Business.Implementations
{
    internal class Executor : IExecutor
    {
        private readonly IAssemblyService _assemblyService;
        private readonly IConsoleService _consoleService;
        private readonly IInputReader _inputReader;
        private readonly IOutputWriter _outputWriter;
        private readonly IToolResolver _toolResolver;

        public Executor(
            IAssemblyService assemblyService,
            IConsoleService consoleService,
            IInputReader inputReader,
            IOutputWriter outputWriter,
            IToolResolver toolResolver)
        {
            _assemblyService = assemblyService;
            _consoleService = consoleService;
            _inputReader = inputReader;
            _outputWriter = outputWriter;
            _toolResolver = toolResolver;
        }

        public async Task ExecuteAsync(string[] args)
        {
            if (ShouldShowHelp(args))
            {
                ShowGenericHelp();
            }
            else
            {
                var argsDictionary = args.Parse();
                string toolName = args.FirstOrDefault();
                var tool = _toolResolver.ResolveTool(toolName);
                if (tool == null)
                {
                    throw new ArmyknifeException(string.Format(ExceptionResources.NoToolFoundMessage, toolName));
                }

                if (args.Length >= 2 && args[1] == Constants.HelpKey)
                {
                    ShowToolHelp(tool);
                }
                else
                {
                    string input = _inputReader.GetInput(args, argsDictionary);
                    if (!string.IsNullOrEmpty(input))
                    {
                        argsDictionary.Add(Constants.InputKey, input);
                    }

                    string result;
                    if (tool is IAsynchronousTool asyncTool)
                    {
                        result = await asyncTool.ExecuteAsync(argsDictionary);
                    }
                    else if (tool is ISynchronousTool syncTool)
                    {
                        result = syncTool.Execute(argsDictionary);
                    }
                    else
                    {
                        throw new InvalidOperationException(string.Format(ExceptionResources.ToolTypeNotSupported, tool.Name));
                    }

                    _outputWriter.WriteOutput(result, argsDictionary);
                }
            }
        }

        private void ShowGenericHelp()
        {
            var builder = new StringBuilder();
            var tools = _toolResolver.GetToolMetData();
            var toolCategoryGroups = tools
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

            string version = _assemblyService.GetVersionNumber();

            _consoleService.WriteLine(string.Format(GenericResources.GenericHelp, builder, version));
        }

        private void ShowToolHelp(ITool tool)
        {
            var builder = new StringBuilder();
            builder.Append(tool.Description);
            builder.Append(Environment.NewLine);
            builder.Append(tool.HelpText);
            _consoleService.WriteLine(builder.ToString());
        }

        private static bool ShouldShowHelp(string[] args)
        {
            return args == null || args.Length == 0 || args.Length >= 1 && args[0] == Constants.HelpKey;
        }
    }
}
