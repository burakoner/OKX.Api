namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Back Testing Duration
/// </summary>
public enum OkxGridBackTestingDuration
{
    /// <summary>
    /// OneWeek
    /// </summary>
    [Map("7D")]
    OneWeek,

    /// <summary>
    /// OneMonth
    /// </summary>
    [Map("30D")]
    OneMonth,

    /// <summary>
    /// SixMonths
    /// </summary>
    [Map("180D")]
    SixMonths,
}