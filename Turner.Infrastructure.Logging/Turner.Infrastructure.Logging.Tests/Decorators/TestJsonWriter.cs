using System.Collections.Generic;
using Newtonsoft.Json;

namespace Turner.Infrastructure.Logging.Tests.Decorators
{
    public class TestJsonWriter : JsonWriter
    {
        public readonly List<string> WrittenValues = new List<string>();

        public override void Flush()
        {
            WrittenValues.Clear();
        }

        public override void WriteValue(string value)
        {
            WrittenValues.Add(value);
        }
    }
}
