namespace OKX.Api.Funding;

/// <summary>
/// OKX buy/sell currency pair
/// </summary>
public record OkxFundingBuySellCurrencyPair
{
    /// <summary>
    /// Trade side supported by the currency pair.
    /// </summary>
    [JsonProperty("side")]
    public OkxTradeOrderSide Side { get; set; }

    /// <summary>
    /// Source currency of the pair.
    /// </summary>
    [JsonProperty("fromCcy")]
    public string FromCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Destination currency of the pair.
    /// </summary>
    [JsonProperty("toCcy")]
    public string ToCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Maximum amount allowed for a single trade.
    /// </summary>
    [JsonProperty("singleTradeMax")]
    public decimal SingleTradeMaximum { get; set; }

    /// <summary>
    /// Minimum amount allowed for a single trade.
    /// </summary>
    [JsonProperty("singleTradeMin")]
    public decimal SingleTradeMinimum { get; set; }

    /// <summary>
    /// Remaining daily quota for fixed-price trading.
    /// </summary>
    [JsonProperty("fixedPxRemainingDailyQuota")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? FixedPriceRemainingDailyQuota { get; set; }

    /// <summary>
    /// Total daily limit for fixed-price trading.
    /// </summary>
    [JsonProperty("fixedPxDailyLimit")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? FixedPriceDailyLimit { get; set; }

    /// <summary>
    /// Payment methods available for the currency pair.
    /// </summary>
    [JsonProperty("paymentMethods")]
    public List<string> PaymentMethods { get; set; } = [];
}
