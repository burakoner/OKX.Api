namespace OKX.Api.Account.Models;

/// <summary>
/// Okx Greeks
/// </summary>
public class OkxGreeks
{
    [JsonProperty("deltaBS")]
    public decimal? DeltaBS { get; set; }

    [JsonProperty("deltaPA")]
    public decimal? DeltaPA { get; set; }

    [JsonProperty("gammaBS")]
    public decimal? GammaBS { get; set; }
    
    [JsonProperty("gammaPA")]
    public decimal? GammaPA { get; set; }
    
    [JsonProperty("thetaBS")]
    public decimal? ThetaBS { get; set; }

    [JsonProperty("thetaPA")]
    public decimal? ThetaPA { get; set; }

    [JsonProperty("vegaBS")]
    public decimal? VegaBS { get; set; }
    
    [JsonProperty("vegaPA")]
    public decimal? VegaPA { get; set; }
    
    [JsonProperty("ccy")]
    public string Currency { get; set; }
    
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}