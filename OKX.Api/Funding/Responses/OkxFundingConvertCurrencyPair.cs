namespace OKX.Api.Funding;

/// <summary>
/// OKX Convert Currency Pair
/// </summary>
public record OkxFundingConvertCurrencyPair
{
    /// <summary>
    /// Currency pair, e.g. BTC-USDT
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Base currency, e.g. BTC in BTC-USDT
    /// </summary>
    [JsonProperty("baseCcy")]
    public string BaseCurrency { get; set; } = string.Empty;
    
    /// <summary>
    /// Maximum amount of base currency
    /// </summary>
    [JsonProperty("baseCcyMax")]
    public decimal BaseCurrencyMaximumAmount { get; set; }

    /// <summary>
    /// Minimum amount of base currency
    /// </summary>
    [JsonProperty("baseCcyMin")]
    public decimal BaseCurrencyMinimumAmount { get; set; }

    /// <summary>
    /// Quote currency, e.g. USDT in BTC-USDT
    /// </summary>
    [JsonProperty("quoteCcy")]
    public string QuoteCurrency { get; set; } = string.Empty;
    
    /// <summary>
    /// Maximum amount of quote currency
    /// </summary>
    [JsonProperty("quoteCcyMax")]
    public decimal QuoteCurrencyMaximumAmount { get; set; }

    /// <summary>
    /// Minimum amount of quote currency
    /// </summary>
    [JsonProperty("quoteCcyMin")]
    public decimal QuoteCurrencyMinimumAmount { get; set; }
}