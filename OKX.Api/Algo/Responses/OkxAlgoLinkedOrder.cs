namespace OKX.Api.Algo;

/// <summary>
/// OKX Algo Linked Order
/// </summary>
public record OkxAlgoLinkedOrder
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("ordId")]
    public long OrderId { get; set; }
}