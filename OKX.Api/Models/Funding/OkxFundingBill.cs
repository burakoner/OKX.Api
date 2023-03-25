namespace OKX.Api.Models.Funding;

public class OkxFundingBill
{
    [JsonProperty("billId")]
    public long? BillId { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("bal")]
    public decimal? Balance { get; set; }

    [JsonProperty("balChg")]
    public decimal? BalanceChange { get; set; }

    [JsonProperty("type"), JsonConverter(typeof(FundingBillTypeConverter))]
    public OkxFundingBillType Type { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}