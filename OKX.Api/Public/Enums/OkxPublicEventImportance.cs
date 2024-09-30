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
    Low = 1,

    /// <summary>
    /// Medium
    /// </summary>
    [Map("2")]
    Medium = 2,

    /// <summary>
    /// High
    /// </summary>
    [Map("3")]
    High = 3,
}