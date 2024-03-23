namespace OKX.Api.Funding.Models;

public class OkxAssetValuation
{
    [JsonProperty("totalBal")]
    public decimal TotalBalance { get; set; }
    
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
    
    [JsonProperty("details")]
    public OkxAssetValuationDetails Details { get; set; }
}

public class OkxAssetValuationDetails
{
    [JsonProperty("funding")]
    public decimal Funding { get; set; }

    [JsonProperty("trading")]
    public decimal Trading { get; set; }

    [Obsolete("Deprecated")]
    [JsonProperty("classic")]
    public decimal Classic { get; set; }

    [JsonProperty("earn")]
    public decimal Earn { get; set; }
}
