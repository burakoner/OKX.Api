namespace OKX.Api.Algo.Models;

/// <summary>
/// OKX Algo Order Request
/// </summary>
public class OkxAlgoOrderRequest
{
    /// <summary>
    /// Algo Client Order ID
    /// </summary>
    [JsonProperty("algoId")]
    public long AlgoOrderId { get; set; }

    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }
}
