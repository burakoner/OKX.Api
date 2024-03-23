using OKX.Api.Funding.Converters;
using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Models;

public class OkxDepositHistory
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("chain")]
    public string Chain { get; set; }

    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    [JsonProperty("from")]
    public string From { get; set; }

    [JsonProperty("areaCodeFrom")]
    public string AreaCodeFrom { get; set; }

    [JsonProperty("to")]
    public string To { get; set; }

    [JsonProperty("txId")]
    public string TransactionId { get; set; }

    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }

    [JsonProperty("state"), JsonConverter(typeof(OkxDepositStateConverter))]
    public OkxDepositState State { get; set; }

    [JsonProperty("depId")]
    public string DepositId { get; set; }

    [JsonProperty("fromWdId")]
    public long? FromWithdrawalId { get; set; }

    [JsonProperty("actualDepBlkConfirm")]
    public decimal? ActualDepositBlockchainConfirm { get; set; }
}
