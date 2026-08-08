namespace OKX.Api.Algo;

/// <summary>
/// Chase fields that can be amended before a trigger order fires.
/// </summary>
public record OkxAlgoAdvancedChaseAmendParameters
{
    /// <summary>
    /// New non-negative chase value, interpreted using the order's existing chase type.
    /// The server does not allow changing between zero and a positive distance mode.
    /// </summary>
    [JsonProperty("newChaseVal", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? NewChaseValue { get; set; }

    /// <summary>
    /// New positive maximum chase value. Only applicable when the original order enabled a maximum chase distance.
    /// </summary>
    [JsonProperty("newMaxChaseVal", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? NewMaximumChaseValue { get; set; }
}
