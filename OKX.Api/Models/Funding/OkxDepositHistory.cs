namespace OKX.Api.Models.Funding;

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

    [JsonProperty("to")]
    public string To { get; set; }

    [JsonProperty("txId")]
    public string TransactionId { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("depId")]
    public string DepositId { get; set; }

    [JsonProperty("state"), JsonConverter(typeof(DepositStateConverter))]
    public OkxDepositState State { get; set; }
}