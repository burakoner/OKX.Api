﻿using OKX.Api.Public.Converters;
using OKX.Api.Public.Enums;
using OKX.Api.Trade.Converters;
using OKX.Api.Trade.Enums;

namespace OKX.Api.Block.Models;

/// <summary>
/// OKX Block Leg Quote
/// </summary>
public class OkxBlockLegQuote
{
    /// <summary>
    /// The Instrument ID of each leg. Example : "BTC-USDT-SWAP"
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Trade mode
    /// Margin mode: cross isolated
    /// Non-Margin mode: cash.
    /// If not provided, tdMode will inherit default values set by the system shown below:
    /// Spot and futures mode & SPOT: cash
    /// Buy options in Spot and futures mode and Multi-currency Margin: isolated
    /// Other cases: cross
    /// </summary>
    [JsonProperty("tdMode"), JsonConverter(typeof(OkxTradeModeConverter))]
    public OkxTradeMode? TradeMode { get; set; }

    /// <summary>
    /// Margin currency.
    /// Only applicable to cross MARGIN orders in Spot and futures mode. The parameter will be ignored in other scenarios.
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Size of the leg in contracts or spot.
    /// </summary>
    [JsonProperty("sz")]
    public string Size { get; set; }

    /// <summary>
    /// The price of the leg.
    /// </summary>
    [JsonProperty("px")]
    public string Price { get; set; }
    
    /// <summary>
    /// The direction of each leg. Valid values can be buy or sell.
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(OkxOrderSideConverter))]
    public OkxOrderSide Side { get; set; }

    /// <summary>
    /// Position side.
    /// The default is net in the net mode. It can only be long or short in the long/short mode.
    /// If not specified, users in long/short mode always open new positions.
    /// Only applicable to FUTURES/SWAP.
    /// </summary>
    [JsonProperty("posSide"), JsonConverter(typeof(OkxPositionSideConverter))]
    public OkxPositionSide? PositionSide { get; set; }
    
    /// <summary>
    /// Defines the unit of the “sz” attribute.
    /// Only applicable to instType = SPOT.
    /// The valid enumerations are base_ccy and quote_ccy. When not specified, this is equal to base_ccy by default.
    /// </summary>
    [JsonProperty("tgtCcy")]
    public string TargetCurrency { get; set; }
}
