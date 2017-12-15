using Armyknife.Services.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace Armyknife.Services.Implementations
{
    internal class WebService : IWebService
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        public async Task<HttpResponseMessage> DoRequestAsync(HttpRequestMessage request)
        {
            return await HttpClient.SendAsync(request);
        }
    }
}
