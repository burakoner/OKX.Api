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
    /// Instrument Id
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("uid")]
    public string UserId { get; set; } = string.Empty;
}
