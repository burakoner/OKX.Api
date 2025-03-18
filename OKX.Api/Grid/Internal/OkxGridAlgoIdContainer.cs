namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Investment Add Response
/// </summary>
internal record OkxGridAlgoIdContainer
{
    /// <summary>
    /// Algo Order Id
    /// </summary>
    [JsonProperty("algoId")]
    public long? Payload { get; set; }
}