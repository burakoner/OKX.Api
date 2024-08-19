namespace OKX.Api.Trade.Models;

/// <summary>
/// OKX Easy Convert Currency List
/// </summary>
public class OkxEasyConvertCurrencyList
{
    /// <summary>
    /// Currently owned and convertible small currency list
    /// </summary>
    [JsonProperty("fromData")]
    public List<OkxEasyConvertCurrencyFromData> FromData { get; set; } = [];

    /// <summary>
    /// Type of mainstream currency convert to, e.g. USDT
    /// </summary>
    [JsonProperty("toCcy")]
    public List<string> ToCurrency { get; set; }
}

/// <summary>
/// OKX Easy Convert Currency From Data
/// </summary>
public class OkxEasyConvertCurrencyFromData
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