namespace OKX.Api.Account.Models;

/// <summary>
/// Okx Simulated Asset Request
/// </summary>
public class OkxSimulatedAsset
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