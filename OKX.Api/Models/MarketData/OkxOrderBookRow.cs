﻿namespace OKX.Api.Models.MarketData;

[JsonConverter(typeof(ArrayConverter))]
public class OkxOrderBookRow
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
    /// The number of liquidated orders at the price
    /// </summary>
    [ArrayProperty(2)]
    public decimal LiquidatedOrders { get; set; }

    /// <summary>
    /// The number of orders at the price
    /// </summary>
    [ArrayProperty(3)]
    public decimal OrdersCount { get; set; }
}
