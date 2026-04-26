namespace OKX.Api.Public;

/// <summary>
/// Event contract series.
/// </summary>
public record OkxPublicEventContractSeries
{
    /// <summary>
    /// Series ID.
    /// </summary>
    [JsonProperty("seriesId")]
    public string SeriesId { get; set; } = string.Empty;

    /// <summary>
    /// Series frequency.
    /// </summary>
    [JsonProperty("freq")]
    public OkxPublicEventContractFrequency Frequency { get; set; }

    /// <summary>
    /// Series title.
    /// </summary>
    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Series category.
    /// </summary>
    [JsonProperty("category")]
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Settlement information.
    /// </summary>
    [JsonProperty("settlement")]
    public OkxPublicEventContractSeriesSettlement Settlement { get; set; } = new();
}

/// <summary>
/// Event contract series settlement details.
/// </summary>
public record OkxPublicEventContractSeriesSettlement
{
    /// <summary>
    /// Settlement method.
    /// </summary>
    [JsonProperty("method")]
    public OkxPublicEventContractSettlementMethod Method { get; set; }

    /// <summary>
    /// Whether the market can settle early.
    /// </summary>
    [JsonProperty("closeEarly")]
    public bool CloseEarly { get; set; }

    /// <summary>
    /// Settlement source name.
    /// </summary>
    [JsonProperty("srcName")]
    public string SourceName { get; set; } = string.Empty;

    /// <summary>
    /// Underlying trading symbol.
    /// </summary>
    [JsonProperty("underlying")]
    public string Underlying { get; set; } = string.Empty;
}
