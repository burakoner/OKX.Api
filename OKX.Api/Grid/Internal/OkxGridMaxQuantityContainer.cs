namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Max Quantity Container
/// </summary>
internal record OkxGridMaxQuantityContainer
{
    /// <summary>
    /// Maximum grid quantity
    /// </summary>
    [JsonProperty("maxGridQty")]
    public decimal? Payload { get; set; }
}