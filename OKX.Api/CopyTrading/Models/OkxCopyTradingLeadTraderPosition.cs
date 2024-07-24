using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;
using OKX.Api.Trade.Converters;
using OKX.Api.Trade.Enums;

namespace OKX.Api.CopyTrading.Models;

/// <summary>
/// OKX Copy Trading Lead Trader Position
/// </summary>
public class OkxCopyTradingLeadTraderPosition
{
    /// <summary>
    /// Instrument ID, e.g. BTC-USDT-SWAP
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Lead position ID
    /// </summary>
    [JsonProperty("subPosId")]
    public long PositionId { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide"), JsonConverter(typeof(OkxPositionSideConverter))]
    public OkxPositionSide Side { get; set; }

    /// <summary>
    /// Margin mode. cross isolated
    /// </summary>
    [JsonProperty("mgnMode"), JsonConverter(typeof(OkxAccountMarginModeConverter))]
    public OkxAccountMarginMode MarginMode { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever")]
    public decimal Leverage { get; set; }

    /// <summary>
    /// Average open price
    /// </summary>
    [JsonProperty("openAvgPx")]
    public decimal AverageOpenPrice { get; set; }

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
    public int PositionCount { get; set; }
    
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Margin
    /// </summary>
    [JsonProperty("margin")]
    public decimal Margin { get; set; }

    /// <summary>
    /// Unrealized profit and loss
    /// </summary>
    [JsonProperty("upl")]
    public decimal Upl { get; set; }

    /// <summary>
    /// Unrealized profit and loss ratio
    /// </summary>
    [JsonProperty("uplRatio")]
    public decimal UplRatio { get; set; }

    /// <summary>
    /// Latest mark price, only applicable to contract
    /// </summary>
    [JsonProperty("markPx")]
    public decimal MarkPrice { get; set; }

    /// <summary>
    /// Lead trader unique code
    /// </summary>
    [JsonProperty("uniqueCode")]
    public string UniqueCode { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }
}
