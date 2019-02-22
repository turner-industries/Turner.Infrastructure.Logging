using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Turner.Infrastructure.Logging.Decorators
{
    public class MaskJsonConverter : JsonConverter
    {
        private readonly string _pattern;
        private readonly string _mask;

        public MaskJsonConverter(string pattern, string mask)
        {
            _pattern = pattern;
            _mask = mask;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var stringValue = (string)value;
            var newValue = new Regex(_pattern).Replace(stringValue, _mask);

            var token = JToken.FromObject(newValue);
            token.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override bool CanRead => false;
    }
}
