namespace OKX.Api.CopyTrading;

/// <summary>
/// OkxProfitSharingTotal
/// </summary>
public record OkxCopyTradingProfitSharingTotal
{
    /// <summary>
    /// The currency of profit sharing.
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Total profit sharing amount.
    /// </summary>
    [JsonProperty("totalProfitSharingAmt")]
    public decimal TotalProfitSharingAmount { get; set; }
    
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }
}
