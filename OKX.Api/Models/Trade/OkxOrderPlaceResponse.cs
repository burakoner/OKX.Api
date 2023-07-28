namespace OKX.Api.Models.Trade;

public class OkxOrderPlaceResponse : OkxRestApiResponseModel
{
    [JsonProperty("ordId")]
    public long? OrderId { get; set; }

    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Order tag. Used for Broker Id
    /// </summary>
    [JsonProperty("tag")]
    internal string Tag { get; set; }
}
