namespace OKX.Api.Funding;

/// <summary>
/// OKX convert currency metadata
/// </summary>
public record OkxFundingConvertCurrency
{
    /// <summary>
    /// Currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Minimum amount to convert. Deprecated by OKX but still present on the wire.
    /// </summary>
    [JsonProperty("min")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? MinimumAmount { get; set; }

    /// <summary>
    /// Maximum amount to convert. Deprecated by OKX but still present on the wire.
    /// </summary>
    [JsonProperty("max")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? MaximumAmount { get; set; }
}
