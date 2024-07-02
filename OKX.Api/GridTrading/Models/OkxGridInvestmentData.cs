namespace OKX.Api.GridTrading.Models;

/// <summary>
/// OKX Grid Investment Data
/// </summary>
public class OkxGridInvestmentData
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonProperty("amt")]
    public decimal Quantity { get; set; }
}