namespace OKX.Api.Public;

/// <summary>
/// OKX Position Tier
/// </summary>
public record OkxPublicPositionTier
{
    /// <summary>
    /// Underlying
    /// </summary>
    [JsonProperty("uly")]
    public string Underlying { get; set; } = string.Empty;

    /// <summary>
    /// Instrument Family
    /// </summary>
    [JsonProperty("instFamily")]
    public string InstrumentFamily { get; set; } = string.Empty;

    /// <summary>
    /// Instrument Id
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Tier
    /// </summary>
    [JsonProperty("tier")]
    public int Tier { get; set; }

    /// <summary>
    /// Minimum Size
    /// </summary>
    [JsonProperty("minSz")]
    public decimal? MinimumSize { get; set; }

    /// <summary>
    /// Maximum Size
    /// </summary>
    [JsonProperty("maxSz")]
    public decimal? MaximumSize { get; set; }

    /// <summary>
    /// Maintenance Margin Requirement
    /// </summary>
    [JsonProperty("mmr")]
    public decimal? MaintenanceMarginRequirement { get; set; }

    /// <summary>
    /// Initial Margin Requirement
    /// </summary>
    [JsonProperty("imr")]
    public decimal? InitialMarginRequirement { get; set; }

    /// <summary>
    /// Maximum Leverage
    /// </summary>
    [JsonProperty("maxLever")]
    public decimal? MaximumLeverage { get; set; }

    /// <summary>
    /// Option Margin Coefficient
    /// </summary>
    [JsonProperty("optMgnFactor")]
    public decimal? OptionMarginCoefficient { get; set; }

    /// <summary>
    /// Maximum Quote Loan
    /// </summary>
    [JsonProperty("quoteMaxLoan")]
    public decimal? MaximumQuoteLoan { get; set; }

    /// <summary>
    /// Maximum Base Loan
    /// </summary>
    [JsonProperty("baseMaxLoan")]
    public decimal? MaximumBaseLoan { get; set; }
}