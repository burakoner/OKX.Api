using OKX.Api.Common.Converters;
using OKX.Api.Common.Enums;

namespace OKX.Api.BlockTrading.Models;

public class OkxBlockTicker
{
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Base Volume
    /// </summary>
    [JsonProperty("volCcy24h")]
    public decimal VolumeCurrency { get; set; }

    /// <summary>
    /// Quote Volume
    /// </summary>
    [JsonProperty("vol24h")]
    public decimal Volume { get; set; }

    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}
