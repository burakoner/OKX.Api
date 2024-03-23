namespace OKX.Api.Public.Models;

public class OkxTime
{
    /// <summary>
    /// System time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// System time
    /// </summary>
    public DateTime Time { get => Timestamp.ConvertFromMilliseconds(); }
}
