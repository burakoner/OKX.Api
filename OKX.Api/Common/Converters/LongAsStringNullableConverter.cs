namespace OKX.Api.Common;

/// <summary>
/// Converts nullable <see cref="long"/> values to and from their string representations when serializing and
/// deserializing JSON.
/// </summary>
/// <remarks>This converter accepts null, empty strings, numeric strings, and integer/float JSON tokens. Empty
/// strings are treated as <see langword="null"/>.</remarks>
public class LongAsStringNullableConverter : JsonConverter<long?>
{
    /// <inheritdoc/>
    public override void WriteJson(JsonWriter writer, long? value, JsonSerializer serializer)
    {
        if (value.HasValue)
            writer.WriteValue(value.Value.ToString(CultureInfo.InvariantCulture));
        else
            writer.WriteNull();
    }

    /// <inheritdoc/>
    public override long? ReadJson(
        JsonReader reader,
        Type objectType,
        long? existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return null;

        if (string.IsNullOrEmpty(reader.Value?.ToString()))
            return null;

        if (reader.TokenType == JsonToken.String &&
            long.TryParse((string)reader.Value!, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            return result;

        if (reader.TokenType == JsonToken.Integer || reader.TokenType == JsonToken.Float)
            return Convert.ToInt64(reader.Value, CultureInfo.InvariantCulture);

        throw new JsonSerializationException($"Invalid token for long?: {reader.TokenType}");
    }
}
