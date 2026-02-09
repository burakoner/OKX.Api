namespace OKX.Api.Common;

/// <summary>
/// Converts nullable <see cref="decimal"/> values to and from their string representations when serializing and
/// deserializing JSON.
/// </summary>
/// <remarks>This converter serializes <see langword="null"/> values as JSON null and non-null <see
/// cref="decimal"/> values as invariant culture strings. It can be used to ensure that decimal values are consistently
/// represented as strings in JSON, which may be required for interoperability with systems that expect decimals as
/// strings. When deserializing, it supports string, integer, and floating-point JSON tokens, returning <see
/// langword="null"/> for empty or null strings.</remarks>
public class DecimalAsStringNullableConverter : JsonConverter<decimal?>
{
    /// <inheritdoc/>
    public override void WriteJson(JsonWriter writer, decimal? value, JsonSerializer serializer)
    {
        if (value.HasValue)
            writer.WriteValue(value.Value.ToString(CultureInfo.InvariantCulture));
        else
            writer.WriteNull();
    }

    /// <inheritdoc/>
    public override decimal? ReadJson(
        JsonReader reader,
        Type objectType,
        decimal? existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return null;

        if (string.IsNullOrEmpty(reader.Value?.ToString()))
            return null;

        if (reader.TokenType == JsonToken.String &&
            decimal.TryParse((string)reader.Value!, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            return result;

        if (reader.TokenType == JsonToken.Float || reader.TokenType == JsonToken.Integer)
            return Convert.ToDecimal(reader.Value, CultureInfo.InvariantCulture);

        throw new JsonSerializationException($"Invalid token for decimal?: {reader.TokenType}");
    }
}