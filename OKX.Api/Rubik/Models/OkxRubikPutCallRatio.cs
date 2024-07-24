namespace OKX.Api.Rubik.Models;

/// <summary>
/// OKX Put Call Ratio
/// </summary>
[JsonConverter(typeof(ArrayConverter))]
public class OkxRubikPutCallRatio
{
    /// <summary>
    /// Time
    /// </summary>
    [ArrayProperty(0)]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Open Interest Ratio
    /// </summary>
    [ArrayProperty(1)]
    public decimal OpenInterestRatio { get; set; }

    /// <summary>
    /// Volume Ratio
    /// </summary>
    [ArrayProperty(2)]
    public decimal VolumeRatio { get; set; }
}
