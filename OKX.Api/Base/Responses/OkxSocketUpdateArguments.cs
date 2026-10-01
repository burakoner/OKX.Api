namespace OKX.Api.Base;

/// <summary>
/// OKX Socket Update Arguments
/// </summary>
public record OkxSocketUpdateArguments
{
    /// <summary>
    /// Channel
    /// </summary>
    [JsonProperty("channel")]
    public string Channel { get; set; } = string.Empty;

    /// <summary>
    /// Instrument type reported by the channel. Absent for channels that do not supply instType.
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType? InstrumentType { get; set; }

    /// <summary>
    /// Instrument family reported by the channel. Absent when not supplied.
    /// </summary>
    [JsonProperty("instFamily")]
    public string? InstrumentFamily { get; set; }

    /// <summary>
    /// Instrument Id
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Spread Id
    /// </summary>
    [JsonProperty("sprdId")]
    public string SpreadId { get; set; } = string.Empty;

    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("uid")]
    public string UserId { get; set; } = string.Empty;
}
