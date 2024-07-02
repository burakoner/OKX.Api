using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Models;

/// <summary>
/// Okx VIP Loan Order
/// </summary>
public class OkxVipLoanOrderDetails
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
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type"), JsonConverter(typeof(OkxAccountVipLoanTypeConverter))]
    public OkxAccountVipLoanType Type { get; set; }
    
    /// <summary>
    /// Rate
    /// </summary>
    [JsonProperty("rate")]
    public decimal Rate { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Failure reason
    /// </summary>
    [JsonProperty("failReason")]
    public string FailReason { get; set; }
}
