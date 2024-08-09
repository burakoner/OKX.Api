namespace OKX.Api.Block.Models;

/// <summary>
/// OKX Block MMP
/// </summary>
public class OkxBlockMmp
{
    /// <summary>
    /// Frozen period (ms).
    /// </summary>
    [JsonProperty("frozenInterval")]
    public int FrozenInterval { get; set; }

    /// <summary>
    /// Time window (ms). MMP interval where monitoring is done
    /// </summary>
    [JsonProperty("timeInterval")]
    public int TimeInterval { get; set; }

    /// <summary>
    /// Limit in number of execution attempts
    /// </summary>
    [JsonProperty("countLimit")]
    public int CountLimit { get; set; }
}

/// <summary>
/// OKX Block MMP Configuration
/// </summary>
public class OkxBlockMmpConfiguration : OkxBlockMmp
{
    /// <summary>
    /// Whether MMP is currently triggered. true or false
    /// </summary>
    [JsonProperty("mmpFrozen")]
    public bool IsFrozen { get; set; }

    /// <summary>
    /// If frozenInterval is not "0" and mmpFrozen = True, it is the time interval (in ms) when MMP is no longer triggered, otherwise ""
    /// </summary>
    [JsonProperty("mmpFrozenUntil")]
    public long? FrozenUntilTimestamp { get; set; }

    /// <summary>
    /// If frozenInterval is not "0" and mmpFrozen = True, it is the time interval (in ms) when MMP is no longer triggered, otherwise ""
    /// </summary>
    [JsonIgnore]
    public DateTime? FrozenUntilTime { get { return FrozenUntilTimestamp?.ConvertFromMilliseconds(); } }
}