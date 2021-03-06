﻿using System.Net.Http;
using System.Threading.Tasks;

namespace Armyknife.Services.Interfaces
{
    public interface IWebService
    {
        Task<HttpResponseMessage> DoRequestAsync(HttpRequestMessage request);
    }
}
