using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NHarvestApi.KeepinItSimpleSmokeSignalConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            When_basic_auth_defaults(args).Wait();
        }

        private static async Task When_basic_auth_defaults(string[] args)
        {
            if (args.Length != 3 || args.Any(string.IsNullOrEmpty))
                throw new ArgumentException("Need all three Harvest args for basic auth smoke test.");

            var subdomain = args[0];
            var username = args[1];
            var password = args[2];

            var apiBasicAuthSettings = new ApiBasicAuthSettings(subdomain);
            apiBasicAuthSettings.SetCredentials(username, password);
            var api = new HarvestApi<ApiBasicAuthSettings>(new BasicAuthHttpClientFactory());

            await Can_get_result_from_who_am_i(api, apiBasicAuthSettings);
        }

        private static async Task Can_get_result_from_who_am_i(HarvestApi<ApiBasicAuthSettings> api, ApiBasicAuthSettings apiBasicAuthSettings)
        {
            var hash = await api.Get<Hash>(apiBasicAuthSettings, factory => factory.WhoAmI());

            if (hash == null || hash.User == null)
                Debug.Fail("who_am_i returned null.");
        }

        private class Hash
        {
            public User User { get; set; }
        }

        private class User
        {
            public int Id { get; set; }
            
            [JsonProperty(PropertyName = "first_name")]
            public string FirstName { get; set; }

            [JsonProperty(PropertyName = "last_name")]
            public string LastName { get; set; }
        }
    }
}
