namespace OKX.Api.Block;

/// <summary>
/// OKX Block Leg Request
/// </summary>
public class OkxBlockLegRequest
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
