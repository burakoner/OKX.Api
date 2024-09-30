namespace OKX.Api.Common;

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