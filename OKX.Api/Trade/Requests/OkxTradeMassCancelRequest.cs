namespace OKX.Api.Trade;

/// <summary>
/// OKX Mass Cancel Request
/// </summary>
public class OkxTradeMassCancelRequest
{
    /// <summary>
    /// Instrument Type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Instrument Family
    /// </summary>
    [JsonProperty("instFamily")]
    public string InstrumentFamily { get; set; } = string.Empty;
}