namespace OKX.Api.GridTrading.Models;

public class OkxGridInvestment
{
    [JsonProperty("minInvestmentData")]
    public List<OkxGridInvestmentData> MinimumInvestmentData { get; set; }

    [JsonProperty("singleAmt")]
    public decimal? SingleQuantity { get; set; }
}