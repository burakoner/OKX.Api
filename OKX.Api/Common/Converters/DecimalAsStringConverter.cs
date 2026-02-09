namespace OKX.Api.Common;

/// <summary>
/// Converts decimal values to and from their string representations when serializing and deserializing JSON using
/// invariant culture formatting.
/// </summary>
/// <remarks>This converter ensures that decimal values are serialized as strings with a period (.) as the decimal
/// separator, regardless of the current culture. It is useful for scenarios where consistent decimal formatting is
/// required across different locales, such as financial or scientific data interchange. When deserializing, the
/// converter accepts both string and numeric JSON tokens and parses them using invariant culture. If the token is not a
/// valid decimal representation, a JsonSerializationException is thrown.</remarks>
public class DecimalAsStringConverter : JsonConverter<decimal>
{
    /// <inheritdoc/>
    public override void WriteJson(JsonWriter writer, decimal value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString(CultureInfo.InvariantCulture));
    }

    /// <inheritdoc/>
    public override decimal ReadJson(
        JsonReader reader,
        Type objectType,
        decimal existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.String &&
            decimal.TryParse((string)reader.Value!, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
        {
            return result;
        }

        if (reader.TokenType == JsonToken.Float || reader.TokenType == JsonToken.Integer)
        {
            return Convert.ToDecimal(reader.Value, CultureInfo.InvariantCulture);
        }

        throw new JsonSerializationException($"Invalid token for decimal: {reader.TokenType}");
    }
}

