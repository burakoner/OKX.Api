namespace OKX.Api.Funding;

/// <summary>
/// OKX Saving Action Response
/// </summary>
public class OkxFundingSavingActionResponse
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = "";

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal? Amount { get; set; }

    /// <summary>
    /// Purchase Rate
    /// </summary>
    [JsonProperty("rate")]
    public decimal? PurchaseRate { get; set; }

    /// <summary>
    /// Side
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(OkxFundingSavingActionSideConverter))]
    public OkxFundingSavingActionSide Side { get; set; }
}