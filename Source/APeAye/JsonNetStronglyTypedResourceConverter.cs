using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace APeAye
{
    public class JsonNetStronglyTypedResourceConverter : IResourceConverter
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public JsonNetStronglyTypedResourceConverter(JsonSerializerSettings jsonSerializerSettings)
        {
            Guard.IsNotNull(jsonSerializerSettings, "jsonSerializerSettings");
            _jsonSerializerSettings = jsonSerializerSettings;
        }

        public async Task<T> Convert<T>(HttpResponseMessage response)
        {
            var responseAsString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseAsString, _jsonSerializerSettings);
        }
    }
}