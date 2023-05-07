namespace OKX.Api.Models.Funding;

public class OkxWithdrawalResponse
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("chain")]
    public string Chain { get; set; }

    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    [JsonProperty("wdId")]
    public long WithdrawalId { get; set; }

    [JsonProperty("clientId")]
    public string ClientOrderId { get; set; }
}