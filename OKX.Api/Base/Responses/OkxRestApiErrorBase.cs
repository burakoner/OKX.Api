namespace OKX.Api.Base;

/// <summary>
/// Api Response Base
/// </summary>
public abstract record OkxRestApiErrorBase
{
    /// <summary>
    /// Error code.
    /// </summary>
    [JsonProperty("sCode")]
    public string? ErrorCode { get; set; }

    /// <summary>
    /// Error message.
    /// </summary>
    [JsonProperty("sMsg")]
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Sub-code of <see cref="ErrorCode"/>.
    /// Returned by order place/amend REST and WebSocket operation responses.
    /// </summary>
    [JsonProperty("subCode")]
    public string? SubCode { get; set; }
}
