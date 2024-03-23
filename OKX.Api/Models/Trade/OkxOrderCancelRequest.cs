namespace OKX.Api.Models.Trade;

public class OkxOrderCancelRequest
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("ordId", NullValueHandling = NullValueHandling.Ignore)]
    public long? OrderId { get; set; }

    [JsonProperty("clOrdId", NullValueHandling = NullValueHandling.Ignore)]
    public string ClientOrderId { get; set; }
}