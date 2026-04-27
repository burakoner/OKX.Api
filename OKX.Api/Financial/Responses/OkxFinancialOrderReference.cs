namespace OKX.Api.Financial;

/// <summary>
/// OKX Financial order reference
/// </summary>
public record OkxFinancialOrderReference
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("ordId")]
    public string OrderId { get; set; } = string.Empty;

    /// <summary>
    /// Order tag
    /// </summary>
    [JsonProperty("tag")]
    public string? Tag { get; set; }
}
