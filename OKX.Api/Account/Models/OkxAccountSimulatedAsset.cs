namespace OKX.Api.Account;

/// <summary>
/// Okx Simulated Asset Request
/// </summary>
public class OkxAccountSimulatedAsset
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amt")]
    public string Amount { get; set; }
}