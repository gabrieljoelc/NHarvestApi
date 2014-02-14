using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NHarvestApi.Tests
{
    [TestClass]
    public class KeepinItSimpleSmokeSignal
    {
        [TestClass]
        public class When_using_basic_auth
        {
            private ApiBasicAuthSettings _apiBasicAuthSettings;

            private const string ChangeIt = "change it!";

            [TestInitialize]
            public void Setup()
            {
                Assert.AreNotEqual(ChangeIt, ConfigurationManager.AppSettings["subdomain"], "Update App.config with your Harvest subdomain.");
                Assert.AreNotEqual(ChangeIt, ConfigurationManager.AppSettings["username"], "Update App.config with your Harvest username.");
                Assert.AreNotEqual(ChangeIt, ConfigurationManager.AppSettings["password"], "Update App.config with your Harvest password.");

                _apiBasicAuthSettings = new ApiBasicAuthSettings(ConfigurationManager.AppSettings["subdomain"],
                    ConfigurationManager.AppSettings["username"], ConfigurationManager.AppSettings["password"]);
            }

            [TestMethod]
            public async Task Who_am_i_can_return_result()
            {
                var api = new HarvestApi<ApiBasicAuthSettings>(new BasicAuthHttpClientFactory());

                var hash = await api.Get<Hash>(_apiBasicAuthSettings, factory => factory.WhoAmI());

                Assert.IsNotNull(hash);
            }

            private class Hash
            {
                public User User { get; set; }
            }

            private class User
            {
                public int Id { get; set; }
                public string FirstName { get; set; }
                public string LastName { get; set; }
            }
        }
    }
}
