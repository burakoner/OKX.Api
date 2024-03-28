using OKX.Api.Common.Converters;
using OKX.Api.Common.Enums;
using OKX.Api.Public.Converters;
using OKX.Api.Public.Enums;
using OKX.Api.Trade.Converters;
using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Models;

/// <summary>
/// Represents a transaction in OKX, a cryptocurrency exchange platform.
/// </summary>
public class OkxTransaction
{
    /// <summary>
    /// Type of the instrument involved in the transaction.
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Unique identifier of the instrument involved in the transaction.
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Unique identifier of the last trade associated with this transaction.
    /// </summary>
    /// <remarks>
    /// For certain categories such as partial_liquidation, full_liquidation, or adl, the value is "0".
    /// </remarks>
    [JsonProperty("tradeId")]
    public long? TradeId { get; set; }

    /// <summary>
    /// Unique identifier of the order associated with this transaction.
    /// </summary>
    /// <remarks>
    /// For block trading, this is always "".
    /// </remarks>
    [JsonProperty("ordId")]
    public long? OrderId { get; set; }

    /// <summary>
    /// Client-supplied unique identifier of the order.
    /// <remarks>
    /// For block trading, this is always "".
    /// </remarks>
    /// </summary>
    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Unique identifier of the bill associated with this transaction.
    /// </summary>
    [JsonProperty("billId")]
    public long? BillId { get; set; }

    /// <summary>
    /// The price at which the last fill occurred.
    /// </summary>
    [JsonProperty("fillPx")]
    public decimal? LastFilledPrice { get; set; }

    /// <summary>
    /// The quantity that was filled in the last trade.
    /// </summary>
    [JsonProperty("fillSz")]
    public decimal? LastFilledQuantity { get; set; }

    /// <summary>
    /// The index price at the moment of trade execution.
    /// </summary>
    /// <remarks>
    /// For cross currency spot pairs (such as BTC-ETH), it returns baseCcy-USDT (BTC-USDT) index price.
    /// </remarks>
    [JsonProperty("fillIdxPx")]
    public decimal? LastFilledIndexPrice { get; set; }

    /// <summary>
    /// Last filled profit and loss, applicable to orders which have a trade and aim to close position. It always is 0 in other conditions
    /// </summary>
    [JsonProperty("fillPnl")]
    public decimal? LastFilledProfitAndLoss { get; set; }

    /// <summary>
    /// Specifies whether the order was a buy or sell.
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(OkxOrderSideConverter))]
    public OkxOrderSide OrderSide { get; set; }

    /// <summary>
    /// Specifies the position side of the order (long, short, or net innet mode).
    /// </summary>
    [JsonProperty("posSide"), JsonConverter(typeof(OkxPositionSideConverter))]
    public OkxPositionSide PositionSide { get; set; }

    /// <summary>
    /// Indicates whether the order acted as liquidity taker or maker. 
    /// </summary>
    /// <remarks>
    /// Not applicable to system orders such as ADL and liquidation.
    /// </remarks>
    [JsonProperty("execType"), JsonConverter(typeof(OkxTradeRoleConverter))]
    public OkxTradeRole OrderFlowType { get; set; }

    /// <summary>
    /// The currency in which the transaction fee is paid.
    /// </summary>
    [JsonProperty("feeCcy")]
    public string FeeCurrency { get; set; }

    /// <summary>
    /// The transaction fee.
    /// </summary>
    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    /// <summary>
    /// Unix timestamp of the transaction's generation time in milliseconds.
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// the transaction's generation time
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Trade time which is the same as fillTime for the order channel.
    /// </summary>
    [JsonProperty("fillTime")]
    public long? FillTimestamp { get; set; }

    /// <summary>
    /// Trade time which is the same as fillTime for the order channel.
    /// </summary>
    [JsonIgnore]
    public DateTime? FillTime { get { return FillTimestamp?.ConvertFromMilliseconds(); } }
}