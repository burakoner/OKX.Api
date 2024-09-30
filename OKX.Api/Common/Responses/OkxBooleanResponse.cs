namespace OKX.Api.Common;

/// <summary>
/// Okx Boolean Response
/// </summary>
internal record OkxBooleanResponse
{
    /// <summary>
    /// Result
    /// </summary>
    [JsonProperty("result")]
    public bool Data { get; set; }
}