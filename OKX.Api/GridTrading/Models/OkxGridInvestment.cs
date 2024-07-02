namespace OKX.Api.GridTrading.Models;

/// <summary>
/// OKX Grid Investment
/// </summary>
public class OkxGridInvestment
{
    /// <summary>
    /// Minimum Investment Data
    /// </summary>
    [JsonProperty("minInvestmentData")]
    public List<OkxGridInvestmentData> MinimumInvestmentData { get; set; }

    /// <summary>
    /// Single Quantity
    /// </summary>
    [JsonProperty("singleAmt")]
    public decimal? SingleQuantity { get; set; }
}