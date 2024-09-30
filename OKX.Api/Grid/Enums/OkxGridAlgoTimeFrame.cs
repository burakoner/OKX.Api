namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Algo Time Frame
/// </summary>
public enum OkxGridAlgoTimeFrame
{
    /// <summary>
    /// ThreeMinutes
    /// </summary>
    [Map("3m")]
    ThreeMinutes,

    /// <summary>
    /// FiveMinutes
    /// </summary>
    [Map("5m")]
    FiveMinutes,

    /// <summary>
    /// FifteenMinutes
    /// </summary>
    [Map("15m")]
    FifteenMinutes,

    /// <summary>
    /// ThirtyMinutes
    /// </summary>
    [Map("30m")]
    ThirtyMinutes,

    /// <summary>
    /// OneHour
    /// </summary>
    [Map("1H")]
    OneHour,

    /// <summary>
    /// FourHours
    /// </summary>
    [Map("4H")]
    FourHours,

    /// <summary>
    /// OneDay
    /// </summary>
    [Map("1D")]
    OneDay,
}