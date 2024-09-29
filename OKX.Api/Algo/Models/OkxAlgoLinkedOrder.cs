namespace OKX.Api.Algo;

/// <summary>
/// OKX Algo Linked Order
/// </summary>
public class OkxAlgoLinkedOrder
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("ordId")]
    public long OrderId { get; set; }
}