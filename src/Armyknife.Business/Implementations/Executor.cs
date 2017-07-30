using System;
using System.Linq;
using Armyknife.Models;
using Armyknife.Utilities;

namespace Armyknife.Business.Implementations
{
    public class Executor : IExecutor
    {
        private readonly IInputReader _inputReader;
        private readonly IOutputWriter _outputWriter;
        private readonly IToolResolver _toolResolver;

        public Executor(
            IInputReader inputReader,
            IOutputWriter outputWriter,
            IToolResolver toolResolver)
        {
            _inputReader = inputReader;
            _outputWriter = outputWriter;
            _toolResolver = toolResolver;
        }

        public void Execute(string[] args)
        {
            var argsDictionary = args.Parse();
            string toolName = args.FirstOrDefault();
            var tool = _toolResolver.ResolveTool(toolName);
            if (tool == null)
            {
                // TODO throw exception of zo
            }

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
