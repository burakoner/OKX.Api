using OKX.Api.Algo.Converters;
using OKX.Api.Algo.Enums;

namespace OKX.Api.Algo.Models;

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