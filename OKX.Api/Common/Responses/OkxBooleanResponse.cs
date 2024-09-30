namespace OKX.Api.Common;

/// <summary>
/// Okx Boolean Response
/// </summary>
public record OkxBooleanResponse
{
    /// <summary>
    /// Result
    /// </summary>
    [JsonProperty("result")]
    public bool Result { get; set; }
}