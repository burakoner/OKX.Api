namespace OKX.Api.Financial;

/// <summary>
/// OKX Dual Investment Currency Pair
/// </summary>
public record OkxFinancialDualInvestmentCurrencyPair
{
    /// <summary>
    /// Base currency
    /// </summary>
    [JsonProperty("baseCcy")]
    public string BaseCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Quote currency
    /// </summary>
    [JsonProperty("quoteCcy")]
    public string QuoteCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Option type
    /// </summary>
    [JsonProperty("optType")]
    public OkxOptionType OptionType { get; set; }

    /// <summary>
    /// Underlying index, e.g. BTC-USD
    /// </summary>
    [JsonProperty("uly")]
    public string Underlying { get; set; } = string.Empty;
}
