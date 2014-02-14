using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace NHarvestApi.Tests
{
    [TestClass]
    public class ApiBasicAuthSettingsTests
    {
        [TestMethod]
        public void Can_serialize_and_deserialize_with_Json_NET()
        {
            var settings = new ApiBasicAuthSettings("fakeit");
            settings.SetCredentials("fakeit", "fakeit");
            var json = JsonConvert.SerializeObject(settings);
            var deserializedSettings = JsonConvert.DeserializeObject<ApiBasicAuthSettings>(json);
            Assert.IsNotNull(deserializedSettings);
            Assert.AreEqual(settings.BaseUri, deserializedSettings.BaseUri);
            Assert.AreEqual(settings.CredentialsValue, deserializedSettings.CredentialsValue);
        }
    }
}
