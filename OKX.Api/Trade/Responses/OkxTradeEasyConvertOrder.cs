namespace OKX.Api.Trade;

/// <summary>
/// OKX Easy Convert Order
/// </summary>
public record OkxTradeEasyConvertOrder
{
    /// <summary>
    /// Current status of easy convert
    /// </summary>
    [JsonProperty("status")]
    public OkxTradeEasyConvertOrderStatus Status { get; set; }
    
    /// <summary>
    /// Type of small payment currency convert from
    /// </summary>
    [JsonProperty("fromCcy")]
    public string FromCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Type of mainstream currency convert to
    /// </summary>
    [JsonProperty("toCcy")]
    public decimal ToCurrency { get; set; }

    /// <summary>
    /// Filled amount of small payment currency convert from
    /// </summary>
    [JsonProperty("fillFromSz")]
    public decimal FromFilledAmount { get; set; }

    /// <summary>
    /// Filled amount of mainstream currency convert to
    /// </summary>
    [JsonProperty("fillToSz")]
    public decimal ToFilledAmount { get; set; }

    /// <summary>
    /// Trade time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("uTime")]
    public long UpdateTimestamp { get; set; }

    /// <summary>
    /// Trade time
    /// </summary>
    [JsonIgnore]
    public DateTime UpdateTime => UpdateTimestamp.ConvertFromMilliseconds();
}
