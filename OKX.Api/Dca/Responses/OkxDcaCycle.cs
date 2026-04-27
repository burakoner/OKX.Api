namespace OKX.Api.Dca;

/// <summary>
/// OKX DCA Cycle
/// </summary>
public record OkxDcaCycle
{
    /// <summary>
    /// Algo ID
    /// </summary>
    [JsonProperty("algoId")]
    public long AlgoOrderId { get; set; }

    /// <summary>
    /// Client-supplied Algo ID
    /// </summary>
    [JsonProperty("algoClOrdId")]
    public string AlgoClientOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Cycle ID
    /// </summary>
    [JsonProperty("cycleId")]
    public long CycleId { get; set; }

    /// <summary>
    /// Whether current cycle
    /// </summary>
    [JsonProperty("currentCycle")]
    public bool? IsCurrentCycle { get; set; }

    /// <summary>
    /// Realized PnL
    /// </summary>
    [JsonProperty("realizedPnl"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? RealizedProfitAndLoss { get; set; }

    /// <summary>
    /// Start time, Unix timestamp format in milliseconds
    /// </summary>
    [JsonProperty("startTime"), JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? StartTimestamp { get; set; }

    /// <summary>
    /// Start time
    /// </summary>
    [JsonIgnore]
    public DateTime? StartTime => StartTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// End time, Unix timestamp format in milliseconds
    /// </summary>
    [JsonProperty("endTime"), JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? EndTimestamp { get; set; }

    /// <summary>
    /// End time
    /// </summary>
    [JsonIgnore]
    public DateTime? EndTime => EndTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Fee
    /// </summary>
    [JsonProperty("fee"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Fee { get; set; }

    /// <summary>
    /// Average price
    /// </summary>
    [JsonProperty("avgPx"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? AveragePrice { get; set; }

    /// <summary>
    /// Take-profit price
    /// </summary>
    [JsonProperty("tpPx"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? TakeProfitPrice { get; set; }
}
