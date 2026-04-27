namespace OKX.Api.Dca;

/// <summary>
/// OKX DCA Sub Order
/// </summary>
public record OkxDcaSubOrder
{
    /// <summary>
    /// Cycle ID
    /// </summary>
    [JsonProperty("cycleId")]
    public long CycleId { get; set; }

    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("ordId")]
    public long OrderId { get; set; }

    /// <summary>
    /// Average fill price
    /// </summary>
    [JsonProperty("avgFillPx"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? AverageFillPrice { get; set; }

    /// <summary>
    /// Position direction
    /// </summary>
    [JsonProperty("direction")]
    public OkxDcaDirection? Direction { get; set; }

    /// <summary>
    /// Order side
    /// </summary>
    [JsonProperty("side")]
    public OkxTradeOrderSide? OrderSide { get; set; }

    /// <summary>
    /// DCA sub order type label
    /// </summary>
    [JsonProperty("ordType")]
    public string OrderType { get; set; } = string.Empty;

    /// <summary>
    /// Order price
    /// </summary>
    [JsonProperty("px"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Price { get; set; }

    /// <summary>
    /// Order size
    /// </summary>
    [JsonProperty("sz"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Filled size
    /// </summary>
    [JsonProperty("filledSz"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? FilledQuantity { get; set; }

    /// <summary>
    /// State
    /// </summary>
    [JsonProperty("state")]
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// Fee
    /// </summary>
    [JsonProperty("fee"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Fee { get; set; }

    /// <summary>
    /// Rebate
    /// </summary>
    [JsonProperty("rebate"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Rebate { get; set; }

    /// <summary>
    /// Rebate currency
    /// </summary>
    [JsonProperty("rebateCcy")]
    public string RebateCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Contract value
    /// </summary>
    [JsonProperty("ctVal"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? ContractValue { get; set; }

    /// <summary>
    /// Fill time, Unix timestamp format in milliseconds
    /// </summary>
    [JsonProperty("fillTime"), JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? FillTimestamp { get; set; }

    /// <summary>
    /// Fill time
    /// </summary>
    [JsonIgnore]
    public DateTime? FillTime => FillTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Create time, Unix timestamp format in milliseconds
    /// </summary>
    [JsonProperty("cTime"), JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? CreateTimestamp { get; set; }

    /// <summary>
    /// Create time
    /// </summary>
    [JsonIgnore]
    public DateTime? CreateTime => CreateTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Update time, Unix timestamp format in milliseconds
    /// </summary>
    [JsonProperty("uTime"), JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? UpdateTimestamp { get; set; }

    /// <summary>
    /// Update time
    /// </summary>
    [JsonIgnore]
    public DateTime? UpdateTime => UpdateTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Quote currency for trading
    /// </summary>
    [JsonProperty("tradeQuoteCcy")]
    public string? TradeQuoteCurrency { get; set; }
}
