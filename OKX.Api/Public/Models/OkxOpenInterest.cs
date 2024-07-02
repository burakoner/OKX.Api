using OKX.Api.Common.Converters;
using OKX.Api.Common.Enums;

namespace OKX.Api.Public.Models;

/// <summary>
/// OKX Open Interest
/// </summary>
public class OkxOpenInterest
{
    /// <summary>
    /// Instrument Type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Instrument Id
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Open Interest Cont
    /// </summary>
    [JsonProperty("oi")]
    public decimal? OpenInterestCont { get; set; }

    /// <summary>
    /// Open Interest Coin
    /// </summary>
    [JsonProperty("oiCcy")]
    public decimal? OpenInterestCoin { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}
