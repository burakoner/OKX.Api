namespace OKX.Api.CopyTrading.Models;

/// <summary>
/// OkxProfitSharingDetails
/// </summary>
public class OkxProfitSharingDetails
{
    /// <summary>
    /// The currency of profit sharing.
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Profit sharing amount. Default is 0.
    /// </summary>
    [JsonProperty("profitSharingAmt")]
    public decimal ProfitSharingAmount { get; set; }

    /// <summary>
    /// Nickname of copy trader.
    /// </summary>
    [JsonProperty("nickName")]
    public string Nickname { get; set; }

    /// <summary>
    /// Profit sharing ID.
    /// </summary>
    [JsonProperty("profitSharingId")]
    public long ProfitSharingId { get; set; }

    /// <summary>
    /// Profit sharing time.
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Profit sharing time.
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}
