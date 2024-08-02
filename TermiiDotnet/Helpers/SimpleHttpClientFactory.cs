using System.Net.Http;

namespace Termii.NET.Helpers
{
    internal class SimpleHttpClientFactory : IHttpClientFactory
    {
        public HttpClient CreateClient(string? name = null)
        {
            return new HttpClient();
        }
    }

}
