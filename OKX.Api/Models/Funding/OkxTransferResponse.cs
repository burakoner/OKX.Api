namespace OKX.Api.Models.Funding;

public class OkxTransferResponse
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("transId")]
    public long? TransferId { get; set; }

    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    [JsonProperty("from"), JsonConverter(typeof(AccountConverter))]
    public OkxAccount? From { get; set; }

    [JsonProperty("to"), JsonConverter(typeof(AccountConverter))]
    public OkxAccount? To { get; set; }
}