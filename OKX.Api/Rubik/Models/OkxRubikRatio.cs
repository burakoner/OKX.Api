namespace OKX.Api.Rubik.Models;

/// <summary>
/// OKX Ratio
/// </summary>
[JsonConverter(typeof(ArrayConverter))]
public class OkxRubikRatio
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
    /// Ratio
    /// </summary>
    [ArrayProperty(1)]
    public decimal Ratio { get; set; }
}
