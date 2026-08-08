namespace OKX.Api.Public;

/// <summary>
/// Market Maker Program Instrument Type Classification
/// </summary>
public record OkxPublicMarketMakerInstrumentType
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Market Maker Program pair classification
    /// </summary>
    [JsonProperty("pairType")]
    public OkxPublicMarketMakerPairType PairType { get; set; }
}