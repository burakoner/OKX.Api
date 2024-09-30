namespace OKX.Api.Trade;

/// <summary>
/// OKX Order Amend Response
/// </summary>
public record OkxTradeOrderAmend : OkxRestApiErrorBase
{
    /// <summary>
    /// Order Id
    /// </summary>
    [JsonProperty("ordId")]
    public long? OrderId { get; set; }

    /// <summary>
    /// Client Order Id
    /// </summary>
    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Request Id
    /// </summary>
    [JsonProperty("reqId")]
    public string RequestId { get; set; } = string.Empty;
}
