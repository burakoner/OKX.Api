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
    OneSecond = 1,

    /// <summary>
    /// 1m
    /// </summary>
    [Map("1m")]
    OneMinute = 60,

    /// <summary>
    /// 3m
    /// </summary>
    [Map("3m")]
    ThreeMinutes = 180,

    /// <summary>
    /// 5m
    /// </summary>
    [Map("5m")]
    FiveMinutes = 300,

    /// <summary>
    /// 15m
    /// </summary>
    [Map("15m")]
    FifteenMinutes = 900,

    /// <summary>
    /// 30m
    /// </summary>
    [Map("30m")]
    ThirtyMinutes = 1800,

    /// <summary>
    /// 1H
    /// </summary>
    [Map("1H")]
    OneHour = 3600,

    /// <summary>
    /// 2H
    /// </summary>
    [Map("2H")]
    TwoHours = 7200,

    /// <summary>
    /// 4H
    /// </summary>
    [Map("4H")]
    FourHours = 14400,

    /// <summary>
    /// 6H
    /// </summary>
    [Map("6H")]
    SixHours = 21600,

    /// <summary>
    /// 12H
    /// </summary>
    [Map("12H")]
    TwelveHours = 43200,

    /// <summary>
    /// 1D
    /// </summary>
    [Map("1D")]
    OneDay = 86400,

    /// <summary>
    /// 1W
    /// </summary>
    [Map("1W")]
    OneWeek = 604800,

    /// <summary>
    /// 1M
    /// </summary>
    [Map("1M")]
    OneMonth = 2_592_000,

    /// <summary>
    /// 3M
    /// </summary>
    [Map("3M")]
    ThreeMonths = 7_776_000,

    /// <summary>
    /// 6M
    /// </summary>
    [Map("6M")]
    SixMonths = 15_552_000,

    /// <summary>
    /// 1Y
    /// </summary>
    [Map("1Y")]
    OneYear = 31_104_000,
}