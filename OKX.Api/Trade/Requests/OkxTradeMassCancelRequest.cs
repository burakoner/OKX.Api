namespace OKX.Api.Trade;

/// <summary>
/// OKX Mass Cancel Request
/// </summary>
public record OkxTradeMassCancelRequest
{
    /// <summary>
    /// Instrument Type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Instrument Family
    /// </summary>
    [JsonProperty("instFamily")]
    public string InstrumentFamily { get; set; } = string.Empty;
}