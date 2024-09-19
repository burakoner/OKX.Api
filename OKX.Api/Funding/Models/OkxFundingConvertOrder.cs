using OKX.Api.Funding.Converters;
using OKX.Api.Funding.Enums;
using OKX.Api.Trade.Converters;
using OKX.Api.Trade.Enums;

namespace OKX.Api.Funding.Models;

/// <summary>
/// OKX Convert Order
/// </summary>
public class OkxFundingConvertOrder
{
    /// <summary>
    /// Trade ID
    /// </summary>
    [JsonProperty("tradeId")]
    public string TradeId { get; set; }

    /// <summary>
    /// Quote ID
    /// </summary>
    [JsonProperty("quoteId")]
    public string QuoteId { get; set; }

    /// <summary>
    /// Client Order ID as assigned by the client
    /// </summary>
    [JsonProperty("clTReqId")]
    public string ClientRequestId { get; set; }

    /// <summary>
    /// Trade state
    /// fullyFilled : success
    /// rejected : failed
    /// </summary>
    [JsonProperty("state"), JsonConverter(typeof(OkxFundingConvertOrderStateConverter))]
    public OkxFundingConvertOrderState State { get; set; }

    /// <summary>
    /// Currency pair, e.g. BTC-USDT
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Base currency, e.g. BTC in BTC-USDT
    /// </summary>
    [JsonProperty("baseCcy")]
    public string BaseCurrency { get; set; }

    /// <summary>
    /// Quote currency, e.g. USDT in BTC-USDT
    /// </summary>
    [JsonProperty("quoteCcy")]
    public string QuoteCurrency { get; set; }

    /// <summary>
    /// Trade side based on baseCcy
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(OkxTradeOrderSideConverter))]
    public OkxTradeOrderSide Side { get; set; }

    /// <summary>
    /// Filled price based on quote currency
    /// </summary>
    [JsonProperty("fillPx")]
    public decimal FilledPrice { get; set; }

    /// <summary>
    /// Filled amount for base currency
    /// </summary>
    [JsonProperty("fillBaseSz")]
    public decimal FilledBaseAmount { get; set; }

    /// <summary>
    /// Filled amount for quote currency
    /// </summary>
    [JsonProperty("fillQuoteSz")]
    public decimal FilledQuoteAmount { get; set; }

    /// <summary>
    /// Convert trade time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Convert trade time
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}