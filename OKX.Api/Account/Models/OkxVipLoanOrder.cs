using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Models;

/// <summary>
/// Okx VIP Loan Order
/// </summary>
public class OkxVipLoanOrder
{
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

    /// <summary>
    /// Next refresh time
    /// </summary>
    [JsonProperty("nextRefreshTime")]
    public long NextRefreshTimestamp { get; set; }

    /// <summary>
    /// Next refresh time
    /// </summary>
    [JsonIgnore]
    public DateTime NextRefreshTime { get { return NextRefreshTimestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Loan currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Order Id
    /// </summary>
    [JsonProperty("ordId")]
    public long OrderId { get; set; }

    /// <summary>
    /// State
    /// </summary>
    [JsonProperty("state"), JsonConverter(typeof(OkxAccountVipLoanStateConverter))]
    public OkxAccountVipLoanState State { get; set; }

    /// <summary>
    /// Original rate
    /// </summary>
    [JsonProperty("origRate")]
    public decimal OriginalRate { get; set; }

    /// <summary>
    /// Current rate
    /// </summary>
    [JsonProperty("curRate")]
    public decimal CurrentRate { get; set; }

    /// <summary>
    /// Due amount
    /// </summary>
    [JsonProperty("dueAmt")]
    public decimal DueAmount { get; set; }

    /// <summary>
    /// Borrow amount
    /// </summary>
    [JsonProperty("borrowAmt")]
    public decimal BorrowAmount { get; set; }

    /// <summary>
    /// Repay amount
    /// </summary>
    [JsonProperty("repayAmt")]
    public decimal RepayAmount { get; set; }
}
