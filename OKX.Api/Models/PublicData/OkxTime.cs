namespace OKX.Api.Models.PublicData;

public class OkxTime
{
    /// <summary>
    /// System time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("ts")]
    public long Epoch { get; set; }
    public DateTime Time { get => Epoch.ConvertFromMilliseconds(); }
}
