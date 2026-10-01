namespace OKX.Api.Base;

/// <summary>
/// OKX Socket Response
/// </summary>
public record OkxSocketResponse
{
    /// <summary>
    /// Optional client message identifier echoed by OKX in subscription acknowledgements.
    /// </summary>
    [JsonProperty("id")]
    public string? RequestId { get; set; }

    /// <summary>
    /// Success
    /// </summary>
    public bool Success => string.IsNullOrEmpty(ErrorCode) || ErrorCode.Trim() == "0";

    /// <summary>
    /// Event
    /// </summary>
    [JsonProperty("event")]
    public string Event { get; set; } = string.Empty;

    /// <summary>
    /// Connection identifier supplied by OKX, including subscription acknowledgements.
    /// Absent for envelopes that do not supply connId.
    /// </summary>
    [JsonProperty("connId")]
    public string? ConnectionId { get; set; }

    /// <summary>
    /// Error Code
    /// </summary>
    [JsonProperty("code")]
    public string ErrorCode { get; set; } = string.Empty;

    /// <summary>
    /// Error Message
    /// </summary>
    [JsonProperty("msg")]
    public string ErrorMessage { get; set; } = string.Empty;
}
