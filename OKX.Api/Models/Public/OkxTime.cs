namespace OKX.Api.Models.Public;

public class OkxTime
{
    /// <summary>
    /// System time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
