namespace OKX.Api.Public.Models;

/// <summary>
/// OKX Index Components
/// </summary>
public class OkxIndexComponents
{
    /// <summary>
    /// Components
    /// </summary>
    [JsonProperty("components")]
    public List<OkxIndexComponent> Components { get; set; }

    /// <summary>
    /// Index
    /// </summary>
    [JsonProperty("index")]
    public string Index { get; set; }

    /// <summary>
    /// Last Price
    /// </summary>
    [JsonProperty("last")]
    public decimal LastPrice { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}

/// <summary>
/// OKX Index Component
/// </summary>
public class OkxIndexComponent
{
    /// <summary>
    /// Exchange
    /// </summary>
    [JsonProperty("exch")]
    public string Exchange { get; set; }

    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    /// <summary>
    /// Price
    /// </summary>
    [JsonProperty("symPx")]
    public decimal Price { get; set; }

    /// <summary>
    /// Weight
    /// </summary>
    [JsonProperty("wgt")]
    public decimal Weight { get; set; }

    /// <summary>
    /// Convert Price
    /// </summary>
    [JsonProperty("cnvPx")]
    public decimal ConvertPrice { get; set; }
}
