namespace OKX.Api.Base;

/// <summary>
/// OKX Socket Response
/// </summary>
public record OkxSocketResponse
{
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
