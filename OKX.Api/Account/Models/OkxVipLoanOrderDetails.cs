using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Models;

/// <summary>
/// Okx VIP Loan Order
/// </summary>
public class OkxVipLoanOrderDetails
{
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
    
    [JsonProperty("ccy")]
    public string Currency { get; set; }
    
    [JsonProperty("type"), JsonConverter(typeof(OkxAccountVipLoanTypeConverter))]
    public OkxAccountVipLoanType Type { get; set; }
    
    [JsonProperty("rate")]
    public decimal Rate { get; set; }

    [JsonProperty("amt")]
    public decimal Amount { get; set; }
    
    [JsonProperty("failReason")]
    public string FailReason { get; set; }
}
