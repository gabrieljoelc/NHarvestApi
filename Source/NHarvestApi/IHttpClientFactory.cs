using System.Net.Http;

namespace NHarvestApi
{
    public interface IHttpClientFactory<in TSettings>
    {
        HttpClient CreateClient(TSettings settings, HttpClientHandler handler = null);
    }
}