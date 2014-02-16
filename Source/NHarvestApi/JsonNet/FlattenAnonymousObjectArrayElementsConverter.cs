using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NHarvestApi.JsonNet
{
    public class FlattenAnonymousObjectArrayElementsConverter<TCollection, T> : JsonConverter
        where TCollection : ICollection<T>, new() where T : new()
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var entries = (objectType == typeof (TCollection))
                ? new TCollection()
                : (TCollection) Activator.CreateInstance(objectType);

            if (reader.TokenType == JsonToken.StartArray)
                reader.Read();

            var contractResolver = serializer.ContractResolver as DefaultContractResolver;
            if (contractResolver == null)
            {
                throw new InvalidOperationException("The IContractResolver must be of type DefaultContractResolver.");
            }

            var resolvedPropertyName = contractResolver.GetResolvedPropertyName(typeof (T).Name);

            while (reader.TokenType != JsonToken.EndArray)
            {
                var entry = new T();
                reader.Read(); // advance past StartObject of anonymous object array element
                if (reader.TokenType != JsonToken.PropertyName || (string) reader.Value != resolvedPropertyName)
                {
                    throw new InvalidDataException("Expected JSON token doesn't exist.");
                }
                reader.Read(); // advance past single property of anonymous object array element

                serializer.Populate(reader, entry);

                entries.Add(entry);

                reader.Read();
                reader.Read();
            }

            return entries;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof (TCollection).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }
    }
}