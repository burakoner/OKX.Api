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
    ThreeMinutes = 180,

    /// <summary>
    /// FiveMinutes
    /// </summary>
    [Map("5m")]
    FiveMinutes = 300,

    /// <summary>
    /// FifteenMinutes
    /// </summary>
    [Map("15m")]
    FifteenMinutes = 900,

    /// <summary>
    /// ThirtyMinutes
    /// </summary>
    [Map("30m")]
    ThirtyMinutes = 1800,

    /// <summary>
    /// OneHour
    /// </summary>
    [Map("1H")]
    OneHour = 3600,

    /// <summary>
    /// FourHours
    /// </summary>
    [Map("4H")]
    FourHours = 14400,

    /// <summary>
    /// OneDay
    /// </summary>
    [Map("1D")]
    OneDay = 86400,
}