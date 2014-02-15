using System;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;

namespace NHarvestApi
{
    public class HarvestApi<TSettings>
    {
        private readonly IHttpClientFactory<TSettings> _httpClientFactory;
        private readonly IResourceConverter _resourceConverter;
        private readonly IResourcePathFactory _resourcePathFactory;

        public HarvestApi(IHttpClientFactory<TSettings> httpClientFactory, IResourceConverter resourceConverter = null,
            IResourcePathFactory resourcePathFactory = null)
        {
            _httpClientFactory = httpClientFactory;
            _resourcePathFactory = resourcePathFactory ?? new DefaultHarvestResourcePathFactory();
            _resourceConverter = resourceConverter ?? new JsonNetStronglyTypedResourceConverter();
        }

        public async Task<T> Get<T>(TSettings settings,
            Expression<Func<IResourcePathFactory, string>> uriFactoryExpression, HttpClientHandler handler = null)
        {
            var httpMethod = HttpMethod.Get;
            return await SendAsync<T>(settings, uriFactoryExpression, httpMethod, handler);
        }

        private async Task<T> SendAsync<T>(TSettings settings,
            Expression<Func<IResourcePathFactory, string>> uriFactoryExpression, HttpMethod httpMethod,
            HttpClientHandler handler = null)
        {
            using (var httpClient = _httpClientFactory.CreateClient(settings, handler))
            {
                var message = new HttpRequestMessage(httpMethod, uriFactoryExpression.Compile()(_resourcePathFactory));
                var response = await httpClient.SendAsync(message);
                response.EnsureSuccessStatusCode();

                return await _resourceConverter.Convert<T>(response);
            }
        }

        // TODO: use fluent API that assumes defaults unless you want to specify your own IHttpClientFactory, IResourceConverter, IResourcePathFactory, etc. 
    }
}