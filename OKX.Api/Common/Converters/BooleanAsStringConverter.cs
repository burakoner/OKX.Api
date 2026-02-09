namespace OKX.Api.Common;

using Newtonsoft.Json;
using System;

/// <summary>
/// Converts Boolean values to and from their string representations when serializing and deserializing JSON.
/// </summary>
/// <remarks>Use this converter to serialize Boolean values as custom strings (such as "Yes"/"No" or "On"/"Off")
/// instead of the default JSON Boolean literals. This is useful when interoperating with systems or APIs that expect
/// Boolean values as specific strings. The converter is initialized with the string values to use for <see
/// langword="true"/> and <see langword="false"/>.</remarks>
/// <remarks>
/// Initializes a new instance of the BoolAsStringConverter class with the specified string representations for
/// boolean values.
/// </remarks>
/// <param name="trueValue">The string value that represents <see langword="true"/> when converting to or from a boolean.</param>
/// <param name="falseValue">The string value that represents <see langword="false"/> when converting to or from a boolean.</param>
/// <exception cref="ArgumentNullException">Thrown if <paramref name="trueValue"/> or <paramref name="falseValue"/> is null.</exception>
public sealed class BooleanAsStringConverter(string trueValue, string falseValue) : JsonConverter<bool>
{
    private readonly string _trueValue = trueValue ?? throw new ArgumentNullException(nameof(trueValue));
    private readonly string _falseValue = falseValue ?? throw new ArgumentNullException(nameof(falseValue));

    /// <inheritdoc/>
    public override void WriteJson(JsonWriter writer, bool value, JsonSerializer serializer)
    {
        writer.WriteValue(value ? _trueValue : _falseValue);
    }

    /// <inheritdoc/>
    public override bool ReadJson(
        JsonReader reader,
        Type objectType,
        bool existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return false;

        if (reader.Value == null)
            return false;

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

        throw new JsonSerializationException($"Invalid token for bool: {reader.TokenType}");
    }
}
