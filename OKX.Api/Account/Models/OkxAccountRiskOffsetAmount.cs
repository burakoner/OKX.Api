namespace OKX.Api.Account;

/// <summary>
/// OKX Risk Offset Amount
/// </summary>
public class OkxAccountRiskOffsetAmount
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = "";

    /// <summary>
    /// Spot risk offset amount defined by users
    /// </summary>
    [JsonProperty("clSpotInUseAmt")]
    public decimal SpotRiskOffsetAmount { get; set; }
}