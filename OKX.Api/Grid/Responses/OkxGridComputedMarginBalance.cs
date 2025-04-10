namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Computed Margin Balance
/// </summary>
public record OkxGridComputedMarginBalance
{
    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever")]
    public decimal Leverage { get; set; }

    /// <summary>
    /// Maximum quantity
    /// </summary>
    [JsonProperty("maxAmt")]
    public decimal MaximumQuantity { get; set; }
}