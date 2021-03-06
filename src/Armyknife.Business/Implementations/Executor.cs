﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Resources;
using Armyknife.Utilities;
using Armyknife.Business.Interfaces;
using Armyknife.Services.Interfaces;
using System.Text;

namespace Armyknife.Business.Implementations
{
   internal class Executor : IExecutor
   {
      private readonly IInputReader _inputReader;
      private readonly ILogger _logger;
      private readonly IOutputWriter _outputWriter;
      private readonly IToolResolver _toolResolver;

      public Executor(
          IInputReader inputReader,
          ILogger logger,
          IOutputWriter outputWriter,
          IToolResolver toolResolver)
      {
         _inputReader = inputReader;
         _logger = logger;
         _outputWriter = outputWriter;
         _toolResolver = toolResolver;
      }

      public async Task<int> ExecuteAsync(string[] args)
      {
         int exitCode = -1;
         bool debug = false;
         string result;
         string toolName = string.Empty;
         try
         {
            if (args == null || args.Length == 0)
            {
               args = new[] { Constants.HelpKey };
            }

            _logger.Log(this, $"Armyknife called with the following arguments: {string.Join(" ", args)}");

            var argsDictionary = args.Parse();
            debug = argsDictionary.Keys.Any(k => string.Equals(k, "debug", StringComparison.OrdinalIgnoreCase));
            toolName = args.FirstOrDefault();
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

            exitCode = 0;
         }
         catch (ArmyknifeException exception)
         {
            result = exception.Message;
            _logger.Log(this, exception);
         }
         catch (Exception exception)
         {
            result = string.Format(GenericResources.SomethingWentWrong, toolName, exception.Message, GenericResources.GithubUrl);
            _logger.Log(this, exception);
         }

         if (debug)
         {
            _logger.Log(this, $"The returned result: {result}");
            var builder = new StringBuilder();
            foreach (string message in _logger.GetLogMessages())
            {
               builder.AppendLine(message);
            }

            _outputWriter.WriteOutput(builder.ToString());
         }
         else
         {
            _outputWriter.WriteOutput(result);
         }

         return exitCode;
      }
   }
}
