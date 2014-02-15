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
                                          // this would get factored out if we ever make the API wrapper portion generic
                                          ContractResolver = new UnderscoredPropertyNamesContractResolver()
                                      };
        }

        public async Task<T> Convert<T>(HttpResponseMessage response)
        {
            var responseAsString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseAsString, _jsonSerializerSettings);
        }
    }
}