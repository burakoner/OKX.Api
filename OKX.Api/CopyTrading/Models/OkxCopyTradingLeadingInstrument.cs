namespace OKX.Api.CopyTrading;

/// <summary>
/// OkxLeadingInstrument
/// </summary>
public class OkxCopyTradingLeadingInstrument
{
    /// <summary>
    /// Instrument ID, e.g. BTC-USDT-SWAP
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Whether instrument is a leading instrument. true or false
    /// </summary>
    [JsonProperty("enabled")]
    public bool Enabled { get; set; }
}
