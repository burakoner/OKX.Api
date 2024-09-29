namespace OKX.Api.Rubik;

/// <summary>
/// OKX Interest Volume Strike
/// </summary>
[JsonConverter(typeof(ArrayConverter))]
public class OkxRubikInterestVolumeStrike
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
    /// Strike
    /// </summary>
    [ArrayProperty(1)]
    public string Strike { get; set; }

    /// <summary>
    /// Call Open Interest
    /// </summary>
    [ArrayProperty(2)]
    public decimal CallOpenInterest { get; set; }

    /// <summary>
    /// Put Open Interest
    /// </summary>
    [ArrayProperty(3)]
    public decimal PutOpenInterest { get; set; }

    /// <summary>
    /// Call Volume
    /// </summary>
    [ArrayProperty(4)]
    public decimal CallVolume { get; set; }

    /// <summary>
    /// Put Volume
    /// </summary>
    [ArrayProperty(5)]
    public decimal PutVolume { get; set; }
}
