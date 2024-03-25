using OKX.Api.Common.Converters;
using OKX.Api.Common.Enums;

namespace OKX.Api.Public.Models;

public class OkxLimitPrice
{
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    [JsonProperty("buyLmt")]
    public decimal BuyLimit { get; set; }

    [JsonProperty("sellLmt")]
    public decimal SellLimit { get; set; }

    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}
