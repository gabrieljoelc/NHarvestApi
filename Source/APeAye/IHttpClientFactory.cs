using System.Net.Http;

namespace APeAye
{
    public interface IHttpClientFactory<in TSettings>
    {
        HttpClient CreateClient(TSettings settings, HttpClientHandler handler = null);
    }
}