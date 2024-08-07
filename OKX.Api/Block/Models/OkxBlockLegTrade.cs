﻿using OKX.Api.Trade.Converters;
using OKX.Api.Trade.Enums;

namespace OKX.Api.Block.Models;

/// <summary>
/// OKX Block Leg Execution
/// </summary>
public class OkxBlockLegTrade
{
    /// <summary>
    /// The price the leg executed
    /// </summary>
    [JsonProperty("px")]
    public decimal Price { get; set; }

    /// <summary>
    /// Size of the leg in contracts or spot.
    /// </summary>
    [JsonProperty("sz")]
    public decimal Size { get; set; }

    /// <summary>
    /// Instrument ID, e.g. BTC-USDT-SWAP
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }
    
    /// <summary>
    /// The direction of the leg from the Takers perspective. Valid value can be buy or sell.
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(OkxOrderSideConverter))]
    public OkxOrderSide Side { get; set; }
    
    /// <summary>
    /// Fee for the individual leg.
    /// Negative fee represents the user transaction fee charged by the platform. Positive fee represents rebate.
    /// </summary>
    [JsonProperty("fee")]
    public decimal Fee { get; set; }
    
    /// <summary>
    /// Fee currency. To be read in conjunction with fee
    /// </summary>
    [JsonProperty("feeCcy")]
    public string FeeCurrency { get; set; }

    /// <summary>
    /// Last traded ID.
    /// </summary>
    [JsonProperty("tradeId")]
    public long TradeId { get; set; }
}
