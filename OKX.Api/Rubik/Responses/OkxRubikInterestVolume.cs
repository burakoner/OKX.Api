namespace OKX.Api.Rubik;

/// <summary>
/// OKX Interest Volume
/// </summary>
[JsonConverter(typeof(ArrayConverter))]
public class OkxRubikInterestVolume
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
    public DateTime Time => Timestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Open Interest
    /// </summary>
    [ArrayProperty(1)]
    public decimal OpenInterest { get; set; }

    /// <summary>
    /// Volume
    /// </summary>
    [ArrayProperty(2)]
    public decimal Volume { get; set; }
}
