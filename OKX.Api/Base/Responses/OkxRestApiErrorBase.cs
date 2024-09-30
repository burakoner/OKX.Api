namespace OKX.Api.Base;

/// <summary>
/// Api Response Base
/// </summary>
public abstract record OkxRestApiErrorBase
{
    /// <summary>
    /// Error Code
    /// </summary>
    [JsonProperty("sCode")]
    public string? ErrorCode { get; set; }

    /// <summary>
    /// Error Message
    /// </summary>
    [JsonProperty("sMsg")]
    public string? ErrorMessage { get; set; }
}