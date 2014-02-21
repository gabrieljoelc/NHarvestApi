using APeAye;
using Newtonsoft.Json;
using NHarvestApi.JsonNet;

namespace NHarvestApi
{
    public static class HarvestResourceConverterDefaults
    {
        public static IResourceConverter Create(params JsonConverter[] jsonConverters)
        {
            var settings = new JsonSerializerSettings
            {
                // this would get factored out if we ever make the API wrapper portion generic
                ContractResolver = new UnderscoredPropertyNamesContractResolver(),
            };
            if (jsonConverters == null) return new JsonNetStronglyTypedResourceConverter(settings);
            
            foreach (var jsonConverter in jsonConverters)
            {
                settings.Converters.Add(jsonConverter);
            }
            return new JsonNetStronglyTypedResourceConverter(settings);
        }
    }
}