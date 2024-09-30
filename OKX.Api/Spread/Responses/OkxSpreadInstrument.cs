namespace OKX.Api.Spread;

/// <summary>
/// OKX Spread Instrument
/// </summary>
public class OkxSpreadInstrument
{
    /// <summary>
    /// Spread ID
    /// </summary>
    [JsonProperty("sprdId")]
    public string SpreadId { get; set; } = string.Empty;

    /// <summary>
    /// spread Type. Valid values are linear, inverse, hybrid
    /// </summary>
    [JsonProperty("sprdType"), JsonConverter(typeof(OkxSpreadInstrumentTypeConverter))]
    public OkxSpreadInstrumentType Type { get; set; }

    /// <summary>
    /// Current state of the spread. Valid values include live, expired, suspend.
    /// </summary>
    [JsonProperty("state"), JsonConverter(typeof(OkxSpreadInstrumentStateConverter))]
    public OkxSpreadInstrumentState State { get; set; }

    /// <summary>
    /// Currency instrument is based in. Valid values include BTC, ETH
    /// </summary>
    [JsonProperty("baseCcy")]
    public string BaseCurrency { get; set; } = string.Empty;

    /// <summary>
    /// The currency the spread order size is submitted to the underlying venue in, e.g. USD, BTC, ETH.
    /// </summary>
    [JsonProperty("szCcy")]
    public string SizeCurrency { get; set; } = string.Empty;

    /// <summary>
    /// The currency the spread is priced in, e.g. USDT, USD
    /// </summary>
    [JsonProperty("quoteCcy")]
    public string QuoteCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Tick size, e.g. 0.0001 in the quoteCcy of the spread.
    /// </summary>
    [JsonProperty("tickSz")]
    public decimal TickSize { get; set; }

    /// <summary>
    /// Minimum order size in the szCcy of the spread.
    /// </summary>
    [JsonProperty("minSz")]
    public decimal MinimumSize { get; set; }

    /// <summary>
    /// The minimum order size increment the spread can be traded in the szCcy of the spread.
    /// </summary>
    [JsonProperty("lotSz")]
    public decimal LotSize { get; set; }

    /// <summary>
    /// The timestamp the spread was created. Unix timestamp format in milliseconds, , e.g. 1597026383085
    /// </summary>
    [JsonProperty("listTime")]
    public long? ListTimestamp { get; set; }

    /// <summary>
    /// The timestamp the spread was created.
    /// </summary>
    [JsonIgnore]
    public DateTime? ListTime => ListTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Expiry time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("expTime")]
    public long? ExpiryTimestamp { get; set; }

    /// <summary>
    /// Expiry time
    /// </summary>
    [JsonIgnore]
    public DateTime? ExpiryTime => ExpiryTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Expiry time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("uTime")]
    public long? UpdateTimestamp { get; set; }

    /// <summary>
    /// Expiry time
    /// </summary>
    [JsonIgnore]
    public DateTime? UpdateTime => UpdateTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Legs
    /// </summary>
    [JsonProperty("legs")]
    public List<OkxSpreadInstrumentLeg> Legs { get; set; } = [];
}

/// <summary>
/// OKX Spread Instrument Leg
/// </summary>
public class OkxSpreadInstrumentLeg
{
    /// <summary>
    /// Instrument ID, e.g. BTC-USDT-SWAP
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// The direction of the leg of the spread. Valid Values include buy and sell.
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(OkxTradeOrderSideConverter))]
    public OkxTradeOrderSide OrderSide { get; set; }
}