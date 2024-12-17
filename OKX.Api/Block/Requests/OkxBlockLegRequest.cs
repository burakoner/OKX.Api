using ApiSharp.Rest;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Drawing;

namespace OKX.Api.Block;

/// <summary>
/// OKX Block Leg Request
/// </summary>
public record OkxBlockLegRequest
{
    /// <summary>
    /// The Instrument ID of each leg. Example : "BTC-USDT-SWAP"
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Trade mode
    /// Margin mode: cross isolated
    /// Non-Margin mode: cash.
    /// If not provided, tdMode will inherit default values set by the system shown below:
    /// Spot and futures mode &amp; SPOT: cash
    /// Buy options in Spot and futures mode and Multi-currency Margin: isolated
    /// Other cases: cross
    /// </summary>
    [JsonProperty("tdMode")]
    public OkxTradeMode? TradeMode { get; set; }

    /// <summary>
    /// Margin currency.
    /// Only applicable to cross MARGIN orders in Spot and futures mode. The parameter will be ignored in other scenarios.
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// The size of each leg
    /// </summary>
    [JsonProperty("sz")]
    public string Size { get; set; } = string.Empty;

    /// <summary>
    /// Taker expected price for the RFQ
    /// If provided, RFQ trade will be automatically executed if the price from the quote is better than or equal to the price specified until the RFQ is canceled or expired.
    /// This field has to be provided for all legs to have the RFQ automatically executed, or leave empty for all legs, otherwise request will be rejected.
    /// The auto execution side depends on the leg side of the RFQ.
    /// For SPOT/MARGIN/FUTURES/SWAP, lmtPx will be in unit of the quote ccy.
    /// For OPTION, lmtPx will be in unit of settle ccy.
    /// The field will not be disclosed to counterparties.
    /// </summary>
    [JsonProperty("lmtPx")]
    public string LimitPrice { get; set; } = string.Empty;

    /// <summary>
    /// The direction of each leg. Valid values can be buy or sell.
    /// </summary>
    [JsonProperty("side")]
    public OkxTradeOrderSide Side { get; set; }

    /// <summary>
    /// Position side.
    /// The default is net in the net mode. It can only be long or short in the long/short mode.
    /// If not specified, users in long/short mode always open new positions.
    /// Only applicable to FUTURES/SWAP.
    /// </summary>
    [JsonProperty("posSide")]
    public OkxTradePositionSide? PositionSide { get; set; }

    /// <summary>
    /// Defines the unit of the “sz” attribute.
    /// Only applicable to instType = SPOT.
    /// The valid enumerations are base_ccy and quote_ccy. When not specified, this is equal to base_ccy by default.
    /// </summary>
    [JsonProperty("tgtCcy")]
    public string TargetCurrency { get; set; } = string.Empty;
}
