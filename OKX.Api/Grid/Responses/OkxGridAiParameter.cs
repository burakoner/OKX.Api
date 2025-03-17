namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid AI Parameter
/// </summary>
public record OkxGridAiParameter
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Algo order type
    /// </summary>
    [JsonProperty("algoOrdType")]
    public OkxGridAlgoOrderType AlgoOrderType { get; set; }

    /// <summary>
    /// Duration
    /// </summary>
    [JsonProperty("duration")]
    public OkxGridBackTestingDuration Duration { get; set; }
    
    /// <summary>
    /// Quantity
    /// </summary>
    [JsonProperty("gridNum")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Maximum price
    /// </summary>
    [JsonProperty("maxPx")]
    public decimal? MaximumPrice { get; set; }

    /// <summary>
    /// Minimum price
    /// </summary>
    [JsonProperty("minPx")]
    public decimal? MinimumPrice { get; set; }

    /// <summary>
    /// Maximum profit rate per grid
    /// </summary>
    [JsonProperty("perMaxProfitRate")]
    public decimal? MaximumProfitRatePerGrid { get; set; }

    /// <summary>
    /// Minimum profit rate per grid
    /// </summary>
    [JsonProperty("perMinProfitRate")]
    public decimal? MinimumProfitRatePerGrid { get; set; }

    /// <summary>
    /// Annualized rate
    /// </summary>
    [JsonProperty("annualizedRate")]
    public decimal? AnnualizedRate { get; set; }

    /// <summary>
    /// Minimum investment
    /// </summary>
    [JsonProperty("minInvestment")]
    public decimal? MinimumInvestment { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Run type
    /// </summary>
    [JsonProperty("runType")]
    public OkxGridRunType RunType { get; set; }

    /// <summary>
    /// Direction
    /// </summary>
    [JsonProperty("direction")]
    public OkxGridContractDirection? Direction { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever")]
    public int? Leverage { get; set; }
}