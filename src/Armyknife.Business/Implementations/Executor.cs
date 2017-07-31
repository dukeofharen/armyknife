using System;
using System.Linq;
using System.Text;
using Armyknife.Business.Tools;
using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Resources;
using Armyknife.Services;
using Armyknife.Utilities;

namespace Armyknife.Business.Implementations
{
    internal class Executor : IExecutor
    {
        private readonly IConsoleService _consoleService;
        private readonly IInputReader _inputReader;
        private readonly IOutputWriter _outputWriter;
        private readonly IToolResolver _toolResolver;

        public Executor(
            IConsoleService consoleService,
            IInputReader inputReader,
            IOutputWriter outputWriter,
            IToolResolver toolResolver)
        {
            _consoleService = consoleService;
            _inputReader = inputReader;
            _outputWriter = outputWriter;
            _toolResolver = toolResolver;
        }

        public void Execute(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                throw new ArmyknifeException(ExceptionResources.NoArgs);
            }

            var argsDictionary = args.Parse();
            string toolName = args.FirstOrDefault();
            if (toolName == Constants.HelpKey)
            {
                ShowGenericHelp();
            }
            else
            {
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

                    var result = tool.Execute(argsDictionary);

                    _outputWriter.WriteOutput(result, argsDictionary);
                }
            }
        }

        private void ShowGenericHelp()
        {
            var builder = new StringBuilder();
            var toolNames = _toolResolver.GetToolNames();
            foreach (string toolName in toolNames)
            {
                builder.AppendLine($"- {toolName}");
            }

            _consoleService.WriteLine(string.Format(ToolResources.GenericHelp, builder));
        }

        private void ShowToolHelp(ITool tool)
        {
            var builder = new StringBuilder();
            builder.Append(tool.Description);
            builder.Append(Environment.NewLine);
            builder.Append(tool.HelpText);
            _consoleService.WriteLine(builder.ToString());
        }
    }
}
