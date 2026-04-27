namespace OKX.Api.Base;

internal class OkxSocketExtraParametersConverter : JsonConverter<Dictionary<string, string>?>
{
    public override void WriteJson(JsonWriter writer, Dictionary<string, string>? value, JsonSerializer serializer)
    {
        if (value is null)
        {
            writer.WriteNull();
            return;
        }

        writer.WriteValue(JsonConvert.SerializeObject(value));
    }

    public override Dictionary<string, string>? ReadJson(
        JsonReader reader,
        Type objectType,
        Dictionary<string, string>? existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return null;

        if (reader.TokenType == JsonToken.String)
        {
            var raw = reader.Value?.ToString();
            if (string.IsNullOrWhiteSpace(raw))
                return null;

            return ParseObject(JObject.Parse(raw!));
        }

        if (reader.TokenType == JsonToken.StartObject)
            return ParseObject(JObject.Load(reader));

        throw new JsonSerializationException($"Invalid token for socket extra parameters: {reader.TokenType}");
    }

    private static Dictionary<string, string> ParseObject(JObject payload)
    {
        var result = new Dictionary<string, string>(StringComparer.Ordinal);
        foreach (var property in payload.Properties())
            result[property.Name] = property.Value.Type == JTokenType.Null ? string.Empty : property.Value.ToString();

        return result;
    }
}
