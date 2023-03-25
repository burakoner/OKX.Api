namespace OKX.Api.Models.Account;

public class OkxMaximumAmount
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("maxBuy")]
    public decimal? MaximumBuy { get; set; }

    [JsonProperty("maxSell")]
    public decimal? MaximumSell { get; set; }
}
