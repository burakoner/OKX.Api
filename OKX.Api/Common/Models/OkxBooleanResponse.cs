namespace OKX.Api.Common.Models;

/// <summary>
/// Okx Boolean Response
/// </summary>
public class OkxBooleanResponse
{
    /// <summary>
    /// Result
    /// </summary>
    [JsonProperty("result")]
    public bool Result { get; set; }
}