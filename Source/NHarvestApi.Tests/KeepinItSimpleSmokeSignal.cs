using System;
using System.Configuration;
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
            public void Who_am_i_can_return_result()
            {
                var api = new HarvestApi<ApiBasicAuthSettings>(new BasicAuthHttpClientFactory());

                var hash = api.Get<Hash>(_apiBasicAuthSettings, factory => factory.WhoAmI());

                Assert.IsNotNull(hash);
            }

            private class Hash
            {
            }
        }
    }
}
