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
    OneWeek = 7,

    /// <summary>
    /// OneMonth
    /// </summary>
    [Map("30D")]
    OneMonth = 30,

    /// <summary>
    /// SixMonths
    /// </summary>
    [Map("180D")]
    SixMonths = 180,
}