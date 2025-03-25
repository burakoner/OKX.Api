namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Algo Sub Order Update
/// </summary>
public record OkxGridAlgoSubOrderUpdate : OkxGridAlgoSubOrder
{
    /// <summary>
    /// Push time of algo grid information, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("pTime")]
    public long? PushTimestamp { get; set; }

    /// <summary>
    /// Push time of algo grid information
    /// </summary>
    [JsonIgnore]
    public DateTime? PushTimes => PushTimestamp?.ConvertFromMilliseconds();
}