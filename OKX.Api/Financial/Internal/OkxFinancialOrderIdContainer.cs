namespace OKX.Api.Financial;

/// <summary>
/// OKX Financial OrderId Container
/// </summary>
internal record OkxFinancialOrderIdContainer
{
    /// <summary>
    /// Order Id
    /// </summary>
    [JsonProperty("ordId")]
    public long? Payload { get; set; }
}