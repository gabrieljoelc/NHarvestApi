using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NHarvestApi
{
    public class HarvestApi<TSettings>
    {
        private readonly IHttpClientFactory<TSettings> _httpClientFactory;
        private readonly IResourceConverter _resourceConverter;
        private readonly IResourcePathFactory _resourcePathFactory;

        public HarvestApi(IHttpClientFactory<TSettings> httpClientFactory, IResourceConverter resourceConverter = null, IResourcePathFactory resourcePathFactory = null)
        {
            _httpClientFactory = httpClientFactory;
            _resourcePathFactory = resourcePathFactory ?? new DefaultResourcePathFactory();
            _resourceConverter = resourceConverter ?? new JsonNetStronglyTypedResourceConverter();
        }

        public async Task<T> Get<T>(TSettings settings, Expression<Func<IResourcePathFactory, string>> uriFactoryExpression)
        {
            using (var httpClient = _httpClientFactory.CreateClient(settings))
            {
                return await _resourceConverter.Get<T>(httpClient, uriFactoryExpression.Compile()(_resourcePathFactory));
            }
        }

        // TODO: use fluent API that assumes defaults unless you want to specify your own IHttpClientFactory, IResourceConverter, IResourcePathFactory, etc. 
    }
}