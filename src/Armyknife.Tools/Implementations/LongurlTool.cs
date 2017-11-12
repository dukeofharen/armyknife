using System.Collections.Generic;
using System.Threading.Tasks;
using Armyknife.Exceptions;
using Armyknife.Resources;
using Armyknife.Models;
using Armyknife.Business.Interfaces;
using Armyknife.Services.Interfaces;
using Armyknife.Utilities;
using System.Net.Http;
using System;

namespace Armyknife.Tools.Implementations
{
   internal class LongurlTool : IAsynchronousTool
   {
      private readonly IWebService _webService;

      public LongurlTool(IWebService webService)
      {
         _webService = webService;
      }

      public string Name => "longurl";

      public string Description => ToolResources.LongurlDescription;

      public string Category => CategoryResources.WebCategory;

      public string HelpText => ToolResources.LongurlHelp;

      public bool ShowToolInHelp => true;

      public async Task<string> ExecuteAsync(IDictionary<string, string> args)
      {
         if (!args.ContainsKey(Constants.InputKey))
         {
            throw new ArmyknifeException(ExceptionResources.NoInput);
         }

         string input = args.GetValue(Constants.InputKey);
         string userAgent = Constants.UserAgent;
         var request = new HttpRequestMessage
         {
            RequestUri = new Uri(input)
         };

         request.Headers.Add("User-Agent", userAgent);

         using (var response = await _webService.DoRequestAsync(request))
         {
            // The request URI is translated to the actual URL.
            return response.RequestMessage.RequestUri.ToString();
         }
      }
   }
}
