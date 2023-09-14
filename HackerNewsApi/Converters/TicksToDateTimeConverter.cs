
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HackerNewsApi.Converters;

public class TicksToDateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number)
        {
            var ticks = reader.GetInt64();
            return DateTimeOffset.FromUnixTimeSeconds(ticks).UtcDateTime;
        }

        throw new JsonException("Expected integer value for ticks.");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy-MM-ddTHH:mm:sszzz"));
    }
}