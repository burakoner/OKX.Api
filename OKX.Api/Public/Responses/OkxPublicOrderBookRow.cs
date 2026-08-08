namespace OKX.Api.Public;

/// <summary>
/// OKX Order Book Row
/// </summary>
[JsonConverter(typeof(ArrayConverter))]
public record OkxPublicOrderBookRow
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
    /// Non-RPI quantity at the price for the books-rpi channel.
    /// This value is a deprecated placeholder fixed to zero for other order book channels.
    /// </summary>
    [ArrayProperty(2)]
    public decimal NonRpiQuantity { get; set; }

    /// <summary>
    /// Legacy name for <see cref="NonRpiQuantity"/>. This value never represented liquidated orders.
    /// </summary>
    [Obsolete("This field never represented liquidated orders. Use NonRpiQuantity for books-rpi; other channels return zero.")]
    [JsonIgnore]
    public decimal LiquidatedOrders
    {
        get => NonRpiQuantity;
        set => NonRpiQuantity = value;
    }

    /// <summary>
    /// The number of orders at the price
    /// </summary>
    [ArrayProperty(3)]
    public decimal OrdersCount { get; set; }
}
