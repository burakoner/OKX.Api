namespace OKX.Api.Algo;

/// <summary>
/// Smart iceberg RSI candlestick timeframe.
/// </summary>
public enum OkxAlgoSmartIcebergTimeFrame : int
{
    /// <summary>Three minutes.</summary>
    [Map("3m")]
    ThreeMinutes = 180,

    /// <summary>Five minutes.</summary>
    [Map("5m")]
    FiveMinutes = 300,

    /// <summary>Fifteen minutes.</summary>
    [Map("15m")]
    FifteenMinutes = 900,

    /// <summary>Thirty minutes.</summary>
    [Map("30m")]
    ThirtyMinutes = 1800,

    /// <summary>One hour.</summary>
    [Map("1H")]
    OneHour = 3600,

    /// <summary>Four hours.</summary>
    [Map("4H")]
    FourHours = 14400,

    /// <summary>One day.</summary>
    [Map("1D")]
    OneDay = 86400,
}
