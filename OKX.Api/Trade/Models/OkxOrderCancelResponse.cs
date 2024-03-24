namespace OKX.Api.Trade.Models;

public class OkxOrderCancelResponse : OkxRestApiResponseModel
{
    [JsonProperty("ordId")]
    public long? OrderId { get; set; }

    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; }
}