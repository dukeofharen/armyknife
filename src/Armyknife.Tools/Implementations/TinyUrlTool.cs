using Armyknife.Business.Interfaces;
using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Resources;
using Armyknife.Services.Interfaces;
using Armyknife.Utilities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Armyknife.Tools.Implementations
{
    internal class TinyUrlTool : IAsynchronousTool
    {
        private readonly IWebService _webService;

        public TinyUrlTool(IWebService webService)
        {
            _webService = webService;
        }

        public string Name => "tinyurl";

        public string Description => ToolResources.TinyUrlDescription;

        public string Category => CategoryResources.WebCategory;

        public string HelpText => ToolResources.TinyUrlHelp;

        public bool ShowToolInHelp => true;

        public async Task<string> ExecuteAsync(IDictionary<string, string> args)
        {
            string input = args.GetValue(Constants.InputKey);
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArmyknifeException(ExceptionResources.NoInput);
            }

            string url = $"http://tinyurl.com/api-create.php?url={WebUtility.UrlEncode(input)}";
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(url)
            };

            var response = await _webService.DoRequest(request);
            if(response.StatusCode != HttpStatusCode.OK)
            {
                throw new ArmyknifeException($"TinyURL returned unexpected HTTP status code '{response.StatusCode}'.");
            }

            string result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
