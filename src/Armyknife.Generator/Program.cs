using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Armyknife.Generator
{
   class Program
   {
      static void Main(string[] args)
      {
         string toolName = "Test";
         string categoryName = "Test";
         var toolType = ToolType.SynchronousTool;
         GenerateTool(toolName, categoryName, toolType);
      }

      private static void GenerateTool(string toolName, string categoryName, ToolType toolType)
      {
         string binFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
         string sourcePath = Path.Combine(binFolder, "..", "..", "..", "..");

         // Define variables
         string actualToolName = $"{toolName.First().ToString().ToUpper()}{toolName.ToLower().Substring(1)}";
         string toolKey = toolName.ToLower();
         string toolClassName = $"{actualToolName}Tool";
         string toolClassPath = Path.Combine(sourcePath, "Armyknife.Tools", "Implementations", $"{toolClassName}.cs");

         string unitTestClassName = $"{toolClassName}Facts";
         string unitTestClassPath = Path.Combine(sourcePath, "Armyknife.Tools.Tests", "Implementations", $"{unitTestClassName}.cs");

         string integrationTestClassName = $"{toolClassName}IntegrationTests";
         string integrationTestClassPath = Path.Combine(sourcePath, "Armyknife.Integration.Tests", "Tools", $"{integrationTestClassName}.cs");

         string toolClass = GenerateToolClass(actualToolName, toolKey, toolClassName, categoryName, toolType);
         File.WriteAllText(toolClassPath, toolClass);

         string unitTestClass = GenerateUnitTestClass(toolClassName, unitTestClassName, toolType);
         File.WriteAllText(unitTestClassPath, unitTestClass);

         string integrationTestClass = GenerateIntegrationTestClass(integrationTestClassName, toolClassName, toolKey, toolType);
         File.WriteAllText(integrationTestClassPath, integrationTestClass);
      }

      private static string GenerateToolClass(string toolName, string toolKey, string toolClassName, string categoryName, ToolType toolType)
      {
         // Create tool class
         string toolClass = GetToolClassTemplate(toolType);
         toolClass = toolClass.Replace("[TOOLCLASSNAME]", toolClassName);
         toolClass = toolClass.Replace("[TOOLNAME]", toolName);
         toolClass = toolClass.Replace("[TOOLKEY]", toolKey);
         toolClass = toolClass.Replace("[CATEGORYNAME]", categoryName);
         toolClass = toolClass.Replace("[TOOLTYPE]", toolType.ToString());

         return toolClass;
      }

      private static string GenerateUnitTestClass(string toolClassName, string unitTestClassName, ToolType toolType)
      {
         // Create tool unit test class
         string unitTestClass = GetUnitTestClassTemplate(toolType);
         unitTestClass = unitTestClass.Replace("[UNITTESTCLASSNAME]", unitTestClassName);
         unitTestClass = unitTestClass.Replace("[TOOLCLASSNAME]", toolClassName);

         return unitTestClass;
      }

      private static string GenerateIntegrationTestClass(string integrationTestClassName, string toolClassName, string toolKey, ToolType toolType)
      {
         // Create integration test class
         string integrationTestClass = GetIntegrationTestClassTemplate(toolType);
         integrationTestClass = integrationTestClass.Replace("[INTEGRATIONTESTCLASSNAME]", integrationTestClassName);
         integrationTestClass = integrationTestClass.Replace("[TOOLCLASSNAME]", toolClassName);
         integrationTestClass = integrationTestClass.Replace("[TOOLKEY]", toolKey);

         return integrationTestClass;
      }

      private static string GetToolClassTemplate(ToolType toolType)
      {
         switch (toolType)
         {
            case ToolType.SynchronousTool:
               return GeneratorResources.SynchronousToolTemplate;
            default:
               throw new NotImplementedException($"Tool type {toolType} not supported.");
         }
      }

      private static string GetUnitTestClassTemplate(ToolType toolType)
      {
         switch (toolType)
         {
            case ToolType.SynchronousTool:
               return GeneratorResources.SynchronousUnitTestTemplate;
            default:
               throw new NotImplementedException($"Tool type {toolType} not supported.");
         }
      }

      private static string GetIntegrationTestClassTemplate(ToolType toolType)
      {
         switch (toolType)
         {
            case ToolType.SynchronousTool:
               return GeneratorResources.SynchronousIntegrationTestTemplate;
            default:
               throw new NotImplementedException($"Tool type {toolType} not supported.");
         }
      }

      private enum ToolType
      {
         SynchronousTool,
         AsynchronousTool
      }
   }
}
