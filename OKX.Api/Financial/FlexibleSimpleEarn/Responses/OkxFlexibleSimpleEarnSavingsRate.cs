namespace OKX.Api.Financial.FlexibleSimpleEarn;

/// <summary>
/// OKX Flexible Simple Earn Savings Lending Rate
/// </summary>
public class OkxFlexibleSimpleEarnSavingsRate
{
    /// <summary>
    /// Currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Annual lending rate
    /// </summary>
    [JsonProperty("rate")]
    public decimal Rate { get; set; }
}
