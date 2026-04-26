namespace OKX.Api.Financial;

/// <summary>
/// Flexible loan asset amount
/// </summary>
public record OkxFinancialFlexibleLoanAsset
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amt"), JsonConverter(typeof(DecimalAsStringConverter))]
    public decimal Amount { get; set; }

    /// <summary>
    /// Notional value in USD when provided by the endpoint
    /// </summary>
    [JsonProperty("notionalUsd"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? NotionalUsd { get; set; }
}
