using System.Net.Http;

namespace NHarvestApi
{
    public class BasicAuthHttpClientFactory : IHttpClientFactory<ApiBasicAuthSettings>
    {
        public HttpClient CreateClient(ApiBasicAuthSettings settings)
        {
            Guard.IsNotNull(settings, "settings");

            // from http://msdn.microsoft.com/en-us/library/windows/apps/hh781239.aspx & http://blogs.msdn.com/b/bclteam/archive/2013/02/18/portable-httpclient-for-net-framework-and-windows-phone.aspx
            var httpClient = new HttpClient { MaxResponseContentBufferSize = settings.MaxResponseContentBufferSize };
            httpClient.DefaultRequestHeaders.Add("Accept", settings.HttpAcceptValue);
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + settings.CredentialsValue);
            httpClient.DefaultRequestHeaders.Add("User-Agent", settings.RequestHeaderUserAgent);
            httpClient.BaseAddress = settings.BaseUri;
            return httpClient;
        }
    }
}