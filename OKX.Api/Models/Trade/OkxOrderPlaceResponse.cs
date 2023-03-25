namespace OKX.Api.Models.Trade;

public class OkxOrderPlaceResponse
{
    [JsonProperty("ordId")]
    public long? OrderId { get; set; }

    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; }

    [JsonProperty("tag")]
    public string Tag { get; set; }

    [JsonProperty("sCode")]
    public string Code { get; set; }

    [JsonProperty("sMsg")]
    public string Message { get; set; }
}
