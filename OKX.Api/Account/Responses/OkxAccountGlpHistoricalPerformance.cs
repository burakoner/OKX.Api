namespace OKX.Api.Account;

/// <summary>
/// Historical GLP performance for one UTC+8 calendar day.
/// </summary>
public record OkxAccountGlpHistoricalPerformance
{
    /// <summary>
    /// Performance date in yyyy-MM-dd format (UTC+8).
    /// </summary>
    [JsonProperty("date")]
    public string Date { get; set; } = string.Empty;

    /// <summary>
    /// Trading volume by pool type in USD notional.
    /// </summary>
    [JsonProperty("volume")]
    public OkxAccountGlpVolume Volume { get; set; } = new();

    /// <summary>
    /// Market share by pool type, expressed as a decimal rather than a percentage.
    /// </summary>
    [JsonProperty("share")]
    public OkxAccountGlpShare Share { get; set; } = new();
}
