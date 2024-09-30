namespace OKX.Api.Public;

/// <summary>
/// OKX Economic Calendar Event Importance
/// </summary>
public enum OkxPublicEventImportance
{
    /// <summary>
    /// Low
    /// </summary>
    [Map("1")]
    Low,

    /// <summary>
    /// Medium
    /// </summary>
    [Map("2")]
    Medium,

    /// <summary>
    /// High
    /// </summary>
    [Map("3")]
    High,
}