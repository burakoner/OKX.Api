using OKX.Api.Public.Converters;
using OKX.Api.Public.Enums;

namespace OKX.Api.Public.Models;

public class OkxDeliveryExerciseHistory
{
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }

    [JsonProperty("details")]
    public List<OkxPublicDeliveryExerciseHistoryDetail> Details { get; set; }
}

public class OkxPublicDeliveryExerciseHistoryDetail
{
    [JsonProperty("insId")]
    public string Instrument { get; set; }

    [JsonProperty("px")]
    public decimal Price { get; set; }

    [JsonProperty("type"), JsonConverter(typeof(OkxDeliveryExerciseHistoryTypeConverter))]
    public OkxDeliveryExerciseHistoryType Type { get; set; }
}
