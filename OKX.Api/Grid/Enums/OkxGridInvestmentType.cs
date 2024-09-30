namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Investment Type
/// </summary>
public enum OkxGridInvestmentType
{
    /// <summary>
    /// Grid
    /// </summary>
    [Map("grid")]
    Grid = 1,

    /// <summary>
    /// Quote
    /// </summary>
    [Map("quote")]
    Quote = 2,

    /// <summary>
    /// Base
    /// </summary>
    [Map("base")]
    Base = 3,

    /// <summary>
    /// Dual
    /// </summary>
    [Map("dual")]
    Dual = 4
}