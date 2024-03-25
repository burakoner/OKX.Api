namespace OKX.Api.Account.Models;

/// <summary>
/// Okx Margin Borrow-Repay
/// </summary>
public class OkxMarginBorrowRepayHistory
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Loan currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// borrow repay
    /// </summary>
    [JsonProperty("side")]
    public string Side { get; set; }

    /// <summary>
    /// Accumulate borrow amount
    /// </summary>
    [JsonProperty("accBorrowed")]
    public decimal AccumulateBorrowAmount { get; set; }

    /// <summary>
    /// borrow/repay amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    /// <summary>
    /// The ID of borrowing or repayment
    /// </summary>
    [JsonProperty("refId")]
    public long ReferenceId { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }

}
