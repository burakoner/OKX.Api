namespace OKX.Api.Models.Trade;

public class OkxOrderAmendResponse
{
    [JsonProperty("ordId")]
    public long? OrderId { get; set; }

    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; }

    [JsonProperty("reqId")]
    public string RequestId { get; set; }

    [JsonProperty("sCode")]
    public string Code { get; set; }

    [JsonProperty("sMsg")]
    public string Message { get; set; }
}
