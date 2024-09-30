namespace OKX.Api.Base;

/// <summary>
/// Api Response Base
/// </summary>
public abstract class OkxRestApiErrorBase
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