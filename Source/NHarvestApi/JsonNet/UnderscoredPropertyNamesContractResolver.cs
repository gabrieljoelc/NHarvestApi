using Newtonsoft.Json.Serialization;

namespace NHarvestApi.JsonNet
{
    public class UnderscoredPropertyNamesContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.Underscore();
        }
    }
}