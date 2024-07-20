namespace OKX.Api.Base.Models;

/// <summary>
/// OKX Rest API Response
/// </summary>
/// <typeparam name="T"></typeparam>
public class OkxRestApiResponse<T>
{
    /// <summary>
    /// Error Code
    /// </summary>
    [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
    public int ErrorCode { get; set; }

    /// <summary>
    /// Error Message
    /// </summary>
    [JsonProperty("msg", NullValueHandling = NullValueHandling.Ignore)]
    public string ErrorMessage { get; set; }

    /// <summary>
    /// Data
    /// </summary>
    [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
    public T Data { get; set; }
}