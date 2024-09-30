namespace OKX.Api.Account;

/// <summary>
/// Okx Greeks
/// </summary>
public record OkxAccountGreeks
{
    /// <summary>
    /// Delta BS
    /// </summary>
    [JsonProperty("deltaBS")]
    public decimal? DeltaBS { get; set; }

    /// <summary>
    /// Delta PA
    /// </summary>
    [JsonProperty("deltaPA")]
    public decimal? DeltaPA { get; set; }

    /// <summary>
    /// Gamma BS
    /// </summary>
    [JsonProperty("gammaBS")]
    public decimal? GammaBS { get; set; }

    /// <summary>
    /// Gamma PA
    /// </summary>
    [JsonProperty("gammaPA")]
    public decimal? GammaPA { get; set; }

    /// <summary>
    /// Theta BS
    /// </summary>
    [JsonProperty("thetaBS")]
    public decimal? ThetaBS { get; set; }

    /// <summary>
    /// Theta PA
    /// </summary>
    [JsonProperty("thetaPA")]
    public decimal? ThetaPA { get; set; }

    /// <summary>
    /// Vega BS
    /// </summary>
    [JsonProperty("vegaBS")]
    public decimal? VegaBS { get; set; }

    /// <summary>
    /// Vega PA
    /// </summary>
    [JsonProperty("vegaPA")]
    public decimal? VegaPA { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Time
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}