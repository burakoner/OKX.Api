namespace OKX.Api.Base.Models;

/// <summary>
/// Api Response Base
/// </summary>
public class OkxRestApiErrorResponse
{
    /// <summary>
    /// Error Code
    /// </summary>
    [JsonProperty("sCode")]
    public string ErrorCode { get; set; }

    /// <summary>
    /// Error Message
    /// </summary>
    [JsonProperty("sMsg")]
    public string ErrorMessage { get; set; }
}