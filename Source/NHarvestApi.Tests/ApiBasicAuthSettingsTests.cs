using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace NHarvestApi.Tests
{
    [TestClass]
    public class HarvestApiBasicAuthSettingsTests
    {
        [TestMethod]
        public void SetCredentials_can_hash_username_and_password()
        {
            var settings = new HarvestApiBasicAuthSettings("fakeit");
            settings.SetCredentials("fakeit", "fakeit");
            Assert.AreEqual("ZmFrZWl0OmZha2VpdA==", settings.CredentialsValue);
        }

        [TestMethod]
        public void Ctor_sets_BaseUri()
        {
            const string subdomain = "fakeit";
            var settings = new HarvestApiBasicAuthSettings(subdomain);
            Assert.AreEqual(new Uri("https://" + subdomain + ".harvestapp.com"), settings.BaseUri);
        }

        [TestMethod]
        public void Can_serialize_with_Json_NET()
        {
            const string expected = "{\"CredentialsValue\":\"ZmFrZWl0OmZha2VpdA==\",\"BaseUri\":\"https://fakeit.harvestapp.com\",\"Subdomain\":\"fakeit\",\"Username\":\"fakeit\",\"MaxResponseContentBufferSize\":256000,\"HttpAcceptValue\":\"application/json\",\"RequestHeaderUserAgent\":\"Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)\"}";
            var settings = new HarvestApiBasicAuthSettings("fakeit");
            settings.SetCredentials("fakeit", "fakeit");
            var actual = JsonConvert.SerializeObject(settings);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Can_deserialize_with_Json_NET()
        {
            var settings = new HarvestApiBasicAuthSettings("fakeit");
            settings.SetCredentials("fakeit", "fakeit");
            var json = JsonConvert.SerializeObject(settings);
            var deserializedSettings = JsonConvert.DeserializeObject<HarvestApiBasicAuthSettings>(json);
            Assert.IsNotNull(deserializedSettings);
            Assert.AreEqual(settings.Subdomain, settings.Subdomain);
            Assert.AreEqual(settings.Username, settings.Username);
            Assert.AreEqual(settings.BaseUri, deserializedSettings.BaseUri);
            Assert.AreEqual(settings.CredentialsValue, deserializedSettings.CredentialsValue);
        }
    }
}
