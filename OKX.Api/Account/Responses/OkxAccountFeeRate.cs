namespace OKX.Api.Account;

/// <summary>
/// OkxFeeRate
/// </summary>
public record OkxAccountFeeRate
{
    /// <summary>
    /// Fee rate Level
    /// </summary>
    [JsonProperty("level")]
    public string Level { get; set; } = string.Empty;

    /// <summary>
    /// Fee groups.
    /// Applicable to SPOT/MARGIN/SWAP/FUTURES/OPTION
    /// </summary>
    [JsonProperty("feeGroup")]
    public List<OkxAccountFeeRateGroup> FeeGroup { get; set; } = [];

    /// <summary>
    /// Delivery fee rate
    /// </summary>
    [JsonProperty("delivery"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Delivery { get; set; }

    /// <summary>
    /// Fee rate for exercising the option
    /// </summary>
    [JsonProperty("exercise"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Exercise { get; set; }

    /// <summary>
    /// Settlement fee rate for users whose positions match the event contract settlement result.
    /// Only applicable to EVENTS.
    /// </summary>
    [JsonProperty("settle"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Settlement { get; set; }

    /// <summary>
    /// Deprecated taker fee rate returned at the response root.
    /// </summary>
    [JsonProperty("taker"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? DeprecatedTaker { get; set; }

    /// <summary>
    /// Deprecated maker fee rate returned at the response root.
    /// </summary>
    [JsonProperty("maker"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? DeprecatedMaker { get; set; }

    /// <summary>
    /// Deprecated USDT-margined contract taker fee rate.
    /// </summary>
    [JsonProperty("takerU"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? DeprecatedUsdtTaker { get; set; }

    /// <summary>
    /// Deprecated USDT-margined contract maker fee rate.
    /// </summary>
    [JsonProperty("makerU"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? DeprecatedUsdtMaker { get; set; }

    /// <summary>
    /// Deprecated USD stablecoin and crypto taker fee rate.
    /// </summary>
    [JsonProperty("takerUSDC"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? DeprecatedUsdcTaker { get; set; }

    /// <summary>
    /// Deprecated USD stablecoin and crypto maker fee rate.
    /// </summary>
    [JsonProperty("makerUSDC"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? DeprecatedUsdcMaker { get; set; }

    /// <summary>
    /// Deprecated trading rule type.
    /// </summary>
    [JsonProperty("ruleType")]
    public OkxInstrumentRuleType? DeprecatedRuleType { get; set; }

    /// <summary>
    /// Deprecated currency category.
    /// </summary>
    [JsonProperty("category")]
    public string DeprecatedCategory { get; set; } = string.Empty;

    /// <summary>
    /// Deprecated fiat fee-rate details.
    /// </summary>
    [JsonProperty("fiat")]
    public List<OkxAccountFiatFeeRate> DeprecatedFiatFeeRates { get; set; } = [];

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Data return time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Data return time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}

/// <summary>
/// OkxAccountFeeRateGroup
/// </summary>
public record OkxAccountFeeRateGroup
{
    /// <summary>
    /// Fiat currency
    /// </summary>
    [JsonProperty("groupId")]
    public string GroupId { get; set; } = string.Empty;
    ///             
    /// <summary>
    /// Taker fee rate
    /// </summary>
    [JsonProperty("taker")]
    public decimal Taker { get; set; }
    ///             
    /// <summary>
    /// Maker fee rate
    /// </summary>
    [JsonProperty("maker")]
    public decimal Maker { get; set; }

    /// <summary>
    /// Legacy ELP maker effective fee rate. Kept while OKX accepts the alias.
    /// </summary>
    [JsonProperty("elpMaker"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? ElpMaker { get; set; }

    /// <summary>
    /// RPI maker effective fee rate.
    /// </summary>
    [JsonProperty("rpiMaker"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? RpiMaker { get; set; }

    /// <summary>
    /// Effective RPI maker rate, falling back to the temporary ELP alias.
    /// </summary>
    [JsonIgnore]
    public decimal? EffectiveRpiMaker => RpiMaker ?? ElpMaker;
}

/// <summary>
/// Deprecated fiat fee-rate detail.
/// </summary>
public record OkxAccountFiatFeeRate
{
    /// <summary>
    /// Fiat currency.
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Fiat taker fee rate.
    /// </summary>
    [JsonProperty("taker")]
    public decimal Taker { get; set; }

    /// <summary>
    /// Fiat maker fee rate.
    /// </summary>
    [JsonProperty("maker")]
    public decimal Maker { get; set; }
}
