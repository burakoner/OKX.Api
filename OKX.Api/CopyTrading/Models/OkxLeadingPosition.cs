using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;
using OKX.Api.Trade.Converters;
using OKX.Api.Trade.Enums;

namespace OKX.Api.CopyTrading.Models;

/// <summary>
/// OkxLeadingPosition
/// </summary>
public class OkxLeadingPosition
{
    /// <summary>
    /// Instrument ID, e.g. BTC-USDT-SWAP
    /// </summary>
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    /// <summary>
    /// Leading position ID
    /// </summary>
    [JsonProperty("subPosId")]
    public long PositionId { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide"), JsonConverter(typeof(OkxPositionSideConverter))]
    public OkxPositionSide PositionSide { get; set; }

    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonProperty("mgnMode"), JsonConverter(typeof(OkxMarginModeConverter))]
    public OkxMarginMode? MarginMode { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// Order ID for opening position
    /// </summary>
    [JsonProperty("openOrdId")]
    public long OpenOrderId { get; set; }

    /// <summary>
    /// Average open price
    /// </summary>
    [JsonProperty("openAvgPx")]
    public decimal? AverageOpenPrice { get; set; }

    /// <summary>
    /// Open time
    /// </summary>
    [JsonProperty("openTime")]
    public long OpenTimestamp { get; set; }

    /// <summary>
    /// Open time
    /// </summary>
    [JsonIgnore]
    public DateTime OpenTime { get { return OpenTimestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Quantity of positions
    /// </summary>
    [JsonProperty("subPos")]
    public int QuantityOfPositions { get; set; }

    /// <summary>
    /// ake-profit trigger price. Take-profit order price will be the market price after triggering.
    /// </summary>
    [JsonProperty("tpTriggerPx")]
    public decimal? TakeProfitTriggerPrice { get; set; }

    /// <summary>
    /// Stop-loss trigger price. Stop-loss order price will be the market price after triggering.
    /// </summary>
    [JsonProperty("slTriggerPx")]
    public decimal? StopLossTriggerPrice { get; set; }

    /// <summary>
    /// Stop order ID
    /// </summary>
    [JsonProperty("algoId")]
    public long? AlgoOrderId { get; set; }
}
