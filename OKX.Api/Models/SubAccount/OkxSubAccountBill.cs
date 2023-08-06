namespace OKX.Api.Models.SubAccount;

public class OkxSubAccountBill
{
    [JsonProperty("billId")]
    public long BillId { get; set; }

    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }

    [JsonProperty("type"), JsonConverter(typeof(SubAccountTransferTypeConverter))]
    public OkxSubAccountTransferType Type { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("subAcct")]
    public string SubAccountName { get; set; }

    [JsonProperty("amt")]
    public decimal? Amount { get; set; }
}
