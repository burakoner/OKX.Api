using OKX.Api.Funding.Converters;
using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Models;

public class OkxWithdrawalHistory
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("chain")]
    public string Chain { get; set; }

    [JsonProperty("nonTradableAsset")]
    public bool? NonTradableAsset { get; set; }

    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }

    [JsonProperty("from")]
    public string From { get; set; }

    [JsonProperty("areaCodeFrom")]
    public string AreaCodeFrom { get; set; }

    [JsonProperty("to")]
    public string To { get; set; }

    [JsonProperty("areaCodeTo")]
    public string AreaCodeTo { get; set; }

    [JsonProperty("tag")]
    public string Tag { get; set; }

    [JsonProperty("pmtId")]
    public string PaymentId { get; set; }

    [JsonProperty("memo")]
    public string Memo { get; set; }

    //[JsonProperty("addrEx")]
    //public addrEx addrEx { get; set; }

    [JsonProperty("txId")]
    public string TransactionId { get; set; }

    [JsonProperty("fee")]
    public decimal Fee { get; set; }

    [JsonProperty("feeCcy")]
    public string FeeCurrency { get; set; }

    [JsonProperty("state"), JsonConverter(typeof(WithdrawalStateConverter))]
    public OkxWithdrawalState State { get; set; }

    [JsonProperty("wdId")]
    public long WithdrawalId { get; set; }

    [JsonProperty("clientId")]
    public string ClientOrderId { get; set; }
}
