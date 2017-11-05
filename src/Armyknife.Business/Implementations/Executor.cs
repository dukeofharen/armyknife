using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Resources;
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
            if (args == null || args.Length == 0)
            {
                args = new[] { Constants.HelpKey };
            }

            var argsDictionary = args.Parse();
            string toolName = args.FirstOrDefault();
            var tool = _toolResolver.ResolveTool(toolName);
            if (tool == null)
            {
                throw new ArmyknifeException(string.Format(ExceptionResources.NoToolFoundMessage, toolName));
            }

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
