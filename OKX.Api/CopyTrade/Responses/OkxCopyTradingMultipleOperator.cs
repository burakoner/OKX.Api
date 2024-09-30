namespace OKX.Api.Account;

/// <summary>
/// OkxMultipleOperation
/// </summary>
public class OkxCopyTradingMultipleOperation
{
    /// <summary>
    /// Instrument ID setted successfully
    /// </summary>
    [JsonProperty("succInstId")]
    public string SuccessInstrumentIds{ get; set; } = string.Empty;

    /// <summary>
    /// Instrument ID setted unsuccessfully
    /// </summary>
    [JsonProperty("failInstId")]
    public string FailInstrumentIds { get; set; } = string.Empty;

    /// <summary>
    /// Result
    /// </summary>
    [JsonProperty("result")]
    public OkxCopyTradingOperationStatus Result { get; set; }
}
