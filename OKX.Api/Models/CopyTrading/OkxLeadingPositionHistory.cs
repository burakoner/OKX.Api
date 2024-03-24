using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;
using OKX.Api.Trade.Converters;
using OKX.Api.Trade.Enums;

namespace OKX.Api.Models.CopyTrading;

/// <summary>
/// OkxLeadingPositionHistory
/// </summary>
public class OkxLeadingPositionHistory
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
    /// Time of opening
    /// </summary>
    [JsonProperty("openTime")]
    public long OpenTimestamp { get; set; }

    /// <summary>
    /// Time of opening
    /// </summary>
    [JsonIgnore]
    public DateTime OpenTime { get { return OpenTimestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Quantity of positions
    /// </summary>
    [JsonProperty("subPos")]
    public int QuantityOfPositions { get; set; }

    /// <summary>
    /// Time of closing position
    /// </summary>
    [JsonProperty("closeTime")]
    public long? CloseTimestamp { get; set; }

    /// <summary>
    /// Time of closing position
    /// </summary>
    [JsonIgnore]
    public DateTime? CloseTime { get { return CloseTimestamp?.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Average price of closing position
    /// </summary>
    [JsonProperty("closeAvgPx")]
    public decimal? AverageClosePrice { get; set; }

    /// <summary>
    /// Profit and loss
    /// </summary>
    [JsonProperty("pnl")]
    public decimal ProfitLoss { get; set; }

    /// <summary>
    /// P&L ratio
    /// </summary>
    [JsonProperty("pnlRatio")]
    public decimal ProfitLossRatio { get; set; }
}
