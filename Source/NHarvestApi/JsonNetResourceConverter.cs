using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NHarvestApi
{
    class JsonNetResourceConverter : IResourceConverter
    {
        public async Task<T> Get<T>(HttpClient httpClient, string resourceRelativePath)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, resourceRelativePath);
            var response = await httpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            
            var responseAsString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseAsString);
        }
    }
}