using System;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NHarvestApi.JsonNet;

namespace NHarvestApi.Harvest
{
    public class HarvestApi<TSettings> : ApiBase<TSettings>
    {
        private readonly IHarvestResourcePathFactory _resourcePathFactory;

        public HarvestApi(IHttpClientFactory<TSettings> httpClientFactory, IHarvestResourcePathFactory resourcePathFactory = null)
            : base(httpClientFactory)
        {
            _resourcePathFactory = resourcePathFactory ?? new DefaultHarvestResourcePathFactory();
        }

        public async Task<T> Get<T>(TSettings settings,
            Expression<Func<IHarvestResourcePathFactory, string>> uriFactoryExpression,
            IResourceConverter resourceConverter = null, HttpClientHandler handler = null)
        {
            var httpMethod = HttpMethod.Get;
            return await SendAsync<T>(settings, uriFactoryExpression, httpMethod, resourceConverter, handler);
        }

        private async Task<T> SendAsync<T>(TSettings settings,
            Expression<Func<IHarvestResourcePathFactory, string>> uriFactoryExpression, HttpMethod httpMethod,
            IResourceConverter resourceConverter, HttpClientHandler handler = null)
        {
            string requestUri = uriFactoryExpression.Compile()(_resourcePathFactory);
            resourceConverter = resourceConverter ?? HarvestResourceConverterDefaults.Create();
            return await SendAsync<T>(settings, httpMethod, handler, requestUri, resourceConverter);
        }

        // TODO: use fluent API that assumes defaults unless you want to specify your own IHttpClientFactory, IResourceConverter, IHarvestResourcePathFactory, etc. 
    }

    public static class HarvestResourceConverterDefaults
    {
        public static IResourceConverter Create(params JsonConverter[] jsonConverters)
        {
            var settings = new JsonSerializerSettings
            {
                // this would get factored out if we ever make the API wrapper portion generic
                ContractResolver = new UnderscoredPropertyNamesContractResolver(),
            };
            if (jsonConverters != null)
            {
                foreach (var jsonConverter in jsonConverters)
                {
                    settings.Converters.Add(jsonConverter);
                }
            }
            return new JsonNetStronglyTypedResourceConverter(settings);
        }
    }
}