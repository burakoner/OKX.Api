namespace OKX.Api.Models;

/// <summary>
/// Api Response Base
/// </summary>
public class OkxRestApiResponseModel
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