using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Models;

/// <summary>
/// Okx VIP Loan Order
/// </summary>
public class OkxVipLoanOrder
{
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }

    [JsonProperty("nextRefreshTime")]
    public long NextRefreshTimestamp { get; set; }

    [JsonIgnore]
    public DateTime NextRefreshTime { get { return NextRefreshTimestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Loan currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("ordId")]
    public long OrderId { get; set; }
    
    [JsonProperty("state"), JsonConverter(typeof(OkxAccountVipLoanStateConverter))]
    public OkxAccountVipLoanState State { get; set; }
    
    [JsonProperty("origRate")]
    public decimal OriginalRate { get; set; }
    
    [JsonProperty("curRate")]
    public decimal CurrentRate { get; set; }
    
    [JsonProperty("dueAmt")]
    public decimal DueAmount { get; set; }
    
    [JsonProperty("borrowAmt")]
    public decimal BorrowAmount { get; set; }
    
    [JsonProperty("repayAmt")]
    public decimal RepayAmount { get; set; }
}
