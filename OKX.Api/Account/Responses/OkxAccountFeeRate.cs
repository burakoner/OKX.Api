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
    /// Taker fee rate for the USDT &amp; USDⓈ &amp; Crypto trading pairs and contracts. It is the fee rate of crypto-margined contracts for FUTURES and SWAP
    /// </summary>
    [JsonProperty("taker")]
    public decimal Taker { get; set; }

    /// <summary>
    /// Maker fee rate for the USDT &amp; USDⓈ &amp; Crypto trading pairs and contracts. It is the fee rate of crypto-margined contracts for FUTURES and SWAP
    /// </summary>
    [JsonProperty("maker")]
    public decimal Maker { get; set; }

    /// <summary>
    /// Taker fee rate of USDT-margined contracts, only applicable to FUTURES/SWAP
    /// </summary>
    [JsonProperty("takerU")]
    public decimal? TakerUsdtPairsAndContracts { get; set; }

    /// <summary>
    /// Maker fee rate of USDT-margined contracts, only applicable to FUTURES/SWAP
    /// </summary>
    [JsonProperty("makerU")]
    public decimal? MakerUsdtPairsAndContracts { get; set; }

    /// <summary>
    /// Delivery fee rate
    /// </summary>
    [JsonProperty("delivery")]
    public decimal? Delivery { get; set; }

    /// <summary>
    /// Fee rate for exercising the option
    /// </summary>
    [JsonProperty("exercise")]
    public decimal? Exercise { get; set; }

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Taker fee rate for the USDC trading pairs(SPOT/MARGIN) and contracts(FUTURES/SWAP)
    /// </summary>
    [JsonProperty("takerUSDC")]
    public decimal? TakerUsdcPairsAndContracts { get; set; }

    /// <summary>
    /// Maker fee rate for the USDC trading pairs(SPOT/MARGIN) and contracts(FUTURES/SWAP)
    /// </summary>
    [JsonProperty("makerUSDC")]
    public decimal? MakerUsdcPairsAndContracts { get; set; }

    /// <summary>
    /// Trading rule types
    /// </summary>
    [JsonProperty("ruleType")]
    public OkxInstrumentRuleType RuleType { get; set; }

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

    /// <summary>
    /// Details of fiat fee rate
    /// </summary>
    [JsonProperty("fiat")]
    public List<OkxAccountFiatFeeRate> FiatFeeRates { get; set; } = [];
}


/// <summary>
/// OkxFiatFeeRate
/// </summary>
public record OkxAccountFiatFeeRate
{
    /// <summary>
    /// Fiat currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;
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
}
