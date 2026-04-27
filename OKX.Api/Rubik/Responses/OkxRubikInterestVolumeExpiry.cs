namespace OKX.Api.Rubik;

/// <summary>
/// OKX Interest Volume Expiry
/// </summary>
[JsonConverter(typeof(ArrayConverter))]
public record OkxRubikInterestVolumeExpiry
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
    /// Contract expiry date in YYYYMMdd format.
    /// Kept as a numeric token for backward compatibility.
    /// </summary>
    [ArrayProperty(1)]
    public long ExpiryTimestamp { get; set; }

    /// <summary>
    /// Contract expiry date in YYYYMMdd format.
    /// </summary>
    [JsonIgnore]
    public string ExpiryDate => ExpiryTimestamp.ToString(CultureInfo.InvariantCulture);

    /// <summary>
    /// Contract expiry date parsed from the YYYYMMdd token returned by OKX.
    /// </summary>
    [JsonIgnore]
    public DateTime? ExpiryDateTime
    {
        get
        {
            if (!DateTime.TryParseExact(ExpiryDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var value))
                return null;

            return DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
    }

    /// <summary>
    /// Contract expiry date parsed from the YYYYMMdd token returned by OKX.
    /// </summary>
    [JsonIgnore]
    public DateTime ExpiryTime => ExpiryDateTime ?? DateTime.MinValue;

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
