using OKX.Api.Funding.Converters;
using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Models;

public class OkxFundingBill
{
    [JsonProperty("billId")]
    public long? BillId { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("clientId")]
    public string ClientOrderId { get; set; }

    [JsonProperty("bal")]
    public decimal? Balance { get; set; }

    [JsonProperty("balChg")]
    public decimal? BalanceChange { get; set; }

    [JsonProperty("type"), JsonConverter(typeof(OkxFundingBillTypeConverter))]
    public OkxFundingBillType Type { get; set; }

    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}