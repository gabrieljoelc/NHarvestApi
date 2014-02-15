using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace NHarvestApi.Tests
{
    [TestClass]
    public class UnderscoredPropertyNamesContractResolverTests
    {
        private JsonSerializerSettings _settingsWithPascalContractResolver;

        [TestInitialize]
        public void Setup()
        {
            _settingsWithPascalContractResolver = new JsonSerializerSettings
            {
                ContractResolver = new UnderscoredPropertyNamesContractResolver()
            };
        }

        [TestMethod]
        public void Can_get_resolved_property_name()
        {
            const string expected = "foo_bar";
            var actual = new UnderscoredPropertyNamesContractResolver().GetResolvedPropertyName("FooBar");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Can_deserialize_underscored_json_properties_to_pascal_cased()
        {
            const string json = "{\"foo_bar\":15}";
            
            var zap = JsonConvert.DeserializeObject<Zap>(json, _settingsWithPascalContractResolver);
            
            Assert.IsNotNull(zap);
            Assert.AreEqual(15, zap.FooBar);
        }

        [TestMethod]
        public void Can_serialize_underscored_json_properties_to_pascal_cased()
        {
            const string expected = "{\"foo_bar\":15}";
            var zap = new Zap {FooBar = 15};

            var actual = JsonConvert.SerializeObject(zap, _settingsWithPascalContractResolver);

            Assert.AreEqual(expected, actual);
        }

        private class Zap
        {
            //[JsonProperty(PropertyName = "foo_bar")]
            public int FooBar { get; set; }
        }
    }
}
