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

    /// <summary>
    /// Order creation timestamp in Unix milliseconds, equivalent to cTime on the orders channel.
    /// This is not the amendment completion time; a successful acknowledgement only accepts the request.
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Order creation time as a UTC instant.
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}
