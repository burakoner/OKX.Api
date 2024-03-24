namespace OKX.Api.GridTrading.Models;

public class OkxGridInvestmentData
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("amt")]
    public decimal Quantity { get; set; }
}