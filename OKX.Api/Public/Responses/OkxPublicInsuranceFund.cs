namespace OKX.Api.Public;

/// <summary>
/// OKX Insurance Fund
/// </summary>
public record OkxPublicInsuranceFund
{
    /// <summary>
    /// Total
    /// </summary>
    [JsonProperty("total")]
    public decimal Total { get; set; }
    
    /// <summary>
    /// Instrument family
    /// Applicable to FUTURES/SWAP/OPTION
    /// </summary>
    [JsonProperty("instFamily")]
    public string InstrumentFamily { get; set; } = string.Empty;
    
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Details
    /// </summary>
    [JsonProperty("details")]
    public List<OkxPublicInsuranceFundDetails> Details { get; set; } = [];
}

/// <summary>
/// OKX Insurance Fund Detail
/// </summary>
public record OkxPublicInsuranceFundDetails
{
    /// <summary>
    /// Balance
    /// </summary>
    [JsonProperty("balance")]
    public decimal Balance { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amt")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Amount { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type")]
    public OkxPublicInsuranceFundDetailType Type { get; set; }

    /// <summary>
    /// Maximum insurance fund balance in the past eight hours
    /// Deprecated by OKX and currently returned as an empty string
    /// </summary>
    [Obsolete("OKX has deprecated this field; it currently returns an empty string and is scheduled for removal.")]
    [JsonProperty("maxBal")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? MaximumBalance { get; set; }

    /// <summary>
    /// Timestamp when insurance fund balance reached maximum in the past eight hours, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// Deprecated by OKX and currently returned as an empty string
    /// </summary>
    [Obsolete("OKX has deprecated this field; it currently returns an empty string and is scheduled for removal.")]
    [JsonProperty("maxBalTs")]
    [JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? MaximumBalanceTimestamp { get; set; }
    
    /// <summary>
    /// Timestamp when insurance fund balance reached maximum in the past eight hours, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// Deprecated by OKX
    /// </summary>
    [Obsolete("OKX has deprecated MaximumBalanceTimestamp; this convenience property is scheduled for removal with it.")]
    [JsonIgnore]
    public DateTime? MaximumBalanceTime => MaximumBalanceTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Real-time insurance fund decline rate (compare balance and maxBal)
    /// Only applicable when type is adl
    /// </summary>
    [Obsolete("OKX has deprecated this field; it currently returns an empty string and is scheduled for removal.")]
    [JsonProperty("decRate")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? DeclineRate { get; set; }

    /// <summary>
    /// ADL related events
    /// rate_adl_start: ADL begins due to high insurance fund decline rate
    /// bal_adl_start: ADL begins due to insurance fund balance falling
    /// adl_end: ADL ends
    /// 
    /// When the rate and balance ADL are triggered at the same time, only bal_adl_start will be returned
    /// Deprecated by OKX and currently returned as an empty string
    /// </summary>
    [Obsolete("OKX has deprecated this field; it currently returns an empty string and is scheduled for removal.")]
    [JsonProperty("adlType")]
    public string AdlType { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}
