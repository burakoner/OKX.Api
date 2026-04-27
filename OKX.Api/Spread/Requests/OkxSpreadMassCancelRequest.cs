namespace OKX.Api.Spread;

/// <summary>
/// Spread websocket cancel-all request.
/// </summary>
public record OkxSpreadMassCancelRequest
{
    /// <summary>
    /// spread ID
    /// </summary>
    [JsonProperty("sprdId", NullValueHandling = NullValueHandling.Ignore)]
    public string? SpreadId { get; set; }
}
