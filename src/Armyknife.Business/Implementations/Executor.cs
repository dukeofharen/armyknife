using System;
using System.Linq;
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
      private readonly ILogger _logger;
      private readonly IOutputWriter _outputWriter;
      private readonly IToolResolver _toolResolver;

      public Executor(
          IAssemblyService assemblyService,
          IConsoleService consoleService,
          IInputReader inputReader,
          ILogger logger,
          IOutputWriter outputWriter,
          IToolResolver toolResolver)
      {
         _assemblyService = assemblyService;
         _consoleService = consoleService;
         _inputReader = inputReader;
         _logger = logger;
         _outputWriter = outputWriter;
         _toolResolver = toolResolver;
      }

      public async Task ExecuteAsync(string[] args)
      {
         string result;
         try
         {
            if (args == null || args.Length == 0)
            {
               args = new[] { Constants.HelpKey };
            }

            _logger.Log(this, $"Armyknife called with the following arguments: {string.Join(" ", args)}");

            var argsDictionary = args.Parse();
            string toolName = args.FirstOrDefault();
            var tool = _toolResolver.ResolveTool(toolName);
            if (tool == null)
            {
               throw new ArmyknifeException(string.Format(ExceptionResources.NoToolFoundMessage, toolName));
            }

            _logger.Log(this, $"Found tool '{tool.Name}'");

            string input = _inputReader.GetInput(args, argsDictionary);
            if (!string.IsNullOrEmpty(input))
            {
               argsDictionary.Add(Constants.InputKey, input);
            }

            _logger.Log(this, argsDictionary);

            if (tool is IAsynchronousTool asyncTool)
            {
               _logger.Log(this, "The tool is asynchronous.");
               result = await asyncTool.ExecuteAsync(argsDictionary);
            }
            else if (tool is ISynchronousTool syncTool)
            {
               _logger.Log(this, "The tool is synchronous.");
               result = syncTool.Execute(argsDictionary);
            }
            else
            {
               throw new InvalidOperationException(string.Format(ExceptionResources.ToolTypeNotSupported, tool.Name));
            }
         }
         catch (ArmyknifeException exception)
         {
            result = exception.Message;
            _logger.Log(this, exception);
         }
         catch (Exception exception)
         {
            result = string.Format(GenericResources.SomethingWentWrong, exception.Message, GenericResources.GithubUrl);
            _logger.Log(this, exception);
         }

         _logger.Log(this, $"The returned result: {result}");
         _outputWriter.WriteOutput(result);
      }
   }
}
