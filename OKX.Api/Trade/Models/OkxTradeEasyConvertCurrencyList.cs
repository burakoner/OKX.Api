namespace OKX.Api.Trade;

/// <summary>
/// OKX Easy Convert Currency List
/// </summary>
public class OkxTradeEasyConvertCurrencyList
{
    /// <summary>
    /// Currently owned and convertible small currency list
    /// </summary>
    [JsonProperty("fromData")]
    public List<OkxTradeEasyConvertCurrencyFromData> FromData { get; set; } = [];

    /// <summary>
    /// Type of mainstream currency convert to, e.g. USDT
    /// </summary>
    [JsonProperty("toCcy")]
    public List<string> ToCurrency { get; set; } = [];
}

/// <summary>
/// OKX Easy Convert Currency From Data
/// </summary>
public class OkxTradeEasyConvertCurrencyFromData
{
    /// <summary>
    /// Amount of small payment currency convert from
    /// </summary>
    [JsonProperty("fromAmt")]
    public decimal FromAmount { get; set; }

    /// <summary>
    /// Type of small payment currency convert from, e.g. BTC
    /// </summary>
    [JsonProperty("fromCcy")]
    public string FromCurrency { get; set; }
}