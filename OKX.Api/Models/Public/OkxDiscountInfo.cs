namespace OKX.Api.Models.Public;

public class OkxDiscountInfo
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("amt")]
    public decimal? Amount { get; set; }

    [JsonProperty("discountLv")]
    public int DiscountLevel { get; set; }

    [JsonProperty("discountInfo")]
    public IEnumerable<OkxPublicDiscountInfoDetail> Details { get; set; }
}

public class OkxPublicDiscountInfoDetail
{
    [JsonProperty("discountRate")]
    public decimal? DiscountRate { get; set; }

    [JsonProperty("maxAmt")]
    public decimal? MaximumAmount { get; set; }

    [JsonProperty("minAmt")]
    public decimal? MinimumAmount { get; set; }
}
