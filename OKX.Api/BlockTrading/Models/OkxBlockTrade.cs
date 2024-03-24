using OKX.Api.Public.Converters;

namespace OKX.Api.BlockTrading.Models;

public class OkxBlockTrade
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("tradeId")]
    public long TradeId { get; set; }

    [JsonProperty("px")]
    public decimal Price { get; set; }

    [JsonProperty("sz")]
    public decimal Quantity { get; set; }

    [JsonProperty("side"), JsonConverter(typeof(OkxTradeSideConverter))]
    public OkxTradeSide Side { get; set; }

    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}
