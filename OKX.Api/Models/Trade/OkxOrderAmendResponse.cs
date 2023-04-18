namespace OKX.Api.Models.Trade;

public class OkxOrderAmendResponse : OkxRestApiResponseModel
{
    [JsonProperty("ordId")]
    public long? OrderId { get; set; }

    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; }

    [JsonProperty("reqId")]
    public string RequestId { get; set; }
}
