namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Investment
/// </summary>
public record OkxGridInvestment
{
    /// <summary>
    /// Minimum Investment Data
    /// </summary>
    [JsonProperty("minInvestmentData")]
    public List<OkxGridInvestmentData> MinimumInvestmentData { get; set; } = [];

    /// <summary>
    /// Single Quantity
    /// </summary>
    [JsonProperty("singleAmt")]
    public decimal? SingleQuantity { get; set; }
}

/// <summary>
/// OKX Grid Investment Data
/// </summary>
public record OkxGridInvestmentData
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonProperty("amt")]
    public decimal Quantity { get; set; }
}