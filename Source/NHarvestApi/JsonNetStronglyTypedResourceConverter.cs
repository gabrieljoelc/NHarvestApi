using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NHarvestApi
{
    class JsonNetStronglyTypedResourceConverter : IResourceConverter
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public JsonNetStronglyTypedResourceConverter(JsonSerializerSettings jsonSerializerSettings = null)
        {
            _jsonSerializerSettings = jsonSerializerSettings ??
                                      new JsonSerializerSettings
                                      {
                                          ContractResolver = new UnderscoredPropertyNamesContractResolver()
                                      };
        }

        public async Task<T> Get<T>(HttpClient httpClient, string resourceRelativePath)
        {
            // TODO: probably factor this out into some other class
            var message = new HttpRequestMessage(HttpMethod.Get, resourceRelativePath);
            var response = await httpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseAsString, _jsonSerializerSettings);
        }
    }
}