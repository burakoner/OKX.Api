namespace OKX.Api.GridTrading.Models;

public class OkxGridComputedMarginBalance
{
    [JsonProperty("lever")]
    public decimal leverage { get; set; }

    [JsonProperty("maxAmt")]
    public decimal MaximumQuantity { get; set; }
}