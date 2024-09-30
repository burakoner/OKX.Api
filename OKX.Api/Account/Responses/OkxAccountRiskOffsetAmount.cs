namespace OKX.Api.Account;

/// <summary>
/// OKX Risk Offset Amount
/// </summary>
public record OkxAccountRiskOffsetAmount
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Spot risk offset amount defined by users
    /// </summary>
    [JsonProperty("clSpotInUseAmt")]
    public decimal SpotRiskOffsetAmount { get; set; }
}