using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Termii.NET.Helpers
{
    internal class Utilities
    {
        private readonly IHttpClientFactory _httpClientFactory;

        internal Utilities(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        internal async Task<HttpResponseMessage> MakeHttpRequest(object request, string baseAddress, string requestUri, HttpMethod method, Dictionary<string, string>? headers = null)
        {
            try
            {
                using (var _httpClient = _httpClientFactory.CreateClient())
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    _httpClient.BaseAddress = new Uri(baseAddress);
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if (headers != null)
                    {
                        foreach (KeyValuePair<string, string> header in headers)
                        {
                            _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                        }
                    }
                    if (method == HttpMethod.Post)
                    {
                        string data = JsonConvert.SerializeObject(request);
                        HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
                        return await _httpClient.PostAsync(requestUri, content);
                    }
                    else if (method == HttpMethod.Get)
                    {
                        return await _httpClient.GetAsync(requestUri);
                    }
                    else if (method == HttpMethod.Put)
                    {
                        string data = JsonConvert.SerializeObject(request);
                        HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
                        return await _httpClient.PutAsync(requestUri, content);
                    }
                    else if (method == HttpMethod.Delete)
                    {
                        return await _httpClient.DeleteAsync(requestUri);
                    }
                    else throw new HttpRequestException("Invalid request type");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
