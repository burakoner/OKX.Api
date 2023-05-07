namespace OKX.Api.Models.Trade;

public class OkxOrderPlaceResponse : OkxRestApiResponseModel
{
    [JsonProperty("ordId")]
    public long? OrderId { get; set; }

    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; }

    //[JsonProperty("tag")]
    //public string Tag { get; set; }
}
