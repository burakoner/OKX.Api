namespace OKX.Api.Account;

/// <summary>
/// OKX Account Manual Borrow-Repay Response
/// </summary>
public record OkxAccountBorrowRepayHistory
{
    /// <summary>
    /// Currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = "";

    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; } = "";

    /// <summary>
    /// amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Accumulated borrow amount
    /// </summary>
    [JsonProperty("accBorrowed")]
    public decimal AccumulatedBorrowAmount { get; set; }

    /// <summary>
    /// Timestamp for the event, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Timestamp for the event
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}
