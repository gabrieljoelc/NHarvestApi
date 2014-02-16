using System.Net.Http;
using System.Threading.Tasks;

namespace NHarvestApi
{
    public abstract class ApiBase<TSettings>
    {
        protected readonly IHttpClientFactory<TSettings> HttpClientFactory;

        protected ApiBase(IHttpClientFactory<TSettings> httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }

        protected async Task<T> SendAsync<T>(TSettings settings, HttpMethod httpMethod, HttpClientHandler handler, string requestUri,
            IResourceConverter converter)
        {
            using (var httpClient = HttpClientFactory.CreateClient(settings, handler))
            {
                var message = new HttpRequestMessage(httpMethod, requestUri);
                var response = await httpClient.SendAsync(message);
                response.EnsureSuccessStatusCode();

                return await converter.Convert<T>(response);
            }
        }
    }
}