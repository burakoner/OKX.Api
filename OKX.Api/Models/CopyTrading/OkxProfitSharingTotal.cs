namespace OKX.Api.Models.CopyTrading;

/// <summary>
/// OkxProfitSharingTotal
/// </summary>
public class OkxProfitSharingTotal
{
    /// <summary>
    /// The currency of profit sharing.
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Total profit sharing amount.
    /// </summary>
    [JsonProperty("totalProfitSharingAmt")]
    public decimal TotalProfitSharingAmount { get; set; }
}
