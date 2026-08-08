namespace OKX.Api.Algo;

/// <summary>
/// Parameters for the chase algo order spawned by a trigger order.
/// These fields are nested under <c>advChaseParams</c> and are separate from a standalone chase order's root fields.
/// </summary>
public record OkxAlgoAdvancedChaseParameters
{
    /// <summary>
    /// Chase distance unit. Defaults server-side to distance.
    /// </summary>
    [JsonProperty("chaseType", NullValueHandling = NullValueHandling.Ignore)]
    public OkxAlgoChaseType? ChaseType { get; set; }

    /// <summary>
    /// Chase value. Zero tracks the best bid or ask; a positive value sets a distance.
    /// When <see cref="ChaseType"/> is ratio, 0.1 represents 10%.
    /// </summary>
    [JsonProperty("chaseVal", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? ChaseValue { get; set; }

    /// <summary>
    /// Maximum chase distance unit. Must be supplied together with <see cref="MaximumChaseValue"/>.
    /// </summary>
    [JsonProperty("maxChaseType", NullValueHandling = NullValueHandling.Ignore)]
    public OkxAlgoChaseType? MaximumChaseType { get; set; }

    /// <summary>
    /// Positive maximum chase distance. The chase order is automatically canceled at this deviation.
    /// </summary>
    [JsonProperty("maxChaseVal", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? MaximumChaseValue { get; set; }
}
