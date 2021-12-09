using System;
using System.Text;
using System.Text.Json;

namespace DreamBook.Application.Converters
{
    public class UnixTimeConvertor : System.Text.Json.Serialization.JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(new DateTimeOffset(value).ToUnixTimeMilliseconds().ToString());
        }
    }
}
