namespace OKX.Api.Common;

/// <summary>
/// Converts <see cref="int"/> values to and from their string representations when serializing and deserializing JSON.
/// </summary>
public class IntAsStringConverter : JsonConverter<int>
{
    /// <inheritdoc/>
    public override void WriteJson(JsonWriter writer, int value, JsonSerializer serializer)
        => writer.WriteValue(value.ToString(CultureInfo.InvariantCulture));

    /// <inheritdoc/>
    public override int ReadJson(
        JsonReader reader,
        Type objectType,
        int existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.String &&
            int.TryParse((string?)reader.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsed))
            return parsed;

        if (reader.TokenType == JsonToken.Integer || reader.TokenType == JsonToken.Float)
            return Convert.ToInt32(reader.Value, CultureInfo.InvariantCulture);

        throw new JsonSerializationException($"Invalid token for int: {reader.TokenType}");
    }
}
