using Armyknife.Business;
using Armyknife.Business.Interfaces;
using Armyknife.Models;
using Armyknife.Resources;
using Armyknife.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using Armyknife.DI.DnCore;
using Microsoft.Extensions.DependencyInjection;

namespace Armyknife.DocGenerator
{
   class Program
   {
      // ReSharper disable once UnusedParameter.Local
      static void Main(string[] args)
      {
         var serviceCollection = new ServiceCollection();
         var wrapper = new DnCoreServiceContainerWrapper(serviceCollection);
         DependencyRegistration.RegisterDependencies(wrapper);
         wrapper.Provider = serviceCollection.BuildServiceProvider();

         var toolResolver = wrapper.Resolve<IToolResolver>();
         var tools = toolResolver.GetToolMetData().ToArray();

         var assemblyService = wrapper.Resolve<IAssemblyService>();
         string version = assemblyService.GetVersionNumber();

         string toolList = GetToolList(tools);
         string toolDetails = GetToolDetails(tools);
         ReplaceDocVariables(toolList, toolDetails, version);
      }

      private static string GetToolList(IEnumerable<ToolMetaDataModel> tools)
      {
         var builder = new StringBuilder();
         var categoryGroups = tools
            .Where(t => t.ShowToolInHelp)
            .GroupBy(t => t.Category);
         foreach(var category in categoryGroups)
         {
            string categoryAnchor = category.Key.Replace(" ", "-");
            builder.AppendLine($@"<li><strong><a href=""#{categoryAnchor}"">{category.Key}</a></strong><ul>");
            foreach (var tool in category)
            {
               string toolAnchor = tool.Key.Replace(" ", "-");
               builder.AppendLine($@"<li><a href=""#{toolAnchor}"">{tool.Key}</a></li>");
            }

            builder.AppendLine("</ul></li>");
         }

         return builder.ToString();
      }

      private static string GetToolDetails(IEnumerable<ToolMetaDataModel> tools)
      {
         var builder = new StringBuilder();
         var categoryGroups = tools
            .Where(t => t.ShowToolInHelp)
            .GroupBy(t => t.Category);
         foreach (var category in categoryGroups)
         {
            string categoryAnchor = category.Key.Replace(" ", "-");
            builder.AppendLine($@"<a name=""{categoryAnchor}""></a>");
            builder.AppendLine($"<h2>{category.Key}</h2>");
            foreach (var tool in category)
            {
               string toolAnchor = tool.Key.Replace(" ", "-");
               string helpText = tool.HelpText;
               builder.AppendLine(@"<div class=""col-md-12"">");
               builder.AppendLine($@"<a name=""{toolAnchor}""></a>");
               builder.AppendLine($"<h3>{tool.Key}</h3>");
               builder.AppendLine($"<p>{tool.ShortDescription}</p>");
               builder.AppendLine($"<pre>{WebUtility.HtmlEncode(helpText)}</pre>");
               builder.AppendLine("</div>");
            }
         }

         return builder.ToString();
      }

      private static void ReplaceDocVariables(string toolList, string toolDetails, string version)
      {
         string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
         string docPath = Path.Combine(assemblyPath, "..", "..", "..", "..", "..", "docs", "index.html");
         string docContents = DocGeneratorResources.Template;
         var now = DateTime.Now;
         docContents = docContents.Replace("[TOOLS-LIST]", toolList);
         docContents = docContents.Replace("[TOOLS]", toolDetails);
         docContents = docContents.Replace("[YEAR]", now.Year.ToString());
         docContents = docContents.Replace("[VERSION]", version);
         docContents = docContents.Replace("[BUILD-DATE]", now.ToString("yyyy-MM-dd HH:mm:ss"));
         docContents = docContents.Replace("[HELPTEXT]", WebUtility.HtmlEncode(GenericResources.GenericHelp));
         File.WriteAllText(docPath, docContents);
      }
   }
}
