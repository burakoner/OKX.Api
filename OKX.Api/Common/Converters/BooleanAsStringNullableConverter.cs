namespace OKX.Api.Common;

using Newtonsoft.Json;
using System;

/// <summary>
/// Converts nullable Boolean values to and from their string representations in JSON using custom true and false string
/// values.
/// </summary>
/// <remarks>This converter allows you to serialize and deserialize nullable Boolean values as specific strings,
/// rather than the default JSON true/false literals. It is useful when integrating with systems or APIs that expect
/// Boolean values to be represented as custom strings (such as "Yes"/"No" or "1"/"0").  When serializing, a value of
/// <see langword="true"/> is written as the specified true string, <see langword="false"/> as the specified false
/// string, and <see langword="null"/> as a JSON null. When deserializing, the converter accepts a variety of common
/// Boolean string representations (such as "true", "yes", "1", "on" for <see langword="true"/>, and "false", "no", "0",
/// "off" for <see langword="false"/>), as well as JSON Boolean and integer tokens. An exception is thrown if the input
/// string does not match a recognized Boolean value.</remarks>
/// <remarks>
/// Initializes a new instance of the NullableBoolAsStringConverter class with the specified string representations
/// for true and false values.
/// </remarks>
/// <param name="trueValue">The string representation to use when converting a value of true.</param>
/// <param name="falseValue">The string representation to use when converting a value of false.</param>
/// <exception cref="ArgumentNullException">Thrown if either trueValue or falseValue is null.</exception>
public sealed class BooleanAsStringNullableConverter(string trueValue, string falseValue) : JsonConverter<bool?>
{
    private readonly string _trueValue = trueValue ?? throw new ArgumentNullException(nameof(trueValue));
    private readonly string _falseValue = falseValue ?? throw new ArgumentNullException(nameof(falseValue));

    /// <inheritdoc/>
    public override void WriteJson(JsonWriter writer, bool? value, JsonSerializer serializer)
    {
        if (!value.HasValue)
        {
            writer.WriteNull();
            return;
        }

        writer.WriteValue(value.Value ? _trueValue : _falseValue);
    }

    /// <inheritdoc/>
    public override bool? ReadJson(
        JsonReader reader,
        Type objectType,
        bool? existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return null;

        if (reader.Value == null)
            return null;

        if (reader.TokenType == JsonToken.Boolean)
            return (bool)reader.Value;

        if (reader.TokenType == JsonToken.Integer)
            return Convert.ToInt32(reader.Value) != 0;

        if (reader.TokenType == JsonToken.String)
        {
            var s = ((string)reader.Value).Trim().ToLowerInvariant();

            return s switch
            {
                "true" or "t" or "1" or "yes" or "y" or "on" => true,
                "false" or "f" or "0" or "no" or "n" or "off" => false,
                _ => throw new JsonSerializationException($"Invalid boolean string: '{reader.Value}'")
            };
        }

        throw new JsonSerializationException($"Invalid token for bool?: {reader.TokenType}");
    }
}
