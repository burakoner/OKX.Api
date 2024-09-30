namespace OKX.Api.Spread;

/// <summary>
/// OKX Spread Order Book Row
/// </summary>
[JsonConverter(typeof(ArrayConverter))]
public record OkxSpreadOrderBookRow
{
    /// <summary>
    /// The price for this row
    /// </summary>
    [ArrayProperty(0)]
    public decimal Price { get; set; }

    /// <summary>
    /// The quantity for this row
    /// </summary>
    [ArrayProperty(1)]
    public decimal Quantity { get; set; }

    /// <summary>
    /// Number of orders at the price
    /// </summary>
    [ArrayProperty(2)]
    public int OrdersCount { get; set; }
}
