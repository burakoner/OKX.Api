namespace OKX.Api.Funding;

/// <summary>
/// OKX Funding Balance
/// </summary>
public class OkxFundingBalance
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Balance
    /// </summary>
    [JsonProperty("bal")]
    public decimal Balance { get; set; }

    /// <summary>
    /// Frozen
    /// </summary>
    [JsonProperty("frozenBal")]
    public decimal Frozen { get; set; }

    /// <summary>
    /// Available
    /// </summary>
    [JsonProperty("availBal")]
    public decimal Available { get; set; }
}
