namespace OKX.Api.Financial;

/// <summary>
/// OKX Dual Investment Product Data
/// </summary>
[JsonConverter(typeof(OkxFinancialDualInvestmentProductsDataConverter))]
public record OkxFinancialDualInvestmentProductsData
{
    /// <summary>
    /// Products
    /// </summary>
    public List<OkxFinancialDualInvestmentProduct> Products { get; set; } = [];
}

internal sealed class OkxFinancialDualInvestmentProductsDataConverter : JsonConverter<OkxFinancialDualInvestmentProductsData>
{
    public override void WriteJson(JsonWriter writer, OkxFinancialDualInvestmentProductsData? value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("products");
        serializer.Serialize(writer, value?.Products ?? []);
        writer.WriteEndObject();
    }

    public override OkxFinancialDualInvestmentProductsData? ReadJson(
        JsonReader reader,
        Type objectType,
        OkxFinancialDualInvestmentProductsData? existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return null;

        var token = JToken.Load(reader);
        return token.Type switch
        {
            JTokenType.Array => new OkxFinancialDualInvestmentProductsData
            {
                Products = token.ToObject<List<OkxFinancialDualInvestmentProduct>>(serializer) ?? []
            },
            JTokenType.Object => new OkxFinancialDualInvestmentProductsData
            {
                Products = token["products"]?.ToObject<List<OkxFinancialDualInvestmentProduct>>(serializer) ?? []
            },
            _ => throw new JsonSerializationException($"Unsupported dual investment products payload: {token.Type}"),
        };
    }
}
