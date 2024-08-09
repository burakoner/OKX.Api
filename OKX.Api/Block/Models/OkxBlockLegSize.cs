using OKX.Api.Public.Converters;
using OKX.Api.Public.Enums;
using OKX.Api.Trade.Converters;
using OKX.Api.Trade.Enums;

namespace OKX.Api.Block.Models;

/// <summary>
/// OKX Block Leg Size Request
/// </summary>
public class OkxBlockLegSize
{
    /// <summary>
    /// The Instrument ID of each leg. Example : "BTC-USDT-SWAP"
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// The size of each leg
    /// </summary>
    [JsonProperty("sz")]
    public string Size { get; set; }
}
