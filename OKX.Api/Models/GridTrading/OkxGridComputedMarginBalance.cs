namespace OKX.Api.Models.GridTrading;

public class OkxGridComputedMarginBalance
{
    [JsonProperty("lever")]
    public decimal leverage { get; set; }

    [JsonProperty("maxAmt")]
    public decimal MaximumQuantity { get; set; }
}