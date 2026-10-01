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
    /// Quantity at this price. On books-rpi, total organic plus currently tradeable RPI quantity.
    /// On books-rpi, hidden/non-tradeable RPI is excluded; without RPI taker access, only NonRpiQuantity is executable.
    /// </summary>
    [ArrayProperty(1)]
    public decimal Quantity { get; set; }

    /// <summary>
    /// Organic (non-RPI) quantity at the price for books-rpi REST and WebSocket data.
    /// Tradeable RPI quantity is Quantity minus this value; equality does not prove no RPI orders exist.
    /// The REST books-rpi endpoint fails closed with equal quantities when RPI tradeability is unavailable.
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
