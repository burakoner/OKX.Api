namespace OKX.Api.Common;

/// <summary>
/// Converts OKX economic calendar <c>dateSpan</c> values to a semantic boolean.
/// </summary>
/// <remarks>
/// OKX uses the string value <c>"0"</c> when the exact event time is known and <c>"1"</c> when only the date is known.
/// This converter exposes that as <see langword="true"/> for "has exact time" and <see langword="false"/> for date-only values.
/// </remarks>
public sealed class EconomicCalendarDateSpanConverter : JsonConverter<bool>
{
    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, bool value, JsonSerializer serializer)
    {
        writer.WriteValue(value ? "0" : "1");
    }

    /// <inheritdoc />
    public override bool ReadJson(JsonReader reader, Type objectType, bool existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null || reader.Value is null)
            return false;

        if (reader.TokenType == JsonToken.Integer)
            return Convert.ToInt32(reader.Value, CultureInfo.InvariantCulture) == 0;

        if (reader.TokenType == JsonToken.Boolean)
            return Convert.ToBoolean(reader.Value, CultureInfo.InvariantCulture);

        if (reader.TokenType == JsonToken.String)
        {
            var value = ((string)reader.Value).Trim();
            return value switch
            {
                "0" => true,
                "1" => false,
                _ => throw new JsonSerializationException($"Invalid economic calendar dateSpan value: '{reader.Value}'")
            };
        }

        throw new JsonSerializationException($"Invalid token for economic calendar dateSpan: {reader.TokenType}");
    }
}
