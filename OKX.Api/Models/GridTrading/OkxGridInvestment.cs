namespace OKX.Api.Models.GridTrading;

public class OkxGridInvestment
{
    [JsonProperty("minInvestmentData")]
    public IEnumerable<OkxGridInvestmentData> MinimumInvestmentData { get; set; }

    [JsonProperty("singleAmt")]
    public decimal? SingleQuantity { get; set; }
}