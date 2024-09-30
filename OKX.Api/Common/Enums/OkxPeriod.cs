namespace OKX.Api.Common;

/// <summary>
/// OKX Period
/// </summary>
public enum OkxPeriod
{
    /// <summary>
    /// 1m
    /// </summary>
    [Map("1s")]
    OneSecond,

    /// <summary>
    /// 1m
    /// </summary>
    [Map("1m")]
    OneMinute,

    /// <summary>
    /// 3m
    /// </summary>
    [Map("3m")]
    ThreeMinutes,

    /// <summary>
    /// 5m
    /// </summary>
    [Map("5m")]
    FiveMinutes,

    /// <summary>
    /// 15m
    /// </summary>
    [Map("15m")]
    FifteenMinutes,

    /// <summary>
    /// 30m
    /// </summary>
    [Map("30m")]
    ThirtyMinutes,

    /// <summary>
    /// 1H
    /// </summary>
    [Map("1H")]
    OneHour,

    /// <summary>
    /// 2H
    /// </summary>
    [Map("2H")]
    TwoHours,

    /// <summary>
    /// 4H
    /// </summary>
    [Map("4H")]
    FourHours,

    /// <summary>
    /// 6H
    /// </summary>
    [Map("6H")]
    SixHours,

    /// <summary>
    /// 12H
    /// </summary>
    [Map("12H")]
    TwelveHours,

    /// <summary>
    /// 1D
    /// </summary>
    [Map("1D")]
    OneDay,

    /// <summary>
    /// 1W
    /// </summary>
    [Map("1W")]
    OneWeek,

    /// <summary>
    /// 1M
    /// </summary>
    [Map("1M")]
    OneMonth,

    /// <summary>
    /// 3M
    /// </summary>
    [Map("3M")]
    ThreeMonths,

    /// <summary>
    /// 6M
    /// </summary>
    [Map("6M")]
    SixMonths,

    /// <summary>
    /// 1Y
    /// </summary>
    [Map("1Y")]
    OneYear,
}