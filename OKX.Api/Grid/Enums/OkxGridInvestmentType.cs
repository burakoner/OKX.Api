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
    Grid,

    /// <summary>
    /// Quote
    /// </summary>
    [Map("quote")]
    Quote,

    /// <summary>
    /// Base
    /// </summary>
    [Map("base")]
    Base,

    /// <summary>
    /// Dual
    /// </summary>
    [Map("dual")]
    Dual
}