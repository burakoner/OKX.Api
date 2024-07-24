namespace OKX.Api.Account.Models;

/// <summary>
/// OKX Fixed Loan Borrow Quote
/// </summary>
public class OkxAccountFixedLoanBorrowingQuote
{
    /// <summary>
    /// Borrowing currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Fixed term for borrowing order
    /// 30D：30 Days
    /// </summary>
    [JsonProperty("term")]
    public string Term { get; set; }
    
    /// <summary>
    /// Estimated available borrowing amount
    /// </summary>
    [JsonProperty("estAvailBorrow")]
    public decimal EstimatedAvailableBorrowAmount { get; set; }
    
    /// <summary>
    /// Estimated borrowing rate, e.g. "0.01"
    /// </summary>
    [JsonProperty("estRate")]
    public decimal EstimatedRate { get; set; }
    
    /// <summary>
    /// Estimated borrowing interest
    /// </summary>
    [JsonProperty("estInterest")]
    public decimal EstimatedInterest { get; set; }
    
    /// <summary>
    /// Penalty interest for reborrowing
    /// Applicate to type = reborrow, else return ""
    /// </summary>
    [JsonProperty("penaltyInterest")]
    public decimal? PenaltyInterest { get; set; }

    /// <summary>
    /// Data return time，Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Data return time
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}
