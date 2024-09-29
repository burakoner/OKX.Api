namespace OKX.Api.SignalBot;

/// <summary>
/// OKX Signal Bot Sub-Order
/// </summary>
public class OkxSignalBotSuborder
{
    /// <summary>
    /// Algo ID
    /// </summary>
    [JsonProperty("algoId")]
    public long AlgoId { get; set; }

    /// <summary>
    /// Client-supplied Algo ID
    /// </summary>
    [JsonProperty("algoClOrdId")]
    public string AlgoClientOrderId { get; set; } = "";

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = "";
    
    /// <summary>
    /// Algo order type
    /// contract: Contract signal
    /// </summary>
    [JsonProperty("algoOrdType")]
    public string AlgoOrderType { get; set; } = "";
    
    /// <summary>
    /// Sub order ID
    /// </summary>
    [JsonProperty("ordId")]
    public long SuborderId { get; set; }

    /// <summary>
    /// Algo order created time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("cTime")]
    public long CreateTimestamp { get; set; }

    /// <summary>
    /// Algo order created time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonIgnore]
    public DateTime CreateTime => CreateTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Algo order updated time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("uTime")]
    public long? UpdateTimestamp { get; set; }
    
    /// <summary>
    /// Algo order updated time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonIgnore]
    public DateTime? UpdateTime => UpdateTimestamp?.ConvertFromMilliseconds();
    
    /// <summary>
    /// Sub order trade mode
    /// Margin mode: cross/isolated
    /// Non-Margin mode: cash
    /// </summary>
    [JsonProperty("tdMode"), JsonConverter(typeof(OkxTradeModeConverter))]
    public OkxTradeMode TradeMode { get; set; }
    
    /// <summary>
    /// Margin currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = "";
    
    /// <summary>
    /// Sub order type
    /// </summary>
    [JsonProperty("ordType"), JsonConverter(typeof(OkxSignalBotOrderTypeConverter))]
    public OkxSignalBotOrderType OrderType { get; set; }
    
    /// <summary>
    /// Sub order quantity to buy or sell
    /// </summary>
    [JsonProperty("sz")]
    public decimal Size { get; set; }
    
    /// <summary>
    /// Sub order state
    /// </summary>
    [JsonProperty("state"), JsonConverter(typeof(OkxSignalBotSuborderStateConverter))]
    public OkxSignalBotSuborderState State { get; set; }

    /// <summary>
    /// Sub order side
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(OkxTradeOrderSideConverter))]
    public OkxTradeOrderSide Side { get; set; }
    
    /// <summary>
    /// Sub order price
    /// </summary>
    [JsonProperty("px")]
    public decimal? Price { get; set; }

    /// <summary>
    /// Fee
    /// </summary>
    [JsonProperty("fee")]
    public decimal Fee { get; set; }

    /// <summary>
    /// Fee currency
    /// </summary>
    [JsonProperty("feeCcy")]
    public string FeeCurrency { get; set; } = "";

    /// <summary>
    /// Sub order average filled price
    /// </summary>
    [JsonProperty("avgPx")]
    public decimal AverageFilledPrice { get; set; }

    /// <summary>
    /// Sub order accumulated fill quantity
    /// </summary>
    [JsonProperty("accFillSz")]
    public decimal AccumulatedFillQuantity { get; set; }
    
    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide"), JsonConverter(typeof(OkxTradePositionSideConverter))]
    public OkxTradePositionSide PositionSide { get; set; }
    
    /// <summary>
    /// Sub order profit and loss
    /// </summary>
    [JsonProperty("pnl")]
    public decimal PNL { get; set; }
    
    /// <summary>
    /// Contract value. Only applicable to FUTURES/SWAP
    /// </summary>
    [JsonProperty("ctVal")]
    public decimal? ContractValue { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever")]
    public decimal? Leverage { get; set; }
}