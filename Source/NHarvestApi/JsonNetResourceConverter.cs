using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NHarvestApi
{
    class JsonNetResourceConverter : IResourceConverter
    {
        public async Task<T> Get<T>(HttpClient httpClient, string resourceRelativePath)
        {
            var response = await httpClient.GetAsync(resourceRelativePath);
            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseAsString);
        }
    }
}