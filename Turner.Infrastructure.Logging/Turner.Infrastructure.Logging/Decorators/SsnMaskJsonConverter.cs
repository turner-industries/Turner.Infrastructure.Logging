using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Turner.Infrastructure.Logging.Decorators
{
    public class SsnMaskJsonConverter : MaskJsonConverter
    {
        public readonly string[] SsnPropertyNames =
        {
            "SSN",
            "SocialSecurityNumber"
        };

        public SsnMaskJsonConverter()
            : base(RegExPatterns.Ssn, RegExPatterns.SsnMask)
        {
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (writer.WriteState == WriteState.Property)
            {
                var propertyNames = writer.Path.Split('.');
                var mostDerivedName = propertyNames[propertyNames.Length - 1];

                if (SsnPropertyNames.Any(x => string.Compare(x, mostDerivedName, StringComparison.OrdinalIgnoreCase) == 0))
                {
                    base.WriteJson(writer, value, serializer);
                    return;
                }
            }

            var token = JToken.FromObject(value);
            token.WriteTo(writer);
        }

        public static class RegExPatterns
        {
            public const string Ssn = "(?=\\d{5})\\d";
            public const string SsnMask = "x";
        }
    }
}
